using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.EntityModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.BusinessImplementation
{
    public class StockFacade : IStockFacade
    {
        private readonly IRepository _repository;

        public StockFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public long Create(StockItemView stockItem)
        {
            if (stockItem.BarcodeNumber < 1 || stockItem.NumberOfUnit < 1 || stockItem.UnitPrice < 0 || string.IsNullOrEmpty(stockItem.Name))
                return -1;
            return _repository.Save<StockItem>(new StockItem
            {
                BarcodeNumber = stockItem.BarcodeNumber,
                Name = stockItem.Name,
                UnitPrice = stockItem.UnitPrice,
                NumberOfUnit = stockItem.NumberOfUnit,
                Status = Status.Active,
                CreatedBy = "M.Halid ÇİFCİ",
                CreatedByFullName = "M.Halid ÇİFCİ",
                CreatedDate = DateTime.Now.ToDate()

            }) ;
        }

        public void Delete(long id)
        {
            var entity = _repository.Get<StockItem>(id);
            if (entity == null)
                return;
            entity.Status = Status.Passive;

            _repository.Save<StockItem>(entity);
        }

        public StockItemView Get(long id)
        {
           var stockItem = _repository.Get<StockItem>(id);
            if (stockItem == null)
                return null;


                return new StockItemView
                {
                    Id = stockItem.Id,
                    BarcodeNumber = stockItem.BarcodeNumber,
                    Name = stockItem.Name,
                    UnitPrice = stockItem.UnitPrice,
                    NumberOfUnit = stockItem.NumberOfUnit,
                    Status = stockItem.Status,

                };
            
          

        }

        public PaginatedList<StockItemView> Search(StockSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var query = _repository.Query<StockItem>();
        

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            if (filter.BarcodeNumber>0)
            {
                query = query.Where(x => x.BarcodeNumber == filter.BarcodeNumber);
            }
            if (filter.UnitPrice > 0)
            {
                query = query.Where(x => x.UnitPrice == filter.UnitPrice);
            }
            if (filter.NumberOfUnit > 0)
            {
                query = query.Where(x => x.NumberOfUnit == filter.NumberOfUnit);
            }


            query = query.Where(x => x.Status == Status.Active);

            var newQuery = query.Select(stockItem => new StockItemView
            {
                Id = stockItem.Id,
                BarcodeNumber = stockItem.BarcodeNumber,
                Name = stockItem.Name,
                UnitPrice = stockItem.UnitPrice,
                NumberOfUnit = stockItem.NumberOfUnit,
                Status = stockItem.Status,
                CreatedDate=stockItem.CreatedDate,
            });

            newQuery = newQuery.OrderByDescending(x => x.CreatedDate);
            PaginatedList<StockItemView> paginatedList = new PaginatedList<StockItemView>(paginationInfo, newQuery);

            return paginatedList;
        }

        public void Update(StockItemView stockItem)
        {
            var entity = _repository.Get<StockItem>(stockItem.Id);
            if (entity == null)
                throw new Exception("Record Not Found!");

            entity.Name = stockItem.Name;
            entity.BarcodeNumber = stockItem.BarcodeNumber;
            entity.NumberOfUnit = stockItem.NumberOfUnit;
            entity.UnitPrice = stockItem.UnitPrice;
            entity.UpdatedBy = "M.Halid ÇİFCİ";
            entity.UpdatedByFullName = "M.Halid ÇİFCİ";
            entity.UpdatedDate = DateTime.Now.ToDate();
            _repository.Save<StockItem>(entity);
        }
    }
}

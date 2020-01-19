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
    public class PriceMapFacade : IPriceMapFacade
    {
        private readonly IRepository _repository;

        public PriceMapFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public long Create(PriceView price)
        {
            var query = _repository.Query<Price>();
            if (price == null||price.OperationTypeId==1)
                return -1;

            query = query.Where(x => x.OperationTypeId == price.OperationTypeId && x.VehicleType == price.VehicleType&&x.Status==Status.Active);

            if (query.Count() > 0)
                return -1;
            return _repository.Save<Price>(new Price
            {
                DefaultPrice = price.DefaultPrice,
                OperationTypeId = price.OperationTypeId,
                VehicleType = price.VehicleType,
                Status=Status.Active,
                
            });

        }

        public void Delete(long id)
        {
            var price = _repository.Get<Price>(id);
            price.Status = Status.Passive;
            _repository.Save<Price>(price);
            

        }

        public PriceView Get(long id)
        {

            var price = _repository.Get<Price>(id);
            if (price == null)
            {
                return null;
            }
            var query = _repository.Query<OperationType>();
            var operationName = query.Where(x => x.Id == price.OperationTypeId).ToList().FirstOrDefault().Name ?? " ";
            var view = new PriceView
            {
                Id=price.Id,
                DefaultPrice = price.DefaultPrice,
                OperationTypeId = price.OperationTypeId,
                VehicleType = price.VehicleType,
                Status=price.Status,
                OperationName=operationName,

            };

            return view;
        }

        public PaginatedList<PriceView> Search(PriceSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var priceQuery = _repository.Query<Price>();
            var opTypeQuery = _repository.Query<OperationType>();
            if (!string.IsNullOrEmpty(filter.VehicleType))
            {
                priceQuery = priceQuery.Where(x => x.VehicleType.Contains(filter.VehicleType));


            }
            if (filter.OperationTypeId>0)
            {
                priceQuery = priceQuery.Where(x => x.OperationTypeId == filter.OperationTypeId);

            }
            if (filter.DefaultPrice > 0)
            {
                priceQuery = priceQuery.Where(x => x.DefaultPrice == filter.DefaultPrice);

            }

            if (!string.IsNullOrEmpty(filter.OperationTypeCode))
            {
                var operationTypeId = opTypeQuery.Where(x => x.Code == filter.OperationTypeCode).ToList().FirstOrDefault().Id;
                if(operationTypeId!=0)
                    priceQuery = priceQuery.Where(x => x.OperationTypeId == operationTypeId);

            }
            var opList = opTypeQuery.ToList();


            priceQuery = priceQuery.Where(x => x.Status == Status.Active);
            //var newQuery = priceQuery.Select(x => new PriceView
            //{
            //    Id = x.Id,
            //    DefaultPrice = x.DefaultPrice,
            //    OperationTypeId = x.OperationTypeId,
            //    OperationName= opList.Where(y=>y.Id==x.OperationTypeId).ToList().FirstOrDefault().Name,
            //    VehicleType = x.VehicleType,

            //});
            var newQuery = priceQuery.Join(opTypeQuery,
             operation => operation.OperationTypeId,
             opType => opType.Id,
             (operation, opType) => new { OP = operation, OPTYPE = opType }).Where(a => a.OP.OperationTypeId == a.OPTYPE.Id).Select(x => new PriceView
             {
                 Id = x.OP.Id,
                 DefaultPrice = x.OP.DefaultPrice,
                 OperationTypeId = x.OP.OperationTypeId,
                 OperationName = x.OPTYPE.Name,
                 VehicleType = x.OP.VehicleType,

             }


             );
            newQuery = newQuery.OrderBy(x=> x.VehicleType);
            PaginatedList<PriceView> paginatedList = new PaginatedList<PriceView>(paginationInfo, newQuery);

            return paginatedList;
        }

        public void Update(PriceView price)
        {
            if (price != null)
            {


                Delete(price.Id);

                Create(price);

            }
        }
    }
}

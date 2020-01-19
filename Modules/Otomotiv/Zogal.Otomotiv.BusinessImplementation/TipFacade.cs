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
    public class TipFacade : ITipFacade
    {
        private readonly IRepository _repository;

        public TipFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public long Create(TipView tip)
        {
            if (tip.OperationDate < 20000)
            {
                tip.OperationDate = DateTime.Now.ToDate();
            }
            return _repository.Save<Tip>(new Tip
            {
                OperationDate = tip.OperationDate,
                PeriodDate = DateTime.Now.ToPeriod(),
                Status = Core.Status.Active,
                WorkerNumber = tip.WorkerNumber,
                TipAmount = tip.TipAmount,
                CreatedBy = "M.Halid ÇİFCİ",
                CreatedByFullName = "M.Halid ÇİFCİ",
                CreatedDate = DateTime.Now.ToDateTime(),

            });
        }

        public void Delete(long id)
        {
            var entity = _repository.Get<Tip>(id);
            if (entity == null)
                return;
            entity.Status = Core.Status.Passive;
            _repository.Save(entity);
        }

        public TipView Get(long id)
        {
            var entity = _repository.Get<Tip>(id);
            if (entity == null)
                return null;
            return new TipView
            {
                Id = entity.Id,
                OperationDate = entity.OperationDate,
                PeriodDate = entity.PeriodDate,
                TipAmount = entity.TipAmount,
                WorkerNumber = entity.WorkerNumber,
            };

        }

        public PaginatedList<TipView> Search(TipSearchFilter filter, PaginationInfoView paginationInfo)
        {

            var tipQuery = _repository.Query<Tip>();

            if (filter.OperationDate > 0)
                tipQuery = tipQuery.Where(x => x.OperationDate == filter.OperationDate);
            if (filter.TipAmount > 0)         
                tipQuery = tipQuery.Where(x => x.TipAmount == filter.TipAmount);

            if (filter.WorkerNumber > 0)
                tipQuery = tipQuery.Where(x => x.WorkerNumber == filter.WorkerNumber);

            tipQuery = tipQuery.Where(x => x.Status == Core.Status.Active);
            var newQuery = tipQuery.Select(x => new TipView
            {
                Id = x.Id,
                TipAmount = x.TipAmount,
                OperationDate = x.OperationDate,
                PeriodDate = x.PeriodDate,
                WorkerNumber = x.WorkerNumber,
            });

            newQuery = newQuery.OrderByDescending(x => x.OperationDate);

            PaginatedList<TipView> paginatedList = new PaginatedList<TipView>(paginationInfo, newQuery);

            return paginatedList;
        }
    }
}

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
    public class CounterFacade : ICounterFacade
    {
        private readonly IRepository _repository;

        public CounterFacade(IRepository repository)
        {          
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public long Create(CounterView counter)
        {
            if (counter == null)
                return -1;
            var query = _repository.Query<Counter>();
            var now = DateTime.Now.Date.ToDateTime();
            query = query.Where(x => x.Date == now && x.Type==counter.Type);
            
          

            if (query.Count() == 0)
            {
                return _repository.Save(new Counter
                {
                    Type = counter.Type,
                    StartValue = counter.Value,
                    Date = DateTime.Now.Date.ToDateTime(),
                    CreatedBy = "USer",
                    CreatedByFullName = "USERNAME",
                    CreatedDate = DateTime.Now.ToDateTime(),
                    Status=Status.Active,

                });


            }
            else
            {
                var entity = query.ToList().FirstOrDefault();
                if (entity.StopValue > 0)
                    return -1;
                entity.StopValue = counter.Value;
                entity.SpentValue = counter.Value - entity.StartValue;
                entity.UpdatedBy = "";
                entity.UpdatedByFullName = "";
                entity.UpdatedDate = DateTime.Now.ToDateTime();
                return _repository.Save(entity);

            }
           
           
        }

        public void Delete(long id)
        {
            var counter = _repository.Get<Counter>(id);
            if (counter != null)
            {
                counter.Status = Status.Passive;
                counter.UpdatedBy = "";
                counter.UpdatedByFullName = "";
                counter.UpdatedDate = DateTime.Now.ToDateTime();

            }

          
        }

        public CounterView Get(long id)
        {        
            var counter = _repository.Get<Counter>(id);

            if (counter == null)
                return null;

            return new CounterView
            {
                Type = counter.Type,
                Value = counter.SpentValue,
                Date = counter.Date,
                Id=counter.Id,
            };

        }

        public PaginatedList<CounterView> Search(CounterSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var counterQuery = _repository.Query<Counter>();

            if (!string.IsNullOrEmpty(filter.Type))
            {
                counterQuery = counterQuery.Where(x => x.Type.Contains(filter.Type));
            }

            if (filter.SpentValue > 0)
            {
                counterQuery = counterQuery.Where(x => x.SpentValue == filter.SpentValue);
            }

            if (filter.Date > 0)
            {
                counterQuery = counterQuery.Where(x => x.Date == filter.Date);
            }

            counterQuery = counterQuery.Where(x => x.Status == Status.Active);
            var newQuery = counterQuery.Select(x => new CounterView
            {
                Id=x.Id,
                Type = x.Type,
                StartValue=x.StartValue,
                StopValue=x.StopValue,
                Value = x.SpentValue,
                Date = x.Date
                
            });

            newQuery = newQuery.OrderByDescending(x => x.Date);
            PaginatedList<CounterView> paginatedList = new PaginatedList<CounterView>(paginationInfo, newQuery);

            return paginatedList;
        }
    }

    
}

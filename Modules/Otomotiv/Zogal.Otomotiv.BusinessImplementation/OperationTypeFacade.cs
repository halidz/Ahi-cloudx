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
    public class OperationTypeFacade : IOperationTypeFacade
    {
        private readonly IRepository _repository;

        public OperationTypeFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public long Create(OperationTypeView operationType)
        {
            if (operationType == null)
                return -1;
           
            return _repository.Save<OperationType>(new OperationType
            {
               Code=operationType.Code,
               AccountType=operationType.AccountType,
               Name=operationType.Name

            });
        }

        public void Delete(long id)
        {

            var entity = _repository.Get<OperationType>(id);
            if (entity == null)
                return;
            _repository.Delete(entity);
        }

        public OperationTypeView Get(long id)
        {
            var entity = _repository.Get<OperationType>(id);
            if (entity == null)
                return null;
            return new OperationTypeView
            {
                Id = entity.Id,
                Code = entity.Code,
                AccountType = entity.AccountType,
                Name = entity.Name,
             
            };
        }

        public void Update(OperationTypeView operationType)
        {
            var entity = _repository.Get<OperationType>(operationType.Id);
            if (entity == null)
                return ;

            entity.Code = operationType.Code;
            entity.AccountType = operationType.AccountType;
            entity.Name = operationType.Name;


            _repository.Save(entity);
        }

    
        public PaginatedList<OperationTypeView> Search(OperationTypeFilter filter, PaginationInfoView paginationInfo)
        {

            var OpTypeQuery = _repository.Query<OperationType>();

            if (!string.IsNullOrEmpty(filter.Name))
                OpTypeQuery = OpTypeQuery.Where(x => x.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Code))
                OpTypeQuery = OpTypeQuery.Where(x => x.Code.Contains(filter.Code));

            if (filter.AccountType>0)
                OpTypeQuery = OpTypeQuery.Where(x => x.AccountType == filter.AccountType);

         
            var newQuery = OpTypeQuery.Select(x => new OperationTypeView
            {              
                Id = x.Id,
                Code = x.Code,
                AccountType = x.AccountType,
                Name = x.Name,
            });

            PaginatedList<OperationTypeView> paginatedList = new PaginatedList<OperationTypeView>(paginationInfo, newQuery);

            return paginatedList;
        }
    }
}






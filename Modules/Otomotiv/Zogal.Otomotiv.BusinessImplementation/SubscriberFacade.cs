using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.Core;
using Zogal.Otomotiv.EntityModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.BusinessImplementation
{
    public class SubscriberFacade : ISubscriberFacade
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public SubscriberFacade(IRepository repository,ICurrentUser currentUser)           
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public long Create(SubscriberItemView subscriber)
        {
            var query = _repository.Query<Subscriber>();

            query = query.Where(x => x.Plate == subscriber.Plate);

            if (query.Any() && query.ToList().First().Status == Core.SubscriberStatus.Active)     //Plaka sistemde var ise ve abonelik pasif durumda değil ise           
            {
               
                return -1; 
                
            }
            else
            {
                if (subscriber.Plate != null)
                {
                    var temp = subscriber.Plate.Trim();
                    temp = temp.Replace(" ", "");
                    temp = temp.ToUpper();
                    subscriber.Plate = temp;
                }
                if (subscriber.MonthlySubscription < 1)
                    subscriber.MonthlySubscription = 1;
                var periodDate = DateTime.Now.ToPeriod();

                return _repository.Save<Subscriber>(new Subscriber
                {

                    CustomerId = subscriber.CustomerId,
                    Status = subscriber.Status,
                    StartDate = subscriber.StartDate,
                    LastStartDate = subscriber.StartDate,
                    MonthlySubscription = subscriber.MonthlySubscription,
                    Deposit=subscriber.Deposit,
                    Plate = subscriber.Plate,
                    Brand=subscriber.Brand,
                    Model = subscriber.Model,
                    Type = subscriber.Type,
                    CreatedBy = _currentUser.UserName,
                    CreatedByFullName = _currentUser.FullName,
                    CreatedDate = DateTime.Now.ToDateTime()
                });

            }

         
        }

        public void Delete(long id)
        {
            var entity = _repository.Get<Subscriber>(id);
            entity.Status = Core.SubscriberStatus.Passive;
            _repository.Save(entity);


        }

        public SubscriberItemView Get(long id)
        {
           var subscriber = _repository.Get<Subscriber>(id);
            if (subscriber == null)
                throw new Exception("Record Not Found!");

            return new SubscriberItemView
            {
                CustomerId = subscriber.CustomerId,
                Status = subscriber.Status,
                StartDate = subscriber.StartDate,
                MonthlySubscription = subscriber.MonthlySubscription,
                Plate = subscriber.Plate,
                Deposit = subscriber.Deposit,
                Brand =subscriber.Brand,
                Model = subscriber.Model,
                Type = subscriber.Type,
                SubscriberId = subscriber.Id,

            };

        }

        

        public PaginatedList<SubscriberItemView> Search(SubscriberSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var query = _repository.Query<Subscriber>();
            var customer = _repository.Get<Customer>(filter.CustomerId);           
            var customerQuery = _repository.Query<Customer>();

            query = query.Where(x => x.Status == Core.SubscriberStatus.Active);



            if (filter.CustomerId > 0) 
                query = query.Where(x => x.CustomerId == filter.CustomerId);




                var nquery = query.Join(customerQuery,
                subs => subs.CustomerId,
                custom => custom.Id,
                 (subs, custom) => new { SUB = subs, CUS = custom }).Where(postAndMeta => postAndMeta.CUS.Id == postAndMeta.SUB.CustomerId)
                 .Select(x=> new SubscriberItemView {
                     FirstName=x.CUS.FirstName,
                     LastName=x.CUS.LastName,
                     PhoneNumber=x.CUS.PhoneNumber,
                     CustomerId=x.CUS.Id,
                     Gender=x.CUS.Gender,
                     MonthlySubscription = x.SUB.MonthlySubscription,
                     Deposit = x.SUB.Deposit,
                     SubscriberId = x.SUB.Id,
                     StartDate = x.SUB.StartDate,
                     Status = x.SUB.Status,
                     Brand=x.SUB.Brand,
                     Model = x.SUB.Model,
                     Plate = x.SUB.Plate,
                     Type = x.SUB.Type
                 });

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                nquery = nquery.Where(x => x.FirstName.Contains(filter.FirstName));
            if (!string.IsNullOrWhiteSpace(filter.LastName))
                nquery = nquery.Where(x => x.LastName.Contains(filter.LastName));
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
                nquery = nquery.Where(x => x.PhoneNumber.Contains(filter.PhoneNumber));
            if (!string.IsNullOrWhiteSpace(filter.Plate))
            {
                var temp = filter.Plate.Trim();
                temp = temp.Replace(" ", "");
                temp = temp.ToUpper();
                filter.Plate = temp;
                nquery = nquery.Where(x => x.Plate.Contains(filter.Plate));
            }

            if (!string.IsNullOrEmpty(filter.Gender)&&filter.Gender!="undefined")
                nquery = nquery.Where(x => x.Gender == filter.Gender);

            nquery = nquery.OrderBy(x => x.FirstName);
            PaginatedList<SubscriberItemView> paginatedList =new  PaginatedList<SubscriberItemView>(paginationInfo,nquery);
            return paginatedList;
         
        }
       

      
        public PaginatedList<MobileStartOpView> MobileSearch(MobileSubscriberSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var query = _repository.Query<Subscriber>();
            var customer = _repository.Get<Customer>(filter.CustomerId);
            var customerQuery = _repository.Query<Customer>();

            query = query.Where(x => x.Status == Core.SubscriberStatus.Active);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(x => customer.FirstName == filter.FirstName);
            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(x => customer.LastName == filter.LastName);
            if (filter.CustomerId > 0)
                query = query.Where(x => x.CustomerId == filter.CustomerId);

            if (!string.IsNullOrWhiteSpace(filter.Plate))
                query = query.Where(x => x.Plate == filter.Plate);

            var priceCalculator = new PriceCalculator(filter.OperationType,filter.VehicleType,_repository);

            if (query.Count() == 0)  
            {
                var nquery = query.Select(x => new MobileStartOpView
                {
                    CalculatedAmount = priceCalculator.CalculatedPrice,
                    
                }) ;

                PaginatedList<MobileStartOpView> paginatedList = new PaginatedList<MobileStartOpView>(paginationInfo, nquery);

                return paginatedList;
            }
            else
            {

                var nquery = query.Join(customerQuery,
                subs => subs.CustomerId,
                custom => custom.Id,
                 (subs, custom) => new { SUB = subs, CUS = custom }).Where(postAndMeta => postAndMeta.CUS.Id == postAndMeta.SUB.CustomerId)
                 .Select(x => new MobileStartOpView
                 {
                     FirstName = x.CUS.FirstName,
                     LastName = x.CUS.LastName,
                     PhoneNumber = x.CUS.PhoneNumber,
                     CustomerId = x.CUS.Id,
                     SubscriberId = x.SUB.Id,
                     Status = x.SUB.Status,
                     Model = x.SUB.Model,
                     Plate = x.SUB.Plate,
                     VehicleType = x.SUB.Type,
                     CalculatedAmount=priceCalculator.CalculatedPrice

                 });

                nquery = nquery.OrderBy(x => x.FirstName);
                PaginatedList<MobileStartOpView> paginatedList = new PaginatedList<MobileStartOpView>(paginationInfo, nquery);

                return paginatedList;
            }
          
        }



        public long Update(SubscriberItemView view)
        {
            var subscriber = _repository.Get<Subscriber>(view.SubscriberId);
            if (subscriber == null)
                throw new Exception("Record Not Found!");


            var query = _repository.Query<Subscriber>();

            query= query.Where(x => x.Plate == view.Plate);

            if(query.Any() && query.ToList().First().Plate!=subscriber.Plate)     //Plaka sistemde var ise                      
            {
                return -1;

            }
            if (view.MonthlySubscription < 1)
                view.MonthlySubscription = 1;
            subscriber.StartDate = view.StartDate;
            subscriber.Brand = view.Brand;
            subscriber.Model = view.Model;
            subscriber.MonthlySubscription = view.MonthlySubscription;
            subscriber.Plate = view.Plate;
            subscriber.Deposit = view.Deposit;
            subscriber.Status = view.Status;
            subscriber.Type = view.Type;
            subscriber.UpdatedBy = _currentUser.UserName;
            subscriber.UpdatedByFullName = _currentUser.FullName;
            subscriber.UpdatedDate = DateTime.Now.ToDateTime();


            _repository.Save(subscriber);
            return subscriber.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Zogal.Core;
using Zogal.Core.ViewModel;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.EntityModel;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.BusinessImplementation
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CustomerFacade(IRepository repository,ICurrentUser currentUser)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public long Create(CustomerView customer)
        {
            var newCustomer = new Customer
            {

                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Status = Status.Active,
                Gender=customer.Gender,
                CreatedBy = _currentUser.UserName,
                CreatedByFullName = _currentUser.FullName,
                CreatedDate = DateTime.Now.ToDateTime(),
                
            };



            return _repository.Save(newCustomer);
        }

        public void Delete(long id)
        {
            //var query = _repository.Query<Subscriber>();

            //var customer = _repository.Get<Customer>(id);

            //query = query.Where(x => x.CustomerId == id);

            //foreach (Subscriber subscriber in query.ToList())
            //{
            //    _repository.Delete(subscriber);
            //};
            //_repository.Delete(customer);


            //TODO WİLL DELETE ITS SUBSCRIBERS TOO
            var query = _repository.Query<Subscriber>();

            var customer = _repository.Get<Customer>(id);

            query = query.Where(x => x.CustomerId == id);

            foreach (Subscriber subscriber in query.ToList())
            {
                subscriber.Status = Core.SubscriberStatus.Passive;
                _repository.Save(subscriber);
            };

            customer.Status = Status.Passive;
            _repository.Save(customer);




        }

        public CustomerView Get(long id)
        {
            var customer = _repository.Get<Customer>(id);
            if (customer == null)
                throw new Exception("Record Not Found!");
            var view = new CustomerView
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber=customer.PhoneNumber,
                Gender=customer.Gender,
               
            };

          
            return view;
        }

        public void Update(CustomerView view)
        {
            var customer = _repository.Get<Customer>(view.Id);
            if (customer == null)
                throw new Exception("Record Not Found!");

            customer.FirstName = view.FirstName;
            customer.LastName = view.LastName;
            customer.PhoneNumber = view.PhoneNumber;
           
            customer.UpdatedBy = _currentUser.UserName;
            customer.UpdatedByFullName = _currentUser.FullName;
            customer.UpdatedDate = DateTime.Now.ToDateTime();
            //var newCustomer = new Customer
            //{
            //    Id = customer.Id,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName,
            //    PhoneNumber=customer.PhoneNumber,
            //    Subscribers = new List<Subscriber>()

            //};
            

            //foreach (SubscriberView s in view.Subscribers.Where(x => x.Id == 0))
            //{
            //   customer.Subscribers.Add(new Subscriber
            //    {
            //        Id = s.Id,
            //        StartDate = s.StartDate,
            //        Customer=customer,
            //        Status = s.Status,
            //        MonthlySubscription = s.MonthlySubscription,               
            //        Model = s.Model,
            //        Plate = s.Plate,
            //        Type = s.Type
                  
            //   });
            //}
            //view.Subscribers.Where(x => x.Id != 0)
            //    .Select(x => new { source = x, target = customer.Subscribers.FirstOrDefault(y => y.Id == x.Id) })
            //    .ToList()
            //    .ForEach(x =>
            //    {
            //        x.target.MonthlySubscription = x.source.MonthlySubscription;
            //        x.target.StartDate = x.source.StartDate;
            //        x.target.Status = x.source.Status;
            //        x.target.Model = x.source.Model;
            //        x.target.Type = x.source.Type;
            //        x.target.Plate = x.source.Plate;
            //    });
            //var subscriberIdList = view.Subscribers.Select(x => x.Id).ToList();
            //customer.Subscribers.Where(x => !subscriberIdList.Contains(x.Id))
            //    .ToList()
            //    .ForEach(x => x.Status = Core.SubscriberStatus.Passive);
            
            _repository.Save(customer);
            

            //customer.Subscribers.Where(x => !subscriberIdList.Contains(x.Id))
            //   .ToList()
            //   .ForEach(x=> {
            //       x.Customer = null;
            //       customer.Subscribers.Remove(x);
            //   });

        }

        public PaginatedList<CustomerItemView> Search(CustomerSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var query = _repository.Query<Customer>();

            query = query.Where(x => x.Status == Status.Active);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query = query.Where(x => x.LastName.Contains(filter.LastName));
            }

            if (!string.IsNullOrWhiteSpace(filter.Gender))
            {
                query = query.Where(x => x.Gender.Contains(filter.Gender));
            }

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(filter.PhoneNumber));
            }


            var newQuery = query.Select(x => new CustomerItemView
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,

            }) ;

            newQuery = newQuery.OrderBy(x => x.FirstName);


            PaginatedList<CustomerItemView> paginatedList = new PaginatedList<CustomerItemView>(paginationInfo, newQuery);
          


            return paginatedList;




        }



        
        //public PaginatedList<CustomerView> Search(CustomerSearchFilter filter, PaginationInfoView paginationInfo)
        //{
        //    var query = _repository.Query<Customer>();

        //    if (!string.IsNullOrWhiteSpace(filter.FirstName))
        //    {
        //        query = query.Where(x => x.FirstName == filter.FirstName);
        //    }

        //    if (!string.IsNullOrWhiteSpace(filter.LastName))
        //    {
        //        query = query.Where(x => x.LastName == filter.LastName);
        //    }


        //    var newQuery = query.Select(x => new CustomerView
        //    {
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,
        //        Id = x.Id,
        //        PhoneNumber = x.PhoneNumber,
        //        Subscribers = x.Subscribers.Select(y => new SubscriberView
        //        { Id = y.Id,
        //            MonthlySubscription = y.MonthlySubscription,
        //            StartDate = y.StartDate,
        //            Status = y.Status,
        //            Vehicle = new VehicleView
        //            {
        //                Id = y.Vehicle.Id,
        //                Model = y.Vehicle.Model,
        //                Plate = y.Vehicle.Plate,
        //                Type = y.Vehicle.Type
        //            }
        //        }).ToList()
        //        });


        //   //// var qu = newQuery.ToList();
        //   // var i = 0;
        //   // foreach (var x in nquery)
        //   // {

        //   //     foreach (var s in x)
        //   //     {
        //   //         //  newQuery.ToList().First().Subscribers.Add

        //   //         qu[i].Subscribers.Add(new SubscriberView
        //   //         {
        //   //             Id = s.Id,
        //   //             StartDate = s.StartDate,
        //   //             Status = s.Status,
        //   //             MonthlySubscription = s.MonthlySubscription,
        //   //             Vehicle = new VehicleView
        //   //             {
        //   //                 Id = s.Vehicle.Id,
        //   //                 Model = s.Vehicle.Model,
        //   //                 Plate = s.Vehicle.Plate,
        //   //                 Type = s.Vehicle.Type,

        //   //             },

        //   //         });

        //   //     }
        //   //     i++;
        //   // }

        //   // var que = qu.AsQueryable<CustomerView>();

        //    PaginatedList<CustomerView> paginatedList = new PaginatedList<CustomerView>(paginationInfo, newQuery);
        //   // i = 0;


        //    return paginatedList;




        //}

    }
}

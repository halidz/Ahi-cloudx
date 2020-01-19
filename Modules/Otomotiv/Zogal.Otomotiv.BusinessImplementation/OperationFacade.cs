using ClosedXML.Excel;
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
    public class OperationFacade : IOperationFacade
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

     


        public OperationFacade(IRepository repository,ICurrentUser currentUser)

        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }



        public long Create(OperationView entity)
        {
            var periodDate = DateTime.Now.ToPeriod();
            var operationType = _repository.Get<OperationType>(entity.OperationTypeId);
            if (entity.OperationDate < 20000)
            {
                entity.OperationDate = DateTime.Now.ToDate();  //Tarih seçilmemişse
                
            }
            else
            {
                periodDate = Convert.ToInt64(entity.OperationDate.ToString().Substring(0, 6));
                if(entity.OperationDate< 20180000)
                {
                    
                    var body = entity.OperationDate.ToString();
                    var day = body[body.Length-1];
                    body = body.Substring(0, body.Length - 1);
                    body = body + "0" + day;
                    entity.OperationDate = Convert.ToInt64(body);

                }
            }

            if (operationType.AccountType == AccountType.Debit)
            {
                entity.Plate = operationType.Code;

            }
               
            if (entity.Plate != null)
            {
                var temp = entity.Plate.Trim();
                temp = temp.Replace(" ", "");
                temp = temp.ToUpper();
                entity.Plate = temp;
            }    
            
            //TO DO
            if (operationType.Code == "ABN")
            {
                var query = _repository.Query<Operation>();
                var subQuery = _repository.Query<Subscriber>();

                subQuery = subQuery.Where(x => x.Plate == entity.Plate && x.Status==Core.SubscriberStatus.Active);
                query = query.Where(x => x.Plate == entity.Plate&& x.OperationTypeId==entity.OperationTypeId&&x.Status==Core.Status.Active);

                var list = query.ToList();

                if (subQuery.ToList().Count == 0)
                {
                    return -1; //abone sistemde kayıtlı değil
                }
                var sub = subQuery.ToList().First();

                if (list.Count!=0)   //ilk ödeme değilse
                {
                
                    var max = list.Max(x => x.PeriodDate); //old period Date
                    var maxOp = list.Where(x => x.PeriodDate == max).ToList().FirstOrDefault();
                    PeriodDateCalculator pCalculator = new PeriodDateCalculator(max);
                    periodDate = pCalculator.PeriodDate;
                    if (maxOp.OperationAmount < sub.MonthlySubscription)
                    {
                        var sum = list.Where(x => x.PeriodDate == maxOp.PeriodDate).Sum(y => y.OperationAmount);
                        if (sum < sub.MonthlySubscription)
                        {
                            var AmountToBePaid = sub.MonthlySubscription - sum;
                            if (entity.OperationAmount < AmountToBePaid)
                                AmountToBePaid = entity.OperationAmount;
                            _repository.Save(new Operation
                            {
                                FirstName = entity.FirstName,
                                LastName = entity.LastName,
                                PhoneNumber = entity.PhoneNumber,
                                Gender = entity.Gender,
                                PaymentMethod = entity.PaymentMethod,
                                OperationDate = entity.OperationDate,
                                PeriodDate = maxOp.PeriodDate,
                                OperationAmount = AmountToBePaid,
                                TipAmount = entity.TipAmount,
                                Plate = entity.Plate,
                                VehicleType = entity.VehicleType,
                                VehicleBrand = entity.VehicleBrand,
                                OperationTypeId = entity.OperationTypeId,
                                Description = entity.Description,
                                Status=Core.Status.Active,
                                CreatedBy = _currentUser.UserName,
                                CreatedByFullName = _currentUser.FullName,
                                CreatedDate = DateTime.Now.ToDateTime()
                            });

                            entity.OperationAmount = entity.OperationAmount - AmountToBePaid;
                          
                        }
                            
                     
                      
                    }

                }
                
            
                if (entity.OperationAmount > sub.MonthlySubscription)  //Tek seferde çok ayın ödemesi ise
                {
                    PeriodDateCalculator pCalculator = new PeriodDateCalculator();
                    while (entity.OperationAmount > sub.MonthlySubscription)
                    {
                        _repository.Save(new Operation
                        {
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            PhoneNumber = entity.PhoneNumber,
                            Gender = entity.Gender,
                            PaymentMethod = entity.PaymentMethod,
                            OperationDate = entity.OperationDate,
                            PeriodDate = periodDate,
                            OperationAmount = sub.MonthlySubscription,
                            TipAmount = entity.TipAmount,
                            Plate = entity.Plate,
                            VehicleType = entity.VehicleType,
                            VehicleBrand=entity.VehicleBrand,
                            OperationTypeId = entity.OperationTypeId,
                            Description = entity.Description,
                            Status = Core.Status.Active,
                            CreatedBy = _currentUser.UserName,
                            CreatedByFullName = _currentUser.FullName,
                            CreatedDate = DateTime.Now.ToDateTime()
                        });

                        entity.OperationAmount = entity.OperationAmount - sub.MonthlySubscription;
                        periodDate = pCalculator.Calculate(periodDate);
                    }

                    

                }
                else if(entity.OperationAmount>0)
                {
                    return _repository.Save(new Operation
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        PhoneNumber = entity.PhoneNumber,
                        Gender = entity.Gender,
                        PaymentMethod = entity.PaymentMethod,
                        OperationDate = entity.OperationDate,
                        PeriodDate = periodDate,
                        OperationAmount = entity.OperationAmount,
                        TipAmount = entity.TipAmount,
                        Plate = entity.Plate,
                        VehicleType = entity.VehicleType,
                        VehicleBrand = entity.VehicleBrand,
                        OperationTypeId = entity.OperationTypeId,
                        Description = entity.Description,
                        Status = Core.Status.Active,
                        CreatedBy = _currentUser.UserName,
                        CreatedByFullName = _currentUser.FullName,
                        CreatedDate = DateTime.Now.ToDateTime()
                    });

                   
                }
                else
                {
                    return 0;                                   
                }

            }

            //İşlem abonelik ödemesi değilse
            return _repository.Save(new Operation
            {   
                FirstName=entity.FirstName,
                LastName=entity.LastName,
                PhoneNumber=entity.PhoneNumber,
                Gender=entity.Gender,
                PaymentMethod= entity.PaymentMethod,
                OperationDate = entity.OperationDate,
                PeriodDate =periodDate,
                OperationAmount = entity.OperationAmount,
                TipAmount = entity.TipAmount,
                Plate = entity.Plate,
                VehicleType=entity.VehicleType,
                VehicleBrand = entity.VehicleBrand,
                OperationTypeId = entity.OperationTypeId,
                Description =entity.Description,
                Status = Core.Status.Active,
                CreatedBy =_currentUser.UserName,
                CreatedByFullName=_currentUser.FullName,
                CreatedDate=DateTime.Now.ToDateTime()
            });
        }

        public long MobileCreate(OperationView entity)
        {
            var periodDate = DateTime.Now.ToPeriod();
            var operationType = _repository.Get<OperationType>(entity.OperationTypeId);


            //TO DO
            if (operationType.Code == "ABN")
            {
                var query = _repository.Query<Operation>();
                var subQuery = _repository.Query<Subscriber>();

                subQuery = subQuery.Where(x => x.Plate == entity.Plate && x.Status == Core.SubscriberStatus.Active);
                query = query.Where(x => x.Plate == entity.Plate && x.OperationTypeId == entity.OperationTypeId);

                var list = query.ToList();

                if (list.Count == 0)   //ilk ödeme ise
                {
                    if (subQuery.ToList().Count == 0)
                    {
                        return -1; //abone sistemde kayıtlı değil
                    }

                }
                else
                {

                    var max = list.Max(x => x.PeriodDate);


                    var oldPeriodDate = max;

                    periodDate = oldPeriodDate + 1;

                    if (periodDate % 100 > 12)  //ay sayısı 12 yi geçtiyse yılı artır.
                    {
                        if (periodDate % 1000 > 912)   //yıl sayısı 9 u geçtiyse onlar basamağını artır.
                        {
                            periodDate = periodDate + 1000 - (periodDate % 1000) + 1;
                        }
                        else
                        {
                            periodDate = periodDate + 100 - (periodDate % 100) + 1;
                        }
                    }

                }
            }

            //_repository.Save(new Subscriber
            //{

            //});

            //_repository.Save(new Customer
            //{

            //});

            return _repository.Save(new Operation
            {
                PaymentMethod = entity.PaymentMethod,
                OperationDate = DateTime.Now.ToDate(),
                PeriodDate = periodDate,
                OperationAmount = entity.OperationAmount,
                TipAmount = entity.TipAmount,
                Plate = entity.Plate,
                OperationTypeId = entity.OperationTypeId,
                Description = entity.Description,
                CreatedBy = _currentUser.UserName,
                CreatedByFullName = _currentUser.FullName,
                CreatedDate = DateTime.Now.ToDateTime()
            });
        }

        public void Delete(long id)
        {
            var entity = _repository.Get<Operation>(id);
            if (entity != null)
            {
                entity.Status = Core.Status.Passive;
                entity.CreatedBy = _currentUser.UserName;
                entity.CreatedByFullName = _currentUser.FullName;
                entity.CreatedDate = DateTime.Now.ToDateTime();
            }
            _repository.Save<Operation>(entity);
        }

        public OperationView Get(long id)
        {
            var entity = _repository.Get<Operation>(id);
            if (entity == null)
            {
                return null;
            }
            var view = new OperationView
            {
                Id = entity.Id,          
                PaymentMethod = entity.PaymentMethod,
                OperationDate = entity.OperationDate,
                OperationAmount = entity.OperationAmount,
                TipAmount = entity.TipAmount,
                Plate = entity.Plate,
                OperationTypeId = entity.OperationTypeId,
                Description=entity.Description
                
            };
            return view;
        }
      
        public void Update(OperationView entity)
        {
          
            _repository.Save<Operation>(new Operation
            {
                Id = entity.Id,            
                PaymentMethod = entity.PaymentMethod,
                OperationDate = entity.OperationDate,
                OperationAmount = entity.OperationAmount,
                TipAmount = entity.TipAmount,               
                Plate = entity.Plate,
                OperationTypeId = entity.OperationTypeId,
                
            });
        }

        public MobileStartOpView GetCustomerInfo(GetInfoFilter filter)
        {
            var operationType = _repository.Get<OperationType>(filter.OperationTypeId);
            var subQuery = _repository.Query<Subscriber>();
            var opQuery = _repository.Query<Operation>();
            if (string.IsNullOrWhiteSpace(filter.Plate) && (string.IsNullOrWhiteSpace(operationType.Name)) && (string.IsNullOrWhiteSpace(filter.VehicleType)))
                throw new Exception("Eksik Bilgi Girildi.");

            var temp = filter.Plate.Trim();
            temp = temp.Replace(" ", "");
            temp = temp.ToUpper();
            filter.Plate = temp;



            subQuery = subQuery.Where(x => (x.Plate == filter.Plate) && (x.Status == SubscriberStatus.Active));
            if (subQuery.Count() != 0)
            {
                var subscriber = subQuery.First();

                var customer = _repository.Get<Customer>(subscriber.CustomerId);

                return new MobileStartOpView
                {
                    Plate = filter.Plate,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Gender = customer.Gender,
                    PhoneNumber = customer.PhoneNumber,
                    VehicleType = subscriber.Type,
                    VehicleBrand = subscriber.Brand,
                    CalculatedAmount = GetPrice(new PriceCalculationFilter
                    {

                        OperationTypeId = operationType.Id,
                        VehicleType = filter.VehicleType,
                    })
                };

            }else
            {
                opQuery = opQuery.Where(x => x.Plate == filter.Plate && x.Status == Core.Status.Active);

                if (opQuery.Count() != 0)
                {
                    //Ayrıntılı operationDate gelmeli
                    var entity = opQuery.OrderByDescending(x => x.CreatedDate).FirstOrDefault();

                    return new MobileStartOpView
                    {
                        Plate = filter.Plate,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Gender = entity.Gender,
                        PhoneNumber = entity.PhoneNumber,
                        VehicleType = entity.VehicleType,
                        VehicleBrand = entity.VehicleBrand,
                        CalculatedAmount = GetPrice(new PriceCalculationFilter
                        {
                            OperationTypeId = operationType.Id,
                            VehicleType = filter.VehicleType,
                        })
                    };

                }
                else
                {
                    return new MobileStartOpView
                    {
                        Plate = filter.Plate,
                        CalculatedAmount = GetPrice(new PriceCalculationFilter
                        {
                            OperationTypeId = operationType.Id,
                            VehicleType = filter.VehicleType,
                        })
                    };


                }

            }
         

        }

        public decimal GetPrice(PriceCalculationFilter filter)
        {
            decimal calculatedPrice = 0;
            var priceQuery = _repository.Query<Price>();

            if ((filter.OperationTypeId>0) && (!string.IsNullOrEmpty(filter.VehicleType)))
            {
                priceQuery = priceQuery.Where(x => x.OperationTypeId == filter.OperationTypeId);

                priceQuery = priceQuery.Where(x => x.VehicleType == filter.VehicleType);
            }
            if (priceQuery.Count() == 0)
                return calculatedPrice;

            calculatedPrice = priceQuery.First().DefaultPrice;


            //if (filter.CustomerDiscount > 0)
            //    calculatedPrice = calculatedPrice - calculatedPrice * filter.CustomerDiscount;

            return calculatedPrice;




        }

        //public long SqlToExcel(OperationView view)
        //{
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Inserting Data");

        //    var opQuery = _repository.Query<Operation>();



        //    var list = opQuery.ToList();

        //    ws.Cell(1, 1).Value = "From Query";
        //    //ws.Range(6, 6, 6, 8).Merge().AddToNamed("Titles");
        //    var rangeWithPeople = ws.Cell(2, 1).InsertData(list.AsEnumerable());
        //    var titlesStyle = wb.Style;
        //    titlesStyle.Font.Bold = true;
        //    titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //    titlesStyle.Fill.BackgroundColor = XLColor.Cyan;

        //    // Format all titles in one shot
        //   // wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;

        //    ws.Columns().AdjustToContents();

        //    wb.SaveAs("InsertingData.xlsx");

        //    return 1;

        //}



        public PaginatedList<OperationViewWithOpType> Search(OperationSearchFilter filter, PaginationInfoView paginationInfo)
        {
            //SqlToExcel(new OperationView { });
            var query = _repository.Query<Operation>();
            var opTypeQuery = _repository.Query<OperationType>();


            if (filter.Year > 2010)
            {


                var startDateString = filter.Year.ToString() + "01" + "01";
                var startDate = Convert.ToInt64(startDateString);

                var stopDateString = filter.Year.ToString() + "12" + "31";
                var stopDate = Convert.ToInt64(stopDateString);

                query = query.Where(x => x.OperationDate >= startDate && x.OperationDate <= stopDate);

             

            }

            if (filter.MonthlyDate > 0)
            {
                var monthString = filter.MonthlyDate.ToString();
                if (filter.MonthlyDate < 10)
                {
                    monthString="0"+ filter.MonthlyDate.ToString();
                }
                var periodDateString = "2019" + monthString;
                var periodDate = Convert.ToInt64(periodDateString);
                query = query.Where(x => x.PeriodDate == periodDate);
              
            }

            if (!string.IsNullOrWhiteSpace(filter.Plate))
            {
                var temp = filter.Plate.Trim();
                temp = temp.Replace(" ", "");
                temp = temp.ToUpper();
                filter.Plate = temp;
                query = query.Where(x => x.Plate.Contains(filter.Plate));
            }
            if (filter.Min > 0)
            {
                query = query.Where(x => x.OperationAmount > filter.Min);
            }

            if (filter.Max > 0)
            {
                query = query.Where(x => x.OperationAmount < filter.Max);
            }

            if (filter.OperationTypeId > 0)
            {
                query = query.Where(x => x.OperationTypeId == filter.OperationTypeId);
            }


            if (filter.TipAmount > 0)
            {
                query = query.Where(x => x.TipAmount == filter.TipAmount);
            }

            if (filter.PaymentMethod != Core.PaymentMethod.None)
            {
                query = query.Where(x => x.PaymentMethod == filter.PaymentMethod);
            }

            if (filter.Date > 0)
            {
                if (filter.EndDate > 0)
                {
                    if (filter.EndDate < filter.Date)
                        filter.EndDate = filter.Date;

                    if (filter.Date < 20180000)   //date 2019125 şeklindeyse
                    {
                        var body = filter.Date.ToString();
                        var day = body[body.Length - 1];
                        body = body.Substring(0, body.Length - 1);
                        body = body + "0" + day;
                        filter.Date = Convert.ToInt64(body);
                    }
                    if (filter.EndDate < 20180000)
                    {
                        var body = filter.EndDate.ToString();
                        var day = body[body.Length - 1];
                        body = body.Substring(0, body.Length - 1);
                        body = body + "0" + day;
                        filter.EndDate = Convert.ToInt64(body);
                    }




                    query = query.Where(x => x.OperationDate >= filter.Date);
                    query = query.Where(x => x.OperationDate <= filter.EndDate);
                }
                else
                {

                    if (filter.Date < 20180000)   //date 2019125 şeklindeyse
                    {
                        var body = filter.Date.ToString();
                        var day = body[body.Length - 1];
                        body = body.Substring(0, body.Length - 1);
                        body = body + "0" + day;
                        filter.Date = Convert.ToInt64(body);
                    }
                    query = query.Where(x => x.OperationDate == filter.Date);
                }
               
            }


            if (filter.AccountType>0)
            {
             
                var filteredQuery = opTypeQuery.Where(opType => opType.AccountType == filter.AccountType);

                var idList = filteredQuery.Select(y => y.Id).ToList();
                query = query.Where(op => idList.Contains(op.OperationTypeId));
            }

            if (filter.PaymentInfo > 0)
            {
                if ((int)filter.PaymentInfo == 1)
                {
                    query = query.Where(x=>x.OperationAmount>0);
                }
                else
                {
                    query = query.Where(x => x.OperationAmount == 0);
                }
            }
            
          
            query = query.Where(x => x.Status == Core.Status.Active);

            //var opTypeMap = opTypeQuery.ToDictionary(x => x.Id, x=> x.Name);
            PeriodMapper mapper = new PeriodMapper();
            //var newQuery = query.Select(x => new OperationViewWithOpType
            //{
            //    Id = x.Id,
            //    Plate = x.Plate,
            //    PaymentMethod = x.PaymentMethod,
            //    OperationDate = x.OperationDate,
            //    OperationAmount = x.OperationAmount,
            //    TipAmount = x.TipAmount,
            //    CalculatedAmount = x.CalculatedAmount,

            //    Description = x.Description ?? "",
            //    PeriodDate = x.PeriodDate,
            //    PeriodMonth = mapper.Map(x.PeriodDate),
            //    FirstName = x.FirstName ?? "",
            //    LastName = x.LastName ?? "",
            //    Gender = x.Gender ?? "",
            //    VehicleModel = x.VehicleModel ?? "",
            //    VehicleType = x.VehicleType ?? "",
            //    VehicleBrand = x.VehicleBrand ?? "",
            //    PhoneNumber = x.PhoneNumber ?? "",
            //    CreatedDate = x.CreatedDate,
            //});


            var newQuery = query.Join(opTypeQuery,
                operation => operation.OperationTypeId,
                opType => opType.Id,
                (operation, opType) => new { OP = operation, OPTYPE = opType }).Where(a => a.OP.OperationTypeId == a.OPTYPE.Id).Select(x => new OperationViewWithOpType
                {
                    Id = x.OP.Id,
                    Plate = x.OP.Plate,
                    PaymentMethod = x.OP.PaymentMethod,
                    OperationDate = x.OP.OperationDate,
                    OperationAmount = x.OP.OperationAmount,
                    TipAmount = x.OP.TipAmount,
                    CalculatedAmount = x.OP.CalculatedAmount,
                    OperationType = x.OPTYPE.Name,
                    Description = x.OP.Description ?? "",
                    PeriodDate = x.OP.PeriodDate,
                    PeriodMonth = mapper.Map(x.OP.PeriodDate),
                    FirstName = x.OP.FirstName ?? "",
                    LastName = x.OP.LastName ?? "",
                    Gender = x.OP.Gender ?? "",
                    VehicleModel = x.OP.VehicleModel ?? "",
                    VehicleType = x.OP.VehicleType ?? "",
                    VehicleBrand = x.OP.VehicleBrand ?? "",
                    PhoneNumber = x.OP.PhoneNumber ?? "",
                    CreatedDate = x.OP.CreatedDate,
                }


                );

            if (filter.EndDate > 0)
            {
                newQuery = newQuery.OrderBy(x => x.OperationDate);
            }
            else
            {
                newQuery = newQuery.OrderByDescending(x => x.CreatedDate);


            }



            PaginatedList<OperationViewWithOpType> paginatedList = new PaginatedList<OperationViewWithOpType>(paginationInfo, newQuery);


            return paginatedList;

        }



        public PaginatedList<OperationTypeView> SearchOperationType(OperationTypeFilter filter, PaginationInfoView paginationInfo)
        {
            //SqlToExcel(new OperationView { });
            var query = _repository.Query<OperationType>();
          

            //if (!string.IsNullOrWhiteSpace(filter.Code))
            //{
            //    query = query.Where(x => x.OperationType == filter.OperationType);
            //}

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => x.Name == filter.Name);
            }
         

            if (filter.Id > 0)
            {
                query = query.Where(x => x.Id == filter.Id);
            }


            if (filter.AccountType > 0)
            {
                query = query.Where(x => x.AccountType == filter.AccountType);
            }




            var newQuery = query.Select(x => new OperationTypeView
            {
                AccountType = x.AccountType,
                Code = x.Code,
                Name = x.Name,
                Id = x.Id

            }
         );

            newQuery = newQuery.OrderBy(x => x.Name);


         


            PaginatedList<OperationTypeView> paginatedList = new PaginatedList<OperationTypeView>(paginationInfo, newQuery);


            return paginatedList;

        }
    }
}

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    public class ReportFacade : IReportFacade
    {
        private readonly IRepository _repository;

        public ReportFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PaginatedListN<WrappedCustomerReportView> CustomerReport(CustomerReportSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var opQuery = _repository.Query<Operation>();
            var subQuery = _repository.Query<Subscriber>();
            var opTypeQuery = _repository.Query<OperationType>();
            var cusQuery = _repository.Query<Customer>();

            opQuery = opQuery.Where(x => x.Status == Core.Status.Active);

            if (!string.IsNullOrEmpty(filter.Plate)) {

                opQuery = opQuery.Where(x => x.Plate.Contains(filter.Plate));

            }
            long year;
            if (filter.Year > 0)
            {
                 year = filter.Year;             
            }
            else
            {
                 year = DateTime.Now.Year;  
            }


            var startDateString = year.ToString() + "01";
            var yearStart = Convert.ToInt64(startDateString);
            var stopDateString = year.ToString() + "12";
            var yearEnd = Convert.ToInt64(stopDateString);
            opQuery = opQuery.Where(x => x.PeriodDate >= yearStart && x.PeriodDate <= yearEnd);

            if (!string.IsNullOrEmpty(filter.FirstName))
            {

                opQuery = opQuery.Where(x => x.FirstName.Contains(filter.FirstName.Trim()));

            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {

                opQuery = opQuery.Where(x => x.LastName.Contains(filter.LastName));

            }

            if (filter.OperationTypeId > 0)
            {
                opQuery = opQuery.Where(x => x.OperationTypeId == filter.OperationTypeId);
            }



            var newQuery = from subs in subQuery.ToList()
                           join op in opQuery on subs.Plate.ToUpper() equals op.Plate.ToUpper()
                           join cus in cusQuery on subs.CustomerId equals cus.Id
                           select new CustomerReportView
                           {
                               CustomerId=cus.Id,
                               Plate = op.Plate,
                               OperationDate = op.OperationDate,
                               PeriodDate = op.PeriodDate,
                               OperationAmount = op.OperationAmount,
                               Description = op.Description,
                               FirstName = cus.FirstName,
                               LastName = cus.LastName,
                           };


            //var newQuery = subQuery.Join(opQuery,

            //    subscriber => subscriber.Plate,
            //      operation => operation.Plate,


            //    (subscriber, operation) => new { OP = operation, SUBS = subscriber }).Where(a => a.OP.Plate == a.SUBS.Plate).Join(cusQuery,
            //    outer => outer.SUBS.CustomerId,
            //    customer => customer.Id,
            //    (outer, customer) => new { OUT = outer, CUS = customer }).Where(b => b.OUT.SUBS.CustomerId == b.CUS.Id).Select(x => new CustomerReportView
            //    {
            //        Plate = x.OUT.OP.Plate,
            //        OperationDate = x.OUT.OP.OperationDate,
            //        PeriodDate = x.OUT.OP.PeriodDate,
            //        OperationAmount = x.OUT.OP.OperationAmount,
            //        Description = x.OUT.OP.Description,
            //        FirstName = x.CUS.FirstName,
            //        LastName = x.CUS.LastName,
            //    }
            //    );

            //  var nquery = newQuery.GroupBy(x => x.Plate).ToDictionary(x => x.Key);

            //Dictionary<string, List<CustomerReportView>> myDictionary = newQuery
            //.GroupBy(o => o.Plate)
            //.ToDictionary(g => g.Key, g => g.ToList());


            //Dictionary<string, List<CustomerReportView>> myDictionaryx = newQuery
            //.GroupBy(o => o.Plate)
            //.ToDictionary(g => g.Key, g => g.Select(x => new CustomerReportView
            //{
            //    FirstName = x.FirstName

            //}).ToList()) ;

            /// var liste = newQuery.ToList();

            

            var n = newQuery.AsQueryable();
            List<WrappedCustomerReportView> reportViewList = new List<WrappedCustomerReportView>();
            

            if (newQuery.Count() != 0)
            {
                var listx = newQuery.ToList();

                var liste = listx.GroupBy(x => x.Plate).Select(x => x.ToList().OrderBy(z=>z.PeriodDate)).ToList();

                foreach(var x in liste)
                {
                    long emptyMonthPointer = 201901;
                    long emptyMonthPointerForInıt = 201901;
                    WrappedCustomerReportView temp = new WrappedCustomerReportView();
                    temp.FirstName = x.FirstOrDefault().FirstName;
                    temp.LastName = x.FirstOrDefault().LastName;
                    temp.Plate = x.FirstOrDefault().Plate;
                    temp.CustomerId = x.FirstOrDefault().CustomerId;
                    temp.List = new List<CustomerReportView>();

                    for(int i = 0; i < 12; i++)
                    {
                        temp.List.Add(new CustomerReportView
                        {
                            CustomerId = x.FirstOrDefault().CustomerId,
                            PeriodDate = emptyMonthPointerForInıt,
                            OperationAmount = 0,

                        });
                        emptyMonthPointerForInıt++;
                    }

                    foreach (var y in x)
                    {
                       
                            var index = ((int)y.PeriodDate % 100)-1;
                            temp.List[index] = y;


                      
                    }
                    //foreach (var y in x)
                    //{
                    //    if (y.PeriodDate == emptyMonthPointer)
                    //    {
                    //        temp.List.Add(y);

                    //    }
                    //    else
                    //    {
                    //        temp.List.Add(new CustomerReportView
                    //        {
                    //            PeriodDate = emptyMonthPointer,
                    //            OperationAmount = 0,

                    //        });

                    //    }
                    //    emptyMonthPointer++;
                    //}

                    reportViewList.Add(temp);
                    temp = null;
                }
                PaginatedListN<WrappedCustomerReportView> paginatedList = new PaginatedListN<WrappedCustomerReportView>(paginationInfo, reportViewList);
                //paginatedList.List = reportViewList;
                
                return paginatedList;
            }
            else
            {
                PaginatedListN<WrappedCustomerReportView> paginatedList = new PaginatedListN<WrappedCustomerReportView>(paginationInfo, new List<WrappedCustomerReportView>());
                paginatedList.List = null;

                return paginatedList;

            }
                            

        }

        public string CustomerReportPdf(CustomerReportSearchFilter filter, PaginationInfoView paginationInfo)
        {
            paginationInfo.PageSize = 200;

            var list = CustomerReport(filter, paginationInfo);

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var z = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            z = String.Concat(z, ".pdf");

            iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontBold = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.BOLD);

            Document document = new Document(iTextSharp.text.PageSize.LETTER, 20, 20, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(z, FileMode.Create));
            document.Open();

            Paragraph paragraph = new Paragraph("Müşteri Aidat Raporu", fontBold);
            paragraph.Alignment = Element.ALIGN_CENTER;

            document.Add(paragraph);




            PdfPTable table = new PdfPTable(14);
            table.SpacingBefore = 10f;

            table.AddCell(new Phrase("Plaka", fontNormal));
            table.AddCell(new Phrase("Ad-Soyad", fontNormal));
            table.AddCell(new Phrase("Ocak", fontNormal));
            table.AddCell(new Phrase("Şubat", fontNormal));
            table.AddCell(new Phrase("Mart", fontNormal));
            table.AddCell(new Phrase("Nisan", fontNormal));
            table.AddCell(new Phrase("Mayıs", fontNormal));
            table.AddCell(new Phrase("Haziran", fontNormal));
            table.AddCell(new Phrase("Temmuz", fontNormal));
            table.AddCell(new Phrase("Ağustos", fontNormal));
            table.AddCell(new Phrase("Eylül", fontNormal));
            table.AddCell(new Phrase("Ekim", fontNormal));
            table.AddCell(new Phrase("Kasım", fontNormal));
            table.AddCell(new Phrase("Aralık", fontNormal));

            foreach (var x in list.List)
            {
             
                //PdfPCell cell = new PdfPCell(new Phrase(x.FirstOrDefault().MonthName, fontNormal));

                //cell.Colspan = 8;

                //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                //table.AddCell(cell);




                table.AddCell(new Phrase(x.Plate, fontNormal));
                table.AddCell(new Phrase(x.FirstName + " " + x.LastName, fontNormal));
                foreach (var field in x.List)
                {

                   
                    table.AddCell(new Phrase(field.OperationAmount.ToString("0.##"), fontNormal));
                  
                }


              

            }

            document.Add(table);
            document.Close();
            byte[] pdfBytes = File.ReadAllBytes(z);
            string pdfBase64 = Convert.ToBase64String(pdfBytes);
            File.Delete(z);


            return pdfBase64;


        }


        public List<List<BalanceReportView>> BalanceReport(BalanceReportSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var opQuery = _repository.Query<Operation>();
            var opTypeQuery = _repository.Query<OperationType>();
            var CurrentMonth = DateTime.Now.Month;
          

            opTypeQuery = opTypeQuery.Where(x => x.Id != -1);
            opQuery = opQuery.Where(x => x.Status == Core.Status.Active);

            var opTypeList = opTypeQuery.ToList();


            var commonStartDateString = filter.Year.ToString() + "01" + "01";
            var commonStartDate = Convert.ToInt64(commonStartDateString);

            var commonStopDateString = filter.Year.ToString() + CurrentMonth.ToString() + "31";
            var commonStopDate = Convert.ToInt64(commonStopDateString);

             opQuery = opQuery.Where(x => x.OperationDate >= commonStartDate && x.OperationDate <= commonStopDate);


            var opList = opQuery.ToList();



            List<List<BalanceReportView>> liste = new List<List<BalanceReportView>>() ;
            PeriodMapper mapper = new PeriodMapper();
            int monthInt = 1;
            string month;
            while (monthInt <= CurrentMonth)
            {
                month = monthInt.ToString();
                if (monthInt < 10)
                {
                    month = "0" + monthInt.ToString();
                }
                
                var startDateString = filter.Year.ToString() + month + "01";
                var startDate = Convert.ToInt64(startDateString);

                var stopDateString = filter.Year.ToString() + month + "31";
                var stopDate = Convert.ToInt64(stopDateString);

                var tempList = opList.Where(x => x.OperationDate >= startDate && x.OperationDate <= stopDate);

 
                //OPTIMIZE EDILEBILIR
                var list = tempList.GroupBy(x => x.OperationTypeId)


              .Select(x => new BalanceReportView
              {
                  AccountType= opTypeList.Where(op => op.Id == x.First().OperationTypeId).ToList().FirstOrDefault().AccountType ,
                  MonthName = mapper.Map(monthInt),
                  OperationName = opTypeList.Where(op => op.Id == x.First().OperationTypeId).ToList().First().Name ?? " " ,
                  CashOpCount= x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Count(),
                  CreditCardOpCount = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Count(),
                  TotalOpCount = x.Count(),
                  CashSum = x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Sum(s => s.OperationAmount),
                  CreditCardSum = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Sum(s => s.OperationAmount),
                  TotalSum = x.Sum(s => s.OperationAmount),
              }).ToList();

                foreach(var x in list)
                {
                    if (x.AccountType == AccountType.Debit)
                    {
                        x.CashSum = x.CashSum * -1;
                        x.CreditCardSum = x.CreditCardSum * -1;
                        x.TotalSum = x.TotalSum * -1;
                    }
                        
                }
                                          
                if (list.Count > 0)
                {
                    list.Add(new BalanceReportView
                    {
                        OperationName = "Toplam",
                        CashOpCount = list.Sum(x => x.CashOpCount),
                        CreditCardOpCount = list.Sum(x => x.CreditCardOpCount),
                        TotalOpCount = list.Sum(x => x.TotalOpCount),
                        CashSum = list.Sum(x => x.CashSum),
                        CreditCardSum = list.Sum(x => x.CreditCardSum),
                        TotalSum = list.Sum(x => x.TotalSum)


                    });

                    liste.Add(list);

                }                 
                monthInt++;
            }
            return liste;

        }

        public string BalanceReportPdf(BalanceReportSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var list = BalanceReport(filter, paginationInfo);
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var z = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            z = String.Concat(z, ".pdf");

            iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontBold = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.BOLD);

            Document document = new Document(iTextSharp.text.PageSize.LETTER, 20, 20, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(z, FileMode.Create));
            document.Open();

            Paragraph paragraph = new Paragraph("Yıllık Bütçe Raporu", fontBold);
            paragraph.Alignment = Element.ALIGN_CENTER;

            document.Add(paragraph);


            foreach (var x in list)
            {
                PdfPTable table = new PdfPTable(7);
                table.SpacingBefore = 10f;
                PdfPCell cell = new PdfPCell(new Phrase(x.FirstOrDefault().MonthName, fontNormal));

                cell.Colspan = 8;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);


                table.AddCell(new Phrase("İşlem Adı", fontNormal));
                table.AddCell(new Phrase("Nakit #", fontNormal));
                table.AddCell(new Phrase("Kredi Kartı #", fontNormal));
                table.AddCell(new Phrase("Toplam #", fontNormal));
                table.AddCell(new Phrase("Nakit $", fontNormal));
                table.AddCell(new Phrase("Kredi Kartı $", fontNormal));
                table.AddCell(new Phrase("Toplam $", fontNormal));
                foreach (var field in x)
                {

                    table.AddCell(new Phrase(field.OperationName, fontNormal));
                    table.AddCell(new Phrase(field.CashOpCount.ToString("0.##"), fontNormal));
                    table.AddCell(new Phrase(field.CreditCardOpCount.ToString("0.##"), fontNormal));
                    table.AddCell(new Phrase(field.TotalOpCount.ToString("0.##"), fontNormal));
                    table.AddCell(new Phrase(field.CashSum.ToString("0.##"), fontNormal));
                    table.AddCell(new Phrase(field.CreditCardSum.ToString("0.##"), fontNormal));
                    table.AddCell(new Phrase(field.TotalSum.ToString("0.##"), fontNormal));
                }
                document.Add(table);

            }

            
            document.Close();
            byte[] pdfBytes = File.ReadAllBytes(z);
            string pdfBase64 = Convert.ToBase64String(pdfBytes);
            File.Delete(z);

            return pdfBase64;


        }
        public List<OperationReportView> OperationReport(OperationReportSearchFilter filter, PaginationInfoView paginationInfo)
        {
            var opQuery = _repository.Query<Operation>();
            var opTypeQuery = _repository.Query<OperationType>();
            var CurrentMonth = DateTime.Now.Month;
          
            opTypeQuery = opTypeQuery.Where(x => x.Id != -1);

            List<List<BalanceReportView>> liste = new List<List<BalanceReportView>>();
            PeriodMapper mapper = new PeriodMapper();

            if (filter.Date==0){
                filter.Date = DateTime.Now.ToDate();
            }

                var tmpQuery = opQuery.Where(x => x.OperationDate==filter.Date&&x.Status==Core.Status.Active);

                var tlist = tmpQuery.ToList();
                //OPTIMIZE EDILEBILIR
                var list = tlist.GroupBy(x => x.OperationTypeId)

              .Select(x => new OperationReportView
              {
                  AccountType = opTypeQuery.Where(op => op.Id == x.First().OperationTypeId).ToList().First().AccountType,               
                  OperationName = opTypeQuery.Where(op => op.Id == x.First().OperationTypeId).ToList().First().Name ?? " ",
                  UniqueName = opTypeQuery.Where(op => op.Id == x.First().OperationTypeId).ToList().First().Code.Replace(" ","").Replace(".","") ?? " ",
                  CashOpCount = x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Count(),
                  CreditCardOpCount = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Count(),
                  TotalOpCount = x.Count(),
                  CashSum = x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Sum(s => s.OperationAmount),
                  CreditCardSum = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Sum(s => s.OperationAmount),
                  TotalSum = x.Sum(s => s.OperationAmount),
                  List=x.Select(y=> new OperationViewWithOpType {

                      Id = y.Id,
                      Plate = y.Plate,
                      PaymentMethod = y.PaymentMethod,
                      OperationDate = y.OperationDate,
                      OperationAmount = y.OperationAmount,
                      TipAmount = y.TipAmount,
                      CalculatedAmount = y.CalculatedAmount,
                      Description = y.Description ?? "",
                      PeriodDate = y.PeriodDate,
                      PeriodMonth = mapper.Map(y.PeriodDate),
                      FirstName = y.FirstName ?? "",
                      LastName = y.LastName ?? "",
                      Gender = y.Gender ?? "",
                      VehicleModel = y.VehicleModel ?? "",
                      VehicleType = y.VehicleType ?? "",
                      VehicleBrand = y.VehicleBrand ?? "",
                      PhoneNumber = y.PhoneNumber ?? "",

                  }).ToList()
              }).ToList();

                foreach (var x in list)
                {
                    if (x.AccountType == AccountType.Debit)
                    {
                        x.CashSum = x.CashSum * -1;
                        
                        x.CreditCardSum = x.CreditCardSum * -1;

                        x.TotalSum = x.TotalSum * -1;
                      
                    }

                }
            if (list.Count != 0)
            {
           
                list.Add(new OperationReportView
                {
                    OperationName = "Toplam",
                    CashOpCount = list.Sum(x => x.CashOpCount),
                    CreditCardOpCount = list.Sum(x => x.CreditCardOpCount),
                    TotalOpCount = list.Sum(x => x.TotalOpCount),
                    CashSum = list.Sum(x => x.CashSum),
                    CreditCardSum = list.Sum(x => x.CreditCardSum),
                    TotalSum = list.Sum(x => x.TotalSum)


                });
            }
            return list;

        }

    }
}



 //.Select(x => new OperationReportView
 //             {
 //                 AccountType = opTypeQuery.Where(op => op.Id == x.First().OperationTypeId).ToList().First().AccountType,               
 //                 OperationName = opTypeQuery.Where(op => op.Id == x.First().OperationTypeId).ToList().First().Name ?? " ",
 //                 CashOpCount = x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Count(),
 //                 CreditCardOpCount = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Count(),
 //                 TotalOpCount = x.Count(),
 //                 CashSum = x.Where(z => z.PaymentMethod == PaymentMethod.Cash).Sum(s => s.OperationAmount),
 //                 CreditCardSum = x.Where(z => z.PaymentMethod == PaymentMethod.CreditCard).Sum(s => s.OperationAmount),
 //                 TotalSum = x.Sum(s => s.OperationAmount),
 //             }).ToList();

 //               foreach (var x in list)
 //               {
 //                   if (x.AccountType == AccountType.Debit)
 //                   {
 //                       x.CashSum = x.CashSum* -1;
                        
 //                       x.CreditCardSum = x.CreditCardSum* -1;

                      
 //                   }

 //               }
 //               list.Add(new OperationReportView
 //               {
 //                   OperationName = "Toplam",
 //                   CashOpCount = list.Sum(x => x.CashOpCount),
 //                   CreditCardOpCount = list.Sum(x => x.CreditCardOpCount),
 //                   TotalOpCount = list.Sum(x => x.TotalOpCount),
 //                   CashSum = list.Sum(x => x.CashSum),
 //                   CreditCardSum = list.Sum(x => x.CreditCardSum),
 //                   TotalSum = list.Sum(x => x.TotalSum)


 //               });


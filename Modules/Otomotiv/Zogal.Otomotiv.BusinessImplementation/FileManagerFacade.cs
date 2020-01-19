using ClosedXML.Excel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Zogal.Core;
using Zogal.Otomotiv.Business;
using Zogal.Otomotiv.EntityModel;
using Zogal.Otomotiv.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Zogal.Otomotiv.BusinessImplementation
{
    public class FileManagerFacade : IFileManagerFacade
    {
        IRepository _repository;

        public FileManagerFacade(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public long SqlToExcel(OperationView view)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Inserting Data");

            var opQuery = _repository.Query<Operation>();

            

            var list = opQuery.ToList();

            ws.Cell(6, 6).Value = "From Query";
            ws.Range(6, 6, 6, 8).Merge().AddToNamed("Titles");
            var rangeWithPeople = ws.Cell(7, 6).InsertData(list.AsEnumerable());
            var titlesStyle = wb.Style;
            titlesStyle.Font.Bold = true;
            titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            titlesStyle.Fill.BackgroundColor = XLColor.Cyan;

            // Format all titles in one shot
            wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;

            ws.Columns().AdjustToContents();

            wb.SaveAs("C:/InsertingData.xlsx");

            return 1;

        }

        public long SqlToPdf(List<List<BalanceReportView>> list)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var z= new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            z = String.Concat(z, ".pdf");

            iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontBold= new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.BOLD);
      
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
                PdfPCell cell = new PdfPCell(new Phrase(x.FirstOrDefault().MonthName,fontNormal));

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
                    table.AddCell(new Phrase(field.CashOpCount.ToString(), fontNormal));
                    table.AddCell(new Phrase(field.CreditCardOpCount.ToString(), fontNormal));
                    table.AddCell(new Phrase(field.TotalOpCount.ToString(), fontNormal));
                    table.AddCell(new Phrase(field.CashSum.ToString(), fontNormal));
                    table.AddCell(new Phrase(field.CreditCardSum.ToString(), fontNormal));
                    table.AddCell(new Phrase(field.TotalSum.ToString(), fontNormal));
                }
                document.Add(table);
                
            }

          
            document.Close();
            return 1;
        }
        public long Upload(UploadFileView file)
        {
            throw new NotImplementedException();


        }




    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Zogal.Otomotiv.ViewModel;

namespace Zogal.Otomotiv.Business
{
    public interface IFileManagerFacade
    {
        long Upload(UploadFileView file);

        long SqlToExcel(OperationView view);

        long SqlToPdf(List<List<BalanceReportView>> List);
    }
}

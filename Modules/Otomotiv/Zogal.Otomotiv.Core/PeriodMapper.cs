using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.Core
{
    public class PeriodMapper
    {
       
        public PeriodMapper()
        {
           
        }

        public String Map(long date)
        {
            string Month = "";
            var tmpDate = date % 100;
            if (tmpDate == 1)
                Month = "Ocak";
            if (tmpDate == 2)
                Month = "Şubat";
            if (tmpDate == 3)
                Month = "Mart";
            if (tmpDate == 4)
                Month = "Nisan";
            if (tmpDate == 5)
                Month = "Mayıs";
            if (tmpDate == 6)
                Month = "Haziran";
            if (tmpDate == 7)
                Month = "Temmuz";
            if (tmpDate == 8)
                Month = "Ağustos";
            if (tmpDate == 9)
                Month = "Eylül";
            if (tmpDate == 10)
                Month = "Ekim";
            if (tmpDate == 11)
                Month = "Kasım";
            if (tmpDate == 12)
                Month = "Aralık";


            return Month;

        }
    }
}

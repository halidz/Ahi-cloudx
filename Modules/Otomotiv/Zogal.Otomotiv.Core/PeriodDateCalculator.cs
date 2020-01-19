using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.Core
{
    public class PeriodDateCalculator
    {
        public long PeriodDate { get; set; }

        public PeriodDateCalculator()
        {

        }
        public PeriodDateCalculator(long oldPeriodDate)
        {
            long periodDate;

                    periodDate = oldPeriodDate + 1;

                    if (periodDate % 100 > 12)  //ay sayısı 12 yi geçtiyse yılı artır.
                    {
                        if (periodDate % 1000 > 912)   //yıl sayısı 9 u geçtiyse onlar basamağını artır.
                        {
                            periodDate = periodDate + 1000 - (periodDate % 1000) + 1;
                        }
                        else
                        {
                            periodDate = periodDate + 100 - (periodDate % 100)+1;
                        }
                    }

            PeriodDate = periodDate;
        }

        public long Calculate(long oldPeriodDate)
        {
            long periodDate;

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
            return periodDate;
        }
    }
}

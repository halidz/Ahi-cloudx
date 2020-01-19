using System;
using System.Collections.Generic;
using System.Text;

namespace Zogal.Otomotiv.ViewModel
{
    public class CounterView
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public  decimal StartValue { get; set; }

        public decimal Value { get; set; }

        public  decimal StopValue { get; set; }

        public  decimal SpentValue { get; set; }

        public long Date { get; set; }
    }
}

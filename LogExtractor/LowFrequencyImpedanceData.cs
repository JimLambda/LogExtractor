using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogExtractor
{
    public class LowFrequencyImpedanceData
    {
        public string Time { get; set; }
        public double ImpedanceValue { get; set; }

        public LowFrequencyImpedanceData(string time, double impedanceValue)
        {
            Time = time;
            ImpedanceValue = impedanceValue;
        }
    }
}

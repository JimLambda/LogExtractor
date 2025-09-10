using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogExtractor
{
    public class LowFrequencyImpedanceData : LogData
    {
        public double ImpedanceValue { get; set; }

        public LowFrequencyImpedanceData(string time, double impedanceValue) : base(time)
        {
            Time = time;
            ImpedanceValue = impedanceValue;
        }

        public override void Display()
        {
            Console.WriteLine($"Time: {Time} | Impedance: {ImpedanceValue}");
        }
    }
}

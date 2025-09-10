using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogExtractor
{
    public class BondForceCalibrationData : LogData
    {
        public string bfc_0g_current { get; set; }
        public string bfc_scale_fct { get; set; }
        public string forceSensorSlope { get; set; }

        public BondForceCalibrationData(string time, string bfc_0g_current, string bfc_scale_fct, string forceSensorSlope) : base(time)
        {
            Time = time;
            this.bfc_0g_current = bfc_0g_current;
            this.bfc_scale_fct = bfc_scale_fct;
            this.forceSensorSlope = forceSensorSlope;
        }

        public override void Display()
        {
            Console.WriteLine($"Time: {Time} | bfc_0g_current: {bfc_0g_current} | bfc_scale_fct: {bfc_scale_fct} | forceSensorSlope: {forceSensorSlope}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogExtractor
{
    public abstract class LogData
    {
        public string Time { get; set; }

        public LogData(string time)
        {
            Time = time;
        }

        // Abstract method to display data.
        public abstract void Display();
    }
}

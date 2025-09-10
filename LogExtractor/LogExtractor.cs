using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogExtractor
{
    public class LogExtractor
    {
        private readonly string _logFilePath;
        private readonly Regex _timeRegex;
        private readonly Regex _impedanceRegex;
        private readonly Regex _bondForceRegex;
        private readonly Regex _bfc_0g_currentRegex;
        private readonly Regex _bfc_scale_fctRegex;
        private readonly Regex _forceSensorSlopeRegex;

        public List<LowFrequencyImpedanceData> ImpedanceResults { get; set; } = new List<LowFrequencyImpedanceData>();
        public List<BondForceCalibrationData> BondForceResults { get; set; } = new List<BondForceCalibrationData>();

        public LogExtractor(string logFilePath)
        {
            // Initialize regex patterns and log file path.
            _logFilePath = logFilePath;
            _timeRegex = new Regex(@"(\w+ \w+ \d+ \d+:\d+:\d+ \d+)   <\w*>");
            _impedanceRegex = new Regex(@"\[ LOW FREQUENCY Impedance : (\d*\.\d*) \]");
            _bondForceRegex = new Regex(@"\[Bond Force Calibration is done with result CAL_OK, data 1 of 3 total:\]");
            _bfc_0g_currentRegex = new Regex(@"\[bfc_0g_current += +(-?\d+\.\d* \w+ \w+)\]");
            _bfc_scale_fctRegex = new Regex(@"\[bfc_scale_fct += +(-?\d+\.\d* \w+ \w+)\]");
            _forceSensorSlopeRegex = new Regex(@"\[forceSensorSlope += +(-?\d+\.\d* \w+ \w+)\]");
        }

        /// <summary>
        /// Extracts data from the log file.
        /// </summary>
        public void ExtractData()
        {
            List<string> logLines = File.ReadAllLines(_logFilePath).ToList();
            ProcessLogLines(logLines);
        }

        private void ProcessLogLines(List<string> logLines)
        {
            string lastSeenTime = "";
            Boolean isBondForce = false;
            string bfc_0g_current = "";
            string bfc_scale_fct = "";
            string forceSensorSlope = "";

            foreach (string line in logLines)
            {
                UpdateLastSeenTime(line, ref lastSeenTime);
                ProcessImpedanceLine(line, ref lastSeenTime);
                ProcessBondForceLine(line, ref lastSeenTime, ref isBondForce, ref bfc_0g_current, ref bfc_scale_fct, ref forceSensorSlope);
            }
        }

        private void ProcessBondForceLine(string line, ref string lastSeenTime, ref bool isBondForce, ref string bfc_0g_current, ref string bfc_scale_fct, ref string forceSensorSlope)
        {
            Match bondForceMatch = _bondForceRegex.Match(line);
            if (bondForceMatch.Success && lastSeenTime != "" && isBondForce == false)
            {
                isBondForce = true;
                //bondForceResults.Add(new Tuple<string, string, string, string>(lastSeenTime, "", "", ""));
                //lastSeenTime = "";
            }

            Match bfc_0g_currentMatch = _bfc_0g_currentRegex.Match(line);
            if (bfc_0g_currentMatch.Success && isBondForce == true && bfc_0g_current == "")
            {
                bfc_0g_current = bfc_0g_currentMatch.Groups[1].Value;
            }

            Match bfc_scale_fctMatch = _bfc_scale_fctRegex.Match(line);
            if (bfc_scale_fctMatch.Success && isBondForce == true && bfc_scale_fct == "")
            {
                bfc_scale_fct = bfc_scale_fctMatch.Groups[1].Value;
            }

            Match forceSensorSlopeMatch = _forceSensorSlopeRegex.Match(line);
            if (forceSensorSlopeMatch.Success && isBondForce == true && forceSensorSlope == "")
            {
                forceSensorSlope = forceSensorSlopeMatch.Groups[1].Value;

                BondForceResults.Add(new BondForceCalibrationData(lastSeenTime, bfc_0g_current, bfc_scale_fct, forceSensorSlope));
                lastSeenTime = "";
                isBondForce = false;
                bfc_0g_current = "";
                bfc_scale_fct = "";
                forceSensorSlope = "";
            }
        }

        private void ProcessImpedanceLine(string line, ref string lastSeenTime)
        {
            Match impedanceMatch = _impedanceRegex.Match(line);
            if (impedanceMatch.Success && lastSeenTime != "")
            {
                double impedanceValue = double.Parse(impedanceMatch.Groups[1].Value);

                ImpedanceResults.Add(new LowFrequencyImpedanceData(lastSeenTime, impedanceValue));

                lastSeenTime = "";
            }
        }

        private void UpdateLastSeenTime(string line, ref string lastSeenTime)
        {
            Match timeMatch = _timeRegex.Match(line);
            if (timeMatch.Success)
            {
                lastSeenTime = timeMatch.Groups[1].Value;
            }
        }
    }
}

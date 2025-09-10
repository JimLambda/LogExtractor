using System.Text.RegularExpressions;

namespace LogExtractor
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string logFilePath = @"..\..\..\logs\maint.log";

            try
            { 
                LogExtractor extractor = new LogExtractor(logFilePath);
                extractor.ExtractData();

                PrintImpedanceResults(extractor.ImpedanceResults);
                Console.WriteLine("\n\n\n\n");
                PrintBondForceResults(extractor.BondForceResults);

                //List<string> logLines = File.ReadAllLines(logFilePath).ToList();

                //// Store results, time and impedance number.
                //List<Tuple<string, double>> lowFrequencyImpedanceResults = new List<Tuple<string, double>>();
                //List<Tuple<string, string, string, string>> bondForceResults = new List<Tuple<string, string, string, string>>();

                //string lastSeenTime = "";
                //Boolean isBondForce = false;
                //string bfc_0g_current = "";
                //string bfc_scale_fct = "";
                //string forceSensorSlope = "";

                //Regex impedanceRegex = new Regex(@"\[ LOW FREQUENCY Impedance : (\d*\.\d*) \]");
                //Regex timeRegex = new Regex(@"(\w+ \w+ \d+ \d+:\d+:\d+ \d+)   <\w*>");
                //Regex bondForceRegex = new Regex(@"\[Bond Force Calibration is done with result CAL_OK, data 1 of 3 total:\]");
                //Regex bfc_0g_currentRegex = new Regex(@"\[bfc_0g_current += +(-?\d+\.\d* \w+ \w+)\]");
                //Regex bfc_scale_fctRegex = new Regex(@"\[bfc_scale_fct += +(-?\d+\.\d* \w+ \w+)\]");
                //Regex forceSensorSlopeRegex = new Regex(@"\[forceSensorSlope += +(-?\d+\.\d* \w+ \w+)\]");

                //foreach (string line in logLines)
                //{ 
                //    Match timeMatch = timeRegex.Match(line);
                //    if (timeMatch.Success) 
                //    {
                //        lastSeenTime = timeMatch.Groups[1].Value;
                //    }

                //    Match impedanceMatch = impedanceRegex.Match(line);
                //    if (impedanceMatch.Success && lastSeenTime != "")
                //    {
                //        double impedanceValue = double.Parse(impedanceMatch.Groups[1].Value);

                //        lowFrequencyImpedanceResults.Add(new Tuple<string, double>(lastSeenTime, impedanceValue));

                //        lastSeenTime = "";
                //    }

                //    Match bondForceMatch = bondForceRegex.Match(line);
                //    if (bondForceMatch.Success && lastSeenTime != "" && isBondForce == false)
                //    {
                //        isBondForce = true;
                //        //bondForceResults.Add(new Tuple<string, string, string, string>(lastSeenTime, "", "", ""));
                //        //lastSeenTime = "";
                //    }

                //    Match bfc_0g_currentMatch = bfc_0g_currentRegex.Match(line);
                //    if (bfc_0g_currentMatch.Success && isBondForce == true && bfc_0g_current == "")
                //    {
                //        bfc_0g_current = bfc_0g_currentMatch.Groups[1].Value;
                //    }

                //    Match bfc_scale_fctMatch = bfc_scale_fctRegex.Match(line);
                //    if (bfc_scale_fctMatch.Success && isBondForce == true && bfc_scale_fct == "")
                //    {
                //        bfc_scale_fct = bfc_scale_fctMatch.Groups[1].Value;
                //    }

                //    Match forceSensorSlopeMatch = forceSensorSlopeRegex.Match(line);
                //    if (forceSensorSlopeMatch.Success && isBondForce == true && forceSensorSlope == "")
                //    {
                //        forceSensorSlope = forceSensorSlopeMatch.Groups[1].Value;

                //        bondForceResults.Add(new Tuple<string, string, string, string>(lastSeenTime, bfc_0g_current, bfc_scale_fct, forceSensorSlope));
                //        lastSeenTime = "";
                //        isBondForce = false;
                //        bfc_0g_current = "";
                //        bfc_scale_fct = "";
                //        forceSensorSlope = "";
                //    }
                //}

                //Console.WriteLine("Extracted LOW FREQUENCY Impedance (time and impedance value):");
                //Console.WriteLine("-----------------------------------------------------------------------------------------");
                //foreach (Tuple<string, double> item in lowFrequencyImpedanceResults)
                //{
                //    Console.WriteLine($"Time: {item.Item1} | Impedance: {item.Item2}");
                //}
                
                //Console.WriteLine("Extracted Bond Force Calibration (time, bfc_0g_current, bfc_scale_fct, forceSensorSlope):");
                //Console.WriteLine("-----------------------------------------------------------------------------------------");
                //foreach (Tuple<string, string, string, string> item in bondForceResults)
                //{
                //    Console.WriteLine($"Time: {item.Item1} | bfc_0g_current: {item.Item2} | bfc_scale_fct: {item.Item3} | forceSensorSlope: {item.Item4}");
                //}
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: Log file not found at {logFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void PrintBondForceResults(List<BondForceCalibrationData> bondForceResults)
        {
            Console.WriteLine("Extracted Bond Force Calibration (time, bfc_0g_current, bfc_scale_fct, forceSensorSlope):");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            foreach (BondForceCalibrationData bondForceData in bondForceResults)
            {
                Console.WriteLine($"Time: {bondForceData.Time} | bfc_0g_current: {bondForceData.bfc_0g_current} | bfc_scale_fct: {bondForceData.bfc_scale_fct} | forceSensorSlope: {bondForceData.forceSensorSlope}");
            }
        }

        private static void PrintImpedanceResults(List<LowFrequencyImpedanceData> impedanceResults)
        {
            Console.WriteLine("Extracted LOW FREQUENCY Impedance (time and impedance value):");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            foreach (LowFrequencyImpedanceData impedanceData in impedanceResults)
            {
                Console.WriteLine($"Time: {impedanceData.Time} | Impedance: {impedanceData.ImpedanceValue}");
            }
        }
    }
}
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
                List<string> logLines = File.ReadAllLines(logFilePath).ToList();

                // Store results, time and impedance number.
                List<Tuple<string, double>> results = new List<Tuple<string, double>>();

                string lastSeenTime = "";

                Regex impedanceRegex = new Regex(@"\[ LOW FREQUENCY Impedance : (\d*\.\d*) \]");
                Regex timeRegex = new Regex(@"(\w+ \w+ \d+ \d+:\d+:\d+ \d+)   <\w*>");

                foreach (string line in logLines)
                { 
                    Match timeMatch = timeRegex.Match(line);
                    if (timeMatch.Success) 
                    {
                        lastSeenTime = timeMatch.Groups[1].Value;
                    }

                    Match impedanceMatch = impedanceRegex.Match(line);
                    if (impedanceMatch.Success && lastSeenTime != "")
                    {
                        double impedanceValue = double.Parse(impedanceMatch.Groups[1].Value);

                        results.Add(new Tuple<string, double>(lastSeenTime, impedanceValue));

                        lastSeenTime = "";
                    }
                }

                Console.WriteLine("Extracted LOW FREQUENCY Impedance (time and impedance value):");
                Console.WriteLine("---------------------------------------");
                foreach (Tuple<string, double> item in results)
                {
                    Console.WriteLine($"Time: {item.Item1} | Impedance: {item.Item2}");
                }
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
    }
}
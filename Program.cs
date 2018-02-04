using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElasticDiagnostic
{
    public class Program
    {
        public static void Main()
        {
            var lines = string.Empty;
            var fileStream = new FileStream("elastic.json", FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                lines = reader.ReadToEnd();
            }

            // var regex = @"(\w+)_\d*";
            var regex = @"\w+_(\d*)";

            var matches = Regex
                    .Matches(lines, regex)
                    .Where(m => m.Groups[1].Value.Length > 0)
                    .Select(m => m.Value)
                    .OrderBy(a => a);

            Console.WriteLine(string.Join(", " + Environment.NewLine, matches));
        }
    }
}

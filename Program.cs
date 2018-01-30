using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ElasticDiagnostic
{
    public class Program
    {
        public static void Main()
        {
            var json = @"
            {
                ""pdts_007"": {},
                ""pdt_003"": {},
                ""pdt_002"": {},
                ""pdts_005"": {},
                ""pdts_006"": {},
                ""pdt_001"": {},
                ""pdts_008"": {},
                ""pdts_009"": {},                
                ""pdt_004"": {}
            }";

            var regex = @"pdt_\d*";

            var matches = Regex
                    .Matches(json, regex)
                    .Select(m => m.Value);
            
            Console.WriteLine(string.Join(", ", matches));
        }
    }
}

using System;
using System.Collections.Generic;
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

            var uniqueEntities = new List<string>();
            var regex = @"(\w+_)(\d*)";

            var allEntities = Regex
                    .Matches(lines, regex)
                    .Where(m => m.Groups[2].Value.Length > 0)
                    .Select(m => m.Value)
                    .OrderBy(a => a);

            foreach (var entity in allEntities)
            {
                var entityName = entity.Split("_")[0];

                if (!uniqueEntities.Contains(entityName))
                {
                    uniqueEntities.Add(entityName);
                }
            }

            foreach (var entity in uniqueEntities)
            {
                var entityCount = allEntities.Where(e => e.StartsWith(entity));

                Console.WriteLine(entity + " " + entityCount.Count());
            }

            //Console.WriteLine(string.Join(", " + Environment.NewLine, allEntities));
        }
    }
}

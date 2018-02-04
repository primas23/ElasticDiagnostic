using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ElasticDiagnostic
{
    public class Program
    {
        public static void Main()
        {
            var fileData = GetFileData("elastic.json");
            var allEntities = GetAllIndexes(fileData);
            var uniqueEntities = GetGroupIndexesByUniqueEntities(allEntities);

            var jObject = JsonConvert.DeserializeObject(fileData);
            var item = jObject["workplaces_001"];
            Console.WriteLine(uniqueEntities.Count);
        }

        private static Dictionary<string, List<string>> GetGroupIndexesByUniqueEntities(IOrderedEnumerable<string> allEntities)
        {
            var uniqueEntities = new Dictionary<string, List<string>>();

            foreach (var entity in allEntities)
            {
                var entityName = entity.Split("_")[0];

                if (!uniqueEntities.ContainsKey(entityName))
                {
                    uniqueEntities.Add(entityName, new List<string>());
                }

                uniqueEntities[entityName].Add(entity);
            }

            return uniqueEntities;
        }

        private static IOrderedEnumerable<string> GetAllIndexes(string fileData)
        {
            var regex = @"(\w+_)(\d*)";

            var allEntities = Regex
                    .Matches(fileData, regex)
                    .Where(m => m.Groups[2].Value.Length > 0)
                    .Select(m => m.Value)
                    .OrderBy(a => a);

            return allEntities;
        }

        private static string GetFileData(string filePath)
        {
            string lines = string.Empty;
            var fileStream = new FileStream(filePath, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                lines = reader.ReadToEnd();
            }

            return lines;
        }
    }
}

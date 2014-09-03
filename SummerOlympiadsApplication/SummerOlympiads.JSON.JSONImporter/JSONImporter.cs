namespace SummerOlympiads.JSON.JSONImporter
{
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using System.IO;
    using System.Linq;

    using Model.MySQL;

    public class JSONImporter
    {
        private const string DirectoryPath = "../../../../JSONReports/";
        private const string JSONExtension = ".json";

        public IList<Goldmedalist> GetAllRecords()
        {
            var allMedalists = new List<Goldmedalist>();
            var serializer = new JavaScriptSerializer();
            var numberOfReports = Directory.GetFiles(DirectoryPath).Count();
            for (int fileId = 1; fileId <= numberOfReports; fileId++)
            {
                var jsonObjectAsString = File.ReadAllLines(DirectoryPath + fileId + JSONExtension).First();
                var currentMedalist = serializer.Deserialize<Goldmedalist>(jsonObjectAsString);
                allMedalists.Add(currentMedalist);
            }

            return allMedalists;
        }
    }
}

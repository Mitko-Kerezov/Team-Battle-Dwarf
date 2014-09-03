namespace SummerOlympiads.JSON.JSONExporter
{
    using System.Linq;
    using System.Web.Script.Serialization;
    using System.IO;

    using Model;
    using System.Collections.Generic;

    public class JSONReporter
    {
        private OlympiadsEntities sqlDatabase;
        private const string FilePath = "../../../../JSONReports/";
        private const string JsonExtension = ".json";

        public JSONReporter(OlympiadsEntities database)
        {
            this.sqlDatabase = database;
        }

        public IDictionary<int, string> GetJSONObjects()
        {
            var goldMedalists = this.sqlDatabase.Rankings.Where(r => r.Rank == 1).Select(r => new
            {
                Name = r.Athlete.FullName,
                Event = r.Event.Name
            });

            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonObjects = new Dictionary<int, string>();
            int fileNameIndexer = 1;
            foreach (var medalist in goldMedalists)
            {
                jsonObjects.Add(fileNameIndexer++, js.Serialize(medalist));
            }

            return jsonObjects;
        }

        public void SaveJSONObjectsToFile()
        {
            var jsonObjects = GetJSONObjects();

            foreach (var item in jsonObjects)
            {
                File.WriteAllText(FilePath + item.Key.ToString() + JsonExtension, item.Value);
            }

        }
    }
}

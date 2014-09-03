namespace SummerOlympiads.Xml.XmlDataLoader
{
    using System.Linq;
    using System.Xml;
    using System.IO;
    using System.Text;
    
    using Model;
    using Data.Mongo;

    public class XmlDataLoader
    {
        private MongoUpdater mongoDatabaseUpdater;

        private OlympiadsEntities sqlDatabase;
        private const string XmlReportFilePath = "../../../../XmlData/";

        private const string XmlExtension = ".xml";

        private const string XmlChildTierOne = "olympiad";
        private const string XmlChildTierTwo = "anthem";

        public XmlDataLoader(OlympiadsEntities database)
        {
            this.mongoDatabaseUpdater = new MongoUpdater();
            this.sqlDatabase = database;
        }

        /// <summary>
        /// Reads from the specified file. The methods counts on an exact format for the xml file.
        /// The name of the file is provided without the .xml extension
        /// </summary>
        /// <param name="fileName">The name of the file without the .xml extension</param>
        private string GetDataFromXml(string fileName)
        {
            var fileContent = new StringBuilder();
            var fileReader = new StreamReader(XmlReportFilePath + fileName + XmlExtension);
            for (string line = fileReader.ReadLine(); line != null; line = fileReader.ReadLine())
            {
                fileContent.AppendLine(line);
            }

            return fileContent.ToString();
        }
        
        public void UpdateFromXml(string fileName)
        {
            var fileContent = GetDataFromXml(fileName);
           
            using (XmlReader reader = XmlReader.Create(new StringReader(fileContent.ToString())))
            {
                while (reader.ReadToFollowing(XmlChildTierOne))
                {
                    using (var transactionScope = this.sqlDatabase.Database.BeginTransaction())
                    {
                        reader.MoveToFirstAttribute();
                        int olympiadYear = int.Parse(reader.Value);

                        var olympiad = this.sqlDatabase.SummerOlympiads.FirstOrDefault(o => o.Year == olympiadYear);
                        if (olympiad == null)
                        {
                            continue;
                        }

                        reader.ReadToFollowing(XmlChildTierTwo);
                        string anthem = reader.ReadElementContentAsString();

                        olympiad.NotableAnthem = anthem;
                        this.sqlDatabase.SaveChanges();
                        transactionScope.Commit();

                        this.mongoDatabaseUpdater.UpdateOlympiad(olympiadYear, anthem);
                    }
                }
            }
        }
    }
}

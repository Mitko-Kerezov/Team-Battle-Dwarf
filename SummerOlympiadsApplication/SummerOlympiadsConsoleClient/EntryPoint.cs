namespace SummerOlympiads.ConsoleClient
{
    using System.Linq;

    using Xml.XmlDataLoader;
    using Xml.XmlReport;
    using Logic.SqlImporter;
    using Model;
    using Utils;

    internal class EntryPoint
    {
        private static void Main()
        {
            //This part of the code can be used to import data from mongo and excel to SQL
            //using (var db = new OlympiadsEntities())
            //{
            //    var sqlImporter = new SqlImporter();
            //    sqlImporter.Import(db);
            //}

            //ZipHandler.CleanUp();

            //This part can be used to generate a report for a specific year
            //Note that some olympiads may not have medal-winners
            //using (var db = new OlympiadsEntities())
            //{
            //    var xmlReporter = new XmlReportGenerator(db);
            //    xmlReporter.GenerateXMLReport(1912);
            //}

            //Used to update SQL Server and MongoDb with the xml data from the file fileName
            //var fileName = "data";
            //using (var db = new OlympiadsEntities())
            //{
            //    var xmlLoader = new XmlDataLoader(db);
            //    xmlLoader.UpdateFromXml(fileName);
            //}
        }
    }
}
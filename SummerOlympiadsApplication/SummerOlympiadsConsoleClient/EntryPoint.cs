namespace SummerOlympiads.ConsoleClient
{
    using Xml.XMLReport;
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
            using (var db = new OlympiadsEntities())
            {
                var xmlReporter = new XMLReportGenerator(db);
                xmlReporter.GenerateXMLReport(1912);
            }
        }
    }
}
namespace SummerOlympiads.ConsoleClient
{
    using System.Linq;
    using System.IO;

    using Data.Pdf;
    using Logic.SqlImporter;
    using Model;
    using Utils;
    using Xml.XmlDataLoader;
    using Xml.XmlReport;
    using JSON.JSONExporter;
    using Model.MySQL;
    using Logic.MySQLImport;
    using Data.SQLite;
    using Model.SQLite;
    using Logic.ExcelExport;

    internal class EntryPoint
    {
        private static void Main()
        {
            // Task 01
            // using (var db = new OlympiadsEntities())
            // {
            // var sqlImporter = new SqlImporter();
            // sqlImporter.Import(db);
            // }
            //ZipHandler.CleanUp();


            // Task 02
            //using (var db = new OlympiadsEntities())
            //{
            //    var report = new ReportMedalsByNations(db);
            //    report.Generate();
                
            //}
            //

            // Task 03
            // using (var db = new OlympiadsEntities())
            // {
            // var xmlReporter = new XmlReportGenerator(db);
            // xmlReporter.GenerateXMLReport(1912);
            // }

            // Task 04
            //using (var db = new OlympiadsEntities())
            //{
            //    var reporter = new JSONReporter(db);
            //    reporter.SaveJSONObjectsToFile();
            //}

            // Task 04 Part II - Load reports in MySQL
            //GoldMedalists mysqlDB = new GoldMedalists();
            //using (mysqlDB)
            //{
            //    var sqlImporter = new MySQLImporter();
            //    sqlImporter.Import(mysqlDB);
            //}

            // Task 05
            // var fileName = "data";
            // using (var db = new OlympiadsEntities())
            // {
            // var xmlLoader = new XmlDataLoader(db);
            // xmlLoader.UpdateFromXml(fileName);
            // }

            //Task 06
            //GoldMedalists mysqlDB = new GoldMedalists();
            //var sqlLiteDB = new GoldMedalistAgesContext();

            //var excelExporter = new ExcelExporter();
            //excelExporter.ExportToExcel(mysqlDB, sqlLiteDB);
            
        }
    }
}
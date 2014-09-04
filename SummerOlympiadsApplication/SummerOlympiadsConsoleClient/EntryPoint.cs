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
        private static void ImportDataToSqlServer()
        {
            using (var db = new OlympiadsEntities())
            {
                var sqlImporter = new SqlImporter();
                sqlImporter.Import(db);
            }
        }

        private static void GeneratePdfReport(string natinalityAbbr)
        {
            using (var db = new OlympiadsEntities())
            {
                var report = new ReportMedalsByNations(db);
                report.Generate(natinalityAbbr);

            }
        }

        private static void GenerateXmlReport(int year)
        {
            using (var db = new OlympiadsEntities())
            {
                var xmlReporter = new XmlReportGenerator(db);
                xmlReporter.GenerateXMLReport(year);
            }
        }

        private static void GenerateJSONReports()
        {
            using (var db = new OlympiadsEntities())
            {
                var reporter = new JSONReporter(db);
                reporter.SaveJSONObjectsToFile();
            }
        }

        public static void ImportJSONToMySQL()
        {
            GoldMedalists mysqlDB = new GoldMedalists();
            using (mysqlDB)
            {
                var sqlImporter = new MySQLImporter();
                sqlImporter.Import(mysqlDB);
            }
        }

        private static void LoadDataFromXml(string xmlFileName)
        {
            using (var db = new OlympiadsEntities())
            {
                var xmlLoader = new XmlDataLoader(db);
                xmlLoader.UpdateFromXml(xmlFileName);
            }
        }

        private static void GenerateExcelReport()
        {
            GoldMedalists mysqlDB = new GoldMedalists();
            var sqlLiteDB = new GoldMedalistAgesContext();

            var excelExporter = new ExcelExporter();
            excelExporter.ExportToExcel(mysqlDB, sqlLiteDB);
        }

        private static void Main()
        {
            // Task 01
            ImportDataToSqlServer();


            // Task 02
            GeneratePdfReport("POL");
            

            // Task 03
            GenerateXmlReport(1996);

            // Task 04
            GenerateJSONReports();

            // Task 04 Part II - Load reports in MySQL
            ImportJSONToMySQL();

            // Task 05
            LoadDataFromXml("data");
            
            //Task 06
            GenerateExcelReport();
            
        }
    }
}
namespace SummerOlympiads.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using SummerOlympiads.Xml.XmlDataLoader;
    using SummerOlympiads.Xml.XmlReport;
    using SummerOlympiads.Logic.SqlImporter;
    using SummerOlympiads.Model;
    using SummerOlympiads.Utils;
    using SummerOlympiads.Data.Pdf;
    using SummerOlympiads.JSON.JSONExporter;
    using SummerOlympiads.Model.MySQL;
    using SummerOlympiads.Logic.MySQLImport;
    using SummerOlympiads.Data.SQLite;
    using SummerOlympiads.Model.SQLite;
    using SummerOlympiads.Logic.ExcelExport;

    public partial class Menu : Window
    {
        private const string XmlDataFileName = "data";
        public OlympiadsEntities SqlDb { get; private set; }
        public GoldMedalists MySqlDb { get; private set; }
        public GoldMedalistAgesContext SqlLiteDb { get; private set; }

        public Menu(OlympiadsEntities db, GoldMedalists mysqlDb, GoldMedalistAgesContext sqlLiteDb)
        {
            this.SqlDb = db;
            this.MySqlDb = mysqlDb;
            this.SqlLiteDb = sqlLiteDb;
            InitializeComponent();
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            var sqlImporter = new SqlImporter();
            sqlImporter.Import(this.SqlDb);
        }

        private void GeneratePdfReports(object sender, RoutedEventArgs e)
        {
            try
            {
                string nationalityName = NationalityBox.Text;
                var report = new ReportMedalsByNations(this.SqlDb);
                report.Generate(nationalityName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void GenerateReportForYear(object sender, RoutedEventArgs e)
        {
            try
            {
                int year = int.Parse(YearBox.Text);
                var xmlReporter = new XmlReportGenerator(this.SqlDb);

                xmlReporter.GenerateXMLReport(year);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void GenerateJSONReports(object sender, RoutedEventArgs e)
        {
            var reporter = new JSONReporter(this.SqlDb);
            reporter.SaveJSONObjectsToFile();

            var sqlImporter = new MySQLImporter();
            sqlImporter.Import(this.MySqlDb);
        }

        private void UpdateDatabase(object sender, RoutedEventArgs e)
        {
            var xmlLoader = new XmlDataLoader(this.SqlDb);
            xmlLoader.UpdateFromXml(XmlDataFileName);
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            var excelExporter = new ExcelExporter();
            excelExporter.ExportToExcel(this.MySqlDb, this.SqlLiteDb);
        }
    }
}
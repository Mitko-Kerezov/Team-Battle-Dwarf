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

namespace SummerOlympiads.WpfClient
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public OlympiadsEntities DB { get; private set; }

        public Menu(OlympiadsEntities db)
        {
            this.DB = db;
            InitializeComponent();
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            using (this.DB = new OlympiadsEntities())
            {
                var sqlImporter = new SqlImporter();
                sqlImporter.Import(this.DB);
            }

            ZipHandler.CleanUp();
        }

        private void GenerateReportForYear(object sender, RoutedEventArgs e)
        {
            const int maxYear = 2004;

            using (this.DB = new OlympiadsEntities())
            {
                while(true)
                {
                    try
                    {
                        int year = int.Parse(YearBox.Text);
                        var xmlReporter = new XmlReportGenerator(this.DB);

                        xmlReporter.GenerateXMLReport(year);
                        
                        break;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        break;
                    }
                }
            }
        }

        private void UpdateDatabase(object sender, RoutedEventArgs e)
        {
            var fileName = "data";
            using (var db = new OlympiadsEntities())
            {
                var xmlLoader = new XmlDataLoader(db);
                xmlLoader.UpdateFromXml(fileName);
            }
        }
    }
}

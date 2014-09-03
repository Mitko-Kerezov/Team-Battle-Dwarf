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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SummerOlympiads.Xml.XmlDataLoader;
using SummerOlympiads.Xml.XmlReport;
using SummerOlympiads.Logic.SqlImporter;
using SummerOlympiads.Model;
using SummerOlympiads.Utils;

namespace SummerOlympiads.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OlympiadsEntities DB { get; private set; }

        public MainWindow()
        {
            this.DB = new OlympiadsEntities();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.DB);
            menu.Show();
            this.Close();
        }
    }
}

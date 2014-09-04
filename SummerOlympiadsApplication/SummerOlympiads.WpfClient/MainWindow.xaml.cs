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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Xml.XmlDataLoader;
    using Xml.XmlReport;
    using Logic.SqlImporter;
    using Model;
    using Utils;
    using Model.MySQL;
    using Data.SQLite;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OlympiadsEntities DB { get; private set; }
        public GoldMedalists MySqlDb { get; private set; }
        public GoldMedalistAgesContext SqlLiteDb { get; private set; }

        public MainWindow()
        {
            this.DB = new OlympiadsEntities();
            this.MySqlDb = new GoldMedalists();
            this.SqlLiteDb = new GoldMedalistAgesContext();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.DB, this.MySqlDb, this.SqlLiteDb);
            menu.Show();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace SoftwareTeamwork
{
    public partial class MainWindow : AIWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.Relative));
            //using (var db = new InfoContext()) {
            //    if (db.FlowInfos.Count().Equals(0))
            //        Console.WriteLine("Noitem");

            //    db.FlowInfos.Add(new FlowInfo { FlowData = 244 , InfoTime = new TimeSpan(5,2,2)});
            //    db.SaveChanges();

            //    foreach (var d in db.FlowInfos) {
            //        Console.WriteLine(d.FlowData + "  " + d.FlowInfoId);
            //    }
            //}
        }
    }
}

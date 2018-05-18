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

namespace SoftwareTeamwork {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {

        public SettingPage() {
            InitializeComponent();
        }

        private void Reset(object sender, RoutedEventArgs e) {
            QuestionDialog dialog = new QuestionDialog {
                Style = (Style)Application.Current.FindResource("QuestionDialog"),
                Context = "确定还原默认设置,包括用户数据?"
            };
            dialog.ShowDialog(Application.Current.MainWindow);

            if (dialog.DialogResult.Equals(true)) {
                OverallSettingManger.Instence.Reset();
            }
        }

        private void CourseFontSize_Click(object sender, RoutedEventArgs e) {
            CourseTable.Open();
        }
    }
}

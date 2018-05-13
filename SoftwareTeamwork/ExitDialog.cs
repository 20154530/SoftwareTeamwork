using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork {
    public class ExitDialog :DialogBase{

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            var ExitAction = GetTemplateChild("ExitAction") as IconToggelButton;
            ExitAction.Click += ExitAction_Click;
            var SaveAction = GetTemplateChild("SaveAction") as IconToggelButton;
            SaveAction.Click += SaveAction_Click;
        }

        private void SaveAction_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.IsExitDialogShow = !(bool)((IconToggelButton)sender).IsChecked;
            Properties.Settings.Default.Save();
        }

        private void ExitAction_Click(object sender, RoutedEventArgs e) {
            Console.WriteLine(Properties.Settings.Default.ExitAction);
            Properties.Settings.Default.ExitAction = (bool)((IconToggelButton)sender).IsChecked;
            Properties.Settings.Default.Save();
        }
    }
}

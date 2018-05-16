using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork {
    public class ExitDialog : DialogBase {

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            var ExitAction = GetTemplateChild("ExitAction") as IconToggelButton;
            ExitAction.Click += ExitAction_Click;
            ExitAction.IsChecked = Properties.Settings.Default.ExitAction;
            var SaveAction = GetTemplateChild("SaveAction") as IconToggelButton;
            SaveAction.Click += SaveAction_Click;
        }

        private void SaveAction_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.IsExitDialogShow = !(bool)((IconToggelButton)sender).IsChecked;
        }

        private void ExitAction_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.ExitAction = (bool)((IconToggelButton)sender).IsChecked;
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
        }

        private void ExitDialog_Loaded(object sender, RoutedEventArgs e) {

        }

        public ExitDialog() {
            this.Loaded += ExitDialog_Loaded;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SoftwareTeamwork {

    public class ErrorPopup : DPopup {

        public override void OnApplyTemplate() {
            if (GetTemplateChild("ExitButton") is IconButton ExitButton)
                ExitButton.Click += ExitButton_Click;

            base.OnApplyTemplate();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            this.IsOpen = false;
        }

        public ErrorPopup(UIElement parent) {
            OverallSettingManger.Instence.ThemeChanged += Instence_ThemeChanged;
            PlacementTarget = parent;
            UseInOutAni = false;
        }

        private void Instence_ThemeChanged(object sender, EventArgs e) {
            this.Style = null;
            this.Style = (Style)Application.Current.FindResource("MainFlowPopup");
        }
    }
}

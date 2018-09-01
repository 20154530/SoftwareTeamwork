using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace SoftwareTeamwork {
    public class MessageService {
        private ErrorPopup Error;
        public static MessageService Instence = new MessageService();

        public void ShowError(UIElement aimobject, string message) {
            if (aimobject is null)
            {
                if (Error.IsOpen)
                    Error.HidePopupAni();
                Error.PlacementTarget = null;
                Error.Content = message;
                Error.ShowPopupAni();
            }
            else
            {
                var RootElement = (DependencyObject)aimobject;
                while (!(RootElement is Window) && !(RootElement is DPopup)) { RootElement = VisualTreeHelper.GetParent(RootElement); }
                if (Error.IsOpen)
                    Error.HidePopupAni();
                Error.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
                Error.PlacementTarget = (UIElement)RootElement;
                Error.Content = message;
                Error.ShowPopupAni();
            }
        }

        public bool GetErrorState() {
            return Error.IsOpen;
        }

        private void MainWindow_StateChanged(object sender, EventArgs e) {
            if (((Window)sender).WindowState.Equals(WindowState.Minimized))
                Error.IsOpen = false;
            else
                Error.IsOpen = true;
            throw new NotImplementedException();
        }

        public MessageService() {
            Error = new ErrorPopup
            {
                Style = (Style)Application.Current.FindResource("ErrorMessagePopup")
            };
            Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;
        }

    }
}

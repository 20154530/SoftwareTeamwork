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
            if (aimobject is null) {
                if (Error.IsOpen)
                    Error.HidePopupAni();
                Error.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
                Console.WriteLine(Error.Child.RenderSize.Height);
                Error.PlacementRectangle = new Rect(0, 0, 0, 0);
                Error.Content = message;
                Error.ShowPopupAni();
            }
            else {
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

        public MessageService() {
            Error = new ErrorPopup {
                Style = (Style)Application.Current.FindResource("ErrorMessagePopup")
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SoftwareTeamwork
{
    public class ErrorMessageService {
        private ErrorPopup Error;
        public static ErrorMessageService Instence = new ErrorMessageService();

        public void ShowError(UIElement aimobject,string message) {
            if (Error.IsOpen)
                Error.HidePopupAni();
            Error.PlacementTarget = aimobject;
            Error.Content = message;
            Error.ShowPopupAni(); 
        }

         public ErrorMessageService() {
            Error = new ErrorPopup {
                Style = (Style)Application.Current.FindResource("ErrorMessagePopup")
            };
        }

    }
}

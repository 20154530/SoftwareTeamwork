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

        public static void ShowError(UIElement aimobject,string message) {
            ErrorPopup error = new ErrorPopup(aimobject) {
                Content = message,
               
            };
            error.Style = (Style)Application.Current.FindResource("ErrorMessage");
            error.IsOpen = true;
        }
    }
}

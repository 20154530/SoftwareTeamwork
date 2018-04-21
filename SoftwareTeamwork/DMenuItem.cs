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

namespace SoftwareTeamwork
{
    public class DMenuItem : MenuItem
    {
        public Color TextColor { get; set; }
        public Color IconColor { get; set; }

        static DMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DMenuItem), new FrameworkPropertyMetadata(typeof(DMenuItem)));
        }
    }
}

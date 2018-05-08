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
using System.Windows.Shapes;

namespace SoftwareTeamwork
{
    /// <summary>
    /// DialogBase.xaml 的交互逻辑
    /// </summary>
    public partial class DialogBase : Window
    {
        #region Context
        public String Context {
            get { return (String)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(String), typeof(DialogBase), new PropertyMetadata(""));
        #endregion

        public DialogBase()
        {
            InitializeComponent();
        }
    }
}

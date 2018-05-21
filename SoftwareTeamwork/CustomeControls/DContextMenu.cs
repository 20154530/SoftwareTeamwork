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
    public class DContextMenu : ContextMenu
    {
        #region CommandParameter
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(Object), typeof(DContextMenu),
            new PropertyMetadata(null,OnAttachedWindowChanged));
        private static void OnAttachedWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DContextMenu dc = (DContextMenu)d;
            dc.OpenOrClose = ((AIWindow)e.NewValue).Visibility.Equals(Visibility.Hidden) ? "显示主界面" : "隐藏主界面";
        }
        public Object CommandParameter
        {
            get { return (Object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region OpenOrClose
        public string OpenOrClose
        {
            get { return (string)GetValue(OpenOrCloseProperty); }
            set { SetValue(OpenOrCloseProperty, value); }
        }
        public static readonly DependencyProperty OpenOrCloseProperty =
            DependencyProperty.Register("OpenOrClose", typeof(string), typeof(DContextMenu), new PropertyMetadata(""));
        #endregion

        #region Content

        #endregion

        #region OpenSubMenu
        public WindowCommand OpenSubMenu {
            get { return (WindowCommand)GetValue(OpenSubMenuProperty); }
            set { SetValue(OpenSubMenuProperty, value); }
        }
        public static readonly DependencyProperty OpenSubMenuProperty =
            DependencyProperty.Register("OpenSubMenu", typeof(WindowCommand),
                typeof(DContextMenu), new PropertyMetadata(new WindowCommand()));
        #endregion

        private void OpenSubMenu_CAction(object para) {
            //DContextMenu sub = (DContextMenu)Application.Current.FindResource(para);
            //sub.PlacementTarget = this;
            //sub.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
            //sub.IsOpen = true;
        }
        
        protected override void OnOpened(RoutedEventArgs e)
        {
            OpenOrClose = ((AIWindow)App.Current.MainWindow).Visibility.Equals(Visibility.Hidden) ? "显示主界面" : "隐藏主界面";
            base.OnOpened(e);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.IsOpen = false;
        }

        public DContextMenu()
        {
            OpenSubMenu.CAction += OpenSubMenu_CAction; ;
            Application.Current.MainWindow.Closing += MainWindow_Closing;
        }

        static DContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DContextMenu), new FrameworkPropertyMetadata(typeof(DContextMenu)));
        }        
    }
}

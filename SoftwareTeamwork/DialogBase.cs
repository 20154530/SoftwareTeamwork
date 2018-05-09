using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static SoftwareTeamwork.OverallSettingManger;

namespace SoftwareTeamwork
{
    class DialogBase : Window {

        #region DialogMode
        public MessageBoxButton DialogMode {
            get { return (MessageBoxButton)GetValue(DialogModeProperty); }
            set { SetValue(DialogModeProperty, value); }
        }
        public static readonly DependencyProperty DialogModeProperty =
            DependencyProperty.Register("DialogMode", typeof(MessageBoxButton), typeof(DialogBase), 
                new PropertyMetadata(MessageBoxButton.YesNoCancel));
        #endregion

        #region
        private MessageBoxResult result = MessageBoxResult.None;
        public MessageBoxResult Result {
            get => result;
            set { result = value; }
        }
        #endregion

        #region 
        public string Context {
            get { return (string)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(string), typeof(Window), new PropertyMetadata(""));
        #endregion

        public override void OnApplyTemplate()
        {
            switch (DialogMode) {
                case MessageBoxButton.OK:
                    (GetTemplateChild("OK") as Button).Click += DialogButtonClick;
                    break;
                case MessageBoxButton.OKCancel:
                    (GetTemplateChild("OK") as Button).Click += DialogButtonClick;
                    (GetTemplateChild("CANCEL") as Button).Click += DialogButtonClick;
                    break;
                case MessageBoxButton.YesNo:
                    (GetTemplateChild("YES") as Button).Click += DialogButtonClick;
                    (GetTemplateChild("NO") as Button).Click += DialogButtonClick;
                    break;
                case MessageBoxButton.YesNoCancel:
                    (GetTemplateChild("YES") as Button).Click += DialogButtonClick;
                    (GetTemplateChild("NO") as Button).Click += DialogButtonClick;
                    (GetTemplateChild("CANCEL") as Button).Click += DialogButtonClick;
                    break;
            }
            base.OnApplyTemplate();
        }

        private void DialogButtonClick(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name) {
                case "YES": DialogResult = true; break;
                case "NO": DialogResult = false; break;
                case "OK": DialogResult = true; break;
                case "CANCEL":DialogResult = false; break;
                default: break;
            }
            Close();
        }

        public void ShowDialog(Window Holder)
        {
            Owner = Holder;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowDialog();
        }

        public DialogBase()
        {
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
        }

        static DialogBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogBase), new FrameworkPropertyMetadata(typeof(DialogBase)));
        }
    }
}

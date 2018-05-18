using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static SoftwareTeamwork.OverallSettingManger;

namespace SoftwareTeamwork
{
    /// <summary>
    /// 弹出式对话框类
    /// 继承自窗口类，有四种模式，用名称标识相关按钮
    /// </summary>
    public class DialogBase : Window {

        #region DialogMode
        public MessageBoxButton DialogMode {
            get { return (MessageBoxButton)GetValue(DialogModeProperty); }
            set { SetValue(DialogModeProperty, value); }
        }
        public static readonly DependencyProperty DialogModeProperty =
            DependencyProperty.Register("DialogMode", typeof(MessageBoxButton), typeof(DialogBase), 
                new PropertyMetadata(MessageBoxButton.YesNoCancel));
        #endregion

        #region Result
        private MessageBoxResult result = MessageBoxResult.None;
        public MessageBoxResult Result {
            get => result;
            set { result = value; }
        }
        #endregion

        #region Context
        public string Context {
            get { return (string)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(string), typeof(Window), new PropertyMetadata(""));
        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            this.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public override void OnApplyTemplate()
        {
            try {
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
                        ((Button)GetTemplateChild("YES")).Click += DialogButtonClick;
                        ((Button)GetTemplateChild("NO")).Click += DialogButtonClick;
                        ((Button)GetTemplateChild("CANCEL")).Click += DialogButtonClick;
                        break;
                }
            }
            catch (NullReferenceException e) {
                
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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SoftwareTeamwork
{
    public class DPopup : Popup
    {
        #region Properties

        #region Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DPopup), new PropertyMetadata(""));
        #endregion

        #region Content
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(DPopup), new PropertyMetadata(""));
        #endregion

        #region Shadow
        public int Shadow
        {
            get { return (int)GetValue(ShadowProperty); }
            set { SetValue(ShadowProperty, value); }
        }
        public static readonly DependencyProperty ShadowProperty =
            DependencyProperty.Register("Shadow", typeof(int), typeof(DPopup), new PropertyMetadata(0));
        #endregion

        #region
        #endregion

        #endregion

        #region Fadein/out ani
        public void ShowPopupAni()
        {
            //Opacity
            DoubleAnimation Opacity = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard Popupshow = new Storyboard()
            {
                FillBehavior = FillBehavior.HoldEnd
            };

            Storyboard.SetTarget(Opacity, Child);
            Storyboard.SetTargetProperty(Opacity, new PropertyPath(OpacityProperty));

            Popupshow.Children.Add(Opacity);

            IsOpen = true;
            Popupshow.Completed += Popupshow_Completed;
            Popupshow.Begin();
        }

        private void Popupshow_Completed(object sender, EventArgs e)
        {
            Shadow = 100;
        }

        public void HidePopupAni()
        {
            //Opacity
            DoubleAnimation Opacity = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard PopupHide = new Storyboard()
            {
                FillBehavior = FillBehavior.HoldEnd
            };

            Storyboard.SetTarget(Opacity, Child);
            Storyboard.SetTargetProperty(Opacity, new PropertyPath(OpacityProperty));

            PopupHide.Children.Add(Opacity);

            Shadow = 0;
            PopupHide.Completed += PopupHide_Completed;
            PopupHide.Begin();
        }

        private void PopupHide_Completed(object sender, EventArgs e)
        {
            IsOpen = false;
        }
        #endregion

        public DPopup()
        {
            AllowsTransparency = true;
        }

        static DPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DPopup), new FrameworkPropertyMetadata(typeof(DPopup)));
        }
    }
}
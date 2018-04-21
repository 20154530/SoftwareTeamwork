using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SoftwareTeamwork
{

    public class DPopup : Popup
    {
        #region Properties

        #region ThemeBg
        public Color ThemeBg
        {
            get { return (Color)GetValue(ThemeBgProperty); }
            set { SetValue(ThemeBgProperty, value); }
        }
        public static readonly DependencyProperty ThemeBgProperty =
            DependencyProperty.Register("ThemeBg", typeof(Color), typeof(DPopup), new PropertyMetadata(null));
        #endregion

        #region ThemeBorderC
        public Color ThemeBorderC
        {
            get { return (Color)GetValue(ThemeBorderCProperty); }
            set { SetValue(ThemeBorderCProperty, value); }
        }
        public static readonly DependencyProperty ThemeBorderCProperty =
            DependencyProperty.Register("ThemeBorderC", typeof(Color), typeof(DPopup), new PropertyMetadata(null));
        #endregion

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

        # region IconPath
        public String IconPath
        {
            get { return (String)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }
        public static readonly DependencyProperty IconPathProperty =
             DependencyProperty.Register("IconPath", typeof(String), typeof(DPopup), new PropertyMetadata(""));
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

        #endregion

        public void SetIconPathByAngle(double a)
        {
            var A = a * 2 * Math.PI / 360;
            var x = 20 * Math.Sin(A);
            var y = 20 * Math.Cos(A);
            x = 30 + x;
            y = 30 - y;
            if (a <= 180)
                IconPath = "M 30,10 A 20,20,0,0,1," + x.ToString() + "," + y.ToString();
            else
                IconPath = "M 30,10 A 20,20,0,1,1," + x.ToString() + "," + y.ToString();
        }

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
            Storyboard.SetTargetProperty(Opacity, new PropertyPath(Grid.OpacityProperty));

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
            Storyboard.SetTargetProperty(Opacity, new PropertyPath(Grid.OpacityProperty));

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

        static DPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DPopup), new FrameworkPropertyMetadata(typeof(DPopup)));
        }
    }
}

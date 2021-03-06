﻿using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SoftwareTeamwork {
    public class DPopup : Popup {
        private System.Timers.Timer PopupHidetimer;

        #region Title
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DPopup), new PropertyMetadata(""));
        #endregion

        #region Content
        public string Content {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(DPopup), new PropertyMetadata(""));
        #endregion

        #region Shadow
        public int Shadow {
            get { return (int)GetValue(ShadowProperty); }
            set { SetValue(ShadowProperty, value); }
        }
        public static readonly DependencyProperty ShadowProperty =
            DependencyProperty.Register("Shadow", typeof(int), typeof(DPopup), new PropertyMetadata(0));
        #endregion

        #region AutoHide
        public bool AutoHide {
            get { return (bool)GetValue(AutoHideProperty); }
            set { SetValue(AutoHideProperty, value); }
        }
        public static readonly DependencyProperty AutoHideProperty =
            DependencyProperty.Register("AutoHide", typeof(bool),
                typeof(DPopup), new PropertyMetadata(true));
        #endregion

        #region TimerInteval
        public int TimerInteval {
            get { return (int)GetValue(TimerIntevalProperty); }
            set { SetValue(TimerIntevalProperty, value); }
        }
        public static readonly DependencyProperty TimerIntevalProperty =
            DependencyProperty.Register("TimerInteval", typeof(int), 
                typeof(DPopup), new PropertyMetadata(3000 , new PropertyChangedCallback(OnTimerIntevalChanged)));
        private static void OnTimerIntevalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((DPopup)d).PopupHidetimer.Interval = (int)e.NewValue;
        }
        #endregion

        #region UseFadeIn/Out
        public bool UseInOutAni {
            get { return (bool)GetValue(UseInOutAniProperty); }
            set { SetValue(UseInOutAniProperty, value); }
        }
        public static readonly DependencyProperty UseInOutAniProperty =
            DependencyProperty.Register("UseInOutAni", typeof(bool), typeof(DPopup),
                new PropertyMetadata(true));
        #endregion

        #region Fadein/out ani
        private void HidePopup(object sender, ElapsedEventArgs e) {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate () {
                HidePopupAni();
            });
        }

        public void ShowPopupAni() {
            if (UseInOutAni) {
                //Opacity
                DoubleAnimation Opacity = new DoubleAnimation() {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1)
                };

                Storyboard Popupshow = new Storyboard() {
                    FillBehavior = FillBehavior.HoldEnd
                };

                Storyboard.SetTarget(Opacity, Child);
                Storyboard.SetTargetProperty(Opacity, new PropertyPath(OpacityProperty));

                Popupshow.Children.Add(Opacity);

                IsOpen = true;
                Popupshow.Completed += Popupshow_Completed;
                Popupshow.Begin();
            }
            else
                IsOpen = true;
        }

        private void Popupshow_Completed(object sender, EventArgs e) {
            Shadow = 100;
            if (AutoHide)
                PopupHidetimer.Enabled = true;
        }

        public void HidePopupAni() {
            if (UseInOutAni) {
                //Opacity
                DoubleAnimation Opacity = new DoubleAnimation() {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1)
                };

                Storyboard PopupHide = new Storyboard() {
                    FillBehavior = FillBehavior.HoldEnd
                };

                Storyboard.SetTarget(Opacity, Child);
                Storyboard.SetTargetProperty(Opacity, new PropertyPath(OpacityProperty));

                PopupHide.Children.Add(Opacity);

                Shadow = 0;
                PopupHide.Completed += PopupHide_Completed;
                PopupHide.Begin();
            }
            else
                IsOpen = false;
        }

        private void PopupHide_Completed(object sender, EventArgs e) {
            IsOpen = false;
            PopupHidetimer.Enabled = false;
        }
        #endregion

        protected override void OnMouseMove(MouseEventArgs e) {
            if (AutoHide)
                PopupHidetimer.Enabled = false;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            if (AutoHide)
                PopupHidetimer.Enabled = true;
            base.OnMouseLeave(e);
        }

        public DPopup() {
            AllowsTransparency = true;
            PopupHidetimer = new System.Timers.Timer();
            PopupHidetimer.Interval = TimerInteval;
            PopupHidetimer.Elapsed += new ElapsedEventHandler(HidePopup);
        }

        static DPopup() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DPopup), new FrameworkPropertyMetadata(typeof(DPopup)));
        }
    }

}
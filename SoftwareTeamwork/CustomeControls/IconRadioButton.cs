﻿using System;
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

namespace SoftwareTeamwork {

    public class IconRadioButton : RadioButton {

        #region Color
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region ForeIcon
        public string ForeIcon {
            get { return (string)GetValue(ForeIconProperty); }
            set { SetValue(ForeIconProperty, value); }
        }
        public static readonly DependencyProperty ForeIconProperty =
            DependencyProperty.Register("ForeIcon", typeof(string), typeof(IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region BackIcon
        public string BackIcon {
            get { return (string)GetValue(BackIconProperty); }
            set { SetValue(BackIconProperty, value); }
        }
        public static readonly DependencyProperty BackIconProperty =
            DependencyProperty.Register("BackIcon", typeof(string), typeof(IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region DisIcon
        public string DisIcon {
            get { return (string)GetValue(DisIconProperty); }
            set { SetValue(DisIconProperty, value); }
        }
        public static readonly DependencyProperty DisIconProperty =
            DependencyProperty.Register("DisIcon", typeof(string),
                typeof(IconRadioButton), new PropertyMetadata(null));
        #endregion

        #region DisIconBack
        public string DisIconBack {
            get { return (string)GetValue(DisIconBackProperty); }
            set { SetValue(DisIconBackProperty, value); }
        }
        public static readonly DependencyProperty DisIconBackProperty =
            DependencyProperty.Register("DisIconBack", typeof(string),
                typeof(IconRadioButton), new PropertyMetadata(""));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region BackToolTip
        public string BackToolTip {
            get { return (string)GetValue(BackToolTipProperty); }
            set { SetValue(BackToolTipProperty, value); }
        }
        public static readonly DependencyProperty BackToolTipProperty =
            DependencyProperty.Register("BackToolTip", typeof(string), typeof(IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region Labelfontsize
        public double LabelFontSize {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double),
                typeof(IconRadioButton), new PropertyMetadata(10.0));
        #endregion

        #region LabelVisibility
        public Visibility LabelVisibility {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(Visibility),
                typeof(IconRadioButton), new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(IconRadioButton),
                new PropertyMetadata(Visibility.Visible));
        #endregion

        public IconRadioButton() {
             
        }

        static IconRadioButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconRadioButton), new FrameworkPropertyMetadata(typeof(IconRadioButton)));
        }
    }
}

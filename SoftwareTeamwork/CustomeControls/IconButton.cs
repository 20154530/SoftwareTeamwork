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

namespace SoftwareTeamwork
{
    public class IconButton : Button {

        #region ContentTextVisiblity
        public Visibility ContentTextVisiblity {
            get { return (Visibility)GetValue(ContentTextVisiblityProperty); }
            set { SetValue(ContentTextVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ContentTextVisiblityProperty =
            DependencyProperty.Register("ContentTextVisiblity", typeof(Visibility), typeof(IconButton),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(IconButton), 
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80,0,0,0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(IconButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(IconButton), 
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region ContentText
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(IconButton),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextFontWeight
        public double ContentTextFontSize {
            get { return (double)GetValue(ContentTextFontSizeProperty); }
            set { SetValue(ContentTextFontSizeProperty, value); }
        }
        public static readonly DependencyProperty ContentTextFontSizeProperty =
            DependencyProperty.Register("ContentTextFontSize", typeof(double), typeof(IconButton), 
                new PropertyMetadata(0.0));
        #endregion

        #region AllowShadow
        public double AllowShadow {
            get { return (double)GetValue(AllowShadowProperty); }
            set { SetValue(AllowShadowProperty, value); }
        }
        public static readonly DependencyProperty AllowShadowProperty =
            DependencyProperty.Register("AllowShadow", typeof(double), typeof(IconButton), new PropertyMetadata(0.0));
        #endregion

        #region AllowAni
        public bool AllowAni {
            get { return (bool)GetValue(AllowAniProperty); }
            set { SetValue(AllowAniProperty, value); }
        }
        public static readonly DependencyProperty AllowAniProperty =
            DependencyProperty.Register("AllowAni", typeof(bool),
                typeof(IconButton), new PropertyMetadata(false));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(IconButton),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(IconButton),
                new PropertyMetadata(Visibility.Visible));
        #endregion

        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }
    }
}

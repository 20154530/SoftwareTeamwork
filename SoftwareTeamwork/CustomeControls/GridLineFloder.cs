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
    public class GridLineFloder : Control,ICommand {

        #region Title
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(GridLineFloder), new PropertyMetadata(""));
        #endregion

        #region ContentVis
        public Visibility ContentVis {
            get { return (Visibility)GetValue(ContentVisProperty); }
            set { SetValue(ContentVisProperty, value); }
        }
        public static readonly DependencyProperty ContentVisProperty =
            DependencyProperty.Register("ContentVis", typeof(Visibility), typeof(GridLineFloder), 
                new PropertyMetadata(Visibility.Visible));
        #endregion

        #region AttachedGrid
        public Grid AttachedGrid {
            get { return (Grid)GetValue(AttachedGridProperty); }
            set { SetValue(AttachedGridProperty, value); }
        }
        public static readonly DependencyProperty AttachedGridProperty =
            DependencyProperty.Register("AttachedGrid", typeof(Grid), typeof(GridLineFloder),
                new PropertyMetadata(null));
        #endregion

        #region CommandPara
        public int CommandPara {
            get { return (int)GetValue(CommandParaProperty); }
            set { SetValue(CommandParaProperty, value); }
        }
        public static readonly DependencyProperty CommandParaProperty =
            DependencyProperty.Register("CommandPara", typeof(int), typeof(GridLineFloder),
                new PropertyMetadata(null));

        public event EventHandler CanExecuteChanged;
        #endregion

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (ContentVis.Equals(Visibility.Visible)) {
                ContentVis = Visibility.Collapsed;
                AttachedGrid.RowDefinitions[(int)parameter].Height = new GridLength(0, GridUnitType.Star);
            }
            else {
                ContentVis = Visibility.Visible;
                AttachedGrid.RowDefinitions[(int)parameter].Height = new GridLength(1, GridUnitType.Star);
            }
        }


        static GridLineFloder() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridLineFloder), new FrameworkPropertyMetadata(typeof(GridLineFloder)));
        }
    }
}

using System;
using System.Collections.Generic;
using Drawing = System.Drawing;
using Color = System.Windows.Media.Color;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace SoftwareTeamwork
{
    class ColorPicker : DialogBase {

        #region ColorA
        public int ColorA {
            get { return (int)GetValue(ColorAProperty); }
            set { SetValue(ColorAProperty, value); }
        }
        public static readonly DependencyProperty ColorAProperty =
            DependencyProperty.Register("ColorA", typeof(int),
                typeof(ColorPicker), new PropertyMetadata(0,
                    new PropertyChangedCallback(ColorChange)));
        #endregion

        #region ColorR
        public int ColorR {
            get { return (int)GetValue(ColorRProperty); }
            set { SetValue(ColorRProperty, value); }
        }
        public static readonly DependencyProperty ColorRProperty =
            DependencyProperty.Register("ColorR", typeof(int),
                typeof(ColorPicker), new PropertyMetadata(0,
                    new PropertyChangedCallback(ColorChange)));
        #endregion

        #region ColorG
        public int ColorG {
            get { return (int)GetValue(ColorGProperty); }
            set { SetValue(ColorGProperty, value); }
        }
        public static readonly DependencyProperty ColorGProperty =
            DependencyProperty.Register("ColorG", typeof(int),
                typeof(ColorPicker), new PropertyMetadata(0,
                    new PropertyChangedCallback(ColorChange)));
        #endregion

        #region ColorB
        public int ColorB {
            get { return (int)GetValue(ColorBProperty); }
            set { SetValue(ColorBProperty, value); }
        }
        public static readonly DependencyProperty ColorBProperty =
            DependencyProperty.Register("ColorB", typeof(int),
                typeof(ColorPicker), new PropertyMetadata(0,
                    new PropertyChangedCallback(ColorChange)));
        #endregion

        #region ColorShow
        public Color ColorNow {
            get { return (Color)GetValue(ColorNowProperty); }
            set { SetValue(ColorNowProperty, value); }
        }
        public static readonly DependencyProperty ColorNowProperty =
            DependencyProperty.Register("ColorNow", typeof(Color),
                typeof(ColorPicker), new PropertyMetadata(null));
        #endregion

        #region Color16
        public string ColorHEX {
            get { return (string)GetValue(ColorHEXProperty); }
            set { SetValue(ColorHEXProperty, value); }
        }
        public static readonly DependencyProperty ColorHEXProperty =
            DependencyProperty.Register("ColorHEX", typeof(string), 
                typeof(ColorPicker), new PropertyMetadata("",new PropertyChangedCallback(ColorhexChange)));
        #endregion

        #region PropertyChangedCallback
        private static void ColorChange(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ColorPicker cp = (ColorPicker)d;
            cp.ColorNow = Color.FromArgb(
                Convert.ToByte(cp.ColorA),
                Convert.ToByte(cp.ColorR),
                Convert.ToByte(cp.ColorG),
                Convert.ToByte(cp.ColorB));
            cp.ColorHEX = String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", cp.ColorA, cp.ColorR, cp.ColorG, cp.ColorB);
        }

        private static void ColorhexChange(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ColorPicker cp = (ColorPicker)d;
            try {
                cp.ColorNow = (Color)ColorConverter.ConvertFromString(e.NewValue.ToString());
                cp.ColorA = cp.ColorNow.A;
                cp.ColorR = cp.ColorNow.R;
                cp.ColorG = cp.ColorNow.G;
                cp.ColorB = cp.ColorNow.B;
            }
            catch {
              //  MessageService.Instence.ShowError(App.Current.MainWindow,"无效的值");
            }
        }
        #endregion

        public override void OnApplyTemplate() {
            ((TextBox)GetTemplateChild("ColorValue")).KeyDown += OnColorHexChanged;
            ((TextBox)GetTemplateChild("AlphaValue")).KeyDown += OnColorHexChanged;
            ((TextBox)GetTemplateChild("RedValue")).KeyDown += OnColorHexChanged;
            ((TextBox)GetTemplateChild("GreenValue")).KeyDown += OnColorHexChanged;
            ((TextBox)GetTemplateChild("BlueValue")).KeyDown += OnColorHexChanged;

            base.OnApplyTemplate();
        }

        private void OnColorHexChanged(object sender, KeyEventArgs e) {
            if (e.Key.Equals(Key.Enter)) {
                ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        public Color GetColor() {
            return ColorNow;
        }

        public Drawing.Color DrawingGetColor() {
            return DataFormater.GetDrawingColor(ColorNow);
        }

        public void ShowDialog(Window Holder, Color color) {
            InitControls(color);
            base.ShowDialog(Holder);
        }

        public void ShowDialogD(Window Holder, System.Drawing.Color color) {
            InitControls(Color.FromArgb(color.A, color.R, color.G, color.B));
            base.ShowDialog(Holder);
        }

        private void InitControls(Color color) {
            ColorA = color.A;
            ColorR = color.R;
            ColorG = color.G;
            ColorB = color.B;
        }

        public ColorPicker() {

        }
    }
}

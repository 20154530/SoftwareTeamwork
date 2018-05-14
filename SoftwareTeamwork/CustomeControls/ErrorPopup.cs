using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoftwareTeamwork {
    public class ErrorPopup :DPopup{

        public event EventHandler PlacementTargetChanged;
        public new UIElement PlacementTarget {
            get => base.PlacementTarget;
            set {
                base.PlacementTarget = value;
                PlacementTargetChanged .Invoke(this,EventArgs.Empty);
            }
        }

        private void OnPlacementTargetChanged(object sender, EventArgs e) {
            if (PlacementTarget is null)
                return;
            else {
                HorizontalOffset = PlacementTarget.RenderSize.Width - 200;
                VerticalOffset = PlacementTarget.RenderSize.Height - 45;
                
            }
        }

        protected override void OnStyleChanged(Style oldStyle, Style newStyle) {
            if (newStyle == null)
                return;
            UIElementCollection Children = ((Grid)((Border)Child).Child).Children;
            foreach(UIElement u in Children) 
                if (u is Control && ((Control)u).Name.Equals("ExitButton")) { ((IconButton)u).Click += Exit_Click;break; }
            
            base.OnStyleChanged(oldStyle, newStyle);
        }

        private void Exit_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.HidePopupAni();
        }

        public ErrorPopup() {
            PlacementTargetChanged += OnPlacementTargetChanged;
            UseInOutAni = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace SoftwareTeamwork {
    public class ErrorPopup :DPopup{

        public event EventHandler PlacementTargetChanged;
        public new UIElement PlacementTarget {
            get => base.PlacementTarget;
            set {
                base.PlacementTarget = value;
                PlacementTargetChanged.Invoke(this,EventArgs.Empty);
            }
        }

        private void OnPlacementTargetChanged(object sender, EventArgs e) {
            if (PlacementTarget is null)
                return;
            else {
                HorizontalOffset = PlacementTarget.RenderSize.Width - 210;
                VerticalOffset = PlacementTarget.RenderSize.Height - 50;
                if(PlacementTarget is Window) {
                    ((Window)PlacementTarget).LocationChanged += ErrorPopup_LocationChanged;
                }else if(PlacementTarget is DPopup) {
                    ((DPopup)PlacementTarget).Closed += ErrorPopup_Closed;
                }
            }
        }

        private void ErrorPopup_Closed(object sender, EventArgs e) {
            HidePopupAni();
        }

        private void ErrorPopup_LocationChanged(object sender, EventArgs e) {
            if (IsOpen) {
                HorizontalOffset = PlacementTarget.RenderSize.Width - 210;
                VerticalOffset = PlacementTarget.RenderSize.Height - 50;
                IsOpen = false;
                IsOpen = true;
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

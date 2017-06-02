using Foundation;
using System;
using UIKit;

namespace PCLNative_TCSafety_014.iOS
{
    public partial class IncidenteReporte2ViewController : UIViewController
    {
        public IncidenteReporte2ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = false;
        }
    }
}
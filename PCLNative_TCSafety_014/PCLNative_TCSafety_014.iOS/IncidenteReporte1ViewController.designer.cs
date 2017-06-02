// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PCLNative_TCSafety_014.iOS
{
    [Register ("IncidenteReporte1ViewController")]
    partial class IncidenteReporte1ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnIncidenteReporte2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTomarFoto1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView cbEmpresa1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView cbInvestigador1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView liveCameraStream { get; set; }

        [Action ("BtnTomarFoto1_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnTomarFoto1_TouchUpInsideAsync (UIKit.UIButton sender);

        [Action ("UIButton6553_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton6553_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnIncidenteReporte2 != null) {
                btnIncidenteReporte2.Dispose ();
                btnIncidenteReporte2 = null;
            }

            if (btnTomarFoto1 != null) {
                btnTomarFoto1.Dispose ();
                btnTomarFoto1 = null;
            }

            if (cbEmpresa1 != null) {
                cbEmpresa1.Dispose ();
                cbEmpresa1 = null;
            }

            if (cbInvestigador1 != null) {
                cbInvestigador1.Dispose ();
                cbInvestigador1 = null;
            }

            if (liveCameraStream != null) {
                liveCameraStream.Dispose ();
                liveCameraStream = null;
            }
        }
    }
}
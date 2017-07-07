using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PCLNative_TCSafety_014.Droid
{
    [Activity(Label = "Enviar Mail")]
    public class MailReporte : Activity
    {
        private int _id_reporte_incidente = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }
    }
}
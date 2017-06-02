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
    [Activity(Label = "Menú")]
    public class Home : Activity
    {
        Button btnIncidentes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);

            btnIncidentes = FindViewById<Button>(Resource.Id.btnIncidentes);

            btnIncidentes.Click += BtnIncidentes_Click;
        }

        private void BtnIncidentes_Click(object sender, EventArgs e)
        {
            var incidente1 = new Intent(this, typeof(IncidenteReporte1));
            StartActivity(incidente1);
        }
    }
}
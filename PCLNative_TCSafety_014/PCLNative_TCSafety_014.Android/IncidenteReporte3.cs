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
    [Activity(Label = "Reporte de incidente: 3")]
    public class IncidenteReporte3 : Activity
    {
        private int _id_investigador;
        private string _titulo;
        private int _id_empresa;
        private string _relato_causa;
        private string _medidas_control;
        private int _id_impactado;
        private int _id_impacto;
        private int _id_potencial;

        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IncidenteReporte2);
            client = new WSTCSafety.WSIncidentes();
            // Create your application here
        }
    }
}
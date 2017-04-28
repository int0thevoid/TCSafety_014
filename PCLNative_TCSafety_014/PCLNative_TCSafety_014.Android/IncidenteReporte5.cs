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
using Newtonsoft.Json;

namespace PCLNative_TCSafety_014.Droid
{
    [Activity(Label = "Reporte de incidente: Material")]
    public class IncidenteReporte5 : Activity
    {
        private int _id_investigador;
        private string _titulo;
        private int _id_empresa;
        private string _relato_causa;
        private string _medidas_control;
        private int _id_impactado;
        private int _id_impacto;
        private int _id_potencial;
        private int _id_area;
        private int _id_subarea;
        private int _id_estado_tipo_incidente;
        private int _id_tipo_incidente;
        private string _lugar;
        
        private List<Clases.INAfectado> listadoAfectado;
        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.IncidenteReporte5);

            _id_investigador = Intent.GetIntExtra("id_investigador", 0);
            _titulo = Intent.GetStringExtra("titulo");
            _id_empresa = Intent.GetIntExtra("id_empresa", 0);
            _relato_causa = Intent.GetStringExtra("relato_causa");
            _medidas_control = Intent.GetStringExtra("medidas_control");
            _id_impactado = Intent.GetIntExtra("id_impactado", 0);
            _id_impacto = Intent.GetIntExtra("id_impacto", 0);
            _id_potencial = Intent.GetIntExtra("id_potencial", 0);
            _id_area = Intent.GetIntExtra("id_area", 0);
            _id_subarea = Intent.GetIntExtra("id_subarea", 0);
            _id_estado_tipo_incidente = Intent.GetIntExtra("id_estado_tipo_incidente", 0);
            _id_tipo_incidente = Intent.GetIntExtra("id_tipo_incidente", 0);
            _lugar = Intent.GetStringExtra("lugar");
            listadoAfectado = JsonConvert.DeserializeObject<List<Clases.INAfectado>>(Intent.GetStringExtra("listadoAfectado"));




        }
    }
}
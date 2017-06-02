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
    [Activity(Label = "Reporte de incidente: 4")]
    public class IncidenteReporte4 : Activity
    {
        private int _numeroLesionado = 1;

        private int _idEmpresaLesionado;
        private int _idTipoLesion;

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
        private int _day, _month, _year;
        private int _hour, _minute;

        private EditText txtNombre;
        private EditText txtApellido;
        private Spinner cbEmpresa3;
        private Spinner cbTipoLesion;
        private Button btnAgregarLesionado;
        private Button btnIncidenteReporte5;
        private TextView lblMsgLesionados;

        private List<Clases.TCEmpresa> listadoEmpresas;
        private List<Clases.INTipoLesion> listadoTipoLesion;
        private List<Clases.INAfectado> listadoAfectado;

        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {            
            base.OnCreate(savedInstanceState);
            client = new WSTCSafety.WSIncidentes();
            SetContentView(Resource.Layout.IncidenteReporte4);

            int n_lesionado = Intent.GetIntExtra("numero_lesionado", 0);

            txtNombre = FindViewById<EditText>(Resource.Id.txtNombre);
            txtApellido = FindViewById<EditText>(Resource.Id.txtApellido);
            cbEmpresa3 = FindViewById<Spinner>(Resource.Id.cbEmpresa3);
            cbTipoLesion = FindViewById<Spinner>(Resource.Id.cbTipoLesion);
            btnAgregarLesionado = FindViewById<Button>(Resource.Id.btnAgregarLesionado);
            btnIncidenteReporte5 = FindViewById<Button>(Resource.Id.btnIncidenteReporte5);
            lblMsgLesionados = FindViewById<TextView>(Resource.Id.lblMsgLesionados);

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
            _day = Intent.GetIntExtra("dia", 0);
            _month = Intent.GetIntExtra("mes", 0);
            _year = Intent.GetIntExtra("ano", 0);
            _hour = Intent.GetIntExtra("hora", 0);
            _minute = Intent.GetIntExtra("minuto", 0);

            this._numeroLesionado = _numeroLesionado + n_lesionado;

            if (this._numeroLesionado != 1)
            {
                
                listadoAfectado = JsonConvert.DeserializeObject<List<Clases.INAfectado>>(Intent.GetStringExtra("listadoAfectado"));
            }
            else
            {
                listadoAfectado = new List<Clases.INAfectado>();
            }

            lblMsgLesionados.Text = "Lesinoado Nº" + _numeroLesionado;

            cbEmpresa3.Enabled = false;
            client.getListadoEmpresaCompleted += Client_getListadoEmpresaCompleted;
            client.getListadoEmpresaAsync();
            cbEmpresa3.ItemSelected += CbEmpresa3_ItemSelected;

            cbTipoLesion.Enabled = false;
            client.getListadoTipoLesionCompleted += Client_getListadoTipoLesionCompleted;
            client.getListadoTipoLesionAsync();
            cbTipoLesion.ItemSelected += CbTipoLesion_ItemSelected;

            btnAgregarLesionado.Click += BtnAgregarLesionado_Click;
            btnIncidenteReporte5.Click += BtnIncidenteReporte5_Click;
        }

        private void BtnIncidenteReporte5_Click(object sender, EventArgs e)
        {
            Clases.INAfectado afectado = new Clases.INAfectado(txtNombre.Text, txtApellido.Text, _idEmpresaLesionado, _idTipoLesion);
            listadoAfectado.Add(afectado);

            Intent incidenteReporte5 = new Intent(this, typeof(IncidenteReporte5));

            incidenteReporte5.PutExtra("id_investigador", _id_investigador);
            incidenteReporte5.PutExtra("titulo", _titulo);
            incidenteReporte5.PutExtra("id_empresa", _id_empresa);
            incidenteReporte5.PutExtra("relato_causa", _relato_causa);
            incidenteReporte5.PutExtra("medidas_control", _medidas_control);
            incidenteReporte5.PutExtra("id_impactado", _id_impactado);
            incidenteReporte5.PutExtra("id_impacto", _id_impacto);
            incidenteReporte5.PutExtra("id_potencial", _id_potencial);

            incidenteReporte5.PutExtra("id_area", _id_area);
            incidenteReporte5.PutExtra("id_subarea", _id_subarea);
            incidenteReporte5.PutExtra("id_estado_tipo_incidente", _id_estado_tipo_incidente);
            incidenteReporte5.PutExtra("id_tipo_incidente", _id_tipo_incidente);
            incidenteReporte5.PutExtra("lugar", _lugar);
            incidenteReporte5.PutExtra("listadoAfectado", JsonConvert.SerializeObject(listadoAfectado));
            incidenteReporte5.PutExtra("dia", _day);
            incidenteReporte5.PutExtra("mes", _month);
            incidenteReporte5.PutExtra("ano", _year);
            incidenteReporte5.PutExtra("hora", _hour);
            incidenteReporte5.PutExtra("minuto", _minute);

            StartActivity(incidenteReporte5);
        }

        private void BtnAgregarLesionado_Click(object sender, EventArgs e)
        {
            Clases.INAfectado afectado = new Clases.INAfectado(txtNombre.Text, txtApellido.Text, _idEmpresaLesionado, _idTipoLesion);
            listadoAfectado.Add(afectado);

            Intent incidenteReporte4 = new Intent(this, typeof(IncidenteReporte4));

            incidenteReporte4.PutExtra("id_investigador", _id_investigador);
            incidenteReporte4.PutExtra("titulo", _titulo);
            incidenteReporte4.PutExtra("id_empresa", _id_empresa);
            incidenteReporte4.PutExtra("relato_causa", _relato_causa);
            incidenteReporte4.PutExtra("medidas_control", _medidas_control);
            incidenteReporte4.PutExtra("id_impactado", _id_impactado);
            incidenteReporte4.PutExtra("id_impacto", _id_impacto);
            incidenteReporte4.PutExtra("id_potencial", _id_potencial);

            incidenteReporte4.PutExtra("id_area", _id_area);
            incidenteReporte4.PutExtra("id_subarea", _id_subarea);
            incidenteReporte4.PutExtra("id_estado_tipo_incidente", _id_estado_tipo_incidente);
            incidenteReporte4.PutExtra("id_tipo_incidente", _id_tipo_incidente);
            incidenteReporte4.PutExtra("lugar", _lugar);
            incidenteReporte4.PutExtra("listadoAfectado", JsonConvert.SerializeObject(listadoAfectado));

            StartActivity(incidenteReporte4);
        }

        private void CbTipoLesion_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _idTipoLesion = this.listadoTipoLesion.ElementAt(e.Position).Id;
        }

        private void Client_getListadoTipoLesionCompleted(object sender, WSTCSafety.getListadoTipoLesionCompletedEventArgs e)
        {
            listadoTipoLesion = JsonConvert.DeserializeObject<List<Clases.INTipoLesion>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.INTipoLesion item in listadoTipoLesion)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBTipoLesion = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbTipoLesion.Adapter = adapterCBTipoLesion;
            cbTipoLesion.Enabled = true;
        }

        private void CbEmpresa3_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _idEmpresaLesionado = this.listadoEmpresas.ElementAt(e.Position).Id;
        }

        private void Client_getListadoEmpresaCompleted(object sender, WSTCSafety.getListadoEmpresaCompletedEventArgs e)
        {
            listadoEmpresas = JsonConvert.DeserializeObject<List<Clases.TCEmpresa>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.TCEmpresa item in listadoEmpresas)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBEmpresa3 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbEmpresa3.Adapter = adapterCBEmpresa3;
            cbEmpresa3.Enabled = true;
        }
    }
}
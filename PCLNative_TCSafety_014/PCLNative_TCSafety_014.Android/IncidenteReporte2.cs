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
    [Activity(Label = "Reporte de incidente: 2")]
    public class IncidenteReporte2 : Activity
    {
        private int _id_investigador;
        private string _titulo;
        private int _id_empresa;
        private string _relato_causa;
        private string _medidas_control;
        private int _id_impactado;
        private int _id_impacto;
        private int _id_potencial;
        //private string _fecha_ocurrencia;
        //private string _hora_ocurrencia;

        private EditText txtTitulo;
        private Spinner cbEmpresa2;
        private MultiAutoCompleteTextView txtRelatoCausa;
        private MultiAutoCompleteTextView txtMedidasControl;
        private Spinner cbImpactado;
        private Spinner cbImpacto;
        private Spinner cbPotencial;
        private Button btnIncidenteReporte3;
        private Button btnGetFecha;
        private Button btnGetHora;
        private TextView lblFecha, lblHora;

        //private DatePicker datePicker;
        //private TimePicker timePicker;

        private List<Clases.TCEmpresa> listadoEmpresas;
        private List<Clases.INImpacto> listadoImpacto;
        private List<Clases.INImpactado> listadoImpactado;
        private List<Clases.INPotencial> listadoPotencial;

        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IncidenteReporte2);
            client = new WSTCSafety.WSIncidentes();

            _id_investigador = Intent.GetIntExtra("id_investigador", 0);

            cbEmpresa2 = FindViewById<Spinner>(Resource.Id.cbEmpresa2);
            cbImpactado = FindViewById<Spinner>(Resource.Id.cbImpactado);
            cbImpacto = FindViewById<Spinner>(Resource.Id.cbImpacto);
            cbPotencial = FindViewById<Spinner>(Resource.Id.cbPotencial);
            txtRelatoCausa = FindViewById<MultiAutoCompleteTextView>(Resource.Id.txtRelatoCausa);
            txtMedidasControl = FindViewById<MultiAutoCompleteTextView>(Resource.Id.txtMedidasControl);
            txtTitulo = FindViewById<EditText>(Resource.Id.txtTitulo);
            btnIncidenteReporte3 = FindViewById<Button>(Resource.Id.btnIncidenteReporte3);

            btnGetFecha = FindViewById<Button>(Resource.Id.btnGetFecha);
            btnGetHora = FindViewById<Button>(Resource.Id.btnGetHora);
            lblFecha = FindViewById<TextView>(Resource.Id.lblFecha);
            lblHora = FindViewById<TextView>(Resource.Id.lblHora);
            
            cbEmpresa2.Enabled = false;
            client.getListadoEmpresaCompleted += Client_getListadoEmpresaCompleted;
            client.getListadoEmpresaAsync();
            cbEmpresa2.ItemSelected += CbEmpresa2_ItemSelected;

            cbImpactado.Enabled = false;
            client.getListadoImpactadoCompleted += Client_getListadoImpactadoCompleted;
            client.getListadoImpactadoAsync();
            cbImpactado.ItemSelected += CbImpactado_ItemSelected;

            cbImpacto.ItemSelected += CbImpacto_ItemSelected;

            cbPotencial.Enabled = false;
            client.getListadoPotencialCompleted += Client_getListadoPotencialCompleted;
            client.getListadoPotencialAsync();
            cbPotencial.ItemSelected += CbPotencial_ItemSelected;

            btnIncidenteReporte3.Click += BtnIncidenteReporte3_Click;

            btnGetFecha.Click += BtnGetFecha_Click;
            btnGetHora.Click += BtnGetHora_Click;

        }

        private void BtnGetHora_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnGetFecha_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnIncidenteReporte3_Click(object sender, EventArgs e)
        {
            _titulo = txtTitulo.Text;
            _relato_causa = txtRelatoCausa.Text;
            _medidas_control = txtMedidasControl.Text;
            //_fecha_ocurrencia = datePicker.DateTime.ToString();
            //_hora_ocurrencia = timePicker.Hour.ToString();

            var incidente3 = new Intent(this, typeof(IncidenteReporte3));
            incidente3.PutExtra("id_investigador", _id_investigador);
            incidente3.PutExtra("titulo", _titulo);
            incidente3.PutExtra("id_empresa", _id_empresa);
            incidente3.PutExtra("relato_causa", _relato_causa);
            incidente3.PutExtra("medidas_control", _medidas_control);
            incidente3.PutExtra("id_impactado", _id_impactado);
            incidente3.PutExtra("id_impacto", _id_impacto);
            incidente3.PutExtra("id_potencial", _id_potencial);
            //incidente3.PutExtra("fecha_ocurrencia", _fecha_ocurrencia);
            //incidente3.PutExtra("hora_ocurrencia", _hora_ocurrencia);

            StartActivity(incidente3);
        }

        private void CbPotencial_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_potencial = this.listadoPotencial.ElementAt(e.Position).Id;
        }

        private void Client_getListadoPotencialCompleted(object sender, WSTCSafety.getListadoPotencialCompletedEventArgs e)
        {
            listadoPotencial = JsonConvert.DeserializeObject<List<Clases.INPotencial>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.INPotencial item in listadoPotencial)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBPotencial = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbPotencial.Adapter = adapterCBPotencial;
            cbPotencial.Enabled = true;
        }

        private void CbImpacto_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_impacto = this.listadoImpacto.ElementAt(e.Position).Id;
        }

        private void CbImpactado_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            cbImpacto.Enabled = false;
            _id_impactado = this.listadoImpactado.ElementAt(e.Position).Id;

            client.getListadoImpactoCompleted += Client_getListadoImpactoCompleted;
            client.getListadoImpactoAsync(_id_impactado, true);
        }

        private void Client_getListadoImpactoCompleted(object sender, WSTCSafety.getListadoImpactoCompletedEventArgs e)
        {
            listadoImpacto = JsonConvert.DeserializeObject<List<Clases.INImpacto>>(e.Result);

            List<string> mItems = new List<string>();

            foreach (Clases.INImpacto item in listadoImpacto)
            {
                mItems.Add(item.Descripcion);
            }

            ArrayAdapter adapterCBImpacto = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbImpacto.Adapter = adapterCBImpacto;
            cbImpacto.Enabled = true;
        }

        private void Client_getListadoImpactadoCompleted(object sender, WSTCSafety.getListadoImpactadoCompletedEventArgs e)
        {
            listadoImpactado = JsonConvert.DeserializeObject<List<Clases.INImpactado>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.INImpactado item in listadoImpactado)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBImpactado = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbImpactado.Adapter = adapterCBImpactado;
            cbImpactado.Enabled = true;
        }

        private void CbEmpresa2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_empresa = this.listadoEmpresas.ElementAt(e.Position).Id;
        }

        private void Client_getListadoEmpresaCompleted(object sender, WSTCSafety.getListadoEmpresaCompletedEventArgs e)
        {
            listadoEmpresas = JsonConvert.DeserializeObject<List<Clases.TCEmpresa>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.TCEmpresa item in listadoEmpresas)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBEmpresa2 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbEmpresa2.Adapter = adapterCBEmpresa2;
            cbEmpresa2.Enabled = true;
        }
    }
}
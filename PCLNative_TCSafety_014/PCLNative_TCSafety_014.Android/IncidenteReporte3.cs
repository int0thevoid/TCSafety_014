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

        private int _id_area;
        private int _id_subarea;
        private int _id_estado_tipo_incidente;
        private int _id_tipo_incidente;
        private string _lugar;

        private EditText txtLugar;
        private Spinner cbArea;
        private Spinner cbSubarea;
        private Spinner cbEstadoTipoIncidente;
        private Spinner cbTipoIncidente;
        private Button btnIncidenteReporte4;
        private TextView lblTipoIncidente;

        private List<Clases.TCArea> listadoArea;
        private List<Clases.TCSubarea> listadoSubarea;
        private List<Clases.INEstadoTipoIncidente> listadoEstadoTipoIncidente;
        private List<Clases.INTipoIncidente> listadoTipoIncidente;

        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IncidenteReporte3);
            client = new WSTCSafety.WSIncidentes();

            _id_investigador = Intent.GetIntExtra("id_investigador", 0);
            _titulo = Intent.GetStringExtra("titulo");
            _id_empresa = Intent.GetIntExtra("id_empresa", 0);
            _relato_causa = Intent.GetStringExtra("relato_causa");
            _medidas_control = Intent.GetStringExtra("medidas_control");
            _id_impactado = Intent.GetIntExtra("id_impactado", 0);
            _id_impacto = Intent.GetIntExtra("id_impacto", 0);
            _id_potencial = Intent.GetIntExtra("id_potencial", 0);

            cbArea = FindViewById<Spinner>(Resource.Id.cbArea);
            cbSubarea = FindViewById<Spinner>(Resource.Id.cbSubarea);
            cbEstadoTipoIncidente = FindViewById<Spinner>(Resource.Id.cbEstadoTipoIncidente);
            cbTipoIncidente = FindViewById<Spinner>(Resource.Id.cbTipoIncidente);
            txtLugar = FindViewById<EditText>(Resource.Id.txtLugar);
            lblTipoIncidente = FindViewById<TextView>(Resource.Id.lblTipoIncidente);
            btnIncidenteReporte4 = FindViewById<Button>(Resource.Id.btnIncidenteReporte4);

            cbArea.Enabled = false;
            client.getListadoAreaCompleted += Client_getListadoAreaCompleted;
            client.getListadoAreaAsync();
            cbArea.ItemSelected += CbArea_ItemSelected;

            cbEstadoTipoIncidente.Enabled = false;
            client.getListadoEstadoTipoIncidenteCompleted += Client_getListadoEstadoTipoIncidenteCompleted;
            client.getListadoEstadoTipoIncidenteAsync();
            cbEstadoTipoIncidente.ItemSelected += CbEstadoTipoIncidente_ItemSelected;

            cbTipoIncidente.Enabled = false;
            client.getListadoTipoIncidenteCompleted += Client_getListadoTipoIncidenteCompleted;
            client.getListadoTipoIncidenteAsync();
            cbTipoIncidente.ItemSelected += CbTipoIncidente_ItemSelected;

            cbSubarea.ItemSelected += CbSubarea_ItemSelected;

            btnIncidenteReporte4.Click += BtnIncidenteReporte4_Click;
        }

        private void BtnIncidenteReporte4_Click(object sender, EventArgs e)
        {
            _lugar = txtLugar.Text;

            var intent = new Intent();
            if (_id_estado_tipo_incidente == 2)
            {
                //con lesiones
                intent = new Intent(this, typeof(IncidenteReporte4));
            }
            else
            {
                //sin lesiones
                intent = new Intent(this, typeof(IncidenteReporte5));
            }

            intent.PutExtra("id_investigador", _id_investigador);
            intent.PutExtra("titulo", _titulo);
            intent.PutExtra("id_empresa", _id_empresa);
            intent.PutExtra("relato_causa", _relato_causa);
            intent.PutExtra("medidas_control", _medidas_control);
            intent.PutExtra("id_impactado", _id_impactado);
            intent.PutExtra("id_impacto", _id_impacto);
            intent.PutExtra("id_potencial", _id_potencial);

            intent.PutExtra("id_area",_id_area);
            intent.PutExtra("id_subarea",_id_subarea);
            intent.PutExtra("id_estado_tipo_incidente",_id_estado_tipo_incidente);
            intent.PutExtra("id_tipo_incidente", _id_tipo_incidente);
            intent.PutExtra("lugar", _lugar);
    
            StartActivity(intent);
        }

        private void CbSubarea_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_subarea = this.listadoSubarea.ElementAt(e.Position).Id;
        }

        private void CbTipoIncidente_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_tipo_incidente = this.listadoTipoIncidente.ElementAt(e.Position).Id;
        }

        private void CbEstadoTipoIncidente_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_estado_tipo_incidente = this.listadoEstadoTipoIncidente.ElementAt(e.Position).Id;
            //string toast = string.Format("The planet is {0}", _id_estado_tipo_incidente);
            //Toast.MakeText(this, toast, ToastLength.Long).Show();

            if (_id_estado_tipo_incidente == 2)
            {
                lblTipoIncidente.Visibility = ViewStates.Gone;
                cbTipoIncidente.Visibility = ViewStates.Gone;
            }
            else
            {
                lblTipoIncidente.Visibility = ViewStates.Visible;
                cbTipoIncidente.Visibility = ViewStates.Visible;
            }
        }

        private void CbArea_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _id_area = this.listadoArea.ElementAt(e.Position).Id;

            cbSubarea.Enabled = false;

            client.getListadoSubareaCompleted += Client_getListadoSubareaCompleted;
            client.getListadoSubareaAsync(_id_area, true);
        }

        private void Client_getListadoSubareaCompleted(object sender, WSTCSafety.getListadoSubareaCompletedEventArgs e)
        {
            listadoSubarea = JsonConvert.DeserializeObject<List<Clases.TCSubarea>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.TCSubarea item in listadoSubarea)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBSubarea = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbSubarea.Adapter = adapterCBSubarea;
            cbSubarea.Enabled = true;
        }

        private void Client_getListadoTipoIncidenteCompleted(object sender, WSTCSafety.getListadoTipoIncidenteCompletedEventArgs e)
        {
            listadoTipoIncidente = JsonConvert.DeserializeObject<List<Clases.INTipoIncidente>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.INTipoIncidente item in listadoTipoIncidente)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBTipoIncidente = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbTipoIncidente.Adapter = adapterCBTipoIncidente;
            cbTipoIncidente.Enabled = true;
        }

        private void Client_getListadoEstadoTipoIncidenteCompleted(object sender, WSTCSafety.getListadoEstadoTipoIncidenteCompletedEventArgs e)
        {
            listadoEstadoTipoIncidente = JsonConvert.DeserializeObject<List<Clases.INEstadoTipoIncidente>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.INEstadoTipoIncidente item in listadoEstadoTipoIncidente)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBEstadoTipoIncidente = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbEstadoTipoIncidente.Adapter = adapterCBEstadoTipoIncidente;
            cbEstadoTipoIncidente.Enabled = true;
        }

        private void Client_getListadoAreaCompleted(object sender, WSTCSafety.getListadoAreaCompletedEventArgs e)
        {
            listadoArea = JsonConvert.DeserializeObject<List<Clases.TCArea>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.TCArea item in listadoArea)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBArea = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbArea.Adapter = adapterCBArea;
            cbArea.Enabled = true;
        }
    }
}
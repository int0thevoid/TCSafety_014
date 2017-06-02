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
using Android.Provider;
using Android.Graphics;
using System.IO;

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
        private int _day, _month, _year;
        private int _hour, _minute;
        private DateTime _fecha_ocurrencia;
        private DateTime _fecha_ingreso = DateTime.Now;

        private Bitmap _bitmap = null;

        private int _id_incidente = 0;

        private List<Clases.INAfectado> listadoAfectado;
        private WSTCSafety.WSIncidentes client;

        private TextView lblMensaje5;
        private Button btnTomarFoto;
        private Button btnReportarIncidente;
        private ImageView imageView;

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
            _day = Intent.GetIntExtra("dia", 0);
            _month = Intent.GetIntExtra("mes", 0);
            _year = Intent.GetIntExtra("ano", 0);
            _hour = Intent.GetIntExtra("hora", 0);
            _minute = Intent.GetIntExtra("minuto", 0);
            _fecha_ocurrencia = new DateTime(_year, _month, _day, _hour, _minute, 0);

            if (_id_estado_tipo_incidente == 2)
            {
                //con lesiones
                listadoAfectado = JsonConvert.DeserializeObject<List<Clases.INAfectado>>(Intent.GetStringExtra("listadoAfectado"));
            }

            btnTomarFoto = FindViewById<Button>(Resource.Id.btnTomarFoto);
            btnReportarIncidente = FindViewById<Button>(Resource.Id.btnReportarIncidente);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            lblMensaje5 = FindViewById<TextView>(Resource.Id.lblMensaje5);

            btnTomarFoto.Click += BtnTomarFoto_Click;
            btnReportarIncidente.Click += BtnReportarIncidente_Click;
        }

        private void BtnReportarIncidente_Click(object sender, EventArgs e)
        {
            client = new WSTCSafety.WSIncidentes();
            btnReportarIncidente.Enabled = false;
            btnTomarFoto.Enabled = false;
            
            lblMensaje5.Text = "Registrando el incidente... por favor, espere.";
            client.guardarIncidenteCompleted += Client_guardarIncidenteCompleted;
            client.guardarIncidenteAsync(_titulo, _id_empresa, true, _fecha_ingreso, true, _fecha_ocurrencia, true, _relato_causa, _medidas_control,
                _id_impacto, true, _id_potencial, true, _lugar, _id_area, true, 1, true, _id_estado_tipo_incidente, true, _id_subarea, true,
                1, true, 1, true);
            
        }

        private void Client_guardarIncidenteCompleted(object sender, WSTCSafety.guardarIncidenteCompletedEventArgs e)
        {
            _id_incidente = e.guardarIncidenteResult;
            if(_bitmap != null)
            {
                if (_id_incidente != 0)
                {
                    try
                    {
                        client.guardarInvolucradoCompleted += Client_guardarInvolucradoCompleted;
                        client.guardarInvolucradoAsync(_id_incidente, true, 9, true, 1, true, _fecha_ocurrencia, true);
                        client.guardarInvolucradoAsync(_id_incidente, true, _id_investigador, true, 2, true, _fecha_ocurrencia, true);
                        if (_id_estado_tipo_incidente == 2)
                        {
                            client.guardarAfectadoCompleted += Client_guardarAfectadoCompleted;
                            //Con Lesiones
                            foreach (Clases.INAfectado item in listadoAfectado)
                            {
                                client.guardarAfectadoAsync("", item.Nombre, item.Apellidos, item.Sexo, item.Edad, true, item.IdEmpresa, true, item.Area, item.Jefedirecto, "", item.Antiguedad, item.IdTipoLesion, true, item.DiasPerdidos, true, item.ConsecuenciaObservacion, _id_incidente, true);
                            }
                        }
                        else
                        {
                            //Sin Lesiones
                            client.guardarIncidenteTipoIncidente(_id_incidente, true, _id_tipo_incidente, true);
                        }

                        MemoryStream stream = new MemoryStream();
                        _bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                        byte[] bitmapData = stream.ToArray();

                        string nombre_imagen = "image_incident_" + _id_incidente.ToString();
                        client.guardarAnexoCompleted += Client_guardarAnexoCompleted;
                        client.guardarAnexoAsync(bitmapData, "jpeg", nombre_imagen, _fecha_ingreso, true);
                    }
                    catch (Exception)
                    {

                        lblMensaje5.Text = "Error al agregar incidente.";
                    }
                }
                else
                {
                    lblMensaje5.Text = "Hubo un problema en registrar el incidente.";
                }
            }
            else
            {
                lblMensaje5.Text = "Debe adjuntar una imagen del incidente.";
            }
            btnReportarIncidente.Enabled = true;
            btnTomarFoto.Enabled = true;
        }

        private void Client_guardarAnexoCompleted(object sender, WSTCSafety.guardarAnexoCompletedEventArgs e)
        {
            string toast = string.Format("El incidente {0} ha sido registrado satisfactoriamente", _id_incidente);
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            Intent home = new Intent(this, typeof(Home));
            StartActivity(home);
        }

        private void Client_guardarAfectadoCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        private void Client_guardarInvolucradoCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        private void BtnTomarFoto_Click(object sender, EventArgs e)
        {
            Intent cameraIntent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(cameraIntent,0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);
                _bitmap = (Bitmap)data.Extras.Get("data");
                imageView.SetImageBitmap(_bitmap);
            }
            catch(Exception e)
            {
                
            }
        }
    }
}
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
    [Activity(Label = "Reporte de incidente: 1")]
    public class IncidenteReporte1 : Activity
    {
        private int id_investigador;

        private Spinner cbEmpresa1;
        private Spinner cbInvestigador1;
        private Button btnIncidenteReporte2;

        private List<Clases.TCEmpresa> listadoEmpresas;
        private List<Clases.TCUsuario> listadoInvestigador;

        private WSTCSafety.WSIncidentes client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IncidenteReporte1);

            cbEmpresa1 = FindViewById<Spinner>(Resource.Id.cbEmpresa1);
            cbInvestigador1 = FindViewById<Spinner>(Resource.Id.cbInvestigador1);
            btnIncidenteReporte2 = FindViewById<Button>(Resource.Id.btnIncidenteReporte2);

            client = new WSTCSafety.WSIncidentes();

            cbEmpresa1.Enabled = false;
            client.getListadoEmpresaCompleted += Client_getListadoEmpresaCompleted;
            client.getListadoEmpresaAsync();

            cbEmpresa1.ItemSelected += CbEmpresa1_ItemSelected;
            cbInvestigador1.ItemSelected += CbInvestigador1_ItemSelected;
            btnIncidenteReporte2.Click += BtnIncidenteReporte2_Click;

        }

        private void CbInvestigador1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            id_investigador = this.listadoInvestigador.ElementAt(e.Position).Id;
        }

        private void BtnIncidenteReporte2_Click(object sender, EventArgs e)
        {
            var incidente2 = new Intent(this, typeof(IncidenteReporte2));
            incidente2.PutExtra("id_investigador",id_investigador);
            StartActivity(incidente2);
        }

        private void CbEmpresa1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            cbInvestigador1.Enabled = false;
            int idEmpresa = this.listadoEmpresas.ElementAt(e.Position).Id;

            client.getListadoUsuarioCompleted += Client_getListadoUsuarioCompleted;
            client.getListadoUsuarioAsync(idEmpresa, true);
        }

        private void Client_getListadoUsuarioCompleted(object sender, WSTCSafety.getListadoUsuarioCompletedEventArgs e)
        {
            listadoInvestigador = JsonConvert.DeserializeObject<List<Clases.TCUsuario>>(e.Result);
            
            List <string> mItems = new List<string>();
            
            foreach (Clases.TCUsuario item in listadoInvestigador)
            {
                mItems.Add(item.Nombre);
            }
            
            ArrayAdapter adapterCBInvestigador1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbInvestigador1.Adapter = adapterCBInvestigador1;
            cbInvestigador1.Enabled = true;
        }

        private void Client_getListadoEmpresaCompleted(object sender, WSTCSafety.getListadoEmpresaCompletedEventArgs e)
        {
            listadoEmpresas = JsonConvert.DeserializeObject<List<Clases.TCEmpresa>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (Clases.TCEmpresa item in listadoEmpresas)
            {
                mItems.Add(item.Nombre);
            }

            ArrayAdapter adapterCBEmpresa1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, mItems);
            cbEmpresa1.Adapter = adapterCBEmpresa1;
            cbEmpresa1.Enabled = true;
        }
    }
}
using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PCLNative_TCSafety_014.Droid
{
	[Activity (Label = "TCSafety", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
	public class MainActivity : Activity
	{
        TextView lblMensaje;
        EditText txtRut;
        EditText txtPassword;
        Button btnIngresar;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            
			SetContentView (Resource.Layout.Main);

            lblMensaje = FindViewById<TextView>(Resource.Id.lblMensaje);
            txtRut = FindViewById<EditText>(Resource.Id.txtRut);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnIngresar = FindViewById<Button>(Resource.Id.btnIngresar);

            btnIngresar.Click += BtnIngresar_Click;

            txtRut.TextChanged += TxtRut_TextChanged;
        }

        private void TxtRut_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string cadena = txtRut.Text.ToString();
            if (cadena.Length == 1)
            {
                txtRut.Text = "-" + cadena;
            }
            else if (cadena.Length == 5)
            {
                txtRut.Text = "." + cadena;
            }
            else if (cadena.Length == 9)
            {
                txtRut.Text = "." + cadena;
            }
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            txtRut.Enabled = false;
            txtPassword.Enabled = false;
            if (txtRut.Text == string.Empty)
            {
                lblMensaje.Text = "Debe indicar un RUT para el inicio de sesión.";
                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                lblMensaje.Text = "Debe indicar una Clave para el inicio de sesión.";
                return;
            }
            
            WSTCSafety.WSIncidentes client = new WSTCSafety.WSIncidentes();
            client.getListadoUsuarioCompleted += Client_getListadoUsuarioCompleted;
            client.getListadoUsuarioAsync(1, false);
            lblMensaje.Text = "Ingresando... por favor espere.";
            btnIngresar.Enabled = false;
        }

        private void Client_getListadoUsuarioCompleted(object sender, WSTCSafety.getListadoUsuarioCompletedEventArgs e)
        {
            List<Clases.TCUsuario> listadoUsuario = JsonConvert.DeserializeObject<List<Clases.TCUsuario>>(e.Result);

            if (txtRut.Text == "11.111.111-1")
            {
                if(txtPassword.Text == "1")
                {
                    var Home = new Intent(this, typeof(Home));
                    StartActivity(Home);
                }
                else
                {
                    lblMensaje.Text = "Usuario y/o contraseña inválidos.";
                    txtRut.Text = "";
                    txtPassword.Text = "";
                }

            }
            else
            {
                lblMensaje.Text = "Usuario y/o contraseña inválidos.";
            }
            txtRut.Enabled = true;
            txtPassword.Enabled = true;
            btnIngresar.Enabled = true;
        }
    }
}



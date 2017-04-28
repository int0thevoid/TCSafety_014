using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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

            btnIngresar.Click += BtnIngresar_Click; ;
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
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
            if (true)
            {
                var Home = new Intent(this, typeof(Home));
                StartActivity(Home);
            }
        }
    }
}



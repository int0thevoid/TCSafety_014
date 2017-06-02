using System;
using System.Net.NetworkInformation;
using Foundation;
using PCLNative_TCSafety_014.iOS.WSCTSafety;
using UIKit;

namespace PCLNative_TCSafety_014.iOS
{
	public partial class ViewController : UIViewController
	{
		

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
            txtPassword.SecureTextEntry = true;
		}

        partial void BtnIngresar_TouchUpInside(UIButton sender)
        {
            bool check = NetworkInterface.GetIsNetworkAvailable();
            if(check){
				var rut = txtRut.Text;
				var password = txtPassword.Text;
				if (rut != "")
				{
					if (password != "")
					{
                        WSCTSafety.WSIncidentes client = new WSCTSafety.WSIncidentes();
                        client.getListadoUsuarioCompleted += Client_getListadoUsuarioCompleted;
						client.getListadoUsuarioAsync(1, false);
						txtMensageLogin.Text = "Ingresando... por favor espere.";
					}
					else
					{
						txtMensageLogin.Text = "Debe ingresar una contraseña";
					}
				}
				else
				{
					txtMensageLogin.Text = "Debe ingresar un usuario";
				}
            }else{
                
            }
        }

        private void Client_getListadoUsuarioCompleted(object sender, getListadoUsuarioCompletedEventArgs e)
        {
            txtRut.Enabled = false;
            txtPassword.Enabled = false;
            btnIngresar.Enabled = false;
            if (txtRut.Text == "11.111.111-1")
            {
                if (txtPassword.Text == "1")
                {
                    HomeViewController controller = this.Storyboard.InstantiateViewController("HomeViewController") as HomeViewController;
                    if(controller != null)
                    {
                        this.NavigationController.PushViewController(controller, true);
                    }
                }
                else
                {
                    txtMensageLogin.Text = "Usuario y/o contraseña inválidos.";
                    txtRut.Text = "";
                    txtPassword.Text = "";
                    return;
                }

            }
            else
            {
                txtMensageLogin.Text = "Usuario y/o contraseña inválidos.";
                txtRut.Text = "";
                txtPassword.Text = "";
                return;
            }
            txtRut.Enabled = true;
            txtPassword.Enabled = true;
            btnIngresar.Enabled = true;

        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
		}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (txtRut.Text != "" && txtPassword.Text != "")
            {
                base.PrepareForSegue(segue, sender);
                var homeViewController = segue.DestinationViewController as HomeViewController;
            }
            else
            {
                return;
            }
        }
    }
}


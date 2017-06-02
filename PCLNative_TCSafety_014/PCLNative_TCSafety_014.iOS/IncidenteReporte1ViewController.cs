using AVFoundation;
using Foundation;
using Newtonsoft.Json;
using PCLNative_TCSafety_014.iOS.Clases;
using PCLNative_TCSafety_014.iOS.PickerModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace PCLNative_TCSafety_014.iOS
{
    public partial class IncidenteReporte1ViewController : UIViewController
    {
        private List<TCEmpresa> listadoEmpresas;
        private List<TCUsuario> listadoInvestigador;

        private EmpresaPickerViewModel empresa1Picker;
        private pickerViewModel investigadorPicker;

        private int _id_investigador = 0;

        private AVCaptureSession _captureSession;
        private AVCaptureDeviceInput _captureInput;
        private AVCaptureStillImageOutput _stillCaptureOutput;
        private AVCaptureVideoPreviewLayer _videoPreviewLayer;

        public IncidenteReporte1ViewController (IntPtr handle) : base (handle)
        {
        }
        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            await AuthorizeCameraUse();
            SetupLiveCameraStream();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBarHidden = false;
            WSCTSafety.WSIncidentes client = new WSCTSafety.WSIncidentes();
            client.getListadoEmpresaCompleted += Client_getListadoEmpresaCompleted;
            client.getListadoEmpresaAsync();
        }

        async Task AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            if(authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }

        public void SetupLiveCameraStream()
        {
            _captureSession = new AVCaptureSession();
            
            var viewLayer = liveCameraStream.Layer;
            _videoPreviewLayer = new AVCaptureVideoPreviewLayer(_captureSession)
            {
                Frame = this.View.Frame
            };
            liveCameraStream.Layer.AddSublayer(_videoPreviewLayer);
            
            var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
            _captureInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            if (_captureInput == null)
            {
                Console.WriteLine("No input - No se ha detectado una cámara");
                //return false;
            }
            ConfigureCameraForDevice(captureDevice);
            _captureSession.AddInput(_captureInput);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            _stillCaptureOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            _captureSession.AddOutput(_stillCaptureOutput);
            _captureSession.StartRunning();
        }

        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if(device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }else if(device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }else if(device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

        private void Client_getListadoEmpresaCompleted(object sender, WSCTSafety.getListadoEmpresaCompletedEventArgs e)
        {
            try
            {
                listadoEmpresas = JsonConvert.DeserializeObject<List<TCEmpresa>>(e.Result);

                List<string> mItems = new List<string>();
                foreach (TCEmpresa item in listadoEmpresas)
                {
                    mItems.Add(item.Nombre);
                }

                empresa1Picker = new EmpresaPickerViewModel(mItems);
                empresa1Picker.EmpresaSelectedChanged += EmpresaPickerViewModel_EmpresaSelectedChanged;
                cbEmpresa1.Model = empresa1Picker;
            }
            catch (Exception)
            {
                UIAlertView _error = new UIAlertView("Error de conexión", "No se ha podido cargar el listado de empresas: " + e.Error, null, "Aceptar", null);
                _error.Show();
            }
            
        }

        private void EmpresaPickerViewModel_EmpresaSelectedChanged(object sender, EventArgs e)
        {
            int _id_empresa_1 = empresa1Picker._selectedIndex;
            WSCTSafety.WSIncidentes client = new WSCTSafety.WSIncidentes();
            client.getListadoUsuarioCompleted += Client_getListadoUsuarioCompleted;
            client.getListadoUsuarioAsync(empresa1Picker._selectedIndex, true);
        }

        private void Client_getListadoUsuarioCompleted(object sender, WSCTSafety.getListadoUsuarioCompletedEventArgs e)
        {
            listadoInvestigador = JsonConvert.DeserializeObject<List<Clases.TCUsuario>>(e.Result);

            List<string> mItems = new List<string>();
            foreach (TCUsuario item in listadoInvestigador)
            {
                mItems.Add(item.Nombre);
            }

            investigadorPicker = new pickerViewModel(mItems);
            investigadorPicker.SelectionChanged += InvestigadorPicker_SelectionChanged;
        }

        private void InvestigadorPicker_SelectionChanged(object sender, EventArgs e)
        {
            _id_investigador = listadoInvestigador[investigadorPicker._selectedIndex].Id;
        }

        partial void UIButton6553_TouchUpInside(UIButton sender)
        {
            if(_id_investigador == 0)
            {
                IncidenteReporte2ViewController controller = this.Storyboard.InstantiateViewController("IncidenteReporte2ViewController") as IncidenteReporte2ViewController;
                if (controller != null)
                {
                    this.NavigationController.PushViewController(controller, true);
                }
            }
            else
            {
                UIAlertView _error = new UIAlertView("Por favor:", "Seleccione un investigador para continuar con el reporte.", null, "Aceptar", null);
                _error.Show();
            }
        }
        /*
        partial void BtnTomarFoto1_TouchUpInside(UIButton sender)
        {
            /*
            _session = new AVCaptureSession();
            var camera = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);
            var input = AVCaptureDeviceInput.FromDevice(camera);
            _session.AddInput(input);

            var output = new AVCaptureStillImageOutput();
            var dict = new NSMutableDictionary();
            dict[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            _session.AddOutput(output);
            */
            /* --preview
            var previewLayer = new AVCaptureVideoPreviewLayer(_session);
            var view = new MyPreviewView(previewLayer);
            
            _session.StartRunning();
            var connection = output.Connections[0];
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                CaptureImageWithMetadata(output, connection);
            });
            */
            /*
            TweetStation.Camera.TakePicture(this, (obj) =>
            {
                var photo = obj.ValueForKey(new NSString("UIIMagePickerControllerOriginalImage")) as UIImage;
                var meta = objValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;
                ALAssetsLibrary library = new ALAssetsLibrary();
                library.WriteImageToSavedPhotosAlbum(photo.CGImage, meta, (assetUrl, error) =>
               {
                   Console.WriteLine("assetURL:" + assetUrl);
               });
            });
            
        }
    */
    }
}
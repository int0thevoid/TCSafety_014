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

namespace PCLNative_TCSafety_014.Droid.Clases
{
    class INListaDistribucionUsuario
    {
        public int Id { get; set; }
        public int IdListaDistribucion { get; set; }
        public int IdUsuario { get; set; }
    }
}
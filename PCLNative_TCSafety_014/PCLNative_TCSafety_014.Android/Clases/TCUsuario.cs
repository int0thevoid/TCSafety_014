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
    class TCUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string RUT { get; set; }
        public string Email { get; set; }
        public int IdEmpresa { get; set; }
        public int IdArea { get; set; }
        public int IdSubarea { get; set; }
        public int IdPerfil { get; set; }
        public int EstadoRegistro { get; set; }
    }
}
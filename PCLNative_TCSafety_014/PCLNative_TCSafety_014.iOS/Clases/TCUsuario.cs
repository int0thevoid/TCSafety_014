using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PCLNative_TCSafety_014.iOS.Clases
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
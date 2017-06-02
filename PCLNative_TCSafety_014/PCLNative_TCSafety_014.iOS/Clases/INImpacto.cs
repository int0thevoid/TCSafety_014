using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PCLNative_TCSafety_014.iOS.Clases
{
    class INImpacto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdImpactado { get; set; }
        public int IdTipoImpacto { get; set; }
    }
}
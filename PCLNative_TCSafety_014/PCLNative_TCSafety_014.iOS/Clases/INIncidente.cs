using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PCLNative_TCSafety_014.iOS.Clases
{
    class INIncidente
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaOcurrencia { get; set; }
        public string Relato { get; set; }
        public string MedidaControl { get; set; }
        public int IdImpacto { get; set; }
        public int IdPotencial { get; set; }
        public string Lugar { get; set; }
        public int IdArea { get; set; }
        public string Observacion { get; set; }
        public int IdVisibilidadIncidente { get; set; }
        public int IdTipoAnalisis { get; set; }
        public int IdEstadoTipoIncidente { get; set; }
        public int IdSubarea { get; set; }
        public int IdOperacion { get; set; }
        public int IdOperacionUnitaria { get; set; }
    }
}
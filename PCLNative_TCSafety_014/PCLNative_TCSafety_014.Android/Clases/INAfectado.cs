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
    class INAfectado
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public string idEmpresa { get; set; }
        public string Area { get; set; }
        public string Jefedirecto { get; set; }
        public string Antiguedad { get; set; }
        public int IdTipoLesion { get; set; }
        public int DiasPerdidos { get; set; }
        public string ConsecuenciaObservacion { get; set; }
        public string IdIncidente { get; set; }

        public INAfectado(string _nombre, string _apellidos, int _idEmpresa,int _idTipoLesion)
        {

        }
    }
}
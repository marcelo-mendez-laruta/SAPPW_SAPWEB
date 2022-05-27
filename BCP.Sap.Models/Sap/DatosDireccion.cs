using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class DatosDireccion
    {
        private string horaRegistro1;

        public string nro { get; set; }
        public string codLocalidad { get; set; }
        public string tipoDireccion { get; set; }
        public string direccionComprimida { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; }
        public string fechaRegistro { get; set; }
        public string horaRegistro { get; set; } //{ get => horaRegistro1; set => horaRegistro1 = LimpiarDatos.hora(value); }
    }
}

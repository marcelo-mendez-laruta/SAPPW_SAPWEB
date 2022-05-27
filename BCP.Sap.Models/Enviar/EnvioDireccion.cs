using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioDireccionCorrespondencia : EnvioOperacion
    {
        public object cliente { get; set; }
        public ParametrosHost usuario { get; set; }
        public EnvioDireccionCorrespondenciaData data { get; set; }
    }
    public class EnvioDireccionCorrespondenciaData
    {
        public int idDireccion { get; set; }
    }
    public class EnvioConsultaDireccion : EnvioOperacion
    {
        public object cliente { get; set; }
        public ParametrosHost usuario { get; set; }
    }
    public class EnvioOperacionDireccion : EnvioOperacion
    {
        public object cliente { get; set; }
        public ParametrosHost usuario { get; set; }
        public DatosDireccion data { get; set; }
    }
}

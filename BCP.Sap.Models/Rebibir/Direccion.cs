using BCP.Framework.Common;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaDireccion : SapResponse
    {
        public Direccion data { get; set; }
    }
    public class CertificadoDireccion
    {
        public string certificado { get; set; }
    }
    public class Direccion:CertificadoDireccion
    {
        public string nombre { get; set; }
        public int total { get; set; }
        public List<DatosDireccion> direcciones { get; set; }
    }
   
}

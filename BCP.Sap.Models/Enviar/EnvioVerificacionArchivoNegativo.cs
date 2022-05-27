using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.ProcesoApertura
{
    public class EnvioVerificacionEnameChecker : EnvioOperacion
    {
        public EnvioVerificacionEnameCheckerData cliente { get; set; }
    }

    public class EnvioVerificacionEnameCheckerData
    {
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string matriculaUsuario { get; set; }
        public string ipEquipo { get; set; }
        public string guid { get; set; }
    }

    public class EnvioVerificacionArchivoNegativo : EnvioOperacion
    {
        public object cliente { get; set; }
    }
    public class ECANPN : EnvioDocumentoPN
    {
        public readonly bool generarCertificacion = false;
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
    }
    public class ECANPJ : EnvioDocumentoPJ
    {
        public string razonSocial { get; set; }
        public readonly bool generarCertificacion = false;
    }
}

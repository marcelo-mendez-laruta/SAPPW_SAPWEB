using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class ParametrosHost
    {
        public  string usuarioExtra { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public  string teti { get; set; }
        public string ipOrigen { get; set; }
        public  string ipDestino { get; set; }
        public  string puertoInicial { get; set; }
        public  string puertoRespaldo { get; set; }
        public string guid { get; set; }
    }
    public class ParametrosEstaciones
    {
        public string usuarioExtra { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string teti { get; set; }
        public string ipOrigen { get; set; }
        public string ipDestino { get; set; }
        public string puertoInicial { get; set; }
        public string puertoRespaldo { get; set; }
        public string guid { get; set; }
        public string usuarioSLK { get; set; }
        public string contrasenaSLK { get; set; }
        public string terminal { get; set; }
        public string numeroTerminal { get; set; }
    }

    public class AdecuacionConfiguracionEstaciones
    {
        public ParametrosSLK slk { get; set; }
        public ParametrosHost host { get; set; }
        public string message { get; set; }
    }

    public class ParametrosSLK
    {
        public string usuarioSLK { get; set; }
        public string contrasenaSLK { get; set; }
        public string terminal { get; set; }
        public string numeroTerminal { get; set; }
    }
}

using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaGUID : SapResponse
    {
        public dataGUID data { get; set; }
    }
    public class dataGUID: EnvioDocumentoPJ
    {
        public string guid { get; set; }
    }
}

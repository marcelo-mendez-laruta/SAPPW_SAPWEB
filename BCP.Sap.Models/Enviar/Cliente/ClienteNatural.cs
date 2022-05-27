using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar.Cliente
{
    public class EnvioDocumentoPN : EnvioDocumentoPJ
    {

        public readonly string idcComplemento = "00";
    }
    public class EnvioDocumentoCic : EnvioDocumentoPJ
    {
        public string cic { get; set; }
    }
}

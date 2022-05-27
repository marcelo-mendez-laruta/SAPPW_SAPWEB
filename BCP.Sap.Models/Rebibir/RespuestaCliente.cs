using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaCreacionCliente:SapResponse
    {
       public ClienteCIC data { get; set; }
    }
    public class ClienteCIC
    {
        public string cic { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaSegurinet:SapResponse
    {
        public Segurinet data { get; set; }
    }
    public class Segurinet
    {
        public bool conAccesso { get; set; }
        public string usuario { get; set; }
        public string nombre { get; set; }
        public string politicas { get; set; }
    }
}

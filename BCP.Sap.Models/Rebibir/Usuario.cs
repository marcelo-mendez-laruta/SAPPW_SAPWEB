using BCP.Sap.Models.Enviar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaUsuario : SapResponse
    {
        public Usuario data { get; set; }
    }
    public class RespuestaConfigUsuario : SapResponse
    {
        public ParametrosEstaciones data { get; set; }
    }
    public class Usuario
    {
        public string nombre { get; set; }
        public string cargo { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
    }
}

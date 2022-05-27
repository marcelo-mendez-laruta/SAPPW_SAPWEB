using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnviarSegurinetV1:EnvioOperacion
    {
        public string usuarioDominio { get; set; }
    }
    public class EnviarSegurinetV2:EnvioOperacion
    {
        public string usuarioSegurinet { get; set; }
        public string passwordSegurinet { get; set; }
    }
    public class CambiarClaveSegurinet : EnvioOperacion
    {
        public string usuarioSegurinet { get; set; }
        public string passwordSegurinet { get; set; }
        public string nuevoPasswordSegurinet { get; set; }
    }
}

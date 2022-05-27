using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioPeticion : EnvioOperacion
    {
        public EnvioGrupoDominio usuario { get; set; }
    }
}
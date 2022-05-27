using BCP.Sap.Models.Enviar.Producto;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioCreacionCuenta : EnvioOperacion
    {
        public object cliente { get; set; }
        public object cuenta { get; set; }
        public ParametrosHost usuario { get; set; }
    }
    public class EnvioRegistroCreacionCuenta : EnvioOperacion
    {
        public List<Aperturante> aperturante { get; set; }
        public Apertura apertura { get; set; }
        public ParametrosHost usuario { get; set; }
        public ContratoDPF data { get; set; }
    }
    public class EnvioConsultaCuenta : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public Cuenta data { get; set; } 
    }

    public class EnvioConsultaMotivoCuenta : EnvioOperacion
    {
        public Cuenta data { get; set; }
    }

    /*public class EnvioCreacionCuentaCorriente : EnvioOperacion
    {
        public EnvioUsuario usuario { get; set; }
        public EnvioDocumentoPersonaComplemento cliente { get; set; }
        public CuentaCorriente cuenta { get; set; }
    }
    public class EnvioCreacionDPF : EnvioOperacion
    {
        public EnvioUsuario usuario { get; set; }
        public EnvioDocumentoPersonaComplemento cliente { get; set; }
        public DPF cuenta { get; set; }
    }*/
}

using BCP.Sap.Models.Enviar.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioCambioFunNeg:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public FunNeg data { get; set; }
        public EnvioDocumentoPN cliente { get; set; }
    }
   public class FunNeg: ConsultaFunNeg
    {
        public string banca { get; set; }
        public bool generarComprobante { get; set; }
    }

    public class EnvioConsultaFunNeg : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ConsultaFunNeg data { get; set; }
    }
    public class ConsultaFunNeg
    {
        public string funcionarioNegocios { get; set; }
    }
}

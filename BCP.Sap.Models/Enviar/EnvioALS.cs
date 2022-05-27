using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioALSConsultaAlfabetica:EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ALSConsultaAlfabeticaRequestData data { get; set; }
    }
    public class EnvioALSProductoCliente : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public ALSProductoClienteRequestData data { get; set; }
    }
    public class EnvioALSInformeCrediticio : EnvioOperacion
    {
        public ParametrosHost usuario { get; set; }
        public AlsInformeCrediticioRequest data { get; set; }
    }
    public class AlsInformeCrediticioRequest
    {
        public string codigoCredito { get; set; }
        public int item { get; set; }
        public string nombre { get; set; }
    }
    public class ALSProductoClienteRequestData
    {
        public string customer { get; set; }
    }
    public class ALSConsultaAlfabeticaRequestData
    {
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }
        public string tipoConsulta { get; set; }
    }
}

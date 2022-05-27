using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioTarjetaBD:EnvioOperacionTarjeta
    {
        public EnvioTarjetaBDData data { get; set; }
    }
    public class EnvioTarjetaBDData
    {
        public string tarjeta { get; set; }
        public string ahorro1 { get; set; }
        public string ahorro2 { get; set; }
        public string corriente1 { get; set; }
        public string corriente2 { get; set; }
        public string direccion { get; set; }
        public string tipoIDC { get; set; }
        public string idc { get; set; }
        public string nombreCompleto { get; set; }
        public string situacion { get; set; }
        public string indMenor { get; set; }
        public string indBancaExclusiva { get; set; }
        public string codTipoOperacion { get; set; }
        public string flagActualizarDatos { get; set; }
        public string matricula { get; set; }
        public string campo2 { get; set; }
    }
}

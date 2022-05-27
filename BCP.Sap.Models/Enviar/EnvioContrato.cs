using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Enviar
{
    public class EnvioContrato : EnvioOperacion
    {
        public EnvioContratoData data { get; set; }
    }

    public class ImpresionContratoFirmante
    {
        public string idcNumero { get; set; }
        public string idcTipo { get; set; }
        public string idcExtension { get; set; }
        public string idcComplemento { get; set; }

        public string paternoRazonSocial { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }

        public string tipoCliente { get; set; }
        public string direccionTexto { get; set; }
        public string localidad { get; set; }
        public string telefono { get; set; }
        public string banca { get; set; }
    }
    public class ImpresionContratoProducto
    {
        public string producto { get; set; }
        public string moneda { get; set; }
        public string nombreCuenta { get; set; }
        public string nroCuenta { get; set; }
        public decimal monto { get; set; }
        public string clave { get; set; }
        public string plazo { get; set; }
        public string unidadTiempo { get; set; }
        public string fechaApertura { get; set; }
        public string renovacion { get; set; }
        public string pagoIntereses { get; set; }
        public string otraCuenta { get; set; }
        public string tasa { get; set; }
        public string penalidad { get; set; }
    }
    public class ImpresionContratoDB
    {
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string fechaApertura { get; set; }
        public string nroTarjeta { get; set; }
        public string tipoOperacion { get; set; }
        public bool original { get; set; }
        public bool copia { get; set; }
        public bool apertura { get; set; }
    }

    public class EnvioContratoData
    {
        public ImpresionContratoFirmante firmante { get; set; }
        public List<ImpresionContratoProducto> productos { get; set; }
        public ImpresionContratoDB contrato { get; set; }
    }
}

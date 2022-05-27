using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RecibirALSConsultaAlfabetica: SapResponse
    {
       public AlsConsultaAlfabeticaResponseData data { get; set; }
    }
    public class RecibirALSProductoCliente : SapResponse
    {
        public ALSProductoClienteResponseData data { get; set; }
    }
    public class RecibirALSInformeCrediticio : SapResponse
    {
        public AlsInformeCrediticioResponse data { get; set; }
    }
    public class AlsInformeCrediticioResponse
    {
       
        public List<AlsInformeCrediticioItem> rows { get; set; }
        public int total { get; set; }
    }

    public class AlsInformeCrediticioItem
    {
        public string nombres { get; set; }
        public string customer { get; set; }
        public string direccionDomicilio { get; set; }
        public string direccionTrabajo { get; set; }
        public string ciudad { get; set; }
        public string codigoPostal { get; set; }
        public string provincia { get; set; }
        public string codigoCiudad { get; set; }
        public string telefono { get; set; }
        public string cargoAutomatico { get; set; }
        public string tipoProducto { get; set; }
        public string sectorista { get; set; }
        public string callCode { get; set; }
        public string bloqueo { get; set; }
        public string deudaTotalActual { get; set; }
        public string importeTotalOriginal { get; set; }
        public string importeSolicitadoPrestamo { get; set; }
        public string importeInteresMonetario { get; set; }
        public string comisionesPagarPeriodo { get; set; }
        public string importePrincipalDeuda { get; set; }
        public string interesesDevengados { get; set; }
        public string pagoMontoRegularCuota { get; set; }
        public string montoPagarProximaCuota { get; set; }
        public string montoPorCuotaVencida { get; set; }
        public string numeroMesesPrestamo { get; set; }
        public string numeroPagosRealizado { get; set; }
        public string numeroCuotasPendientes { get; set; }
        public string numeroMesesPostergacion { get; set; }
        public string indicadorRenovacionesPlazo { get; set; }
        public string codigoGarantia { get; set; }
        public string tipoInteres { get; set; }
        public string estadoCuenta { get; set; }

        public string fechaApertura { get; set; }
        public string fechaVencimiento { get; set; }
        public string fechaPagoSiguienteCuota { get; set; }
        public string fechaCuotaImpagaAntigua { get; set; }
        public string fechaUltimaTransaccion { get; set; }
        public string fechaUltimoCambioSaldo { get; set; }
        public string fechacierrePrestamo { get; set; }

        public string diaRetraso01 { get; set; }
        public string diaRetraso02 { get; set; }
        public string diaRetraso03 { get; set; }
        public string diaRetraso04 { get; set; }
        public string diaRetraso05 { get; set; }
        public string diaRetraso06 { get; set; }

        public string numeroRetraso01 { get; set; }
        public string numeroRetraso02 { get; set; }
        public string numeroRetraso03 { get; set; }
        public string numeroRetraso04 { get; set; }
        public string numeroRetraso05 { get; set; }
        public string numeroRetraso06 { get; set; }
    }
    public class ALSProductoClienteResponseData
    {
        public string customer { get; set; }
        public List<AlsProductoClienteItem> rows { get; set; }
        public int total { get; set; }
    }
    public class AlsProductoClienteItem
    {
        public string codigoCredito { get; set; }
        public string tipoRelacion { get; set; }
        public string tipoProducto { get; set; }
        public string saldoActual { get; set; }
        public string fechaVencimiento { get; set; }
        public string nombreCorto { get; set; }
    }
    public class AlsConsultaAlfabeticaResponseData
    {
        public List<AlsItem> rows { get; set; }
        public int total { get; set; }
    }

    public class AlsItem
    {
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string customer { get; set; }
        public string fechaNacimiento { get; set; }
    }

}

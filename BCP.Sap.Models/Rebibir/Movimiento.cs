using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir
{
    public class RespuestaConsultaMotivoCuenta : SapResponse
    {
        public RespuestaConsultaMotivoCuentaData data { get; set; }
    }
    public class RespuestaConsultaMotivoCuentaData
    {
        public string motivoCuenta { get; set; }
    }
    public class RespuestaMovimientoCuentaCorriente: SapResponse
    {
        public MovimientoCuentaCorriente data { get; set; }
    }
    public class RespuestaMovimientoCajaAhorro : SapResponse
    {
        public MovimientoCajaAhorro data { get; set; }
    }

    public class MovimientoCajaAhorro:Movimiento
    {
        public string desTipoCuenta { get; set; }
        public string clienteExclusivo { get; set; }
        public string desUltimoTipoBloqueo { get; set; }
        public string fechaDesbloqueo { get; set; }
        public string depositoInicial { get; set; }
        public string plazo { get; set; }
        public string fechaVencimiento { get; set; }
        public string saldoDisponible { get; set; }
        public string seguro { get; set; }
        
    }
    public class Movimiento
    {
        public string fechaApertura { get; set; }//super
        public string agencia { get; set; }//
        public string nombreTitular { get; set; }//
        public string idc { get; set; }//
        public string direccion { get; set; }//
        public string desEstado { get; set; }//
        public string tarjeta { get => LimpiarDatos.credimas(tarjeta1); set => tarjeta1 = string.Join("", value.Split('-')); }
        private string tarjeta1;
        public string saldoPromedio { get; set; }//
        public string saldoContable { get; set; }//
        public DetallesMovimientos movimientos { get; set; }
    }
    public class MovimientoCuentaCorriente : Movimiento
    {
        public string banca { get; set; }
        public string segmento { get; set; }
        public string fechaUltimoEstado { get; set; }
        public string indicadorVip { get; set; }
        public string chequeDevuelto { get; set; }
        public string saldoLiquido { get; set; }
        public string retenciones { get; set; }
        public string chequesTramite { get; set; }
        public string consultasTramite { get; set; }
        public string liquidezCheques { get; set; }
        public string lineaCheques { get; set; }
        public string lineaInterna { get; set; }
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public string saldoFP { get; set; }
        public string funcionarioNegocios { get; set; }
        public string clasificacion { get; set; }
        public string actividad { get; set; }
    }
    public class DetallesMovimientos
    {
        public int nroMovimientos { get; set; }
        public List<MovimientosProducto> rows { get; set; }
    }
    public class MovimientosProducto
    {
        public string fecha { get; set; }
        public string valuta { get; set; }
        public string referencia { get; set; }
        public string importe { get; set; }
        public string sucursalAgencia { get; set; }
        public string usuario { get; set; }
        public string nroTransaccion { get; set; }
    }
}

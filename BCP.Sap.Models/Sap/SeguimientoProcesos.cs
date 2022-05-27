using BCP.Sap.Models.Configuracion;
using BCP.Sap.Models.Enviar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Sap
{
    public class SeguimientoProcesos
    {
        public string nombreProceso { get; set; }
        public bool registro { get; set; }
        public string tipoPersona { get; set; }       
        public int nMancomuna { get; set; }
        public List<DatosCliente> detalleCliente { get; set; }
        public List<string> listaSeguros { get; set; }
    }
    public class TipoCliente
    {
        public string tipoPersona { get; set; }
        public string cantidadPersonas { get; set; }
    }
    public class DatosCliente
    {
        public string idC { get; set; }
        public string idT { get; set; }
        public bool AN { get; set; }
        public bool EC { get; set; }
        public PersonaNatural pn { get; set; }
        public PersonaJuridica pj { get; set; }
        public string tarjeta { get; set; }
        public string tarjetaAnterior { get; set; }
        public string situacionTarjeta { get; set; }
        public string situacionBloqueo { get; set; }
        public string localidadDescripcion { get; set; }
        public string estadoCivilDescripcion { get; set; }
        public bool filtroAN { get; set; }
        public bool flujo { get; set; }
        public string codigoOperacion { get; set; }
        public List<AperturaCuenta> detalleCuenta { get; set; }
    }
    public class AperturaCuenta
    {
        public ProductoModalidad cuentaProducto { get; set; }
        public DetalleCuenta detalleCuenta { get; set; }
    }

    public class ProductoModalidad
    {
        public string producto { get; set; }
        public string idModalidad { get; set; }
        public string codigoProducto { get; set; }
        public string codigoModalidad { get; set; }
    }


    public class DetalleCuenta : Cuenta
    {
        public bool apertura { get; set; }
        public bool asignadoTarjeta { get; set; }
        public string producto { get; set; }
        public string indModalidad { get; set; }
        public string moneda { get; set; }
        public string nombreCuenta { get; set; }
        public string monto { get; set; }
        public string motivoCuenta { get; set; }
        public string fechaApertura { get; set; }
        public string plazo { get; set; }
        public bool tiempoMeses { get; set; }
        public string renovacion { get; set; }
        public string modoPagoIntereses { get; set; }
        public string conCuenta { get; set; }
        public string dpfCuentaAfiliada { get; set; } 
        public string tasa { get; set; }
        public string penalidad { get; set; }
        public bool custodia { get; set; }
        public string dpfTipoCuenta { get; set; }
        public bool segundoPiso { get; set; }
    }
    /*class salida
    {
        private Dictionary<string, object> c = new Dictionary<string, object> 
        {
            "AHS"="dsdsd"
        };
    }*/
}

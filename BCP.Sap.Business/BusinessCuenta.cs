using BCP.Framework.Common;
using BCP.Sap.Models.Rebibir.Tarjeta;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCP.Sap.Business
{
    public class BusinessCuenta
    {
        public static string managerEstado(string estado)
        {
            string resultado = "";
            try
            {
                switch (estado.Trim())
                {
                    case "0":
                        resultado = "CONFIRMADA";
                        break;
                    case "1":
                        resultado = "PENDIENTE";
                        break;
                    case "2":
                        resultado = "BLOQUEADA";
                        break;
                    case "3":
                        resultado = "ELIMINADA";
                        break;
                }
            }
            catch (Exception)
            {

            }
            return resultado;
        }
        public static ConsultaTarjeta limpiarCuentas(ConsultaTarjeta datos)
        {
            datos.idcCompleto = datos.tipoIDC+datos.idc;
            string ctsp = datos.indTipoTarjeta.Trim().Equals("N")?"NO":"SI";
            datos.indTipoTarjeta = ctsp;
            string cobro = "0";
            try {
                switch (datos.flagCobro)
                {
                    case "N":
                        cobro = "3";
                        break;
                    case "S":
                        cobro = "1";
                        break;
                    case "V":
                        cobro = "2";
                        break;
            }}
        catch(Exception){}
            datos.flagCobro = cobro;
            if(datos.flagCta1)
            {
                datos.ahorro1 = LimpiarDatos.formatoCuenta(datos.ahorro1);
                datos.estadoCta1 = managerEstado(datos.estadoCta1);
            }
            else
            {
                datos.ahorro1 = "";
                datos.estadoCta1 = "";
            }
            if (datos.flagCta2)
            {
                datos.ahorro2 = LimpiarDatos.formatoCuenta(datos.ahorro2);
                datos.estadoCta2 = managerEstado(datos.estadoCta2);
            }
            else
            {
                datos.ahorro2 = "";
                datos.estadoCta2 = "";
            }
            if (datos.flagCta3)
            {
                datos.corriente1 = LimpiarDatos.formatoCuenta(datos.corriente1);
                datos.estadoCta3 = managerEstado(datos.estadoCta3);
            }
            else
            {
                datos.corriente1 = "";
                datos.estadoCta3 = "";
            }
            if (datos.flagCta4)
            {
                datos.corriente2 = LimpiarDatos.formatoCuenta(datos.corriente2);
                datos.estadoCta4 = managerEstado(datos.estadoCta4);
            }
            else
            {
                datos.corriente2 = "";
                datos.estadoCta4 = "";
            }
            return datos;
        }
    }
}

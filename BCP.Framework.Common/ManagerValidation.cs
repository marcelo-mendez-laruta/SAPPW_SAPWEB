using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BCP.Framework.Common
{
    public class ManagerValidation
    {
        public static bool validarSucursalAgencia(string sucursal,string agencia)
        {
            bool exito = false;
            try
            {
                if (!string.IsNullOrEmpty(sucursal) && !string.IsNullOrEmpty(agencia))
                {
                    if (sucursal.Length == 3 && agencia.Length == 3)
                    {
                        if (sucursal.Equals("000")){ }
                        if (isNumeric(sucursal) && isNumeric(agencia))
                        {
                            int aux = int.Parse(sucursal[0].ToString());
                            if (aux>0 && sucursal.Substring(1, 2).Equals("01"))
                            {
                                exito = agencia[0].Equals(sucursal[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            return exito;
        }
        public static bool isAlfa(string cadena)
        {
            Regex regla = new Regex("[A-Z]");
            return regla.IsMatch(cadena);
        }
        public static bool isNumeric(string cadena)
        {
            long result;
            return long.TryParse(cadena, out result);
        }
        public static string[] obtenerIDC(string id, string tId)
        {
            string idNU = "?";
            string idCE = "?";
            bool[] validar = new bool[2];
            switch (tId)
            {
                case "Q":
                    if (id.Length > 2)
                    {
                        id = id.PadLeft(10, '0')+" ";
                        validar[0] = isNumeric(id.Substring(0, 8));
                        validar[1] = isAlfa(id.Substring(8,3).Trim());//validar extension
                    }
                    break;
                case "P":
                    if (id.Length > 2)
                    {
                        id = id.PadLeft(11, '0');
                        validar[0] = isNumeric(id.Substring(0,9));
                        validar[1] = true; //id.Substring(9,2);//validar pais
                    }
                    break;
                case "S":
                    if (id.Length > 1)
                    {
                        id = id.PadLeft(11, '0');
                        validar[0] = isNumeric(id.Substring(0, 10));
                        validar[1] = isAlfa(id.Substring(10,1));
                    }
                    break;
                case "R":
                    if (id.Length > 1)
                    {
                        if (id.Length < 9)
                        {
                            id = id.PadLeft(8,'0')+("").PadLeft(3,' ');
                        }
                        else
                        {
                            id = id.PadLeft(11, '0');
                        }
                        validar[0]= isNumeric(id.Trim());
                        validar[1] = true;
                    }
                    break;
                case "J":
                case "K":
                case "V":
                case "Y":
                case "Z":
                    if (id.Length > 1)
                    {
                        if (id.Length > 8)
                        {
                            id = id.Substring(0, 8) + ("").PadLeft(3, ' ');
                        }
                        else
                        {
                            id = id.PadLeft(8,'0') + ("").PadLeft(3, ' ');
                        }
                        validar[0] = isNumeric(id.Trim());
                        validar[1] = true;
                    }
                    break;
                case "W":
                    if (id.Length>4)
                    {
                        id = id.PadLeft(11, '0');
                        validar[1] = true;// id.Substring(0, 8);
                        validar[0] = isNumeric(id.Substring(8, 3));//validar codigo fiscal
                    }
                    break;
                default:
                    break;
            }
            if (!validar.Contains(false))
            {
                idNU = id.Substring(0, 8);
                idCE = id.Substring(8, 3).Trim();
            }
            return new string[] { idNU, idCE };
            //var idcN = (from t in id where char.IsDigit(t) select t).ToArray();
            //var idcE = id.Where(c => Regex.Match(new String(c, 1), @"[a-zA-Z]").Success).ToArray();*/
        }
        public static bool validarProceoApertura(string nombreProceso)
        {
            nombreProceso = nombreProceso.Trim();
            if (nombreProceso.Equals("Apertura") || nombreProceso.Equals("Ingreso a Custodia DPF") || nombreProceso.Equals("Mantenimiento de Ahorros"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string validarSituacionTarjeta(string situacion)
        {
            string response = string.Empty;
            try
            {
                switch (situacion)
                {
                    case "00":
                        response = "CONFIRMADA";
                        break;
                    case "01":
                        response = "SIN CONFIRMAR";
                        break;
                    case "02":
                        response = "POR ASIGNAR";
                        break;
                    case "03":
                        response = "EXTRAVIADA";
                        break;
                    case "04":
                        response = "DETERIORADA";
                        break;
                    case "05":
                        response = "OLVIDO DE CLAVE";
                        break;
                    case "06":
                        response = "BLOQUEO INTERNO";
                        break;
                    case "07":
                        response = "TARJETA EN TRAMITE";
                        break;
                    case "08":
                        response = "ENTREGADA";
                        break;
                    case "09":
                        response = "BLOQ.INSPECTORADO";
                        break;
                    case "10":
                        response = "CANJE TELEBANCO";
                        break;
                    case "11":
                        response = "RETENIDA EN CAJERO";
                        break;
                    case "12":
                        response = "BLOQUEO POR VENCIMIENTO";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
            return response;
        }
        public static string validarOperacion(string nombreProceso,string codigoProceso)
        {
            string proceso = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(codigoProceso))
                {
                    codigoProceso = "NA";
                }
                /*if(nombreProceso.ToUpper().Equals("APERTURA")&& codigoProceso.Equals("MO"))
                {
                    codigoProceso = "AF";
                }*/
                switch (codigoProceso.ToUpper())
                {
                    case "CO":
                        proceso = "CONSULTA DE SERVICIOS";
                        break;
                    case "MO":
                        proceso = "MODIF. DE SERVICIOS";
                        break;
                    case "AP":
                        proceso = "ENTREGA POR APERTURA";
                        break;
                    case "AF":
                        proceso = "ENTREGA POR AFILIACION";
                        break;
                    case "TT":
                    case "CA":
                        proceso = "ENTREGA POR CAMBIO";
                        break;
                    case "CR":
                        proceso = "CAMBIO RAPIDO";
                        break;
                    case "EN":
                        proceso = "ENTREGA DIFERIDA DE TARJETA";
                        break;
                    case "EE":
                        proceso = "ELIMINACION DE ENTREGA";
                        break;
                    case "EV":
                            proceso = "ENTREGA VISTO BUENO";
                        break;
                    case "BL":
                        proceso = "BLOQUEO INTERNO";
                        break;
                    case "EX":
                        proceso = "BANCA EXCLUSIVA";
                        break;
                    default:
                        proceso = nombreProceso.ToUpper();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return proceso;
        } 
        public static bool validarTipoClienteApertura(string tipoCliente,string[] politicas,ref string mensaje, ref bool modificarTipoCliente)
        {
            bool response = false;
            try
            {
                switch (tipoCliente)
                {
                    case "CP":
                    case "XC":
                        modificarTipoCliente = true;
                        mensaje = "CLIENTE DEBE REGULARIZAR SU SITUACION";
                        break;
                    case "NC":
                        if (politicas.Contains("SAPP_BO_PRO_APE_NO_CLI"))
                        {
                            modificarTipoCliente=true;
                        }
                        else 
                        {
                            mensaje = "NO CLIENTE (NC).      *MIENTRAS NO REGULARICE SU CONDICION EN DBP/DBE*";
                        }
                        break;
                    /*case "CN":
                        if (politicas.Contains("SAPP_BO_PRO_APE_CLI_NEG"))
                        {
                            response = true;
                        }
                        else
                        {
                            mensaje = "CLIENTE NEGATIVO (CN).      *SOLICITE EL ACCESO CORRESPONDIENTE*";
                        }
                        break;*///permite la apertura de algunas cuentas
                    case "CD":
                        mensaje = "CLIENTE DUPLICADO";
                        break;
                    case "CF":
                        mensaje = "CLIENTE FALLECIDO";
                        break;
                    default:
                        response = true;
                        break;
                }
            }
            catch(Exception)
            {
                mensaje = "ERROR AL VERIFICAR LA SITUACION DEL CLIENTE";
            }
            return response;
        }
    }
}

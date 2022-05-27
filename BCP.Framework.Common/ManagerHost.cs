using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Framework.Common
{
    public class ManagerHost
    {
        public static string CuentaComercial(string cuentaExtra, string moneda)
        {
            string cuenta = string.Empty;
            try
            {
                if (!cuentaExtra.Substring(0, 4).Equals("T999"))
                {
                    string sucursal = cuentaExtra.Substring(0, 3);
                    string tipoProducto = cuentaExtra.Substring(3, 1);
                    string nroCuenta = (int.Parse(cuentaExtra.Substring(4))).ToString();
                    string Nmoneda = moneda.Equals("BOLIVIANO") ? "MNA" : "USD";
                    if (tipoProducto.Equals("A") || tipoProducto.Equals("H"))
                    {
                        cuenta = sucursal + nroCuenta.PadLeft(8, '0');
                        cuenta = ManagerSystematic.CrearDigitodeControl(cuenta, "AHS", Nmoneda);
                        cuenta = ManagerSystematic.FormatCuenta(cuenta, "AHS");
                    }
                    else if (tipoProducto.Equals("C"))
                    {
                        cuenta = sucursal + nroCuenta.PadLeft(7, '0');
                        cuenta = ManagerSystematic.CrearDigitodeControl(cuenta, "CCS", Nmoneda);
                        cuenta = ManagerSystematic.FormatCuenta(cuenta, "CCS");
                    }
                    else if (tipoProducto.Equals("L") || tipoProducto.Equals("Z"))
                    {
                        cuenta = sucursal + nroCuenta.PadLeft(14, '0');
                        cuenta = ManagerSystematic.CrearDigitodeControl(cuenta, "DOR", Nmoneda);
                        cuenta = ManagerSystematic.FormatCuenta(cuenta, "DOR");
                    }
                }
            }
            catch (Exception ex)
            {
                cuenta = string.Empty;
            }
            return cuenta;
        }
        public static int IdPegarCuentas(string cuenta)
        {
            int id = 0;
            try
            {
                if (!string.IsNullOrEmpty(cuenta))
                {
                    cuenta = cuenta.Trim().Replace("-","");
                    char moneda = cuenta[cuenta.Length - 3];
                    if (cuenta.Length == 14)
                    {
                        if (moneda.Equals('3'))
                        {
                            id = 1;
                        }
                        else if (moneda.Equals('2'))
                        {
                            id = 2;
                        }
                    }
                    else if (cuenta.Length == 13)
                    {
                        if (moneda.Equals('3'))
                        {
                            id = 3;
                        }
                        else if (moneda.Equals('2'))
                        {
                            id = 4;
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                id =  0;
            }
            return id;
        }
        public static string cambiarformatoAHSPJ(string producto)
        {
            string response = string.Empty;
            try
            {
                switch (producto)
                {
                    case "AHT":
                        response = "AHL";
                        break;
                    case "AHC":
                        response = "AHG";
                        break;
                    case "AHO":
                        response = "AHP";
                        break;
                    case "DOR1":
                        response = "DPO";
                        break;
                    case "DOR2":
                        response = "DPR";
                        break;
                    default:
                        response = producto;
                        break;
                }
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
        public static string formatoAHSAP(string producto,string subcodigo)
        {
            string response = string.Empty;
            try
            {
                if (producto.Equals("AHS"))
                {
                    response = "AH";
                    switch (subcodigo.ToUpper())
                    {
                        case "LIBRE":
                            response = response + "L";
                            break;
                        case "GANANC":
                            response = response + "G";
                            break;
                        case "PROMOC":
                            response = response + "P";
                            break;
                        case "SUELDO":
                            response = response + "A";
                            break;
                        default:
                            response = response + "L";
                            break;
                    }
                }
                else
                {
                    response = producto;
                }
            }
            catch (Exception) { response = producto; }
            return response;
        }
        public static string formatoDPFAP(string producto,string tipo)
        {
            string resultado =producto;
            try
            {
                if (producto.Substring(0, 3).Equals("DOR"))
                {
                    if (tipo.Equals("0"))
                    {
                        resultado = "DPO";
                    }
                    else
                    {
                        resultado = "DPR";
                    }
                }
            }
            catch (Exception) { }
            return resultado;
        }
        public static string ex(string valor)
        {
            string respuesta = "NO";
            try
            {
                switch (valor[valor.Length - 1])
                {
                    case 'E':
                        respuesta = "BEX";
                        break;
                    case 'V':
                        respuesta = "VIP";
                        break;
                }
            }
            catch (Exception) { }
            return respuesta;
        }
        public static string situacionTarjeta(string valor)
        {
            string resultado = "SIN CONFIRMAR";
            try
            {
                switch (valor)
                {
                    case "00":
                    resultado= "CONFIRMADA";
                    break;
                    case "01":
                        resultado = "SIN CONFIRMAR";
                        break;
                    case "02":
                        resultado = "POR ASIGNAR";
                        break;
                    case "03":
                        resultado = "EXTRAVIADA";
                        break;
                    case "04":
                        resultado = "DETERIORADA";
                        break;
                    case "05":
                        resultado = "OLVIDO DE CLAVE";
                        break;
                    case "06":
                        resultado = "BLOQUEO INTERNO";
                        break;
                    case "07":
                        resultado = "TARJETA EN TRAMITE";
                        break;
                    case "08":
                        resultado = "ENTREGADA";
                        break;
                    case "09":
                        resultado = "BLOQ.INSPECTORADO";
                        break;
                    case "10":
                        resultado = "CANJE TELEBANCO";
                        break;
                    case "11":
                        resultado = "RETENIDA EN CAJERO";
                        break;
                    case "12":
                        resultado = "BLOQUEO POR VENCIMIENTO";
                        break;
                }
                
            }
            catch (Exception) { }
            return resultado;
        }
    }
}

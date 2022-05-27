using BCP.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.Framework.Common
{
    public class LimpiarDatos
    {
        public static int indiceCuenta(string cuenta)
        {
            int indice = 0;
            cuenta = cuenta.Replace("-", "");
            char moneda = cuenta[cuenta.Length - 3];
            if (cuenta.Length == 14)
            {
                if (moneda.Equals('3'))
                {
                    indice = 0;
                }
                else
                {
                    indice = 1;
                }
            }
            else
            {
                if (moneda.Equals('3'))
                {
                    indice = 2;
                }
                else
                {
                    indice = 3;
                }
            }
            return indice;
        }
        public static string limpiarCuenta(string cuenta)
        {
            string respuesta = "";
            try
            {
                cuenta = cuenta.Replace("-", "");
                long a = long.Parse(cuenta);
                if (a > 0)
                {
                    respuesta = cuenta;
                }
            }
            catch (Exception ex)
            {

            }
            return respuesta;
        }
        public static string EnDecryp(string valor, bool encriptar)
        {
            return SegCrypt.EncryptDecrypt(encriptar, valor);
        }
        public static string armarCuenta(string cuenta, string moneda)
        {
            string Ncuenta = "";
            string sucursal = cuenta.Substring(0, 3);
            int numero = int.Parse(cuenta.Substring(4));
            int Nmoneda = 2;
            if (moneda[0].Equals('B'))
            {
                Nmoneda = 3;
            }
            Ncuenta = sucursal + "-" + numero.ToString() + "-" + Nmoneda.ToString() + "-00";
            return Ncuenta;
        }
        public static string formatoEstadoCuenta(string valor)
        {
            string nuevo = "";
            if (valor.Equals("0"))
            {
                nuevo = "CONFIRMADA";
            }
            return nuevo;
        }
        public static string formatoCuenta(string valor)
        {
            string nuevo = "";
            if (valor != null)
            {
                if (valor.Length > 6)
                {
                    int n = valor.Length - 3;
                    nuevo = valor.Substring(0, 3) + "-" + valor.Substring(3, n - 3) + "-" + valor.Substring(n, 1) + "-" + valor.Substring(n + 1, 2);
                }
            }
            return nuevo;

        }
        public static string relimpiarNumeros(string dato)
        {
            try
            {
                if (string.IsNullOrEmpty(dato))
                {
                    dato=string.Empty;
                }
                return dato;
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
        public static string limpiarMatricula(string dato)
        {
            if (string.IsNullOrEmpty(dato))
            {
                return "";
            }
            string[] nombre = dato.Split('\\');
            string matricula;
            if (nombre.Length == 2)
            {
                matricula = nombre[1];
            }
            else
            {
                matricula = nombre[0];
            }
            return matricula;
        }
        public static string espacioFinal(string nombre)
        {
            while (nombre[nombre.Length - 1] == ' ')
            {
                nombre = nombre.Substring(0, nombre.Length - 1);
            }
            return nombre;
        }
        public static DateTime ParsearFecha(string fecha)
        {
            int dia;
            int mes;
            int anio;
            try
            {
                if (fecha == null)
                {
                    return new DateTime(DateTime.Now.Year-18, 1, 1);
                }
                else
                {
                    if (fecha.Length == 8)
                    {
                        dia = int.Parse(fecha.Substring(0, 2));
                        mes = int.Parse(fecha.Substring(2, 2));
                        anio = int.Parse(fecha.Substring(4, 4));
                    }
                    else
                    {
                        dia = int.Parse(fecha.Substring(0, 2));
                        mes = int.Parse(fecha.Substring(3, 2));
                        anio = int.Parse(fecha.Substring(6, 4));
                    }
                    if (DateTime.Now.Year-anio>100)
                    {
                        anio = DateTime.Now.Year - 18;
                    }
                    DateTime nacimiento = new DateTime(anio, mes, dia);
                    return nacimiento;
                }
            }
            catch (Exception)
            {
                return new DateTime(DateTime.Now.Year - 18, 1, 1);
            }
        }
        public static string edad(string fecha)
        {
            DateTime nacimiento = ParsearFecha(fecha);
            int fedad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            return fedad.ToString();
        }

        public static string validarFechaUltimaActualizacion(string fecha)
        {
            string response = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                {
                    int diferenciaDias = DiferenciaenDias(fecha);
                    if (diferenciaDias == 0) 
                    {
                        response = "AC";
                    }
                    else if (diferenciaDias < 120)
                    {
                        response = "NA";
                    }
                    else
                    {
                        DateTime nacimiento = ParsearFecha(fecha);
                        if (nacimiento.Year <= 1901)
                        {
                            response = "NC";
                        }
                        else
                        {
                            response = "AC";
                        }
                    }
                }
                else
                {
                    response = "NC";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static int DiferenciaenDias(string fecha)
        {
            int diferenciaenDias = 0;
            try
            {
                DateTime fechaParseada = ParsearFecha(fecha);
                diferenciaenDias = (DateTime.Now.Date - fechaParseada).Days;
            }
            catch(Exception ex)
            {

            }
            return diferenciaenDias;
        }
        public static string credimas(string valor)
        {
            string nuevo = "";
            if (!string.IsNullOrEmpty(valor))
            {
                if (valor.Length >= 16)
                {
                    if (valor.Split('-').Length == 4)
                    {
                        return valor;
                    }
                    else if (valor.Length == 16)
                    {
                        int n = valor.Length / 4;
                        for (int i = 0; i < n; i++)
                        {
                            nuevo += valor.Substring((i * 4), 4) + "-";
                        }
                        return nuevo.Substring(0, nuevo.Length - 1);
                    }
                }
            }
            return "";
        }
        public static string formatoFecha(string fecha)
        {
            string modificado = "";
            if (fecha != null)
            {
                if (!fecha.Equals(""))
                {
                    if (fecha.Length == 8)
                    {
                        modificado = fecha.Substring(0, 2) + "/" + fecha.Substring(2, 2) + "/" + fecha.Substring(4, 4);
                    }
                    else if (fecha.Length > 8)
                    {
                        modificado = fecha;
                    }
                    else
                    {
                        modificado = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
            }
            return modificado;
        }

        public static bool combinarDatos<T>(T target, T modificado)
        {
            bool resultado = false;
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(modificado, null);
                if (value != null)
                {
                    if (!resultado)
                    {
                        if (prop.GetValue(target, null) != value)
                        {
                            resultado = true;
                        }
                    }
                    prop.SetValue(target, value, null);
                }
            }
            return resultado;
        }
        public static T limpiarNulo<T>(T salida)
        {
            Dictionary<string, object> limpiar = ManagerJson.DeserializarObjecto<Dictionary<string, object>>(ManagerJson.SerializarObjecto(salida));
            foreach (string apoyo in limpiar.Keys.ToList())
            {
                if (limpiar[apoyo] == null)
                {
                    limpiar[apoyo] = "";
                }
            }
            salida = ManagerJson.DeserializarObjecto<T>(ManagerJson.SerializarObjecto(limpiar));
            return salida;
        }
        public static string hora(string valor)
        {
            string dato = "";
            while (valor.Length < 6)
            {
                valor = "0" + valor;
            }
            dato = valor.Substring(0, 2) + ":" + valor.Substring(2, 2) + ":" + valor.Substring(4, 2);
            return dato;
        }       
        
    }
}

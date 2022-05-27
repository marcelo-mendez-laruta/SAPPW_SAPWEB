using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Common
{
    public static class ManagerSystematic
    {
        public static string modalidadDPFBD(string producto,string indicadorModalidad,bool indicadorCustodia,string conCuenta,string cuentaAfiliada)
        {
            string response = string.Empty;
            try
            {
                string a = "";
                string d = indicadorModalidad;
                string e = !indicadorCustodia ? "" : ",CUS";
                switch (producto)
                {
                    case "DOR":
                        if (conCuenta.Equals("1"))
                        {
                            a = cuentaAfiliada.Replace("-", "");
                        }
                        else
                        {
                            a = " ";
                        }
                        break;
                    case "DNI":
                        if (conCuenta.Equals("1"))
                        {
                            a = cuentaAfiliada.Replace("-", "");
                        }
                        else
                        {
                            a = " ";
                        }
                        break;
                }
                response = a + "*" + d + e + "*";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static string indicadorDPF(string producto, string indicadorModalidad,bool indicadorCustodia, string plazoManual,string conCuenta,string cuentaAfiliada,string tipoCuentaDpf,string tasaNominal)
        {
            string response = string.Empty;
            try 
            {
                string[] dpf = new string[6];
                #region swamp
                string b = "";
                string c = "";                
                string f = plazoManual;
                string g = DateTime.Now.ToString("dd/MM/yyyy");
                string h = "";
                producto = producto.Substring(0, 3);
                switch (producto)
                {
                    case "DOR":
                        if (conCuenta.Equals("1"))
                        {
                            b = "RA";
                            c = "AC";
                        }
                        else
                        {
                            b = "RA";
                            c = tipoCuentaDpf.Equals("0") ? "CA" : "AC";
                        }
                        h = "*";
                        break;
                    case "DNI":
                        if (conCuenta.Equals("1"))
                        {
                            b = "RA";
                            c = "AC";
                        }
                        else
                        {
                            b = "RA";
                            c = tipoCuentaDpf.Equals("0") ? "CA" : "AC";
                        }
                        h = tasaNominal;
                        break;
                }
                dpf[0] = modalidadDPFBD(producto,indicadorModalidad,indicadorCustodia,conCuenta,cuentaAfiliada);
                dpf[1] = b;
                dpf[2] = c;
                dpf[3] = f;
                dpf[4] = g;
                dpf[5] = h;
                #endregion
                response = string.Join(';', dpf);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
            
        }
        public static string productoSwamp(string producto, string subproducto,string tipoPersona,bool apertura)
        {
            string response = producto;
            try
            {
                if (producto[0].Equals('A'))
                {
                    if (tipoPersona.Equals("N") && apertura)
                    {
                        response = "AHS";
                    }
                    else
                    {
                        response = "AHT";
                    }
                }
                else if (producto.Equals("DOR"))
                {
                    if (subproducto.Equals("DPO"))
                    {
                        response = "DOR1";
                    }
                    else if(subproducto.Equals("DPR"))
                    {
                        response = "DOR2";
                    }
                    else
                    {
                        response = subproducto;
                    }
                }
                else if (producto.Equals("CUE"))
                {
                    response = "DNI";
                }
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
        public static string indiceAHS(string subcodigo)
        {
            string nombre = string.Empty;
            try
            {
                switch (subcodigo.ToUpper())
                {
                    case "AHL":
                        nombre =  "0";
                        break;
                    case "AHG":
                        nombre =  "1";
                        break;
                    case "AHP":
                        nombre = "2";
                        break;
                    case "AHA":
                        nombre =  "3";
                        break;
                    default:
                        nombre = "";
                        break;
                }
            }
            catch (Exception) 
            { 
                throw; 
            }
            return nombre;
        } 
        private static int InStr(String SearchString, String SearchFor)
        {
            int resposne = 0;
            resposne = SearchString.IndexOf(SearchFor, 0);
            if (resposne < 0)
            {
                resposne = 0;
            }
            return resposne;
        }

        public static string StrToken(string request, string sSep, int iPos)
        {
            string response = request.Replace(sSep, "|");
            string[] aux = response.Split('|');
            return aux[iPos - 1];
        }
        public static string CambiarFormato(string request)
        {
            string response = string.Empty;
            try
            {
                string codigoBanco = string.Empty;
                string moneda = string.Empty;
                string sucursal = string.Empty;
                string producto = string.Empty;
                string nroCuenta = string.Empty;
                string sCtl4 = string.Empty;

                codigoBanco = "10";
                sCtl4 = "0000";
                    moneda = ManagerSystematic.Mid(request, 10, 1).Equals("3") ? "303" : "302";
                    sucursal = ManagerSystematic.Mid(request, 0, 3);
                    nroCuenta = ManagerSystematic.Mid(request, 3, 7).PadLeft(10, '0');
                    response = codigoBanco + moneda + sucursal + sCtl4 + nroCuenta;
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }
        public static string ProcesarTrama(string tipo, string subtipo, string tramaHost)
        {
            string response = string.Empty;
            switch (tipo)
            {
                case "TPLA":
                    if (subtipo.Equals("APER"))
                    {
                        string g_sTxnError = Mid(tramaHost, 7, 2);
                        response = Mid(tramaHost, 40, Len(tramaHost) - 40);
                        if (InStr(response, "@") > 0)
                        {
                            g_sTxnError = Mid(response, 1, InStr(response, "@") - 6);
                        }
                        else
                        {
                            g_sTxnError = Mid(response, 40, Len(response) - 6);
                        }
                        string delimitador = (char)17 + "" + (char)66 + "" + (char)45 + "" + (char)29 + "" + (char)45 + "";
                        int i = InStr(g_sTxnError, delimitador);

                    }
                    break;
                default:
                    break;
            }
            return response.Trim();
        }

        //        Public Function StrToken(ByVal s As String, sSep As String, iPos As Integer) As String
        //  Dim iIndex As Integer
        //  Dim iCurElem As Integer
        //  iIndex = 1
        //  iCurElem = 1
        //  Do While (Len(s) > 0)
        //    iIndex = InStr(s, sSep)
        //    If (iIndex = 0) Then
        //      iIndex = Len(s)
        //    Else
        //      iIndex = iIndex - 1
        //    End If
        //    If (iCurElem = iPos) Then
        //      StrToken = Mid(s, 1, iIndex)
        //      Exit Function
        //    Else
        //      iCurElem = iCurElem + 1
        //      s = Mid(s, iIndex + 2)
        //    End If
        //  Loop
        //  StrToken = s
        //End Function

        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;
            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = string.Empty;
            if ((startIndex + length) > param.Length)
            {
                result = string.Empty;
            }
            else
            {
                result = param.Substring(startIndex, length);
            }

            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }

        //public static bool IsNumeric(string s)
        //{
        //    Int64 output;
        //    return Int64.TryParse(s, out output);
        //}

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
            //return false;
        }

        public static int Len(string request)
        {
            if (string.IsNullOrEmpty(request))
                return 0;
            else
                return request.Length;
        }

        public static int CInt(string request)
        {
            if (!string.IsNullOrEmpty(request))
            {
                return Convert.ToInt32(request);
            }
            else
            {
                return 0;
            }
        }

        public static string CapturaCuentaSyst(string g_sCampos)
        {
            string response = string.Empty;

            try
            {
                string sDelimitador1 = "";
                string sDelimitador2 = "";
                string Campo = "";
                int i = 0;
                string error = "";
                string CapturaCuentaSyst = "";
                string ori = g_sCampos;
                sDelimitador1 = Convert.ToString(Convert.ToChar(17)) + "" + "" + Convert.ToString(Convert.ToChar(66)) + "" + "" + Convert.ToString(Convert.ToChar(45)) + "" + "" + Convert.ToString(Convert.ToChar(29)) + "" + "" + Convert.ToString(Convert.ToChar(45)) + "";
                sDelimitador2 = Convert.ToString(Convert.ToChar(64));

                int intDelimitador1 = InStr(g_sCampos, sDelimitador1);

                g_sCampos = g_sCampos.Substring(intDelimitador1 + 2);
                int intDelimitador2 = InStr(g_sCampos, sDelimitador2);
                g_sCampos = g_sCampos.Substring(intDelimitador2 + 2);
                if (intDelimitador2 == 0)
                    error = "Sin cuenta";

                Campo = "";
                string nuevo_campo = "";
                for (int j = 1; j <= 3; j++)
                {
                    Campo = Campo + (Mid(StrToken(g_sCampos, sDelimitador2, j), 4)).Trim() + ";";
                }
                Campo = Campo.Substring(0, Campo.Length - 1);
                if (InStr(Campo, sDelimitador1) != 0)
                {
                    Campo = Mid(Campo, 0, InStr(Campo, sDelimitador1));
                }
                CapturaCuentaSyst = Campo.Replace("@", "");
                CapturaCuentaSyst = CapturaCuentaSyst.Replace(" ", "");
                response = CapturaCuentaSyst;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        #region S50334: PROCESAR TIPOS DE PRODUCTOS
        public static string ProcesaDataAHS_BCB(string trama, string m_sTipoMoneda, int mProducto)
        {
            string response = string.Empty;

            string straux;
            string m_sNumeroCuenta = string.Empty;
            string sTipoCtaAbierta = string.Empty;

            straux = trama;
            if (straux.Equals(""))
            {
                response = "NO EXISTE MENSAJE DE RESPUESTA DISPONIBLE. SYSTEMATICS NO RESPONDIO A LA TRANSACCION DE APERTURA. LA CUENTA NO SE HA GENERADO";
            }
            m_sNumeroCuenta = StrToken(straux, ";", 3);
            m_sNumeroCuenta = StrToken(straux, ";", 2) + Right(m_sNumeroCuenta, 8);

            //straux = "Cuenta de Ahorros Abierta" + Convert.ToString(Convert.ToChar(13));
            straux = "" + "";
            if (m_sTipoMoneda == "303")
            {
                switch (mProducto)
                {
                    case 1:
                        sTipoCtaAbierta = "CtaAHSMN;";
                        break;
                    case 2:
                        sTipoCtaAbierta = "CtaAHSMN;";
                        break;
                    case 3:
                        sTipoCtaAbierta = "CtaAHSMN;";
                        break;
                    case 4:
                        sTipoCtaAbierta = "CtaAHSMN;";
                        break;
                    case 5:
                        sTipoCtaAbierta = "CtaAHSMN;";
                        break;
                    default:
                        break;
                }
                //straux = straux + "MN: ";
                straux = straux + "";
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "AHS", "MNA");
                m_sNumeroCuenta = Left(m_sNumeroCuenta, 11) + "3" + Right(m_sNumeroCuenta, 2);
                straux = straux + FormatCuenta(m_sNumeroCuenta, "AHS");
            }
            else if (m_sTipoMoneda == "302")
            {
                switch (mProducto)
                {
                    case 1:
                        sTipoCtaAbierta = "CtaAHSME;";
                        break;
                    case 2:
                        sTipoCtaAbierta = "CtaAHSME;";
                        break;
                    case 3:
                        sTipoCtaAbierta = "CtaAHSME;";
                        break;
                    case 5:
                        sTipoCtaAbierta = "CtaAHSME;";
                        break;
                    default:
                        break;
                }
                //straux = straux + "ME: ";
                straux = straux + "";
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "AHS", "USD");
                m_sNumeroCuenta = Left(m_sNumeroCuenta, 11) + "2" + Right(m_sNumeroCuenta, 2);
                straux = straux + FormatCuenta(m_sNumeroCuenta, "AHS");
            }
            response = straux;
            return response;
        }

        public static string ProcesaDataCCS(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + Right(m_sNumeroCuenta, 7);
                if (moneda.Equals("302"))
                {
                    m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "CCS", "MNE");
                }
                else
                {
                    m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "CCS", "MNA");
                }
                response = FormatCuenta(m_sNumeroCuenta, "CCS");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string ProcesaDataDPFO(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + m_sNumeroCuenta;
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "DOR", moneda);
                response = FormatCuenta(m_sNumeroCuenta, "DOR");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string ProcesaDataDPFR(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                //m_sNumeroCuenta = StrToken(trama, ";", 2) + Right(m_sNumeroCuenta, 7);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + m_sNumeroCuenta;
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "DOR", moneda);
                response = FormatCuenta(m_sNumeroCuenta, "DOR");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string ProcesaDataDPFNI(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + m_sNumeroCuenta;
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "DIN", moneda);
                response = FormatCuenta(m_sNumeroCuenta, "DIN");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string ProcesaDataDPFI(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + m_sNumeroCuenta;
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "DIN", moneda);
                response = FormatCuenta(m_sNumeroCuenta, "DIN");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string ProcesaDataDPFDec(string trama, string moneda, string producto)
        {
            string response = string.Empty;
            try
            {
                string m_sNumeroCuenta = StrToken(trama, ";", 3);
                m_sNumeroCuenta = StrToken(trama, ";", 2) + m_sNumeroCuenta;
                m_sNumeroCuenta = CrearDigitodeControl(m_sNumeroCuenta, "DIN", moneda);
                response = FormatCuenta(m_sNumeroCuenta, "DIN");
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string CrearDigitodeControl(string sCta, string sTipoCta, string sMoneda)
        {
            string response = string.Empty;
            try
            {
                string sDigitodeControl;
                int iSucursal;
                string sAux = string.Empty;
                string sCtaTemp;
                int iAux;

                iSucursal = CInt(Mid(sCta, 0, 3));
                if (iSucursal <= 200)
                {
                    iSucursal = 0;
                }
                if (sTipoCta.Equals("AHC") || sTipoCta.Equals("AHO") || sTipoCta.Equals("AHT") || sTipoCta.Equals("AHS") || sTipoCta.Equals("AHB"))
                {
                    if (sMoneda.Equals("MNA"))
                    {
                        sDigitodeControl = "3";
                    }
                    else
                    {
                        sDigitodeControl = "2";
                    }
                    if (iSucursal == 0)
                    {
                        sCtaTemp = "0000" + "0" + Right(sCta, 8);
                        sDigitodeControl = sDigitodeControl+ Right("0" + (CInt(Mid(sCtaTemp, 11, 2)) + CInt(Mid(sCtaTemp, 9, 2)) + CInt(Mid(sCtaTemp, 7, 2)) + CInt(Mid(sCtaTemp, 5, 2)) + CInt(Mid(sCtaTemp, 3, 2)) + CInt(Mid(sCtaTemp, 1, 2)) + CInt(Mid(sCtaTemp, 0, 1))), 2);
                    }
                    else
                    {
                        sCtaTemp = iSucursal + "8" + "0" + Right(sCta, 8);
                        sDigitodeControl = sDigitodeControl+Right("0" + (CInt(Mid(sCtaTemp, 11, 2)) + CInt(Mid(sCtaTemp, 9, 2)) + CInt(Mid(sCtaTemp, 7, 2)) + CInt(Mid(sCtaTemp, 5, 2)) + CInt(Mid(sCtaTemp, 3, 2)) + CInt(Mid(sCtaTemp, 1, 2)) + CInt(Mid(sCtaTemp, 0, 1))), 2);
                    }
                }
                else if (sTipoCta.Equals("CCS") || sTipoCta.Equals("CCB"))
                {
                    if (iSucursal == 0)
                    {
                        if (sMoneda == "MNA")
                            sCtaTemp = "0003000" + Right(sCta, 7);
                        else
                            sCtaTemp = "0002000" + Right(sCta, 7);
                    }
                    else
                    {
                        if (sMoneda == "MNA")
                            sCtaTemp = iSucursal + "3" + "000" + Right(sCta, 7);
                        else
                            sCtaTemp = iSucursal + "2" + "000" + Right(sCta, 7);
                    }
                    if (sMoneda.Equals("MNA"))
                    {
                        sDigitodeControl = "3";
                    }
                    else
                    {
                        sDigitodeControl = "2";
                    }
                    sDigitodeControl = sDigitodeControl + Right("0" + (CInt(Mid(sCtaTemp, 13, 1) + Mid(sCtaTemp, 12, 1)) + CInt(Mid(sCtaTemp, 11, 1) + Mid(sCtaTemp, 10, 1)) + CInt(Mid(sCtaTemp, 9, 1) + Mid(sCtaTemp, 8, 1)) + CInt(Mid(sCtaTemp, 7, 1) + Mid(sCtaTemp, 6, 1)) + CInt(Mid(sCtaTemp, 5, 1) + Mid(sCtaTemp, 4, 1)) + CInt(Mid(sCtaTemp, 3, 1) + Mid(sCtaTemp, 2, 1)) + CInt(Mid(sCtaTemp, 1, 1) + Mid(sCtaTemp, 0, 1))), 2);
                }
                else
                {
                    if (sTipoCta.Equals("DOR") || sTipoCta.Equals("DIN") || sTipoCta.Equals("DNI"))
                    {
                        if (sMoneda.Equals("MNA"))
                            sAux = "303" + Left(sCta, 3) + "003" + Right(sCta, 14);
                        else
                            sAux = "302" + Left(sCta, 3) + "003" + Right(sCta, 14);
                    }
                    sDigitodeControl = "0";
                    iAux = 0;
                    for (int i = 23; i >= 1; i--)
                    {
                        if (i % 2 != 0)
                        {
                            iAux = CInt(Mid(sAux, i - 1, 1)) * 2;
                            if (iAux > 9)
                            {
                                iAux = 1 + (iAux % 10);
                            }
                        }
                        else
                        {
                            iAux = CInt(Mid(sAux, i - 1, 1));
                        }
                        sDigitodeControl = (CInt(sDigitodeControl) + iAux).ToString();
                    }
                    sDigitodeControl = Right((10 - (CInt(sDigitodeControl) % 10)).ToString(), 1);
                    sCta = sAux;
                }
                response = sCta + sDigitodeControl;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string FormatCuenta(string sCuenta, string sTipo)
        {
            string resposen = string.Empty;
            try
            {
                string sValret = string.Empty;
                bool sds = IsNumeric(sCuenta);
                switch (sTipo)
                {
                    case "AHO":
                    case "AHT":
                    case "AHC":
                    case "AHS":
                    case "AHB":
                        if (IsNumeric(sCuenta) && sCuenta.Length == 14)
                            sValret = Left(sCuenta, 3) + "-" + Mid(sCuenta, 3, 8) + "-" + Mid(sCuenta, 11, 1) + "-" + Mid(sCuenta, 12);
                        else
                            sValret = sCuenta;
                        break;
                    case "CCS":
                    case "CCB":
                        if (IsNumeric(sCuenta) && sCuenta.Length == 13)
                            sValret = Left(sCuenta, 3) + "-" + Mid(sCuenta, 3, 7) + "-" + Mid(sCuenta, 10, 1) + "-" + Mid(sCuenta, 11, 2);
                        else
                            sValret = sCuenta;
                        break;
                    case "DOR":
                    case "DIN":
                    case "DNI":
                        if (IsNumeric(sCuenta) && sCuenta.Length == 24)
                            sValret = Mid(sCuenta, 3, 3) + "-" + Mid(sCuenta, 15, 8) + "-" + Mid(sCuenta, 2, 1) + "-" + Right(sCuenta, 1);
                        else
                            sValret = sCuenta;
                        break;
                    default:
                        break;
                }
                resposen = sValret;
            }
            catch (Exception)
            {
                throw;
            }
            return resposen;
        }
        #endregion

        #region S50334: CAMBIOS DE FORMATOS
        public static string ComercialARepextDpf(string nroCuenta)
        {
            string nroCuentaRepext = nroCuenta.Trim().Replace("-", "");
            string codigoBanco = "10";
            string response = string.Empty;
            string moneda = string.Empty;
            string sucursal = string.Empty;
            string producto = "003";

            try
            {
                if (Mid(nroCuentaRepext, 11, 1).Equals("3"))
                {
                    moneda = "303";
                }
                else
                {
                    moneda = "302";
                }
                sucursal = Mid(nroCuentaRepext, 0, 3);
                nroCuentaRepext = Mid(nroCuentaRepext, 3, 8).PadLeft(14, '0');
                response = codigoBanco + moneda + sucursal + producto + nroCuentaRepext;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region SECCION: CAJA AHORRO
        public static string TipoCuenta(string request)
        {
            string response = string.Empty;
            try
            {
                if (request.Equals("201"))
                    response = "JURIDICA";
                else if (request.Equals("202"))
                    response = "ENTIDAD FINANCIERA DEL PAIS JURIDICA";
                else if (request.Equals("203"))
                    response = "OTRAS INST. FINANCIERAS";
                else if (request.Equals("603"))
                    response = "MUTUAL/COOP FDOS FINANCIEROS";
                else if (request.Equals("601"))
                    response = "ASOC. S/F LUCRO";
                else if (request.Equals("602"))
                    response = "INST. ARMADOS";
                else if (request.Equals("101"))
                    response = "INDIVIDUAL";
                else if (request.Equals("102"))
                    response = "MANC. INDISTINTA";
                else if (request.Equals("103"))
                    response = "MANC. CONJUNTA";
                else if (request.Equals("104"))
                    response = "MENOR";
                else if (request.Equals("105"))
                    response = "MENOR DONACION";
                else if (request.Equals("106"))
                    response = "MENOR CON PODER";
                else if (request.Equals("107"))
                    response = "ANALFABETO";
                else if (request.Equals("108"))
                    response = "BENEMERITO";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string EstadoCajaAhorro(string request)
        {
            string response = "INDETERMINADO";
            try
            {
                if (request.Equals("00"))
                    response = "ACTIVA";
                else if (request.Equals("01"))
                    response = "INACTIVA";
                else if (request.Equals("02"))
                    response = "DORMANT";
                else if (request.Equals("03"))
                    response = "CERRADA";
                else if (request.Equals("04"))
                    response = "TERMINADA";
                else if (request.Equals("05"))
                    response = "PURGADA";
                else if (request.Equals("06"))
                    response = "UNREDEENED";
                else if (request.Equals("07"))
                    response = "CONGELADA, NO HAY ACTIVIDAD MONETARIA";
                else if (request.Equals("08"))
                    response = "CONGELADA, NO HAY ACTIVIDAD";

            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string Bloqueo(string request)
        {
            string response = "INDETERMINADO";
            try
            {
                if (request.Equals("INT"))
                    response = "INTERNO";
                else if (request.Equals("FAL"))
                    response = "FALLECIDO";
                else if (request.Equals("UIF"))
                    response = "UNIDAD DE INVEST.FINANCIERA";
                else if (request.Equals("   "))
                    response = "SIN BLOQUEO";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region CUENTA CORRIENTE
        public static string TipoCuentaCorriente(string codigo)
        {
            string response = "CTI";
            try
            {
                switch (codigo)
                {
                    case "100":
                    case "200":
                        response = "Cuenta Corriente Systematics";
                        break;
                    case "210":
                        response = "Cuenta Fiscal";
                        break;
                    case "120":
                    case "220":
                        response="Cuenta Corriente UFV";
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static string EstadoCuentaCorriente(string request)
        {
            string response = "INDETERMINADO";
            try
            {
                if (request.Equals("00"))
                    response = "NORMAL";
                else if (request.Equals("04"))
                    response = "CERRADA";
                else if (request.Equals("05"))
                    response = "PURGADA";
                else if (request.Equals("08"))
                    response = "TERMINADA";
                else if (request.Equals("09"))
                    response = "BLOQUEADA";
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public static string FormatoDinero(string requet)
        {
            string response = "";
            try
            {
                response = decimal.Parse(requet, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region ALS
        //Public Function CapturaInformacionALS(LenFileds As Integer) As String
        //  Dim ValorData As String

        //  ValorData = Left$(g_sFrameInAux, LenFileds)
        //  g_sFrameInAux = Mid(g_sFrameInAux, LenFileds + 1)
        //  If (InStr(ValorData, Chr(12)) > 0) Or (InStr(ValorData, Chr(15)) > 0) Then
        //    ValorData = ""
        //  End If
        //  CapturaInformacionALS = ValorData
        //End Function     

        public static string CapturaInformacionALS(string trama, int LenFileds, ref string nuevaTrama)
        {
            string ValorData = string.Empty;
            try
            {
                ValorData = Left(trama, LenFileds);
                nuevaTrama = Mid(trama, LenFileds);
                if (InStr(ValorData, Convert.ToChar(12).ToString()) > 0 || InStr(ValorData, Convert.ToChar(15).ToString()) > 0)
                {
                    ValorData = string.Empty;
                }
            }
            catch (Exception)
            {
                ValorData = string.Empty;
            }
            return ValorData;
        }
        #endregion
    }
}
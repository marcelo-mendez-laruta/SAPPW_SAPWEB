using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Connectors;
using BCP.Sap.Models.Configuracion;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Enviar.ProcesoApertura;
using BCP.Sap.Models.Enviar.Producto;
using BCP.Sap.Models.Enviar.Reporte;
using BCP.Sap.Models.Enviar.SmartLink;
using BCP.Sap.Models.Enviar.Tarjeta;
using BCP.Sap.Models.Rebibir;
using BCP.Sap.Models.Rebibir.SmartLink;
using BCP.Sap.Models.Rebibir.Tarjeta;
using BCP.Sap.Models.Sap;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using BCP.Sap.Models.Cache;
using System.Text.Json;
using BCP.Sap.Models.Seguros;

namespace BCP.Sap.Business
{
    public interface ISapBusiness
    {
        #region inicio
        string[] cuentasAN();
        string QR();
        RespuestaUsuario ActiveDirectory(string usuario);
        AdecuacionConfiguracionEstaciones DatosEstacion(string usuario, ref bool exito);
        RespuestaSegurinet Login(Login login, out string contrasena);
        RespuestaSegurinet LoginUsuario(string matricula);
        SapResponse ClaveSegurinet(string contrasena, Sesion sesion);
        Sesion pGUID(Sesion sesion);
        bool VoBoSupervisor(Login login);
        bool ValidarSucursal(string sucursal);
        #endregion
        #region cliente
        RespuestaPersonaNatural ConsultaClientePN(ParametrosHost usuario, string[] idC, string tid, bool datosAdicionales, bool procesoApertura);
        RespuestaPersonaJuridica ConsultaClientePJ(ParametrosHost usuario, string[] idC, string tid);
        RespuestaCreacionCliente CrearClientePN(ParametrosHost usuario, string[] idC, string tid, CreacionClientePN datos);
        RespuestaCreacionCliente CrearClientePJ(ParametrosHost usuario, string[] idC, string tid, CreacionClientePJ datos);
        SapResponse RegistrarINFOCLIENTE(string[] idC, string tid, RegistroINFOCLIENTE datos, string usuario);
        SapResponse ModificarIdcPNJ(Sesion sesion, string id, string tid, string Nid, string Ntid, string tipo);
        SapResponse ModificarNombresPNJ(Sesion sesion, string id, string tid, string datos, string tipoPersona);
        SapResponse ModificarTipoClientePNJ(ParametrosHost usuario, string id, string tid, string tipoPersona, string tipoCliente);
        SapResponse ModificarPNJ(Sesion sesion, string id, string tid, ModificacionDataPN pn, ModificacionDataPJ pj, string tipo);
        RespuestaConsultaAlfabetica dataConAlfCli(ParametrosHost usuario, string cliente, bool completo, string idc, string idt, string ide);
        #endregion
        #region funneg
        RespuestaCambioFunNeg CambioFunNeg(Sesion sesion, string id, string tid, FunNeg funNeg, string tipoCliente);
        SapResponse ConsultaFunNeg(ParametrosHost host, string funNeg);
        #endregion
        #region fatca
        RespuestaConsultaFatca ConsultaFacta(string id, string tid, string cic, Sesion sesion);
        SapResponse CreacionFacta(string id, string tid, Fatca facta, Sesion sesion);
        #endregion
        #region producto
        RespuestaConsultaProducto ConsultaProCli(string id, string tid, string ini, string fin, Sesion sesion);
        RespuestaConsultaProductoCliente ConsultaCliPro(Sesion sesion, ProductoCliente datos);
        object ConsultaMoviProductos(string tipo, Cuenta nCuenta, Sesion sesion);
        SapResponse RelacionarDirProd(EnvioRegistroProductoDireccionData datos, Sesion sesion);
        RespuestaCreacionCuenta AperturaProductoCorriente(CuentaCorriente corriente, ref SeguimientoProcesos pasos, Sesion sesion);
        RespuestaCreacionCuenta AperturaProductoAhorro(CajaAhorro ahorro, ref SeguimientoProcesos pasos, Sesion sesion);
        RespuestaCreacionCuenta AperturaProductoDPF(DPF dpf, ref SeguimientoProcesos pasos, Sesion sesion);
        RespuestaCreacionCuenta AperturaProductoCustodia(Custodia custodia, ref SeguimientoProcesos pasos, Sesion sesion);
        #endregion
        #region archivo negativo - ename checker
        RespuestaRegistro ArchivoNegativo(string id, string tid, string tipo, string paterno, string materno, string nombres);
        RespuestaRegistro EnameChecker(Sesion sesion, string paterno, string materno, string nombres);
        #endregion
        #region swamp
        bool F20190(SeguimientoProcesos pasos, int indice, Sesion sesion);
        #endregion
        #region smart link
        RespuestaSLGenerico SLCuenta(string[] cuentas, string tarjeta, string nombreTarjeta);
        RespuestaSLGenerico SLActualizacion(string[] cuentas, string tarjeta);
        RespuestaSLGenerico SLDesafiliar(string cuenta, string tarjeta);
        void SLCambio(string[] cuentas, string tarjetaAnterior, string tarjeta);
        #endregion
        #region direccion
        RespuestaConsultaDireccion BusquedaDireccionPNJ(ParametrosHost usuario, string[] idC, string tid);
        SapResponse OperacionesDireccionPNJ(ParametrosHost usuario, string[] idC, string tid, DatosDireccion datos, bool registro);
        SapResponse ModificarCorrespondencia(ParametrosHost usuario, string[] idC, string tid, int direccion);
        #endregion
        #region rec
        RespuestaConsultaRec ConsultaRecPNJ(ParametrosHost usuario, string[] idC, string tid);
        SapResponse OperacionesRecPNJ(ParametrosHost usuario, string[] idC, string tid, DatosRec datos, bool registro);
        SapResponse EliminarRecPNJ(Sesion sesion, string id, string tid, string idRel, string tidRel, string codRelacion);
        #endregion
        #region emp
        RespuestaEmpleadorBusqueda BusquedaEmpPNJ(Sesion sesion, string id, string tid);
        SapResponse OperacionesEmpPNJ(Sesion sesion, string[] idC, string tid, string[] idCEmp, string etid, DatosEmpleador datos, bool registro);
        #endregion
        #region aep
        RespuestaConsultaAep BusquedaAepPNJ(Sesion sesion, string id, string tid, string fecha);
        SapResponse OperacionesAepPNJ(Sesion sesion, string id, string tid, string fecha, DatosAep datos, bool registro);
        SapResponse EliminarAepPNJ(Sesion sesion, string id, string tid, string fecha);
        #endregion
        #region tarjeta
        RespuestaBusquedaTarjeta BusquedaTarjeta(Sesion sesion, string nombre);
        RespuestaConsultaTarjeta ConsultaTarjeta(Sesion sesion, string tarjeta, string bancaExclusiva, int indice, ref SeguimientoProcesos pasos);
        SapResponse EntregaTarjeta(Sesion sesion, Entrega datos, bool registro, ref SeguimientoProcesos pasos);
        SapResponse CambioTarjeta(Sesion sesion, Cambio datos, ref SeguimientoProcesos pasos);
        RespuestaBloqueo BloqueoTarjeta(Sesion sesion, Bloqueo datos, ref SeguimientoProcesos pasos);
        SapResponse CambioTarjetaRapido(Sesion sesion, CambioRapido datos, ref SeguimientoProcesos pasos);
        SapResponse OperacionTarjeta(Sesion sesion, Datos datos, bool afiliacion, string situacion, string bancaExclusiva, int indice, ref SeguimientoProcesos pasos);
        SapResponse BancaExclusiva(Sesion sesion, BancaExclusivaData datos, string tipo, ref SeguimientoProcesos pasos);
        RespuestaStockTarjeta TarjetaStocks(StockConsulta datos, Sesion sesion);
        SapResponse RecepcionTarjeta(Recepcion datos, Sesion sesion);
        SapResponse ActualizacionTarjeta(ActualizacionData datos, Sesion sesion);
        RespuestaRelacionTarjeta TarjetasRelacionadas(Relacion datos, Sesion sesion);
        #endregion
        #region als
        RecibirALSInformeCrediticio ConsultaInformeCrediticioALS(Sesion sesion, AlsInformeCrediticioRequest datos);
        RecibirALSProductoCliente ConsultaProductoClienteALS(Sesion maestro, ALSProductoClienteRequestData datos);
        RecibirALSConsultaAlfabetica ConsultaAlfabeticaALS(Sesion sesion, ALSConsultaAlfabeticaRequestData datos);
        #endregion
        #region flujo
        ConsultaTarjeta sinTarjeta(ConsultaTarjeta datos, DatosCliente cliente);
        ConsultaTarjeta asignarCuenta(ConsultaTarjeta resultado, List<AperturaCuenta> cuenta);
        #endregion
        #region aperturaspas
        RespuestaParametro obtenerParametro();
        RespuestaTipoCuenta obtenerListaCuentas(string tipoPersona);
        public RespuestaProducto ListaModalidad(string valor, SeguimientoProcesos pasos);
        RespuestaConsultaBloqueo ConsultaBloqueoBD(BloqueoDBDataCondicional datos, ParametrosHost usuario);
        SapResponse RegistroFiltro(SeguimientoProcesos pasos, Sesion sesion);
        SapResponse RegistroBloqueoDB(Sesion sesion, BloqueoDBData datos);
        #endregion
        #region reportes
        RespuestaReporte ReporteApertura(ReporteAperturaData datos, Sesion sesion);
        RespuestaReporte ReporteTarjeta(ReporteTarjetaData datos, Sesion sesion);
        RespuestaReporte ReporteStocks(Stock datos);
        RespuestaReporte ImpresionContrato(SeguimientoProcesos pasos, Sesion sesion, int indice, bool original, bool copia, ref string operacion);
        #endregion
        #region Seguros
        RegistrarSeguroResponse AfiliacionSeguro(RegistrarSeguroRequest datos);
        GeneraContratoResponse CertificadoSeguro(GeneraContratoRequest datos);
        List<ConfiguracionSeguros> GetListSeguros(string Producto);
        #endregion
    }
    public class SapBusiness : ISapBusiness
    {
        private IConfiguration _config;
        private ConfiguracionBase _configuracion;
        private ConsumirAPI _consumirAPI;
        private string _operacion;
        private IMemoryCache _cache;
        public SapBusiness(IMemoryCache cache, IConfiguration config)
        {
            _config = config;
            var appconfig = _config.GetSection("aplicacion").Get<ConfiguracionBase>();
            _configuracion = appconfig;
            _consumirAPI = new ConsumirAPI(_configuracion.configuracionAPI, _configuracion.configuracionMicroservicios.url);
            _cache = cache;
        }

        #region SECCION: INICIO
        public string[] cuentasAN()
        {
            return this._configuracion.configuracionProducto.ANProductos.Split('|');
        }
        public string QR()
        {
            return this._configuracion.configuracionQR.modificacion;
        }
        public RespuestaUsuario ActiveDirectory(string usuario)
        {
            RespuestaUsuario response = new RespuestaUsuario();
            try
            {
                string metodo = "ActiveDirectory/api/Metodo/usuario/consulta";
                EnvioPeticion data = new EnvioPeticion
                {
                    usuario = new EnvioGrupoDominio { matricula = usuario, grupos = this._configuracion.configuracionGrupoDominio.grupos }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaUsuario>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaSegurinet LoginUsuario(string matricula)
        {
            RespuestaSegurinet response = new RespuestaSegurinet();
            try
            {
                string metodo = "Segurinet/api/Segurinet/login/v1";
                EnviarSegurinetV1 data = new EnviarSegurinetV1 { usuarioDominio = matricula };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaSegurinet>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "USUARIO CON AUTORIZACION DE USO DEL APLICATIVO SAP PASIVOS SEGUN SU PERFIL");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public AdecuacionConfiguracionEstaciones DatosEstacion(string usuario, ref bool exito)
        {
            AdecuacionConfiguracionEstaciones response = new AdecuacionConfiguracionEstaciones();
            try
            {
                string metodo = "Parametros/api/Configuracion/lista";
                EnviarSegurinetV1 data = new EnviarSegurinetV1 { usuarioDominio = usuario };//no en realidad usuario dominio sino usuario extra-segurinet
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                RespuestaConfigUsuario respuesta = _consumirAPI.consulta<RespuestaConfigUsuario>(metodo, data);
                if (respuesta.success)
                {
                    if (ValidarSucursal(respuesta.data.sucursal))
                    {
                        exito = respuesta.success;
                        if (exito)
                        {
                            response.host = ManagerJson.DeserializarObjecto<ParametrosHost>(ManagerJson.SerializarObjecto(respuesta.data));
                            response.slk = ManagerJson.DeserializarObjecto<ParametrosSLK>(ManagerJson.SerializarObjecto(respuesta.data));
                        }
                        else
                        {
                            response.message = respuesta.message;
                        }
                    }
                    else
                    {
                        response.message = "Usuario no pertenece a la region del aplicativo.";
                    }
                }
                else
                {
                    response.message = "No se pudo determinar la region a la que pertenece el usuario.";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaSegurinet Login(Login login, out string contrasena)
        {
            contrasena = string.Empty;
            RespuestaSegurinet response = new RespuestaSegurinet();
            try
            {
                string metodo = "Segurinet/api/Segurinet/login/v2";
                contrasena = LimpiarDatos.EnDecryp(login.contrasena, true);
                EnviarSegurinetV2 data = new EnviarSegurinetV2
                {
                    usuarioSegurinet = LimpiarDatos.EnDecryp(login.usuario, true),
                    passwordSegurinet = contrasena
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaSegurinet>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, "LOGIN POR CREDENCIALES DE SEGURINET");
            }
            finally
            {
                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "USUARIO CON AUTORIZACION DE USO DEL APLICATIVO SAP PASIVOS SEGUN SU PERFIL");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public bool VoBoSupervisor(Login login)
        {
            RespuestaSegurinet response = new RespuestaSegurinet();
            bool respuesta = false;
            try
            {
                string metodo = "Segurinet/api/Segurinet/login/v2";
                EnviarSegurinetV2 data = new EnviarSegurinetV2
                {
                    usuarioSegurinet = LimpiarDatos.EnDecryp(login.usuario, true),
                    passwordSegurinet = LimpiarDatos.EnDecryp(login.contrasena, true)
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaSegurinet>(metodo, data);
                if (response.success)
                {
                    string[] politicas = response.data.politicas.Split('|');
                    respuesta = politicas.Contains("SAPP_BO_PRO_APE_PRO_DEP_DNI_SUP");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, "LOGIN POR CREDENCIALES DE SEGURINET");
            }
            finally
            {
                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "USUARIO CON AUTORIZACION DE USO DEL APLICATIVO SAP PASIVOS SEGUN SU PERFIL");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return respuesta;
        }
        public bool ValidarSucursal(string sucursal)
        {
            List<string> validas = _configuracion.Sucursales;
            bool flag = false;
            foreach (string suc in validas)
            {
                if (suc == sucursal) { flag = true; }
            }
            return flag;
        }
        public SapResponse ClaveSegurinet( string contrasena, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Segurinet/api/Segurinet/cambio/password";
                CambiarClaveSegurinet data = new CambiarClaveSegurinet
                {
                    usuarioSegurinet = LimpiarDatos.EnDecryp(sesion.credenciales.usuario, true),
                    passwordSegurinet = sesion.credenciales.contrasena,
                    nuevoPasswordSegurinet = LimpiarDatos.EnDecryp(contrasena, true)
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public Sesion pGUID(Sesion sesion)
        {
            string guid = sesion.host.guid;
            if (sesion.datosGUID == null)
            {
                try
                {
                    string metodo = "OperacionesDb/api/Swamp/datos/cliente";
                    envioGUID data = new envioGUID { guid = guid };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    sesion.datosGUID = _consumirAPI.consulta<RespuestaGUID>(metodo, data);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return sesion;
        }
        #endregion

        #region SECCION: CLIENTE
        public RespuestaPersonaNatural ConsultaClientePN(ParametrosHost usuario, string[] idC, string tid, bool datosAdicionales, bool procesoApertura)
        {
            RespuestaPersonaNatural response = new RespuestaPersonaNatural();
            try
            {
                string metodoH = "Cliente/api/Consulta/cliente/consulta/pn";
                EnvioExistenciaPersonaNatural data = new EnvioExistenciaPersonaNatural
                {
                    cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    },
                    usuario = usuario
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaPersonaNatural>(metodoH, data);
                if (!response.success)
                {
                    if (procesoApertura && response.message.Contains("NO EXISTE"))
                    {
                        string metodoDB = "OperacionesDB/api/INFOCLIENTE/cliente/consultar";
                        response = _consumirAPI.consulta<RespuestaPersonaNatural>(metodoDB, data);
                        if (response.success)
                        {
                            if (!response.data.cic.Contains("FIC"))
                            {
                                response.data.fechaUltActualizacion = DateTime.Now.ToString("ddMMyyyy");
                                response.data.tipoCliente = "CL";
                                response.data.tipoBanca = "P";
                                if (datosAdicionales && (response.data.situacionLaboral.Equals("DEP") || response.data.situacionLaboral.Equals("EST") || response.data.situacionLaboral.Equals("JUB")))
                                {
                                    switch (response.data.situacionLaboral)
                                    {
                                        case "DEP":
                                            response.data.ciiu = "00091";
                                            break;
                                        case "EST":
                                            response.data.ciiu = "99002";
                                            break;
                                        case "JUB":
                                            response.data.ciiu = "99001";
                                            break;
                                    }
                                }
                            }
                            else 
                            {
                                response.success = false;
                                response.state = "101";
                                response.message = "CIC FICTICIO.       *CONSULTE LA INFORMACION EN INFOCLIENTE*";
                                response.data = new PersonaNatural();
                            }
                        }
                        else
                        {
                            response.data = new PersonaNatural();
                        }
                    }
                }
                else
                {
                    response.data.negocioPropio = response.data.tipoCuenta.Equals("I") ? "S" : "N";
                    if (datosAdicionales)
                    {
                        #region CONSULTA EMAIL
                        RespuestaConsultaDireccion respuestaDir = BusquedaDireccionPNJ(usuario, idC, tid);
                        if (respuestaDir.success)
                        {
                            for (int i = 0; i < respuestaDir.data.total; i++)
                            {
                                if (respuestaDir.data.direcciones[i].tipoDireccion.Equals("M"))
                                {
                                    response.data.mail = respuestaDir.data.direcciones[i].direccionComprimida;
                                    response.data.celular = respuestaDir.data.direcciones[i].telefono;
                                    break;
                                }
                            }
                        }
                        #endregion

                        #region CONSULTA REC
                        if (response.data.situacionLaboral.Equals("DEP") || response.data.situacionLaboral.Equals("EST") || response.data.situacionLaboral.Equals("JUB"))
                        {
                            switch (response.data.situacionLaboral)
                            {
                                case "DEP":
                                    response.data.ciiu = "00091";
                                    break;
                                case "EST":
                                    response.data.ciiu = "99002";
                                    break;
                                case "JUB":
                                    response.data.ciiu = "99001";
                                    break;
                            }
                            RespuestaConsultaRec respuestaRec = ConsultaRecPNJ(usuario, idC, tid);
                            if (respuestaRec.success)
                            {
                                for (int i = 0; i < respuestaRec.data.total; i++)
                                {
                                    if (respuestaRec.data.rows[i].codigoRelacion.Equals("PE"))
                                    {
                                        response.data.ruc = respuestaRec.data.rows[i].idcPersona + respuestaRec.data.rows[0].extensionPersona;
                                        response.data.nombreEmpresa = respuestaRec.data.rows[i].nombre;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaPersonaJuridica ConsultaClientePJ(ParametrosHost usuario, string[] idC, string tid)
        {
            RespuestaPersonaJuridica response = new RespuestaPersonaJuridica();
            try
            {
                string metodo = "Cliente/api/Consulta/cliente/consulta/pj";
                EnvioExistenciaPersonaJuridica data = new EnvioExistenciaPersonaJuridica
                {
                    cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    },
                    usuario = usuario
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaPersonaJuridica>(metodo, data);
                if (!response.success && response.message.Contains("NO EXISTE"))
                {
                    response.state = "11";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaCreacionCliente CrearClientePJ(ParametrosHost usuario, string[] idC, string tid, CreacionClientePJ datos)
        {
            RespuestaCreacionCliente response = new RespuestaCreacionCliente();
            try
            {
                datos = LimpiarDatos.limpiarNulo<CreacionClientePJ>(datos);
                if (!string.IsNullOrEmpty(datos.oficina))
                {
                    usuario.agencia = datos.oficina;
                    usuario.sucursal = datos.oficina.Substring(0, 1) + "01";
                }
                if (string.IsNullOrEmpty(datos.tipoCliente))
                {
                    datos.tipoCliente = "NC";
                }
                if (string.IsNullOrEmpty(datos.residente))
                {
                    datos.residente = "N";
                }
                if (string.IsNullOrEmpty(datos.negocioPropio))
                {
                    datos.negocioPropio = "S";
                }
                string metodo = "Cliente/api/Creacion/cliente/creacion/pj";
                EnvioCreacionCliente data = new EnvioCreacionCliente
                {
                    usuario = usuario,
                    cliente = new EnvioDocumentoPJ
                    {
                        idcExtension = idC[1],
                        idcNumero = idC[0],
                        idcTipo = tid
                    },
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaCreacionCliente>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaCreacionCliente CrearClientePN(ParametrosHost usuario, string[] idC, string tid, CreacionClientePN datos)
        {
            RespuestaCreacionCliente response = new RespuestaCreacionCliente();
            try
            {
                datos = LimpiarDatos.limpiarNulo<CreacionClientePN>(datos);
                if (!string.IsNullOrEmpty(datos.oficina))
                {
                    usuario.agencia = datos.oficina;
                    usuario.sucursal = datos.oficina.Substring(0, 1) + "01";
                }
                datos.tipoCliente = "CL";
                string metodoH = "Cliente/api/Creacion/cliente/creacion/pn";
                EnvioDocumentoPN clienteIDC = new EnvioDocumentoPN
                {
                    idcNumero = idC[0],
                    idcTipo = tid,
                    idcExtension = idC[1]
                };
                EnvioCreacionCliente data = new EnvioCreacionCliente
                {
                    usuario = usuario,
                    cliente = clienteIDC,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaCreacionCliente>(metodoH, data);
                if (response.success)
                {
                    datos.cic = response.data.cic;

                    #region DIRECCCION ELECTRONICA
                    if (!(datos.mail == null || datos.celular == null))
                    {
                        if (!datos.celular.Equals(""))
                        {
                            if (datos.mail.Equals(""))
                            {
                                datos.mail = "SIN E@MAIL";
                            }
                            DatosDireccion correo = new DatosDireccion
                            {
                                codLocalidad = "0000",
                                tipoDireccion = "M",
                                direccionComprimida = datos.mail,
                                estado = "C",
                                telefono = datos.celular
                            };
                            SapResponse respuestacorreo = OperacionesDireccionPNJ(usuario, idC, tid, correo, true);
                        }
                    }
                    #endregion

                    #region REC
                    if (datos.situacionLaboral.Equals("DEP") || datos.situacionLaboral.Equals("EST") || datos.situacionLaboral.Equals("JUB"))
                    {
                        datos.nit = datos.ruc;
                        DateTime hoy = DateTime.Now;
                        DateTime vencimiento = hoy.AddYears(1);
                        while (vencimiento.DayOfWeek == DayOfWeek.Saturday || vencimiento.DayOfWeek == DayOfWeek.Sunday)
                        {
                            vencimiento = vencimiento.AddDays(1);
                        }
                        DatosRec dataRec = new DatosRec
                        {
                            idcPersona = datos.ruc,
                            tipoDocumentoPersona = "R",
                            fechaInicio = hoy.ToString("dd/MM/yyyy"),
                            fechaValor = "",
                            fechaVerificacion = hoy.ToString("dd/MM/yyyy"),
                            fechaTermino = hoy.ToString("dd/MM/yyyy"),
                            nombre = datos.nombreEmpresa,
                            vigente = "S",
                            codigoRelacion = "PE",
                            codMoneda = "",
                            codigoAsociado = "",
                            valorAsociado = ""
                        };
                        SapResponse respuestaRec = OperacionesRecPNJ(usuario, idC, tid, dataRec, true);
                    }
                    else
                    {
                        datos.nombreEmpresa = datos.nombreComercial;
                    }
                    #endregion                   
                }

            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public SapResponse RegistrarINFOCLIENTE(string[] idC, string tid, RegistroINFOCLIENTE datos, string usuario)
        {
            SapResponse response = new SapResponse();
            try
            {
                datos = LimpiarDatos.limpiarNulo<RegistroINFOCLIENTE>(datos);
                string metodoBD = "OperacionesDB/api/INFOCLIENTE/cliente/insertar";
                if (!datos.celular.Equals("") && datos.mail.Equals(""))
                {
                    datos.mail = "SIN E@MAIL";
                }
                EnvioCreacionINFOCLIENTE data = new EnvioCreacionINFOCLIENTE
                {
                    usuario = usuario,
                    cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    },
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodoBD, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public SapResponse ModificarIdcPNJ(Sesion sesion, string id, string tid, string Nid, string Ntid, string tipo)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string metodo = "Cliente/api/Actualizacion/idc/p" + tipo;
                string[] idA = ManagerValidation.obtenerIDC(id, tid);
                string[] idN = ManagerValidation.obtenerIDC(Nid, Ntid);

                if (!idA.Contains("?") && !idN.Contains("?"))
                {
                    EnvioModificarIDC data = new EnvioModificarIDC
                    {
                        usuario = sesion.host,
                        data = new ModificarIDCData
                        {
                            idcNumeroActual = idA[0],
                            idcTipoActual = tid,
                            idcExtensionActual = idA[1],
                            idcNumeroNuevo = idN[0],
                            idcExtensionNuevo = idN[1],
                            idcTipoNuevo = Ntid
                        }
                    };
                    if (tipo.Equals("n"))
                    {
                        data.data = ManagerJson.DeserializarObjecto<ModificarIDCDataPN>(ManagerJson.SerializarObjecto(data.data));
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse ModificarNombresPNJ(Sesion sesion, string id, string tid, string datos, string tipoPersona)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string metodo = "Cliente/api/Actualizacion/nombre/p" + tipoPersona;
                string[] ids = ManagerValidation.obtenerIDC(id, tid);

                if (!ids.Contains("?"))
                {
                    EnvioModificarNombre data = new EnvioModificarNombre
                    {
                        usuario = sesion.host,
                        cliente = new EnvioDocumentoPJ
                        {
                            idcTipo = tid,
                            idcNumero = ids[0],
                            idcExtension = ids[1]
                        }
                    };
                    if (tipoPersona.Equals("n"))
                    {
                        data.data = ManagerJson.DeserializarObjecto<ModificarNombrePersonaNatural>(datos);
                        data.cliente = ManagerJson.DeserializarObjecto<EnvioDocumentoPN>(ManagerJson.SerializarObjecto(data.cliente));
                    }
                    else
                    {
                        data.data = ManagerJson.DeserializarObjecto<ModificarNombrePersonaJuridica>(datos);
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse ModificarTipoClientePNJ(ParametrosHost usuario, string id, string tid, string tipoPersona, string tipoCliente)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string metodo = "Cliente/api/Actualizacion/tipoCliente/p" + tipoPersona;
                string[] ids = ManagerValidation.obtenerIDC(id, tid);

                if (!ids.Contains("?"))
                {
                    EnvioModificarTipoCliente data = new EnvioModificarTipoCliente
                    {
                        usuario = usuario
                    };
                    if (tipoPersona.Equals("n"))
                    {
                        data.data = new ModificarTCPersonaNatural
                        {
                            idcTipo = tid,
                            idcNumero = ids[0],
                            idcExtension = ids[1],
                            tipoCliente = tipoCliente
                        };
                    }
                    else
                    {
                        data.data = new ModificarTCPersonaJuridica
                        {
                            idcTipo = tid,
                            idcNumero = ids[0],
                            idcExtension = ids[1],
                            tipoCliente = tipoCliente
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse ModificarPNJ(Sesion sesion, string id, string tid, ModificacionDataPN pn, ModificacionDataPJ pj, string tipo)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Cliente/api/Actualizacion/dbc/p" + tipo;
                string[] ids = ManagerValidation.obtenerIDC(id, tid);

                if (!ids.Contains("?"))
                {
                    EnvioModificacionDatos data = new EnvioModificacionDatos
                    {
                        usuario = sesion.host,
                        cliente = new EnvioDocumentoPJ
                        {
                            idcTipo = tid,
                            idcNumero = ids[0],
                            idcExtension = ids[1]
                        }
                    };
                    if (tipo.Equals("n"))
                    {
                        data.data = LimpiarDatos.limpiarNulo<ModificacionDataPN>(pn);
                        data.cliente = ManagerJson.DeserializarObjecto<EnvioDocumentoPN>(ManagerJson.SerializarObjecto(data.cliente));
                        data.usuario.agencia = pn.oficina;
                        data.usuario.sucursal = pn.oficina.Substring(0, 1) + "01";
                    }
                    else
                    {
                        data.data = LimpiarDatos.limpiarNulo<ModificacionDataPJ>(pj);
                        data.usuario.agencia = pj.oficina;
                        data.usuario.sucursal = pj.oficina.Substring(0, 1) + "01";
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    response = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return (response);
        }
        public RespuestaConsultaAlfabetica dataConAlfCli(ParametrosHost usuario, string cliente, bool completo, string idc, string idt, string ide)
        {
            RespuestaConsultaAlfabetica resultado = new RespuestaConsultaAlfabetica();
            try
            {
                string[] datosCliente = new string[3] { "", "", "" };
                string[] nombres = cliente.Split('|');

                for (int i = 0; i < nombres.Length; i++)
                {
                    datosCliente[i] = nombres[i];
                }
                string metodo = "Cliente/api/BusquedaAlfabetica/cliente/busqueda/pn";
                EnvioConsultaAlfabetica data = new EnvioConsultaAlfabetica
                {
                    usuario = usuario,
                    data = new EnvioDatosPersonaNatural
                    {
                        paterno = datosCliente[0],
                        materno = datosCliente[1],
                        nombres = datosCliente[2],
                        idcNumero = idc,
                        idcTipo = idt,
                        idcExtension = ide
                    }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<RespuestaConsultaAlfabetica>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion

        #region SECCION: FUNCIONARIO DE NEGOCIOS
        public RespuestaCambioFunNeg CambioFunNeg(Sesion sesion, string id, string tid, FunNeg funNeg, string tipoCliente)
        {
            RespuestaCambioFunNeg response = new RespuestaCambioFunNeg();
            try
            {
                string metodo = "CambioFunNeg/api/v1/Metodo/cliente/" + (tipoCliente.ToLower().Equals("n") ? "natural" : "juridico") + "/cambiocompleto";
                string[] ids = ManagerValidation.obtenerIDC(id, tid);
                if (!ids.Contains("?"))
                {
                    EnvioCambioFunNeg data = new EnvioCambioFunNeg
                    {
                        cliente = new EnvioDocumentoPN
                        {
                            idcNumero = ids[0],
                            idcExtension = ids[1],
                            idcTipo = tid
                        },
                        usuario = sesion.host,
                        data = funNeg
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<RespuestaCambioFunNeg>(metodo, data);
                }
                else
                {
                    response = new RespuestaCambioFunNeg
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public SapResponse ConsultaFunNeg(ParametrosHost host, string funNeg)
        {
            SapResponse response = new SapResponse();
            try
            {
                if (!string.IsNullOrEmpty(funNeg))
                {
                    string metodo = "CambioFunNeg/api/v1/Consulta/cliente";
                    EnvioConsultaFunNeg data = new EnvioConsultaFunNeg
                    {
                        usuario = host,
                        data = new ConsultaFunNeg { funcionarioNegocios = funNeg }
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<SapResponse>(metodo, data);
                    if (response.success)
                    {
                        if (response.message.Substring(0, 2).Equals("00"))
                        {
                            response.message = response.message.Substring(2);
                        }
                    }
                }
                else
                {
                    response.message = "FUNCIONARIO DE NEGOCIOS INVALIDO";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        private string validarSegmento(ParametrosHost host, string funNeg)
        {
            string response = host.sucursal.Trim().PadRight(5, '0');
            try
            {
                SapResponse respuesta = ConsultaFunNeg(host,funNeg);
                if (respuesta.success)
                {
                    if (respuesta.message.Substring(0, 2).Equals("00"))
                    {
                        respuesta.message = respuesta.message.Substring(2);
                    }
                    string segmento = respuesta.message.Substring(30, 6).Replace("-", "");
                    if (segmento.Trim().Length == 5 && ManagerValidation.isNumeric(segmento))
                    {
                        response = segmento;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        #endregion

        #region SECCION: FATCA
        public RespuestaConsultaFatca ConsultaFacta(string id, string tid, string cic, Sesion sesion)
        {
            RespuestaConsultaFatca response = new RespuestaConsultaFatca();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    string metodo = "Fatca/api/Consulta/fatca/consulta";
                    EnvioConsultaFatca data = new EnvioConsultaFatca
                    {
                        cic = cic,
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1],
                        usuario = sesion.host
                    };
                    data.usuario.usuarioExtra = sesion.usuario.usuario;
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<RespuestaConsultaFatca>(metodo, data);
                }
                else
                {
                    response.state = "88";
                    response.message = "FORMATO DE IDC INVALIDO";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse CreacionFacta(string id, string tid, Fatca facta, Sesion sesion)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    facta.idcNumero = idC[0];
                    facta.idcTipo = tid;
                    facta.idcExtension = idC[1];
                    facta.razonSocial = "";
                    string metodo = "Fatca/api/Registro/fatca/registro";
                    EnviarCreacionFatca data = new EnviarCreacionFatca
                    {
                        usuario = sesion.host,
                        data = facta
                    };
                    data.usuario.usuarioExtra = sesion.usuario.usuario;
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    resultado = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    resultado.state = "88";
                    resultado.message = "IDC INVALIDO";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion

        #region SECCION: DIRECCION
        public RespuestaConsultaDireccion BusquedaDireccionPNJ(ParametrosHost usuario, string[] idC, string tid)
        {
            RespuestaConsultaDireccion resultado = new RespuestaConsultaDireccion();
            try
            {
                string metodo = "Direccion/api/Consulta/consulta/p";
                EnvioConsultaDireccion data = new EnvioConsultaDireccion
                {
                    usuario = usuario
                };
                string[] pn = { "P", "Q", "S", "Y" };
                if (pn.Contains(tid))
                {
                    metodo = metodo + "n";
                    data.cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                else
                {
                    metodo = metodo + "j";
                    data.cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                resultado = _consumirAPI.consulta<RespuestaConsultaDireccion>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public SapResponse OperacionesDireccionPNJ(ParametrosHost usuario, string[] idC, string tid, DatosDireccion datos, bool registro)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                string metodo = "Direccion/api/";
                if (registro)
                {
                    metodo = metodo + "Creacion/creacion/p";
                }
                else
                {
                    metodo = metodo + "Actualizacion/actualizacion/p";
                }
                EnvioOperacionDireccion data = new EnvioOperacionDireccion
                {
                    data = datos,
                    usuario = usuario
                };
                string[] pn = { "P", "Q", "S", "Y" };
                if (pn.Contains(tid))
                {
                    metodo = metodo + "n";
                    data.cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                else
                {
                    metodo = metodo + "j";
                    data.cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public SapResponse ModificarCorrespondencia(ParametrosHost usuario, string[] idC, string tid, int direccion)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                string metodo = "Direccion/api/Actualizacion/correspondencia/p";
                EnvioDireccionCorrespondencia data = new EnvioDireccionCorrespondencia
                {
                    data = new EnvioDireccionCorrespondenciaData { idDireccion = direccion },
                    usuario = usuario
                };
                string[] pn = { "P", "Q", "S", "Y" };
                if (pn.Contains(tid))
                {
                    metodo = metodo + "n";
                    data.cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                else
                {
                    metodo = metodo + "j";
                    data.cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion

        #region SECCION: REC
        public RespuestaConsultaRec ConsultaRecPNJ(ParametrosHost usuario, string[] idC, string tid)
        {
            RespuestaConsultaRec resultado = new RespuestaConsultaRec();
            try
            {
                string metodo = "Rec/api/Busqueda/p";
                EnvioConsultaRec data = new EnvioConsultaRec
                {
                    usuario = usuario
                };
                string[] pn = { "P", "Q", "S", "Y" };
                if (pn.Contains(tid))
                {
                    metodo = metodo + "n";
                    data.cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                else
                {
                    metodo = metodo + "j";
                    data.cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<RespuestaConsultaRec>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public SapResponse OperacionesRecPNJ(ParametrosHost usuario, string[] idC, string tid, DatosRec datos, bool registro)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                string[] idE = ManagerValidation.obtenerIDC(datos.idcPersona, datos.tipoDocumentoPersona);
                if (!idE.Contains("?"))
                {
                    datos.idcPersona = idE[0];
                    datos.extensionPersona = idE[1];
                    datos = LimpiarDatos.limpiarNulo<DatosRec>(datos);
                    #region VALIDAR FECHAS
                    if (datos.fechaInicio.Replace("/", "").Replace("-", "").Length != 8)
                    {
                        datos.fechaInicio = string.Empty;
                    }
                    else if (int.Parse(datos.fechaInicio.Replace("/", "").Replace("-", "").Substring(4, 4)) < 578)
                    {
                        datos.fechaInicio = string.Empty;
                    }
                    if (datos.fechaTermino.Replace("/", "").Replace("-", "").Length != 8)
                    {
                        datos.fechaTermino = string.Empty;
                    }
                    else if (int.Parse(datos.fechaTermino.Replace("/", "").Replace("-", "").Substring(4, 4)) < 578)
                    {
                        datos.fechaTermino = string.Empty;
                    }
                    if (datos.fechaVerificacion.Replace("/", "").Replace("-", "").Length != 8)
                    {
                        datos.fechaVerificacion = string.Empty;
                    }
                    else if (int.Parse(datos.fechaVerificacion.Replace("/", "").Replace("-", "").Substring(4, 4)) < 578)
                    {
                        datos.fechaVerificacion = string.Empty;
                    }
                    if (datos.fechaValor.Replace("/", "").Replace("-", "").Length != 8)
                    {
                        datos.fechaValor = string.Empty;
                    }
                    else if (int.Parse(datos.fechaValor.Replace("/", "").Replace("-", "").Substring(4, 4)) < 578)
                    {
                        datos.fechaValor = string.Empty;
                    }
                    #endregion
                    string metodo = "Rec/api/";
                    if (registro)
                    {
                        metodo = metodo + "Registro/p";
                    }
                    else
                    {
                        metodo = metodo + "Actualizacion/p";
                    }
                    EnvioOperacionesRec data = new EnvioOperacionesRec
                    {
                        data = datos,
                        usuario = usuario
                    };
                    string[] pn = { "P", "Q", "S", "Y" };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioDocumentoPN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioDocumentoPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    resultado = _consumirAPI.consulta<SapResponse>(metodo, data);
                    /*temporal*/
                    /* if (resultado.success)
                     {
                         resultado = _consumirAPI.consulta<SapResponse>(metodo, data);
                     }*/
                    /*fin*/
                }
                else
                {
                    resultado = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public SapResponse EliminarRecPNJ(Sesion sesion, string id, string tid, string idRel, string tidRel, string codRelacion)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                string[] idE = ManagerValidation.obtenerIDC(idRel, tidRel);

                if (!idC.Contains("?") && !idE.Contains("?"))
                {
                    string metodo = "Rec/api/Eliminacion/p";
                    string[] pn = { "P", "Q", "S", "Y" };
                    EnvioEliminacionRec data = new EnvioEliminacionRec
                    {
                        usuario = sesion.host,
                        data = new DatosPersonaRec
                        {
                            codigoRelacion = codRelacion,
                            idcPersona = idE[0],
                            extensionPersona = idE[1],
                            tipoDocumentoPersona = tidRel
                        }
                    };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioDocumentoPN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioDocumentoPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INCORRECTO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        #endregion

        #region SECCION: EMP
        public RespuestaEmpleadorBusqueda BusquedaEmpPNJ(Sesion sesion, string id, string tid)
        {
            RespuestaEmpleadorBusqueda respuesta = new RespuestaEmpleadorBusqueda();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);

                if (!idC.Contains("?"))
                {
                    string metodo = "Emp/empleador/busqueda/p";
                    string[] pn = { "P", "Q", "S", "Y" };
                    EnvioBusquedaEmpleador data = new EnvioBusquedaEmpleador
                    {
                        data = new DatosEnvioEmpleador(),
                        usuario = sesion.host
                    };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioDocumentoPN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioDocumentoPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<RespuestaEmpleadorBusqueda>(metodo, data);
                }
                else
                {
                    respuesta = new RespuestaEmpleadorBusqueda
                    {
                        state = "88",
                        message = "FORMATO DE IDC INCORRECTO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse OperacionesEmpPNJ(Sesion sesion, string[] idC, string tid, string[] idCEmp, string etid, DatosEmpleador datos, bool registro)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string metodo = "Emp/empleador/";
                if (registro)
                {
                    metodo = metodo + "registro/p";
                }
                else
                {
                    metodo = metodo + "actualizacion/p";
                }
                string[] pn = { "P", "Q", "S", "Y" };
                EnvioModificacionRegistroEmpleador data = new EnvioModificacionRegistroEmpleador
                {
                    data = datos,
                    empleador = new DatosEnvioEmpleador
                    {
                        empleadorIdcNumero = idCEmp[0],
                        empleadorIdcTipo = etid,
                        empleadorIdcExtension = idCEmp[1]
                    },
                    usuario = sesion.host
                };
                if (pn.Contains(tid))
                {
                    metodo = metodo + "n";
                    data.cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                else
                {
                    metodo = metodo + "j";
                    data.cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = tid,
                        idcExtension = idC[1]
                    };
                }
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        #endregion

        #region SECCION: AEP
        public RespuestaConsultaAep BusquedaAepPNJ(Sesion sesion, string id, string tid, string fecha)
        {
            RespuestaConsultaAep respuesta = new RespuestaConsultaAep();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    string metodo = "Aep/api/busqueda/p";
                    string[] pn = { "P", "Q", "S", "Y" };
                    string fechValor = LimpiarDatos.formatoFecha(fecha);
                    EnvioBusquedaAep data = new EnvioBusquedaAep
                    {
                        usuario = sesion.host
                    };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioClientePN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            fechaValor = fechValor
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioClientePJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            fechaValor = fechValor
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<RespuestaConsultaAep>(metodo, data);
                }
                else
                {
                    respuesta = new RespuestaConsultaAep
                    {
                        state = "88",
                        message = "FORMATO DE IDC INCORRECTO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse OperacionesAepPNJ(Sesion sesion, string id, string tid, string fecha, DatosAep datos, bool registro)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    string metodo = "Aep/api/";
                    if (registro)
                    {
                        metodo = metodo + "Registro/p";
                    }
                    else
                    {
                        metodo = metodo + "Actualizacion/p";
                    }
                    string[] pn = { "P", "Q", "S", "Y" };
                    string fechValor = LimpiarDatos.formatoFecha(fecha);
                    datos.fechaValor = fechValor;
                    EnvioOperacionAep data = new EnvioOperacionAep
                    {
                        usuario = sesion.host,
                        data = datos
                    };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioDocumentoPN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioDocumentoPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1]
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<RespuestaConsultaAep>(metodo, data);
                }
                else
                {
                    respuesta = new RespuestaConsultaAep
                    {
                        state = "88",
                        message = "FORMATO DE IDC INCORRECTO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        public SapResponse EliminarAepPNJ(Sesion sesion, string id, string tid, string fecha)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);

                if (!idC.Contains("?"))
                {
                    string metodo = "Aep/api/Eliminacion/p";
                    string[] pn = { "P", "Q", "S", "Y" };
                    string fechValor = LimpiarDatos.formatoFecha(fecha);
                    EnvioBusquedaAep data = new EnvioBusquedaAep
                    {
                        usuario = sesion.host
                    };
                    if (pn.Contains(tid))
                    {
                        metodo = metodo + "n";
                        data.cliente = new EnvioClientePN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            fechaValor = fechValor
                        };
                    }
                    else
                    {
                        metodo = metodo + "j";
                        data.cliente = new EnvioClientePJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            fechaValor = fechValor
                        };
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "88",
                        message = "FORMATO DE IDC INCORRECTO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        #endregion

        #region SECCION: CUENTA
        private RespuestaCreacionCuenta AperturaProducto(SeguimientoProcesos pasos, object cuenta, ParametrosHost usuario,string sucursal,string agencia ,string metodo)
        {
            RespuestaCreacionCuenta respuesta = new RespuestaCreacionCuenta();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(pasos.detalleCliente.First().idC, pasos.detalleCliente.First().idT);

                if (!idC.Contains("?"))
                {
                    #region ENVIAR A HOST LA SUCURSAL-AGENCIA DEL FORMULARIO
                    bool cambiarSucAge = ManagerValidation.validarSucursalAgencia(sucursal, agencia);
                    if (cambiarSucAge)
                    {
                        usuario.sucursal = sucursal;
                        usuario.agencia = agencia;
                    }
                    #endregion
                    EnvioCreacionCuenta data = new EnvioCreacionCuenta
                    {
                        usuario = usuario,
                        cuenta = cuenta
                    };
                    if (pasos.tipoPersona.Equals("J"))
                    {
                        data.cliente = new EnvioDocumentoPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = pasos.detalleCliente[0].idT,
                            idcExtension = idC[1]
                        };
                    }
                    else
                    {
                        data.cliente = new EnvioDocumentoPN
                        {
                            idcNumero = idC[0],
                            idcTipo = pasos.detalleCliente[0].idT,
                            idcExtension = idC[1]
                        };

                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<RespuestaCreacionCuenta>(metodo, data);
                }
                else
                {
                    respuesta = new RespuestaCreacionCuenta
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        #endregion

        #region SECCION: ARCHIVO NEGATIVO - ENAME CHECKER
        public RespuestaRegistro ArchivoNegativo(string id, string tid, string tipo, string paterno, string materno, string nombres)
        {
            RespuestaRegistro response = new RespuestaRegistro();
            try
            {
                if (paterno == null)
                {
                    paterno = string.Empty;
                }
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    if (string.IsNullOrEmpty(paterno))
                    {
                        paterno = string.Empty;
                    }
                    if (string.IsNullOrEmpty(materno))
                    {
                        materno = string.Empty;
                    }
                    if (string.IsNullOrEmpty(nombres))
                    {
                        nombres = string.Empty;
                    }
                    EnvioVerificacionArchivoNegativo data = new EnvioVerificacionArchivoNegativo();
                    if (!tipo.Equals("j"))
                    {
                        data.cliente = new ECANPN
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            apellidoPaterno = paterno.Trim(),
                            apellidoMaterno = materno.Trim(),
                            nombres = nombres.Trim()
                        };
                    }
                    else
                    {
                        data.cliente = new ECANPJ
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            razonSocial = paterno.Trim()
                        };
                    }
                    string metodo = "ArchivoNegativo/api/v1/Verificacion/consulta";
                    if (tipo.Equals("j"))
                    {
                        metodo = metodo + "/pj";
                    }
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<RespuestaRegistro>(metodo, data);
                }
                else
                {
                    response = new RespuestaRegistro
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }

        public RespuestaRegistro EnameChecker(Sesion sesion, string paterno, string materno, string nombres)
        {
            RespuestaRegistro response = new RespuestaRegistro();
            try
            {
                if (paterno == null)
                {
                    paterno = string.Empty;
                }
                EnvioVerificacionEnameChecker data = new EnvioVerificacionEnameChecker();
                if (materno == null)
                {
                    materno = string.Empty;
                }
                if (nombres == null)
                {
                    nombres = string.Empty;
                }
                data.cliente = new EnvioVerificacionEnameCheckerData
                {
                    apellidoPaterno = paterno.Trim(),
                    apellidoMaterno = materno.Trim(),
                    nombres = nombres.Trim(),
                    guid = sesion.guid,
                    ipEquipo = sesion.host.ipOrigen,
                    matriculaUsuario = sesion.credenciales.usuario
                };
                string metodo = "enamechecker/api/v1/Verificacion/consulta";
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaRegistro>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        #endregion

        #region SECCION: FORMULARIO 20190
        public bool F20190(SeguimientoProcesos pasos, int indice, Sesion sesion)
        {
            bool respuesta;
            try
            {
                bool[] isproceso = new bool[3];
                sesion.host.usuarioExtra = sesion.credenciales.usuario;
                AperturaCuenta cuenta = null;
                int firmantes = 1;
                int productos = 0;
                bool apertura = ManagerValidation.validarProceoApertura(pasos.nombreProceso);
                if (pasos.nombreProceso.Equals("Apertura"))
                {
                    firmantes = pasos.detalleCliente.Count;
                }
                if (pasos.detalleCliente[indice].detalleCuenta != null)
                {
                    if (pasos.detalleCliente[indice].detalleCuenta.Count > 0)
                    {
                        cuenta = buscarCuenta(pasos.detalleCliente[indice].detalleCuenta, apertura);
                        if (cuenta != null)
                        {
                            productos = pasos.detalleCliente[indice].detalleCuenta.Count;
                        }
                    }
                }
                int idCuenta = RegistroCuentaSwamp(sesion.guid, sesion.host, pasos, indice, firmantes, productos, cuenta, apertura);
                if (idCuenta > 0)
                {
                    isproceso[0] = true;
                    if (pasos.detalleCliente[indice].detalleCuenta != null)
                    {
                        isproceso[1] = RegistroCuentaProductoSwamp(sesion.guid, pasos.detalleCliente[indice].detalleCuenta, sesion.credenciales.usuario, pasos.tipoPersona, idCuenta, apertura);
                    }
                    else
                    {
                        isproceso[1] = true;
                    }
                    isproceso[2] = RegistroCuentaFirmanteSwamp(sesion.guid, sesion.credenciales.usuario, pasos.detalleCliente[indice], pasos.tipoPersona, idCuenta);
                }
                respuesta = !isproceso.Contains(false);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        private int RegistroCuentaSwamp(string guid, ParametrosHost usuario, SeguimientoProcesos pasos, int indice, int firmantes, int productos, AperturaCuenta cuenta, bool apertura)
        {
            int id = 0;
            RespuestaRegistroSwampCuenta response = new RespuestaRegistroSwampCuenta();
            try
            {
                string metodo = "OperacionesDB/api/Swamp/registro/cuenta";
                DatosCliente cliente = pasos.detalleCliente[indice];
                string[] idc = ManagerValidation.obtenerIDC(cliente.idC, cliente.idT);
                EnvioRegistroSwampCuenta dataCuenta = new EnvioRegistroSwampCuenta
                {
                    usuario = usuario,
                    cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idc[0],
                        idcExtension = idc[1],
                        idcTipo = cliente.idT
                    }
                };
                string indicadorModalidad = string.Empty;
                if (productos > 0)
                {
                    if (cuenta.detalleCuenta.producto[0].Equals('D') || cuenta.cuentaProducto.producto.Equals("ICB"))
                    {
                        indicadorModalidad = ManagerSystematic.indicadorDPF(cuenta.detalleCuenta.producto, cuenta.detalleCuenta.indModalidad, cuenta.detalleCuenta.custodia, cuenta.detalleCuenta.plazo, cuenta.detalleCuenta.conCuenta, cuenta.detalleCuenta.dpfCuentaAfiliada, cuenta.detalleCuenta.dpfTipoCuenta, cuenta.detalleCuenta.tasa);
                    }
                }
                dataCuenta.data = new EnvioRegistroSwampCuentaData();
                dataCuenta.data.ses_guid = guid;
                dataCuenta.data.cta_countfirmantes = 1;
                dataCuenta.data.cta_numfirmantes = firmantes;
                dataCuenta.data.cta_countproductos = productos;
                dataCuenta.data.cta_apertura = apertura;
                dataCuenta.data.cta_clientedelbanco = "S";
                dataCuenta.data.cta_tarjetabancaexclusiva = (pasos.tipoPersona.Equals("N") ? (cliente.pn.tipoCliente.Equals("CX") || cliente.pn.tipoCliente.Equals("VP") ? "S" : "N") : (cliente.pj.tipoCliente.Equals("CX") || cliente.pj.tipoCliente.Equals("VP") ? "S" : "N"));
                dataCuenta.data.cta_appaterno = (pasos.tipoPersona.Equals("N") ? cliente.pn.paterno : "");
                dataCuenta.data.cta_apmaterno = (pasos.tipoPersona.Equals("N") ? cliente.pn.materno : "");
                dataCuenta.data.cta_nombres_razsocial = (pasos.tipoPersona.Equals("N") ? cliente.pn.nombres : cliente.pj.razonSocial);
                dataCuenta.data.cta_codtipobanca = (pasos.tipoPersona.Equals("N") ? cliente.pn.tipoBanca : cliente.pj.tipoBanca);
                dataCuenta.data.cta_direccion = (pasos.tipoPersona.Equals("N") ? cliente.pn.direccion : cliente.pj.direccion);
                dataCuenta.data.cta_localidaddescripcion = cliente.localidadDescripcion;
                dataCuenta.data.cta_telefono = (pasos.tipoPersona.Equals("N") ? cliente.pn.telefono : cliente.pj.telefono);
                dataCuenta.data.cta_tipocuenta = pasos.detalleCliente.Count == 1 || productos == 0 ? "I" : cuenta.detalleCuenta.nombreCuenta.Contains("-O-") ? "N" : cuenta.detalleCuenta.nombreCuenta.Contains("-Y-") ? "C" : "M";
                dataCuenta.data.cta_monto = string.IsNullOrEmpty(cuenta.detalleCuenta.monto) ? "" : cuenta.detalleCuenta.monto;
                dataCuenta.data.cta_ctaaplazoinfo = indicadorModalidad;
                dataCuenta.data.cta_numerocredimas = !string.IsNullOrEmpty(cliente.tarjeta) ? cliente.tarjeta.Replace("-", "") : "";
                dataCuenta.data.cta_codtipotarjetacredimas = string.IsNullOrEmpty(cliente.tarjeta) ? "" : cliente.tarjeta[1].Equals('4') ? "0" : "1";
                dataCuenta.data.cta_gremio = "";
                dataCuenta.data.cta_codciiu = pasos.tipoPersona.Equals("N") ? cliente.pn.ciiu : cliente.pj.ciiu;
                dataCuenta.data.cta_codigosectorista = productos > 0 ? (cuenta.detalleCuenta.producto.Substring(0, 2).Equals("CC") || cuenta.cuentaProducto.producto.Equals("FIS") ? (cuenta.detalleCuenta.indModalidad) : "") : "";
                dataCuenta.data.cta_ctaexcinfo = "";
                dataCuenta.data.cta_nomcomerc_nomcuenta = productos > 0 ? cuenta.detalleCuenta.nombreCuenta : (pasos.tipoPersona.Equals("N") ? cliente.pn.paterno + " " + cliente.pn.materno + " " + cliente.pn.nombres : cliente.pj.razonSocial);
                dataCuenta.data.cta_situaciontarjetadescripcion = ManagerValidation.validarSituacionTarjeta(cliente.situacionTarjeta);
                dataCuenta.data.cta_tipooperacioncredimas = string.IsNullOrEmpty(cliente.tarjeta) ? "" : ManagerValidation.validarOperacion(pasos.nombreProceso, cliente.codigoOperacion);
                dataCuenta.data.cta_codSucursalAgencia = !apertura ? usuario.sucursal + usuario.agencia : "";
                this._operacion = ManagerOperation.GenerateOperation(dataCuenta.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuenta));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuenta));
                response = _consumirAPI.consulta<RespuestaRegistroSwampCuenta>(metodo, dataCuenta);
                if (response.success)
                {
                    id = response.data.idCuenta;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return id;
        }
        private bool RegistroCuentaProductoSwamp(string guid, List<AperturaCuenta> detalleCuenta, string matricula, string tipoPersona, int idCuenta, bool apertura)
        {
            SapResponse response = new SapResponse();
            bool[] iscliente = new bool[detalleCuenta.Count];
            try
            {
                AperturaCuenta cuenta;
                string metodo = "OperacionesDB/api/Swamp/registro/producto";
                string indicadorModalidad = string.Empty;
                for (int i = 0; i < detalleCuenta.Count; i++)
                {
                    cuenta = detalleCuenta[i];
                    EnvioRegistroSwampCuentaProducto dataCuentaProducto = new EnvioRegistroSwampCuentaProducto { usuario = matricula };
                    bool nueva = true;
                    indicadorModalidad = cuenta.detalleCuenta.indModalidad;
                    if (cuenta.detalleCuenta.producto[0].Equals('D'))
                    {
                        indicadorModalidad = ManagerSystematic.indicadorDPF(cuenta.detalleCuenta.producto, cuenta.detalleCuenta.indModalidad, cuenta.detalleCuenta.custodia, cuenta.detalleCuenta.plazo, cuenta.detalleCuenta.conCuenta, cuenta.detalleCuenta.dpfCuentaAfiliada, cuenta.detalleCuenta.dpfTipoCuenta, cuenta.detalleCuenta.tasa);
                    }
                    if (apertura)
                    {
                        nueva = cuenta.detalleCuenta.apertura;
                    }
                    dataCuentaProducto.data = new EnvioRegistroSwampCuentaProductoData();
                    dataCuentaProducto.data.pro_nueva = nueva;
                    dataCuentaProducto.data.pro_clave = indicadorModalidad;
                    dataCuentaProducto.data.pro_codmoneda = cuenta.detalleCuenta.moneda.Equals("MNA") ? "01" : "02";
                    dataCuentaProducto.data.pro_codtipoproducto = ManagerSystematic.productoSwamp(cuenta.cuentaProducto.producto, cuenta.detalleCuenta.producto, tipoPersona, apertura);
                    dataCuentaProducto.data.pro_monto = cuenta.detalleCuenta.monto;
                    dataCuentaProducto.data.pro_numerocuenta = string.IsNullOrEmpty(cuenta.detalleCuenta.motivoCuenta) ? cuenta.detalleCuenta.nroCuenta : cuenta.detalleCuenta.nroCuenta + "|" + cuenta.detalleCuenta.motivoCuenta;
                    dataCuentaProducto.data.pro_subcodtipoproducto = ManagerSystematic.indiceAHS(cuenta.detalleCuenta.producto);
                    dataCuentaProducto.data.pro_tipodpf = "";
                    dataCuentaProducto.data.pro_tipoplazo = "";
                    dataCuentaProducto.data.ses_guid = guid;
                    dataCuentaProducto.data.cta_id = idCuenta;

                    this._operacion = ManagerOperation.GenerateOperation(dataCuentaProducto.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuentaProducto));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuentaProducto));
                    response = _consumirAPI.consulta<SapResponse>(metodo, dataCuentaProducto);
                    iscliente[i] = response.success;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return !iscliente.Contains(false);
        }
        private bool RegistroCuentaFirmanteSwamp(string guid, string usuario, DatosCliente cliente, string tipoPersona, int id)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "OperacionesDB/api/Swamp/registro/firmante";
                string[] idc;
                idc = ManagerValidation.obtenerIDC(cliente.idC, cliente.idT);
                EnvioRegistroSwampCuentaFirmante dataCuentaFirmante = new EnvioRegistroSwampCuentaFirmante
                {
                    usuario = usuario,
                    cliente = new EnvioDocumentoPN
                    {
                        idcNumero = idc[0],
                        idcExtension = idc[1],
                        idcTipo = cliente.idT
                    }
                };
                dataCuentaFirmante.data = new EnvioRegistroSwampFirmanteData
                {
                    fir_actualizadatos = tipoPersona.Equals("N") ? (LimpiarDatos.validarFechaUltimaActualizacion(cliente.pn.fechaUltActualizacion).Equals("AC")) : (LimpiarDatos.validarFechaUltimaActualizacion(cliente.pj.fechaUltActualizacion).Equals("AC")),
                    fir_clientenuevo = tipoPersona.Equals("N") ? cliente.pn.tipoCliente.Equals("NC") : cliente.pj.tipoCliente.Equals("NC"),
                    fir_appaterno = tipoPersona.Equals("N") ? cliente.pn.paterno : "",
                    fir_apmaterno = tipoPersona.Equals("N") ? cliente.pn.materno : "",
                    fir_fechanac = tipoPersona.Equals("N") ? cliente.pn.fechaNacimiento : cliente.pj.fechaConstitucion,
                    fir_estadocivil = string.IsNullOrEmpty(cliente.estadoCivilDescripcion) ? "" : cliente.estadoCivilDescripcion,
                    fir_nombres_razsocial = tipoPersona.Equals("N") ? cliente.pn.nombres : cliente.pj.razonSocial,
                    fir_numerocredimas = !string.IsNullOrEmpty(cliente.tarjeta) ? cliente.tarjeta.Replace("-", "") : "",
                    ses_guid = guid,
                    cta_id = id
                };
                this._operacion = ManagerOperation.GenerateOperation(dataCuentaFirmante.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuentaFirmante));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(dataCuentaFirmante));
                response = _consumirAPI.consulta<SapResponse>(metodo, dataCuentaFirmante);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response.success;
        }

        #endregion

        #region SECCION: SMART LINK
        public RespuestaSLGenerico SLCuenta(string[] cuentas, string tarjeta, string nombreTarjeta)
        {
            RespuestaSLGenerico response = new RespuestaSLGenerico();
            try
            {
                string metodo = "OperacionesDb/api/SmartLink/cuenta";
                bool[] completado = new bool[cuentas.Length];
                for (int i = 0; i < cuentas.Length; i++)
                {
                    if (!string.IsNullOrEmpty(cuentas[i]))
                    {
                        EnvioSLCuenta data = new EnvioSLCuenta
                        {
                            data = new SLCuenta
                            {
                                nombreCliente = nombreTarjeta,
                                tarjeta = tarjeta,
                                cuenta = cuentas[i]
                            }
                        };
                        this._operacion = ManagerOperation.GenerateOperation(data.operation);
                        Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                        Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                        response = _consumirAPI.consulta<RespuestaSLGenerico>(metodo, data);
                        completado[i] = response.success;
                    }
                    else
                    {
                        completado[i] = true;
                    }
                }
                response.success = !completado.Contains(false);
                if (response.success)
                {
                    response.state = "00";
                    response.message = "NO SE PUDO EJECUTAR EL PORCESO";
                }
                else
                {
                    response.state = "01";
                    response.message = "PROCESO EJECUTADO CORRECTAMENTE";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaSLGenerico SLActualizacion(string[] cuentas, string tarjeta)
        {
            RespuestaSLGenerico response = new RespuestaSLGenerico();
            try
            {
                string metodo = "OperacionesDb/api/SmartLink/actualizacion";
                EnvioSLActualizacion data = new EnvioSLActualizacion
                {
                    data = new SLActualizacion
                    {
                        tarjeta = tarjeta.Replace("-", ""),
                        cuenta_ch_mn = cuentas[0].Replace("-", ""),
                        cuenta_ch_me = cuentas[1].Replace("-", ""),
                        cuenta_cc_mn = cuentas[2].Replace("-", ""),
                        cuenta_cc_me = cuentas[3].Replace("-", "")
                    }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaSLGenerico>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaSLGenerico SLDesafiliar(string cuenta, string tarjetaAnterior)
        {
            RespuestaSLGenerico respuesta = new RespuestaSLGenerico();
            try
            {
                string metodoC = "OperacionesDb/api/SmartLink/desafiliacion";
                EnvioSLDesafiliar data = new EnvioSLDesafiliar
                {
                    data = new SLDesafiliar
                    {
                        tarjeta = tarjetaAnterior,
                        cuenta = cuenta
                    }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<RespuestaSLGenerico>(metodoC, data);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public SapResponse SLValidarPin(string tarjeta, string pin, string terminal, string nroTerminal)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "OperacionesDb/api/SmartLink/validacion";
                EnvioSLValidar data = new EnvioSLValidar
                {
                    data = new SLValidarData
                    {
                        tarjeta = tarjeta,
                        pin = pin,
                        terminal = terminal,
                        numeroTerminal = nroTerminal
                    }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public void SLCambio(string[] cuentas, string tarjetaAnterior, string tarjeta)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "OperacionesDb/api/SmartLink/cambio";
                EnvioSLCambio data = new EnvioSLCambio
                {
                    data = new SLCambioData
                    {
                        tarjetaAntigua = tarjetaAnterior,
                        tarjetaNueva = tarjeta,
                        cuenta_ch_mn = cuentas[0],
                        cuenta_ch_me = cuentas[1],
                        cuenta_cc_mn = cuentas[2],
                        cuenta_cc_me = cuentas[3]
                    }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            //return resultado;
        }
        #endregion

        #region SECCION: TARJETA
        public RespuestaBusquedaTarjeta BusquedaTarjeta(Sesion sesion, string nombre)
        {
            RespuestaBusquedaTarjeta consulta = new RespuestaBusquedaTarjeta();
            try
            {
                string metodo = "Tarjeta/tarjeta/busqueda";
                EnvioBusquedaTarjeta data = new EnvioBusquedaTarjeta
                {
                    data = new EnvioNombreTarjeta
                    {
                        nombreTarjeta = nombre
                    },
                    usuario = sesion.host
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                consulta = _consumirAPI.consulta<RespuestaBusquedaTarjeta>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return consulta;
        }
        public RespuestaConsultaTarjeta ConsultaTarjeta(Sesion sesion, string tarjeta, string bancaExclusiva, int indice, ref SeguimientoProcesos pasos)
        {
            RespuestaConsultaTarjeta resultado = new RespuestaConsultaTarjeta();
            try
            {
                string metodo0 = "Tarjeta/tarjeta/consulta";
                EnvioConsultaTarjeta data = new EnvioConsultaTarjeta
                {
                    data = new EnvioNumeroTarjeta { tarjeta = tarjeta },
                    usuario = sesion.host
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<RespuestaConsultaTarjeta>(metodo0, data);
                if (resultado.success)
                {
                    #region registrar aperturaPas
                    resultado.data = BusinessCuenta.limpiarCuentas(resultado.data);
                    EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                    {
                        tarjeta = tarjeta,
                        direccion = resultado.data.direccion,
                        tipoIDC = resultado.data.tipoIDC,
                        idc = resultado.data.idc,
                        nombreCompleto = resultado.data.nombreCompleto,
                        situacion = resultado.data.situacion,
                        indMenor = int.Parse(LimpiarDatos.edad(resultado.data.fecNac)) > 18 ? "" : "S",
                        indBancaExclusiva = bancaExclusiva == null || bancaExclusiva.Equals("") ? "N" : "S",
                        codTipoOperacion = "CO",
                        matricula = sesion.credenciales.usuario,
                        ahorro1 = resultado.data.flagCta1 ? resultado.data.ahorro1 : "",
                        ahorro2 = resultado.data.flagCta2 ? resultado.data.ahorro2 : "",
                        corriente1 = resultado.data.flagCta3 ? resultado.data.corriente1 : "",
                        corriente2 = resultado.data.flagCta4 ? resultado.data.corriente2 : ""
                    };
                    pasos.detalleCliente[indice].situacionTarjeta = registro.situacion;
                    grabarBD(registro, sesion.host, ref pasos, indice);
                    #endregion
                    #region registrar contrato
                    if (!pasos.registro)
                    {
                        pasos.nMancomuna = 1;
                        string[] pn = { "P", "Q", "S", "Y" };
                        pasos.detalleCliente[indice].filtroAN = false;
                        pasos.detalleCliente[indice].AN = false;
                        pasos.detalleCliente[indice].EC = false;
                        pasos.detalleCliente[indice].flujo = false;
                        pasos.detalleCliente[indice].idC = resultado.data.idc;
                        pasos.detalleCliente[indice].idT = resultado.data.tipoIDC;
                        pasos.detalleCliente[indice].localidadDescripcion = string.Empty;
                        pasos.detalleCliente[indice].estadoCivilDescripcion = string.Empty;
                        pasos.detalleCliente[indice].tarjeta = resultado.data.tarjeta;
                        pasos.detalleCliente[indice].tarjetaAnterior = string.Empty;
                        pasos.detalleCliente[indice].pn = null;
                        pasos.detalleCliente[indice].pj = null;
                        if (pn.Contains(pasos.detalleCliente[indice].idT))
                        {
                            pasos.tipoPersona = "N";
                            pasos.detalleCliente[indice].pn = new PersonaNatural
                            {
                                nombres = resultado.data.nombreCompleto,
                                paterno = "",
                                materno = "",
                                ciiu = "",
                                direccion = resultado.data.direccion,
                                telefono = "",
                                tipoCliente = "CL",
                                fechaNacimiento = resultado.data.fecNac,
                                tipoBanca = "P"
                            };
                        }
                        else
                        {
                            pasos.tipoPersona = "J";
                            pasos.detalleCliente[indice].pj = new PersonaJuridica
                            {
                                razonSocial = resultado.data.nombreCompleto,
                                ciiu = "",
                                direccion = resultado.data.direccion,
                                telefono = "",
                                tipoCliente = "CL",
                                fechaConstitucion = "",
                                tipoBanca = "E"
                            };
                        }
                    }
                    string[] cuentas = { LimpiarDatos.limpiarCuenta(resultado.data.ahorro1), LimpiarDatos.limpiarCuenta(resultado.data.ahorro2), LimpiarDatos.limpiarCuenta(resultado.data.corriente1), LimpiarDatos.limpiarCuenta(resultado.data.corriente2) };
                    for (int i = 0; i < cuentas.Length; i++)
                    {
                        if (!cuentas[i].Equals(""))
                        {
                            AperturaCuenta aperturaCuenta=new AperturaCuenta
                            {
                                detalleCuenta = new DetalleCuenta
                                {
                                    apertura = false,
                                    asignadoTarjeta = true,
                                    nombreCuenta = string.Empty,//resultado.data.nombreTarjeta,
                                    monto = string.Empty,
                                    nroCuenta = LimpiarDatos.formatoCuenta(cuentas[i]),
                                    indModalidad = string.Empty,
                                    moneda = cuentas[i][cuentas[i].Length - 3].Equals('3') ? "MNA" : "USD",
                                    producto = cuentas[i].Length == 14 ? "AHT" : "CCS",
                                    dpfCuentaAfiliada = string.Empty,
                                    conCuenta = string.Empty,
                                    custodia = false,
                                    dpfTipoCuenta = string.Empty,
                                    modoPagoIntereses = string.Empty,
                                    //motivoCuenta = string.Empty,
                                    penalidad = string.Empty,
                                    fechaApertura = resultado.data.fecApertura,
                                    plazo = string.Empty,
                                    renovacion = string.Empty,
                                    tasa = string.Empty,
                                    tiempoMeses = false,
                                    segundoPiso = false
                                },
                                cuentaProducto = new ProductoModalidad
                                {
                                    producto = cuentas[i].Length == 14 ? "AHL" : "CCS",
                                    idModalidad = string.Empty,
                                    codigoModalidad = string.Empty,
                                    codigoProducto = string.Empty
                                }
                            };
                            aperturaCuenta.detalleCuenta.motivoCuenta = motivoCuenta(aperturaCuenta.detalleCuenta.nroCuenta);
                            pasos.detalleCliente[indice].detalleCuenta.Add(aperturaCuenta);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }


        public SapResponse OperacionTarjeta(Sesion sesion, Datos datos, bool afiliacion, string situacion, string bancaExclusiva, int indice, ref SeguimientoProcesos pasos)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                datos = LimpiarDatos.limpiarNulo<Datos>(datos);
                string[] idc = ManagerValidation.obtenerIDC(datos.idcNumero + datos.idcExtension, datos.idcTipo);
                if (!idc.Contains("?"))
                {
                    datos.idcNumero = idc[0];
                    datos.idcExtension = idc[1];
                    string metodo = "Tarjeta/tarjeta/";
                    if (afiliacion)
                    {
                        metodo += "afiliacion";
                    }
                    else
                    {
                        metodo += "apertura";
                    }
                    EnvioOperacionTarjeta data = new EnvioOperacionTarjeta
                    {
                        usuario = sesion.host,
                        data = datos
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
                    if (respuesta.success)
                    {
                        if (datos.confirmado.Equals("S"))
                        {
                            respuesta.message = respuesta.message.Replace(".", "");
                            EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                            {
                                tarjeta = datos.tarjeta.Replace("-", ""),
                                ahorro1 = datos.cuenta_ch_me.Replace("-", ""),
                                ahorro2 = datos.cuenta_ch_mn.Replace("-", ""),
                                corriente1 = datos.cuenta_cc_me.Replace("-", ""),
                                corriente2 = datos.cuenta_cc_mn.Replace("-", ""),
                                direccion = datos.direccion,
                                tipoIDC = datos.idcTipo,
                                idc = datos.idcNumero + datos.idcExtension,
                                nombreCompleto = datos.nombreCompleto,
                                situacion = afiliacion ? situacion : "00",
                                indMenor = int.Parse(LimpiarDatos.edad(datos.fechaNacimiento)) > 18 ? "" : "S",
                                indBancaExclusiva = bancaExclusiva == null || bancaExclusiva.Equals("") ? "N" : "S",
                                codTipoOperacion = afiliacion ? "MO" : "AP",
                                flagActualizarDatos = afiliacion ? "" : (pasos.tipoPersona.ToUpper().Equals("N") ? (LimpiarDatos.validarFechaUltimaActualizacion(pasos.detalleCliente[indice].pn.fechaUltActualizacion).Equals("AC") ? "S" : "N") : (LimpiarDatos.validarFechaUltimaActualizacion(pasos.detalleCliente[indice].pj.fechaUltActualizacion).Equals("AC") ? "S" : "N")),
                                matricula = sesion.credenciales.usuario
                            };
                            pasos.detalleCliente[indice].situacionTarjeta = registro.situacion;
                            grabarBD(registro, sesion.host, ref pasos, indice);

                            #region actualiza datos sesion

                            if (pasos.nombreProceso.Equals("Apertura") || pasos.nombreProceso.Equals("Afiliacion"))
                            {
                                string[] cuentas = { datos.cuenta_ch_mn, datos.cuenta_ch_me, datos.cuenta_cc_mn, datos.cuenta_cc_me };
                                List<int> eliminarCuentas = new List<int>();
                                var detalleCuenta = pasos.detalleCliente[indice].detalleCuenta;
                                if (pasos.nombreProceso.Equals("Apertura"))
                                {
                                    for (int i = 0; i < detalleCuenta.Count; i++)
                                    {
                                        if (cuentas.Contains(detalleCuenta[i].detalleCuenta.nroCuenta))
                                        {
                                            detalleCuenta[i].detalleCuenta.asignadoTarjeta = true;
                                        }
                                        else if (!detalleCuenta[i].detalleCuenta.apertura)
                                        {
                                            eliminarCuentas.Add(i);
                                        }
                                    }
                                }
                                else
                                {
                                    bool[] cuentasExistentes = new bool[cuentas.Length];
                                    string cuentaExistente = string.Empty;
                                    for (int i = 0; i < detalleCuenta.Count; i++)
                                    {
                                        cuentaExistente = detalleCuenta[i].detalleCuenta.nroCuenta.Replace("-", "").Trim();
                                        if (cuentaExistente.Length == 14)
                                        {
                                            if (cuentaExistente[11].Equals('3'))
                                            {
                                                if (string.IsNullOrEmpty(cuentas[0]))
                                                {
                                                    eliminarCuentas.Add(i);
                                                }
                                                else
                                                {
                                                    detalleCuenta[i].detalleCuenta.nroCuenta = cuentas[0];
                                                }
                                                cuentasExistentes[0] = true;
                                            }
                                            else if (cuentaExistente[11].Equals('2'))
                                            {
                                                if (string.IsNullOrEmpty(cuentas[1]))
                                                {
                                                    eliminarCuentas.Add(i);
                                                }
                                                else
                                                {
                                                    detalleCuenta[i].detalleCuenta.nroCuenta = cuentas[1];
                                                }
                                                cuentasExistentes[1] = true;
                                            }
                                        }
                                        else if (cuentaExistente.Length == 13)
                                        {
                                            if (cuentaExistente[10].Equals('3'))
                                            {
                                                if (string.IsNullOrEmpty(cuentas[2]))
                                                {
                                                    eliminarCuentas.Add(i);
                                                }
                                                else
                                                {
                                                    detalleCuenta[i].detalleCuenta.nroCuenta = cuentas[2];
                                                }
                                                cuentasExistentes[2] = true;
                                            }
                                            else if (cuentaExistente[10].Equals('2'))
                                            {
                                                if (string.IsNullOrEmpty(cuentas[3]))
                                                {
                                                    eliminarCuentas.Add(i);
                                                }
                                                else
                                                {
                                                    detalleCuenta[i].detalleCuenta.nroCuenta = cuentas[3];
                                                }
                                                cuentasExistentes[3] = true;
                                            }
                                        }
                                    }
                                    #region adicionar cuentas
                                    if (cuentasExistentes.Contains(false))
                                    {
                                        for (int i = 0; i < cuentas.Length; i++)
                                        {
                                            if (!cuentasExistentes[i] && !string.IsNullOrEmpty(cuentas[i]))
                                            {
                                                detalleCuenta.Add(new AperturaCuenta
                                                {
                                                    detalleCuenta = new DetalleCuenta
                                                    {
                                                        apertura = false,
                                                        asignadoTarjeta = true,
                                                        nombreCuenta = string.Empty,//data.data.nombreCompleto,
                                                        monto = string.Empty,
                                                        nroCuenta = cuentas[i],
                                                        indModalidad = string.Empty,
                                                        moneda = i % 2 == 0 ? "MNA" : "USD",
                                                        producto = i < 2 ? "AHT" : "CCS",
                                                        dpfCuentaAfiliada = string.Empty,
                                                        conCuenta = string.Empty,
                                                        custodia = false,
                                                        dpfTipoCuenta = string.Empty,
                                                        modoPagoIntereses = string.Empty,
                                                        motivoCuenta = motivoCuenta(cuentas[i]),
                                                        penalidad = string.Empty,
                                                        fechaApertura = DateTime.Now.ToString("dd/MM/yyyy"),//revisar
                                                        plazo = string.Empty,
                                                        renovacion = string.Empty,
                                                        tasa = string.Empty,
                                                        tiempoMeses = false,
                                                        segundoPiso = false
                                                    },
                                                    cuentaProducto = new ProductoModalidad
                                                    {
                                                        producto = i < 2 ? "AHL" : "CCS",
                                                        idModalidad = string.Empty,
                                                        codigoModalidad = string.Empty,
                                                        codigoProducto = string.Empty
                                                    }
                                                });
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                if (eliminarCuentas.Count > 0)
                                {
                                    eliminarCuentas = eliminarCuentas.OrderByDescending(x => x).ToList();
                                    for (int i = 0; i < eliminarCuentas.Count; i++)
                                    {
                                        SLDesafiliar(detalleCuenta[eliminarCuentas[i]].detalleCuenta.nroCuenta, datos.tarjeta);
                                        detalleCuenta.RemoveAt(eliminarCuentas[i]);
                                    }
                                }
                                pasos.detalleCliente[indice].detalleCuenta = detalleCuenta;
                            }
                            #endregion

                        }
                    }
                    else
                    {
                        if (respuesta.message == null)
                        {
                            respuesta.message = "NO SE PUDO REALIZAR LA OPERACION";
                        }
                    }
                }
                else
                {
                    respuesta = new SapResponse
                    {
                        state = "77",
                        message = "IDC NO VALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public SapResponse EntregaTarjeta(Sesion sesion, Entrega datos, bool registro, ref SeguimientoProcesos pasos)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodoC = "Tarjeta/tarjeta/entrega/";
                string codigo = string.Empty;
                switch (datos.tipoEntrega)
                {
                    case "1":
                        metodoC = metodoC + "normal";
                        codigo = "EN";
                        break;
                    case "2":
                        metodoC = metodoC + "eliminar";
                        codigo = "EE";
                        break;
                    case "3":
                        metodoC = metodoC + "vb";
                        codigo = "EV";
                        datos.usuarioPassword = LimpiarDatos.EnDecryp(datos.usuarioPassword, true);
                        datos.usuarioAutorizador = LimpiarDatos.EnDecryp(datos.usuarioAutorizador, true);
                        break;
                }
                EnvioEntregaTarjeta data = new EnvioEntregaTarjeta
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodoC, data);
                if (response.success)
                {
                    if (registro)
                    {
                        if (!response.message.Contains("CON VB.DE FUN"))
                        {
                            EnvioTarjetaBDData registroData = new EnvioTarjetaBDData
                            {
                                tarjeta = datos.tarjeta.Replace("-", ""),
                                direccion = datos.direccion,
                                tipoIDC = datos.tipoIDC,
                                idc = datos.idc,
                                nombreCompleto = datos.nombreCompleto,
                                situacion = datos.tipoEntrega.Equals("2") ? "01" : "00",
                                indMenor = int.Parse(LimpiarDatos.edad(datos.fecNac)) > 18 ? "" : "S",
                                indBancaExclusiva = datos.bcex == null || datos.bcex.Equals("") ? "N" : "S",
                                codTipoOperacion = codigo,
                                matricula = sesion.credenciales.usuario,
                                ahorro1 = datos.cuenta_ch_me,
                                ahorro2 = datos.cuenta_ch_mn,
                                corriente1 = datos.cuenta_cc_me,
                                corriente2 = datos.cuenta_cc_mn
                            };
                            pasos.detalleCliente.First().situacionTarjeta = registroData.situacion;
                            grabarBD(registroData, sesion.host, ref pasos);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse CambioTarjeta(Sesion sesion, Cambio datos, ref SeguimientoProcesos pasos)
        {
            SapResponse resultado = new SapResponse();
            try
            {               
                bool continuar = true;
                bool conPin = !datos.cobro;
                Logger.Error("[{0}] ->  REQUEST PC: {1}", this._operacion, conPin);
                Logger.Debug("[{0}] ->  REQUEST PC: {1}", this._operacion, conPin);
                string[] datosBloqueo = pasos.detalleCliente.First().situacionBloqueo.Split('|');
                if (conPin)
                {
                    resultado = SLValidarPin(datos.tarjeta, datos.pinEncriptado, sesion.estacion.terminal, sesion.estacion.numeroTerminal);
                    continuar = resultado.success;
                    datos.cobro = datosBloqueo[1].Equals("S");
                }
                if (continuar)
                {
                    datos.codigoBloqueo = datosBloqueo[0];
                    datos.idc = pasos.detalleCliente.First().idC;
                    datos.tipoIDC = pasos.detalleCliente.First().idT;
                    if (pasos.tipoPersona.Equals("J"))
                    {
                        datos.direccion = pasos.detalleCliente.First().pj.direccion;
                        datos.nombreCompleto = pasos.detalleCliente.First().pj.razonSocial;
                        datos.indMenor = "";
                    }
                    else
                    {
                        datos.direccion = pasos.detalleCliente.First().pn.direccion;
                        datos.nombreCompleto = pasos.detalleCliente.First().pn.paterno + " " + pasos.detalleCliente.First().pn.materno + " " + pasos.detalleCliente.First().pn.nombres;
                        datos.indMenor = int.Parse(LimpiarDatos.edad(pasos.detalleCliente.First().pn.fechaNacimiento)) > 18 ? "" : "S";
                    }
                    string metodoCambio = "Tarjeta/tarjeta/cambio";
                    EnvioCambioTarjeta data = new EnvioCambioTarjeta
                    {
                        usuario = sesion.host,
                        data = datos
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    RespuestaCambioTarjeta respuesta = _consumirAPI.consulta<RespuestaCambioTarjeta>(metodoCambio, data);
                    resultado.success = respuesta.success;
                    if (respuesta.success)
                    {
                        if (!respuesta.data.tipoEntrega.Equals("2"))
                        {
                            if (conPin)
                            {
                                Entrega dataEntrega = new Entrega
                                {
                                    tarjeta = string.Join("", datos.tarjetaNueva.Split('-')),
                                    usuarioAutorizador = LimpiarDatos.EnDecryp(sesion.estacion.usuarioSLK, false),
                                    usuarioPassword = LimpiarDatos.EnDecryp(sesion.estacion.contrasenaSLK, false),
                                    tipoEntrega = "3"
                                };
                                SapResponse resultadoEntrega = EntregaTarjeta(sesion, dataEntrega, false, ref pasos);
                                if (resultadoEntrega.success)
                                {
                                    respuesta.data.tipoEntrega = "1";
                                }
                            }
                            #region Parsear cuentas
                            string[] cuentas = new string[4];
                            var detalleCuenta = pasos.detalleCliente.Last().detalleCuenta;
                            for (int i = 0; i < detalleCuenta.Count; i++)
                            {
                                if (detalleCuenta[i].detalleCuenta.producto[0].Equals('A'))
                                {
                                    if (detalleCuenta[i].detalleCuenta.moneda.Equals("USD"))
                                    {
                                        cuentas[0] = detalleCuenta[i].detalleCuenta.nroCuenta;
                                    }
                                    else
                                    {
                                        cuentas[1] = detalleCuenta[i].detalleCuenta.nroCuenta;
                                    }
                                }
                                else
                                {
                                    if (detalleCuenta[i].detalleCuenta.moneda.Equals("USD"))
                                    {
                                        cuentas[2] = detalleCuenta[i].detalleCuenta.nroCuenta;
                                    }
                                    else
                                    {
                                        cuentas[3] = detalleCuenta[i].detalleCuenta.nroCuenta;
                                    }
                                }
                            }
                            #endregion
                            EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                            {
                                tarjeta = datos.tarjetaNueva,
                                direccion = datos.direccion,
                                tipoIDC = datos.tipoIDC,
                                idc = datos.idc,
                                nombreCompleto = datos.nombreCompleto,
                                situacion = respuesta.data.tipoEntrega.Equals("0") ? "07" : "00",
                                indMenor = datos.indMenor,
                                indBancaExclusiva = "N",
                                codTipoOperacion = respuesta.data.tipoEntrega.Equals("0") ? "TT" : "CA",
                                matricula = sesion.credenciales.usuario,
                                ahorro1 = cuentas[0],
                                ahorro2 = cuentas[1],
                                corriente1 = cuentas[2],
                                corriente2 = cuentas[3]
                            };
                            pasos.detalleCliente.First().situacionTarjeta = registro.situacion;
                            grabarBD(registro, sesion.host, ref pasos);
                            resultado.state = respuesta.state;
                            if (respuesta.data.tipoEntrega.Equals("0"))
                            {
                                resultado.message = "OK";//"LA TARJETA DEBERA SER ENTREGADA EL DIA DE MAÑANA";//mensaje oculto a peticion de plataforma
                            }
                            else if (conPin)
                            {
                                resultado.message = "LA TARJETA YA FUE ACTIVADA";                           
                                
                            }
                            else
                            {
                                resultado.message = "LA TARJETA PUEDE SER ENTREGADA INMEDIATAMENTE";
                                //pasos.detalleCliente.First().situacionTarjeta = "TARJETA EN TRAMITE";//"POR ASIGNAR"
                            }
                        }
                        else
                        {
                            resultado.state = "101";
                            resultado.message = "SE DEBE ELIMINAR LA ENTREGA DE TARJETA";
                            pasos.detalleCliente.First().situacionTarjeta = "01";
                        }
                        pasos.detalleCliente.First().tarjeta = LimpiarDatos.credimas(datos.tarjetaNueva);
                        pasos.detalleCliente.First().tarjetaAnterior = LimpiarDatos.credimas(datos.tarjeta);
                    }
                    else
                    {
                        resultado.state = "99";
                        resultado.message = respuesta.message;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public RespuestaBloqueo BloqueoTarjeta(Sesion sesion, Bloqueo datos, ref SeguimientoProcesos pasos)
        {
            RespuestaBloqueo resultado = new RespuestaBloqueo();
            try
            {
                bool continuar = true;
                if (datos.tipoBloqueo.Equals("12"))
                {
                    datos.tipoBloqueo = "06";
                }
                if (!string.IsNullOrEmpty(datos.pin))
                {
                    SapResponse validarPin = SLValidarPin(datos.tarjeta, datos.pin, sesion.estacion.terminal, sesion.estacion.numeroTerminal);
                    continuar = validarPin.success;
                }
                if (continuar)
                {
                    string metodoC = "Tarjeta/tarjeta/bloqueo";
                    EnvioBloqueoTarjeta data = new EnvioBloqueoTarjeta
                    {
                        usuario = sesion.host,
                        data = datos
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    resultado = _consumirAPI.consulta<RespuestaBloqueo>(metodoC, data);
                    //resultado = new RespuestaBloqueo { success = true, message = "OPERACION OK",data=new RespuestaBloqueoData { codBloqueo = "02390" } };
                    if (resultado.success)
                    {
                        resultado.data.fechaBloqueo = DateTime.Now.ToString("dd/MM/yyyy");
                        resultado.data.oficinaBloqueo = data.usuario.sucursal + data.usuario.agencia;
                        EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                        {
                            tarjeta = datos.tarjeta,
                            direccion = datos.direccion,
                            tipoIDC = datos.tipoIDC,
                            idc = datos.idc,
                            nombreCompleto = datos.nombreCompleto,
                            situacion = datos.tipoBloqueo,
                            indMenor = int.Parse(LimpiarDatos.edad(datos.fecNac)) > 18 ? "" : "S",
                            indBancaExclusiva = datos.bcex == null || datos.bcex.Equals("") ? "N" : "S",
                            codTipoOperacion = "BL",
                            matricula = sesion.credenciales.usuario,
                            ahorro1 = datos.cuenta_ch_me,
                            ahorro2 = datos.cuenta_ch_mn,
                            corriente1 = datos.cuenta_cc_me,
                            corriente2 = datos.cuenta_cc_mn
                        };
                        pasos.detalleCliente.First().situacionTarjeta = registro.situacion;
                        grabarBD(registro, sesion.host, ref pasos);
                    }
                }
                else
                {
                    resultado = new RespuestaBloqueo
                    {
                        state = "102",
                        message = "NO SE PUDO VALIDAR TARJETA EN SMATLINK"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public SapResponse CambioTarjetaRapido(Sesion sesion, CambioRapido datos, ref SeguimientoProcesos pasos)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                resultado = SLValidarPin(datos.tarjetaAntigua, datos.pin, sesion.estacion.terminal, sesion.estacion.numeroTerminal);
                if (resultado.success)
                {
                    string metodoC = "Tarjeta/tarjeta/cambioRapido";
                    datos.situacionTarjeta = "06";
                    datos.usuarioAutorizador = sesion.estacion.usuarioSLK;
                    datos.passwordAutorizador = sesion.estacion.contrasenaSLK;
                    EnvioCambioRapido data = new EnvioCambioRapido
                    {
                        usuario = sesion.host,
                        data = datos
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    RespuestaCambioRapidoTarjeta consulta = _consumirAPI.consulta<RespuestaCambioRapidoTarjeta>(metodoC, data);
                    resultado.success = consulta.success;
                    resultado.state = consulta.state;
                    resultado.message = consulta.message;
                    if (consulta.success)
                    {
                        #region REGISTRO PASOS
                        pasos = new SeguimientoProcesos
                        {
                            nombreProceso = "Cambio Rapido",
                            detalleCliente = new List<DatosCliente>()
                        };
                        guardarDatosConsultaTarjeta(consulta.data, ref pasos);
                        #endregion
                        #region Parsear cuentas
                        string[] cuentas = new string[4];
                        var detalleCuenta = pasos.detalleCliente.Last().detalleCuenta;
                        for (int i = 0; i < detalleCuenta.Count; i++)
                        {
                            if (detalleCuenta[i].detalleCuenta.producto[0].Equals('A'))
                            {
                                if (detalleCuenta[i].detalleCuenta.moneda.Equals("USD"))
                                {
                                    cuentas[0] = detalleCuenta[i].detalleCuenta.nroCuenta.Replace("-", "");
                                }
                                else
                                {
                                    cuentas[1] = detalleCuenta[i].detalleCuenta.nroCuenta.Replace("-", "");
                                }
                            }
                            else
                            {
                                if (detalleCuenta[i].detalleCuenta.moneda.Equals("USD"))
                                {
                                    cuentas[2] = detalleCuenta[i].detalleCuenta.nroCuenta.Replace("-", "");
                                }
                                else
                                {
                                    cuentas[3] = detalleCuenta[i].detalleCuenta.nroCuenta.Replace("-", "");
                                }
                            }
                        }
                        #endregion
                        #region SMART LINK
                        SLCambio(cuentas, datos.tarjetaAntigua, datos.tarjetaNueva);
                        #endregion   
                        #region BD APERTURAS PAS
                        EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                        {
                            tarjeta = datos.tarjetaAntigua,
                            direccion = consulta.data.direccion,
                            tipoIDC = consulta.data.tipoIDC,
                            idc = consulta.data.idc,
                            nombreCompleto = consulta.data.nombreCompleto,
                            situacion = consulta.data.tipoBloqueo,
                            indMenor = int.Parse(LimpiarDatos.edad(consulta.data.fecNac)) > 18 ? "" : "S",
                            indBancaExclusiva = "N",
                            codTipoOperacion = "BL",
                            matricula = sesion.credenciales.usuario,
                            ahorro1 = cuentas[0],
                            ahorro2 = cuentas[1],
                            corriente1 = cuentas[2],
                            corriente2 = cuentas[3]
                        };
                        pasos.detalleCliente.First().situacionTarjeta = registro.situacion;
                        grabarBD(registro, sesion.host, ref pasos);
                        if (!resultado.success.Equals("205"))
                        {
                            registro.tarjeta = datos.tarjetaNueva;
                            registro.codTipoOperacion = "CR";
                            grabarBD(registro, sesion.host, ref pasos);
                        }
                        #endregion
                        #region BD_SWAMP
                        if (!string.IsNullOrEmpty(sesion.guid))
                        {
                            F20190(pasos, 0, sesion);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public RespuestaRelacionTarjeta TarjetasRelacionadas(Relacion datos, Sesion sesion)
        {
            RespuestaRelacionTarjeta response = new RespuestaRelacionTarjeta();
            try
            {
                string metodo = "Tarjeta/tarjeta/relacion";
                EnvioRelacionTarjeta data = new EnvioRelacionTarjeta
                {
                    data = datos,
                    usuario = sesion.host
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaRelacionTarjeta>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse BancaExclusiva(Sesion sesion, BancaExclusivaData datos, string tipo, ref SeguimientoProcesos pasos)
        {
            SapResponse resultado = new SapResponse();
            try
            {
                string metodoC = "Tarjeta/";
                switch (tipo)
                {
                    case "D":
                        metodoC += "bcex/duplicado";
                        break;
                    case "B":
                        metodoC += "bcex/depurar";
                        break;
                    case "A":
                        metodoC += "bcex/ingreso";
                        break;
                }
                BancaExclusiva data = new BancaExclusiva
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                resultado = _consumirAPI.consulta<SapResponse>(metodoC, data);
                if (resultado.success)
                {
                    EnvioTarjetaBDData registro = new EnvioTarjetaBDData
                    {
                        tarjeta = datos.tarjeta,
                        direccion = datos.direccion,
                        tipoIDC = datos.tipoIDC,
                        idc = datos.idc,
                        nombreCompleto = datos.nombreCompleto,
                        situacion = datos.situacion,
                        indMenor = int.Parse(LimpiarDatos.edad(datos.fecNac)) > 18 ? "" : "S",
                        indBancaExclusiva = "S",
                        codTipoOperacion = "EX",
                        matricula = sesion.credenciales.usuario,
                        ahorro1 = datos.cuenta_ch_me,
                        ahorro2 = datos.cuenta_ch_mn,
                        corriente1 = datos.cuenta_cc_me,
                        corriente2 = datos.cuenta_cc_mn
                    };
                    pasos.detalleCliente.First().situacionTarjeta = registro.situacion;
                    grabarBD(registro, sesion.host, ref pasos);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public RespuestaStockTarjeta TarjetaStocks(StockConsulta datos, Sesion sesion)
        {
            RespuestaStockTarjeta response = new RespuestaStockTarjeta();
            try
            {
                datos.aux = "8";
                string metodo = "Tarjeta/consulta/stock";
                EnvioStockTarjeta data = new EnvioStockTarjeta
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaStockTarjeta>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;

        }
        public SapResponse RecepcionTarjeta(Recepcion datos, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Tarjeta/recepcion/stock";
                datos.aux = "8";
                EnvioRecepcionTarjeta data = new EnvioRecepcionTarjeta
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse ActualizacionTarjeta(ActualizacionData datos, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Tarjeta/actualizacion/stock";
                datos.aux = "8";
                EnvioActualizacionTarjeta data = new EnvioActualizacionTarjeta
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region SECCION: ALS
        public RecibirALSInformeCrediticio ConsultaInformeCrediticioALS(Sesion sesion, AlsInformeCrediticioRequest datos)
        {
            RecibirALSInformeCrediticio respuesta = new RecibirALSInformeCrediticio();
            try
            {
                string metodo = "ALS/api/Producto/als/producto/consulta";
                EnvioALSInformeCrediticio data = new EnvioALSInformeCrediticio
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<RecibirALSInformeCrediticio>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public RecibirALSConsultaAlfabetica ConsultaAlfabeticaALS(Sesion sesion, ALSConsultaAlfabeticaRequestData datos)
        {
            RecibirALSConsultaAlfabetica respuesta = new RecibirALSConsultaAlfabetica();
            try
            {
                string metodo = "ALS/api/Persona/als/persona/consulta";
                EnvioALSConsultaAlfabetica data = new EnvioALSConsultaAlfabetica
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<RecibirALSConsultaAlfabetica>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public RecibirALSProductoCliente ConsultaProductoClienteALS(Sesion sesion, ALSProductoClienteRequestData datos)
        {
            RecibirALSProductoCliente respuesta = new RecibirALSProductoCliente();
            try
            {
                string metodo = "ALS/api/Producto/als/producto/cliente";
                EnvioALSProductoCliente data = new EnvioALSProductoCliente
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<RecibirALSProductoCliente>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return (respuesta);
        }
        #endregion

        #region SECCION: PRODUCTO
        public RespuestaConsultaProducto ConsultaProCli(string id, string tid, string ini, string fin, Sesion sesion)
        {
            RespuestaConsultaProducto response = new RespuestaConsultaProducto();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    string metodo = "Producto/api/Busqueda/productos";
                    EnvioConsultaProducto data = new EnvioConsultaProducto
                    {
                        usuario = sesion.host,
                        data = new PaginacionCuenta
                        {
                            idcNumero = idC[0],
                            idcTipo = tid,
                            idcExtension = idC[1],
                            codigoFin = fin,
                            codigoInicio = ini
                        }
                    };
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<RespuestaConsultaProducto>(metodo, data);
                }
                else
                {
                    response = new RespuestaConsultaProducto
                    {
                        state = "88",
                        message = "FORMATO DE IDC INVALIDO"
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public RespuestaConsultaProductoCliente ConsultaCliPro(Sesion sesion, ProductoCliente datos)
        {
            RespuestaConsultaProductoCliente respuesta = new RespuestaConsultaProductoCliente();
            try
            {
                string metodo = "Producto/api/Consulta/cliente";
                EnvioProductoCliente data = new EnvioProductoCliente
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<RespuestaConsultaProductoCliente>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public object ConsultaMoviProductos(string tipo, Cuenta nCuenta, Sesion sesion)
        {
            object respuesta = null;
            try
            {
                string metodo = string.Empty;
                EnvioConsultaCuenta data = new EnvioConsultaCuenta
                {
                    data = nCuenta,
                    usuario = sesion.host
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                switch (tipo)
                {
                    case "0":
                        metodo = metodo + "CajaAhorro/api/Consulta/caja/ahorro/pn/consulta";
                        respuesta = _consumirAPI.consulta<RespuestaMovimientoCajaAhorro>(metodo, data);
                        break;
                    case "1":
                        metodo = metodo + "CuentaCorriente/api/Consulta/productos";
                        respuesta = _consumirAPI.consulta<RespuestaMovimientoCuentaCorriente>(metodo, data);
                        break;
                    case "2":
                        metodo = metodo + "DPF/api/Consulta/dpf";
                        respuesta = _consumirAPI.consulta<RespuestaMovimientoCajaAhorro>(metodo, data);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public SapResponse RelacionarDirProd(EnvioRegistroProductoDireccionData datos, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Producto/api/Relacion/direccion";
                EnvioRegistroProductoDireccion data = new EnvioRegistroProductoDireccion
                {
                    usuario = sesion.host,
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return (response);
        }
        public RespuestaCreacionCuenta AperturaProductoAhorro(CajaAhorro ahorro, ref SeguimientoProcesos pasos, Sesion sesion)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            try
            {
                string[] cic = new string[] { "", "", "" };
                var detalleCuenta = pasos.detalleCliente.First().detalleCuenta.Last();
                for (int i = 0; i < pasos.detalleCliente.Count; i++)
                {
                    cic[i] = pasos.tipoPersona.Equals("J") ? pasos.detalleCliente[i].pj.cic : pasos.detalleCliente[i].pn.cic;
                }
                ahorro.tipoPersona = pasos.tipoPersona;
                ahorro.tipoCuenta = detalleCuenta.cuentaProducto.codigoProducto;
                ahorro.tipoProducto = detalleCuenta.cuentaProducto.codigoModalidad;
                ahorro.nroClientes = pasos.detalleCliente.Count.ToString();
                ahorro.cic = cic[0];
                ahorro.cicSegundoCliente = cic[1];
                ahorro.cicTercerCliente = cic[2];
                ahorro.filtroArchivoNegativo = pasos.detalleCliente.First().AN;
                ahorro.filtroEnameChecker = pasos.detalleCliente.First().EC;
                ahorro.codProducto = this._configuracion.configuracionProducto.codProducto;
                string metodo = "CajaAhorro/api/Creacion/caja/ahorro/p" + pasos.tipoPersona;
                response = AperturaProducto(pasos, ahorro, sesion.host,ahorro.sucursal,ahorro.agencia, metodo);
                if (response.success)
                {
                    string subTipoProducto = ManagerHost.formatoAHSAP(detalleCuenta.cuentaProducto.producto, ahorro.subTipoProducto);
                    Apertura apertura = new Apertura();
                    apertura.codMoneda = ahorro.moneda.Equals("303") ? "01" : "02";
                    apertura.codTipoProducto = subTipoProducto;
                    apertura.numeroCuenta = response.data.nroCuenta.Trim();
                    apertura.nombreCuenta = ahorro.nombreCuenta + "|" + ahorro.motivoCuenta;
                    apertura.codTipoBanca = pasos.tipoPersona.Equals("N") ? "P" : "E";
                    apertura.monto = ahorro.montoInicial;
                    apertura.campoStr1 = ahorro.indModalidad;
                    apertura.campoStr2 = "";
                    apertura.campoStr3 = sesion.credenciales.usuario;
                    //apertura.campoDate1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    apertura.campoInt1 = "0";
                    apertura.campoMoney1 = "0.0";
                    apertura.campoReal1 = "0";

                    guardarResultadoAperCuenta(ref pasos, response, ahorro.moneda, ahorro.nombreCuenta, ahorro.montoInicial, ahorro.indModalidad, ahorro.motivoCuenta, subTipoProducto, DateTime.Now.ToString("dd/MM/yyyy"), string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty, false);
                    SapResponse aperturasPas = RegistroAperturaBD(apertura, pasos.detalleCliente, false, null, sesion);
                    if (aperturasPas.success) { }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public RespuestaCreacionCuenta AperturaProductoCorriente(CuentaCorriente corriente, ref SeguimientoProcesos pasos, Sesion sesion)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            try
            {
                var detallecuenta = pasos.detalleCliente.First().detalleCuenta.Last();
                string[] cic = new string[] { "", "", "" };
                for (int i = 0; i < pasos.detalleCliente.Count; i++)
                {
                    cic[i] = pasos.tipoPersona.Equals("J") ? pasos.detalleCliente[i].pj.cic : pasos.detalleCliente[i].pn.cic;
                }
                corriente.cic = cic[0];
                corriente.cicSegundoCliente = cic[1];
                corriente.cicTercerCliente = cic[2];
                if (pasos.tipoPersona.Equals("J"))
                {
                    corriente.tipoCliente = pasos.detalleCliente.First().pj.tipoCliente;                    
                    corriente.tipoBanca = pasos.detalleCliente.First().pj.tipoBanca;
                    if (detallecuenta.cuentaProducto.producto.Equals("FIS"))
                    {
                        if (string.IsNullOrEmpty(corriente.indicadorMancomunos))
                        {
                            corriente.indicadorMancomunos = string.Empty;
                        }
                        corriente.cicTercerCliente = corriente.indicadorMancomunos.PadRight(8, ' ') + corriente.segmento.Substring(corriente.segmento.Length - 1).PadRight(4, ' ');
                    }
                    corriente.segmento = validarSegmento(sesion.host, pasos.detalleCliente.First().pj.funcionarioNegocios);
                    corriente.ciiu = pasos.detalleCliente.First().pj.ciiu;
                }
                else
                {                    
                    corriente.tipoCliente = pasos.detalleCliente.First().pn.tipoCliente;
                    corriente.tipoBanca = pasos.detalleCliente.First().pn.tipoBanca;
                    corriente.segmento = validarSegmento(sesion.host, pasos.detalleCliente[0].pn.funcionarioNegocios);
                    corriente.ciiu = pasos.detalleCliente.First().pn.ciiu;
                }
                corriente.tipoCuenta = detallecuenta.cuentaProducto.producto;
                corriente.indicadorMancomunos = pasos.detalleCliente.Count > 1 ? "SI" : "NO";
                corriente.nroClientes = pasos.detalleCliente.Count.ToString();
                string metodo = "CuentaCorriente/api/Creacion/cuenta/corriente/p" + pasos.tipoPersona;
                response = AperturaProducto(pasos, corriente, sesion.host,corriente.sucursal,corriente.agencia, metodo);
                if (response.success)
                {
                    Apertura apertura = new Apertura
                    {
                        codMoneda = corriente.moneda.Equals("303") ? "01" : "02",
                        codTipoProducto = detallecuenta.cuentaProducto.producto,
                        numeroCuenta = response.data.nroCuenta.Trim(),
                        nombreCuenta = corriente.nombreCuenta + "|" + corriente.motivoCuenta,
                        codTipoBanca = pasos.tipoPersona.Equals("N") ? "P" : "E",
                        monto = "0",
                        campoStr1 = corriente.indModalidad,
                        campoStr2 = "",
                        campoStr3 = sesion.credenciales.usuario,
                        //campoDate1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        campoInt1 = "0",
                        campoMoney1 = corriente.montoInicial,
                        campoReal1 = "0"
                    };
                    guardarResultadoAperCuenta(ref pasos, response, corriente.moneda, corriente.nombreCuenta, corriente.montoInicial, corriente.indModalidad, corriente.motivoCuenta, detallecuenta.cuentaProducto.producto, DateTime.Now.ToString("dd/MM/yyyy"), string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty, false);
                    SapResponse aperturasPas = RegistroAperturaBD(apertura, pasos.detalleCliente, false, null, sesion);
                    if (aperturasPas.success) { }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public RespuestaCreacionCuenta AperturaProductoDPF(DPF dpf, ref SeguimientoProcesos pasos, Sesion sesion)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            try
            {
                var detallecuenta = pasos.detalleCliente.First().detalleCuenta.Last();
                bool conContrato = false;
                #region COMPLETAR DATOS NULOS
                if (string.IsNullOrEmpty(dpf.tipoCuentaDpf))
                {
                    dpf.tipoCuentaDpf = "0";
                }
                if (string.IsNullOrEmpty(dpf.tipoPlazo))
                {
                    dpf.tipoPlazo = "0";
                }
                if (string.IsNullOrEmpty(dpf.renovacion))
                {
                    dpf.renovacion = "";
                }
                if (string.IsNullOrEmpty(dpf.tipoDeposito))
                {
                    dpf.tipoDeposito = "1";
                }
                if (string.IsNullOrEmpty(dpf.modoPagoInteres))
                {
                    dpf.modoPagoInteres = "0";
                }
                dpf.conCuenta = dpf.modoPagoInteres;
                if (string.IsNullOrEmpty(dpf.tipoCuenta))
                {
                    dpf.tipoCuenta = "0";
                }
                if (dpf.conCuenta.Equals("0"))
                {
                    dpf.nroCuenta = "";
                }
                if (string.IsNullOrEmpty(dpf.plazoManual))
                {
                    string[] vplazo = { "30", "60", "90", "180", "360", "370", "740", "1100" };
                    int indice = dpf.tipoCuentaDpf.Equals("0") ? int.Parse(dpf.plazo) - 1 : int.Parse(dpf.plazo) + 1;
                    dpf.plazoManual = vplazo[indice];
                }
                else
                {
                    try
                    {
                        int plazo = int.Parse(dpf.plazoManual);
                        dpf.plazoManual = plazo.ToString();
                    }
                    catch (Exception)
                    {
                        throw new Exception("PLAZO NO VALIDO");
                    }
                }
                if (string.IsNullOrEmpty(dpf.plazo))
                {
                    dpf.plazo = "0";
                }
                if (string.IsNullOrEmpty(dpf.penalidad))
                {
                    dpf.penalidad = "000.000";
                }
                else
                {
                    try
                    {
                        decimal penalidad = decimal.Parse(dpf.penalidad, CultureInfo.InvariantCulture);
                        dpf.penalidad = penalidad.ToString("000.000", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        dpf.penalidad = "000.000";
                    }
                }

                if (string.IsNullOrEmpty(dpf.tasaNominal))
                {
                    dpf.tasaNominal = "0.00";
                }
                else
                {
                    try
                    {
                        decimal tasaNominal = decimal.Parse(dpf.tasaNominal, CultureInfo.InvariantCulture);
                        dpf.tasaNominal = tasaNominal.ToString("0.00", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        dpf.tasaNominal = "0.00";
                    }
                }
                string[] cic = new string[] { "", "", "" };
                for (int i = 0; i < pasos.detalleCliente.Count; i++)
                {
                    cic[i] = pasos.tipoPersona.Equals("J") ? pasos.detalleCliente[i].pj.cic : pasos.detalleCliente[i].pn.cic;
                }
                dpf.cic01 = cic[0];
                dpf.cic02 = cic[1];
                dpf.cic03 = cic[2];
                dpf.tipoBanca = pasos.tipoPersona.Equals("J") ? pasos.detalleCliente.First().pj.tipoBanca : pasos.detalleCliente.First().pn.tipoBanca;
                dpf.nroClientes = pasos.detalleCliente.Count.ToString();
                if (string.IsNullOrEmpty(dpf.dpfProducto))
                {
                    dpf.dpfProducto = detallecuenta.cuentaProducto.producto;
                }
                else
                {
                    if (dpf.tipoPlazo.Equals("1"))
                    {
                        if (dpf.dpfProducto.Equals("441"))
                        {
                            dpf.dpfProducto = "445";
                        }
                        else if (dpf.dpfProducto.Equals("443"))
                        {
                            dpf.dpfProducto = "444";
                        }
                    }
                }
                bool segundoPiso = dpf.dpfProducto.Equals("443") || dpf.dpfProducto.Equals("444") || dpf.dpfProducto.Equals("446");
                if (string.IsNullOrEmpty(dpf.cui))
                {
                    dpf.cui = "0";
                }
                dpf.modoCuenta = detallecuenta.cuentaProducto.idModalidad;
                switch (dpf.dpfProducto)
                {
                    case "DOR":
                        dpf.tipoDpf = dpf.tipoCuentaDpf;
                        if (dpf.tipoDpf.Equals("1"))
                        {
                            dpf.modoPagoInteres = "0";
                            dpf.plazo = (int.Parse(dpf.plazo) - 1).ToString();
                        }
                        #region CAMBIO DOL
                        if (dpf.tipoPlazo.Equals("1"))
                        {
                            dpf.dpfProducto = "DOL";
                        }
                        #endregion
                        break;
                    case "CUE":
                        #region CAMBIOS CUE
                        EnvioIAData consultaIA = new EnvioIAData();
                        consultaIA.agencia = dpf.agencia;
                        consultaIA.monto = dpf.monto;
                        consultaIA.plazo = dpf.plazoManual;
                        consultaIA.tasa = dpf.tasaNominal;
                        consultaIA.tipoPlazo = "1";
                        consultaIA.usuario = sesion.usuario.usuario;
                        RespuestaInteresAdelantado respuestaIA = ConsultaInteresAdelantado(consultaIA);
                        if (respuestaIA.success)
                        {
                            dpf.monto = "0.00";
                            dpf.conCuenta = "1";
                            dpf.nroCuenta = respuestaIA.data.nroCuenta;
                            dpf.tipoCuenta = respuestaIA.data.tipoCuenta.Equals("AHO") ? "0" : "1";
                            dpf.dpfProducto = "DNI";
                            dpf.interesAdelantado = true;
                            dpf.tipoDpf = "2";
                            //conContrato = true;
                        }
                        else
                        {
                            throw new Exception(respuestaIA.message);
                        }
                        #endregion
                        break;
                    case "DEC":
                    case "442":
                        dpf.tipoDpf = "4";
                        conContrato = true;
                        break;
                    case "DIN":
                    case "441":
                    case "443":
                    case "444":
                    case "445":
                        dpf.tipoDpf = "3";
                        break;
                    case "DNI":
                        dpf.tipoDpf = "2";
                        conContrato = true;
                        break;
                }
                //dpf = LimpiarDatos.limpiarNulo<DPF>(dpf);
                #endregion

                dpf.usuario = sesion.usuario.usuario;
                string metodo = "Dpf/api/Creacion/dpf/p" + pasos.tipoPersona;
                response = AperturaProducto(pasos, dpf, sesion.host,dpf.sucursal,dpf.agencia, metodo);
                if (response.success)
                {
                    Apertura apertura = new Apertura
                    {
                        codMoneda = dpf.moneda.Equals("303") ? "01" : "02",
                        codTipoProducto = ManagerHost.formatoDPFAP(detallecuenta.cuentaProducto.producto, dpf.tipoDpf),
                        numeroCuenta = response.data.nroCuenta.Trim(),
                        nombreCuenta = dpf.nombreCuenta + "|" + dpf.motivoCuenta,
                        codTipoBanca = pasos.tipoPersona.Equals("N") ? "P" : "E",
                        monto = dpf.monto,
                        campoStr1 = ManagerSystematic.modalidadDPFBD(dpf.dpfProducto, dpf.indModalidad, dpf.indicadorCustodia.Equals("1"), dpf.conCuenta, dpf.nroCuenta),
                        campoStr2 = (dpf.dpfProducto.Equals("DOR") || dpf.dpfProducto.Equals("DOL") || dpf.dpfProducto.Equals("DNI")) ? "RA" : "",
                        campoStr3 = sesion.credenciales.usuario,
                        campoDate1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        campoInt1 = dpf.plazoManual,
                        campoMoney1 = "0.0",
                        campoReal1 = "0",
                    };
                    ContratoDPF contrato = null;
                    if (conContrato)
                    {
                        conContrato = true;
                        contrato = new ContratoDPF
                        {
                            modoPagoInteres = dpf.modoPagoInteres,
                            penalidad = dpf.penalidad,
                            tasaNominal = dpf.tasaNominal,
                            plazoManual = dpf.plazoManual,
                            renovacion = dpf.renovacion,
                            tipoPlazo = dpf.tipoPlazo,
                            usuario = dpf.usuario
                        };

                    }
                    switch (dpf.dpfProducto)
                    {
                        case "DOR":
                            dpf.dpfProducto = "DOR" + (int.Parse(dpf.tipoCuentaDpf) + 1).ToString();
                            break;
                        case "441":
                        case "443":
                        case "444":
                        case "445":
                            dpf.dpfProducto = "DIN";
                            break;
                        case "442":
                            dpf.dpfProducto = "DEC";
                            break;
                        case "DOL":
                            dpf.dpfProducto = "DNI";
                            break;
                        default:
                            break;
                    }                    
                    guardarResultadoAperCuenta(ref pasos, response, dpf.moneda, dpf.nombreCuenta, dpf.monto, dpf.indModalidad, dpf.motivoCuenta, dpf.dpfProducto, DateTime.Now.ToString("dd/MM/yyyy"), dpf.plazoManual, dpf.tipoPlazo.Equals("1"), dpf.renovacion, dpf.modoPagoInteres, dpf.conCuenta, dpf.nroCuenta, dpf.tasaNominal, dpf.penalidad, dpf.indicadorCustodia.Equals("1"), dpf.tipoCuentaDpf, segundoPiso);
                    SapResponse aperturasPas = RegistroAperturaBD(apertura, pasos.detalleCliente, conContrato, contrato, sesion);
                    if (aperturasPas.success) { }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        private RespuestaInteresAdelantado ConsultaInteresAdelantado(EnvioIAData datos)
        {
            RespuestaInteresAdelantado response = new RespuestaInteresAdelantado();
            try
            {
                string metodo = "DPF/api/Contrato/consulta/ia";
                EnvioInteresAdelantado data = new EnvioInteresAdelantado();
                data.cuenta = datos;
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaInteresAdelantado>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaCreacionCuenta AperturaProductoCustodia(Custodia custodia, ref SeguimientoProcesos pasos, Sesion sesion)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            try
            {
                string[] cic = new string[] { "", "", "" };
                for (int i = 0; i < pasos.detalleCliente.Count; i++)
                {
                    cic[i] = pasos.tipoPersona.Equals("J") ? pasos.detalleCliente[i].pj.cic : pasos.detalleCliente[i].pn.cic;
                }
                custodia.montoInicial = "0.00";
                custodia.nroClientes = pasos.detalleCliente.Count.ToString();
                custodia.cic01 = cic[0];
                custodia.cic02 = cic[1];
                custodia.cic03 = cic[2];
                string metodo = "DPF/api/Custodia/dpf";
                response = AperturaProducto(pasos, custodia, sesion.host,null,null, metodo);
                if (response.success)
                {
                    response.data = new Cuenta { nroCuenta= custodia.nroCuenta };
                    custodia.moneda = "30" + custodia.nroCuenta.Replace("-", "").Substring(11, 1).ToString();
                    custodia.indModalidad = custodia.indModalidad + ",CUS";
                    Apertura apertura = new Apertura
                    {
                        codMoneda = custodia.moneda.Equals("303") ? "01" : "02",
                        codTipoProducto = "ICB",
                        numeroCuenta = response.data.nroCuenta.Trim(),
                        nombreCuenta = custodia.nombreCuenta,
                        codTipoBanca = pasos.tipoPersona.Equals("N") ? "P" : "E",
                        monto = custodia.montoInicial,
                        campoStr1 = custodia.indModalidad,
                        campoStr2 = "",
                        campoStr3 = sesion.usuario.usuario,
                        campoDate1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        campoInt1 = "0",
                        campoMoney1 = "0.0",
                        campoReal1 = "0",
                    };
                    guardarResultadoAperCuenta(ref pasos, response, custodia.moneda, custodia.nombreCuenta, custodia.montoInicial, custodia.indModalidad + ",CUS", custodia.motivoCuenta, string.Empty, DateTime.Now.ToString("dd/MM/yyyy"), string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, string.Empty, false);
                    RegistroAperturaBD(apertura, pasos.detalleCliente, false, null, sesion);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion

        #region SECCION: APERTURAS PAS
        public RespuestaParametro obtenerParametro()
        {
            var cacheKey = "parametros";
            RespuestaParametro ParametrosCache;

            if (!_cache.TryGetValue(cacheKey, out ParametrosCache))
            {
                RespuestaParametro response = new RespuestaParametro();
                try
                {
                    string metodo = "Parametros/api/Parametro/lista";
                    EnvioOperacion data = new EnvioOperacion();
                    this._operacion = ManagerOperation.GenerateOperation(data.operation);
                    Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                    response = _consumirAPI.consulta<RespuestaParametro>(metodo, data);
                    //Almacenar en cache los parametros
                    // Key not in cache, so get data.
                    ParametrosCache = response;
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3));

                    // Save data in cache.
                    _cache.Set(cacheKey, ParametrosCache, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
                }
                finally
                {
                    if (response.success)
                    {
                        Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "PARAMETROS OK");
                    }
                    else
                    {
                        Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                    }
                }
                return response;
            }
            else
            {
                if (ParametrosCache.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "PARAMETROS OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(ParametrosCache));
                }
                return ParametrosCache;
            }
        }
        public RespuestaTipoCuenta obtenerListaCuentas(string tipoPersona)
        {
            RespuestaTipoCuenta response = new RespuestaTipoCuenta();
            try
            {
                string metodo = "Parametros/api/Cuenta/lista";
                EnvioTipoCuenta data = new EnvioTipoCuenta
                {
                    data = new EnvioTipoPersona { tipoPersona = tipoPersona }
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaTipoCuenta>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        public RespuestaProducto ListaModalidad(string valor, SeguimientoProcesos pasos)
        {
            RespuestaProducto response = new RespuestaProducto();
            try
            {
                EnvioTipoProducto data = new EnvioTipoProducto
                {
                    data = new EnvioModalidad
                    {
                        codigoTipoCuenta = valor,
                        tipoPersona = pasos.tipoPersona
                    }
                };
                string metodo = "Parametros/api/Modalidad/lista";
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaProducto>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }       
        public RespuestaConsultaBloqueo ConsultaBloqueoBD(BloqueoDBDataCondicional datos, ParametrosHost usuario)
        {
            RespuestaConsultaBloqueo response = new RespuestaConsultaBloqueo();
            try
            {
                string metodo = "Parametros/api/Tarjeta/ConsultaBD/bloqueo";
                if (!string.IsNullOrEmpty(datos.idc) && string.IsNullOrEmpty(datos.tidc))
                {
                    datos.idc = datos.idc + "-" + datos.tidc;
                }
                EnvioConsultaBloqueo data = new EnvioConsultaBloqueo { condicionales = datos,usuario=usuario };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaConsultaBloqueo>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse RegistroFiltro(SeguimientoProcesos pasos, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Parametros/api/Apertura/registro/filtro";
                int indice = 0;
                DatosCliente cliente = pasos.detalleCliente[indice];
                string[] idc = ManagerValidation.obtenerIDC(cliente.idC, cliente.idT);
                string nombres = string.Empty;
                if (pasos.tipoPersona.Equals("J"))
                {
                    nombres = cliente.pj.razonSocial;
                }
                else
                {
                    nombres = cliente.pn.nombres + " " + cliente.pn.paterno + " " + cliente.pn.materno;
                }
                EnvioRegistroFiltro data = new EnvioRegistroFiltro
                {
                    data = new EnvioRegistroFiltroData
                    {
                        archivoNegativo = cliente.AN,
                        enameChecker = cliente.EC
                    },
                    cliente = new FiltroPersona
                    {
                        idcTipo = cliente.idT,
                        idcNumero = idc[0],
                        idcExtension = idc[1],
                        nombresRazonSocial = nombres
                    },
                    usuario = sesion.host
                };
                data.usuario.usuarioExtra = sesion.usuario.usuario;
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public SapResponse RegistroBloqueoDB(Sesion sesion, BloqueoDBData datos)
        {
            SapResponse respuesta = new SapResponse();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(datos.idcDB, datos.tidcDB);
                string metodo = "Parametros/api/Tarjeta/RegistroBD/bloqueo";
                sesion.host.usuarioExtra = sesion.credenciales.usuario;
                EnvioRegistroBloqueo data = new EnvioRegistroBloqueo
                {
                    usuario = sesion.host,
                    data = datos,
                    cliente = new EnvioDocumentoPJ
                    {
                        idcNumero = idC[0],
                        idcTipo = datos.tidcDB,
                        idcExtension = idC[1]
                    }
                };
                data.data.fechaBloqueo = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                respuesta = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        private SapResponse RegistroAperturaBD(Apertura apertura, List<DatosCliente> Pcliente, bool conContrato, ContratoDPF contrato, Sesion sesion)
        {
            SapResponse response = new SapResponse();
            try
            {
                string metodo = "Parametros/api/Apertura/";
                if (conContrato)
                {
                    metodo = metodo + "contrato/dpf";
                }
                else
                {
                    metodo = metodo + "registro/ambos";
                }
                List<Aperturante> aperturantes = new List<Aperturante>();
                for (int i = 0; i < Pcliente.Count; i++)
                {
                    string[] idC = ManagerValidation.obtenerIDC(Pcliente[i].idC, Pcliente[i].idT);
                    if (!idC.Contains("?"))
                    {
                        Aperturante cliente = new Aperturante
                        {
                            nroDoc = idC[0],
                            tipoDoc = Pcliente[i].idT,
                            extensionDoc = idC[1]
                        };
                        if (Pcliente[i].pj == null)
                        {
                            cliente.codCiiu = Pcliente[i].pn.ciiu;
                            cliente.codLocalidad = Pcliente[i].pn.localidad;
                            cliente.direccion = Pcliente[i].pn.direccion;
                            cliente.fechaNac = LimpiarDatos.ParsearFecha(Pcliente[i].pn.fechaNacimiento.Replace("/", "")).ToString("yyyy-MM-dd");//
                            cliente.nombreRazonSocial = Pcliente[i].pn.nombres;
                            cliente.paterno = Pcliente[i].pn.paterno;
                            cliente.materno = Pcliente[i].pn.materno;
                            cliente.gremio = LimpiarDatos.validarFechaUltimaActualizacion(Pcliente[i].pn.fechaUltActualizacion).Equals("AC") ? "S" : "N";
                            cliente.telefono = Pcliente[i].pn.telefono;
                        }
                        else
                        {
                            cliente.codLocalidad = Pcliente[i].pj.localidad;
                            cliente.codCiiu = Pcliente[i].pj.ciiu;
                            cliente.direccion = Pcliente[i].pj.direccion;
                            cliente.fechaNac = LimpiarDatos.ParsearFecha(Pcliente[i].pj.fechaConstitucion).ToString("yyyy-MM-dd");//
                            cliente.nombreRazonSocial = Pcliente[i].pj.razonSocial;
                            cliente.paterno = "";
                            cliente.materno = "";
                            cliente.gremio = LimpiarDatos.validarFechaUltimaActualizacion(Pcliente[i].pj.fechaUltActualizacion).Equals("AC") ? "S" : "N";
                            cliente.telefono = Pcliente[i].pj.telefono;
                        }
                        aperturantes.Add(cliente);
                    }
                }
                if (conContrato)
                {
                    if (Pcliente.First().pj == null)
                    {
                        contrato.tipoCliente = Pcliente.First().pn.tipoCliente;
                    }
                    else
                    {
                        contrato.tipoCliente = Pcliente.First().pj.tipoCliente;
                    }
                }
                EnvioRegistroCreacionCuenta data = new EnvioRegistroCreacionCuenta
                {
                    usuario = sesion.host,
                    apertura = apertura,
                    aperturante = aperturantes,
                    data = contrato
                };
                data.usuario.usuarioExtra = sesion.credenciales.usuario;
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return response;
        }
        private void grabarBD(EnvioTarjetaBDData datos, ParametrosHost usuario, ref SeguimientoProcesos pasos, int indice = 0)
        {
            SapResponse response = new SapResponse();
            try
            {
                string respuesta = string.Empty;
                string metodo = "Parametros/api/Tarjeta/RegistroBD";
                datos = LimpiarDatos.limpiarNulo<EnvioTarjetaBDData>(datos);
                EnvioTarjetaBD data = new EnvioTarjetaBD();
                data.usuario = usuario;
                data.data = datos;
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<SapResponse>(metodo, data);
                if (response.success)
                {
                    pasos.detalleCliente[indice].codigoOperacion = datos.codTipoOperacion;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
        }
        private string motivoCuenta(string nroCuenta)
        {
            string motivo = string.Empty;
            try
            {
                string metodo = "Parametros/api/Apertura/consulta/motivoCuenta";
                EnvioConsultaMotivoCuenta request = new EnvioConsultaMotivoCuenta
                {
                    data=new Cuenta
                    {
                        nroCuenta= nroCuenta
                    }
                };
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(request));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(request));
                RespuestaConsultaMotivoCuenta response = _consumirAPI.consulta<RespuestaConsultaMotivoCuenta>(metodo, request);
                if (response.success)
                {
                    motivo = response.data.motivoCuenta;
                }
            }
            catch(Exception ex)
            {
                motivo = string.Empty;
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            return motivo;
        }
        #endregion

        #region SECCION: FLUJO
        public void guardarDatosConsultaTarjeta(ConsultaTarjeta data, ref SeguimientoProcesos pasos)
        {
            pasos.detalleCliente.Add(new DatosCliente
            {
                idT = data.tipoIDC,
                idC = data.idc,
                tarjeta = data.tarjeta,
                localidadDescripcion = "",
                estadoCivilDescripcion = "",
                detalleCuenta = new List<AperturaCuenta>()
            });
            string[] pn = { "P", "Q", "S", "Y" };
            if (pn.Contains(pasos.detalleCliente.Last().idT))
            {
                pasos.tipoPersona = "N";
                pasos.detalleCliente.Last().pn = new PersonaNatural
                {
                    nombres = data.nombreCompleto,
                    paterno = "",
                    materno = "",
                    ciiu = "",
                    direccion = data.direccion,
                    telefono = "",
                    tipoCliente = "CL",
                    fechaNacimiento = data.fecNac,
                    tipoBanca = "P"
                };
            }
            else
            {
                pasos.tipoPersona = "J";
                pasos.detalleCliente.Last().pj = new PersonaJuridica
                {
                    razonSocial = data.nombreCompleto,
                    ciiu = "",
                    direccion = data.direccion,
                    telefono = "",
                    tipoCliente = "CL",
                    fechaConstitucion = "",//resultado.data.fecNac
                    tipoBanca = "E"
                };
            }
            string nombrecuenta = data.nombreCompleto;
            string[] cuentas = { LimpiarDatos.limpiarCuenta(data.ahorro1), LimpiarDatos.limpiarCuenta(data.ahorro2), LimpiarDatos.limpiarCuenta(data.corriente1), LimpiarDatos.limpiarCuenta(data.corriente2) };
            for (int i = 0; i < cuentas.Length; i++)
            {
                if (!cuentas[i].Equals(""))
                {
                    AperturaCuenta aperturaCuenta= new AperturaCuenta
                    {
                        detalleCuenta = new DetalleCuenta
                        {
                            nombreCuenta = nombrecuenta,
                            monto = string.Empty,
                            nroCuenta = LimpiarDatos.formatoCuenta(cuentas[i]),
                            indModalidad = string.Empty,
                            moneda = cuentas[i][cuentas.Length - 3].Equals('3') ? "MNA" : "USD",
                            producto = cuentas[i].Length == 13 ? "AHT" : "CCS",
                            dpfCuentaAfiliada = string.Empty,
                            conCuenta = string.Empty,
                            custodia = false,
                            dpfTipoCuenta = string.Empty,
                            modoPagoIntereses = string.Empty,
                            //motivoCuenta = string.Empty,
                            penalidad = string.Empty,
                            fechaApertura = data.fecApertura,
                            plazo = string.Empty,
                            renovacion = string.Empty,
                            tasa = string.Empty,
                            tiempoMeses = false,
                            segundoPiso = false
                        },
                        cuentaProducto = new ProductoModalidad
                        {
                            producto = cuentas[i].Length == 13 ? "AHL" : "CCS",
                            idModalidad = string.Empty,
                            codigoModalidad = string.Empty,
                            codigoProducto = string.Empty
                        }
                    };
                    aperturaCuenta.detalleCuenta.motivoCuenta = motivoCuenta(aperturaCuenta.detalleCuenta.nroCuenta);
                    pasos.detalleCliente.Last().detalleCuenta.Add(aperturaCuenta);
                }
                //HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
            }
        }
        public ConsultaTarjeta sinTarjeta(ConsultaTarjeta datos, DatosCliente cliente)
        {
            if (cliente.pj == null)
            {
                datos.fecNac = cliente.pn.fechaNacimiento;
                datos.direccion = cliente.pn.direccion;
                datos.nombreCompleto = cliente.pn.paterno + " " + cliente.pn.materno + " " + cliente.pn.nombres;
            }
            else
            {
                datos.fecNac = cliente.pj.fechaConstitucion;
                datos.direccion = cliente.pj.direccion;
                datos.nombreCompleto = cliente.pj.razonSocial;
            }
            datos.idc = cliente.idC;
            datos.tipoIDC = cliente.idT;
            Dictionary<string, object> limpiar = ManagerJson.DeserializarObjecto<Dictionary<string, object>>(ManagerJson.SerializarObjecto(datos));
            foreach (string apoyo in limpiar.Keys.ToList())
            {
                if (limpiar[apoyo] == null)
                {
                    limpiar[apoyo] = "";
                }
            }
            datos = ManagerJson.DeserializarObjecto<ConsultaTarjeta>(ManagerJson.SerializarObjecto(limpiar));
            datos.idcCompleto = datos.tipoIDC + datos.idc;
            datos.descrpBloqueo = datos.idcCompleto;
            return datos;
        }
        public ConsultaTarjeta asignarCuenta(ConsultaTarjeta resultado, List<AperturaCuenta> cuenta)
        {
            foreach (var w in cuenta)
            {
                string a = w.cuentaProducto.producto;
                string moneda = w.detalleCuenta.moneda;
                if (a[0] == 'A')
                {
                    if (moneda.Equals("USD"))
                    {
                        if (!resultado.flagCta1)
                        {
                            resultado.ahorro1 = w.detalleCuenta.nroCuenta;
                        }
                    }
                    else
                    {
                        if (!resultado.flagCta2)
                        {
                            resultado.ahorro2 = w.detalleCuenta.nroCuenta;
                        }
                    }
                }
                else if (a[0] == 'C')
                {
                    if (moneda.Equals("USD"))
                    {
                        if (!resultado.flagCta3)
                        {
                            resultado.corriente1 = w.detalleCuenta.nroCuenta;
                        }
                    }
                    else
                    {
                        if (!resultado.flagCta4)
                        {
                            resultado.corriente2 = w.detalleCuenta.nroCuenta;
                        }
                    }
                }
            }
            return resultado;
        }
        private AperturaCuenta buscarCuenta(List<AperturaCuenta> cuentas, bool apertura)
        {
            int c = -1;
            bool continuar = true;
            for (int i = 0; i < cuentas.Count; i++)
            {
                if (apertura)
                {
                    continuar = cuentas[i].detalleCuenta.apertura;
                }
                if (cuentas[i].detalleCuenta.producto[0].Equals('A') && continuar)
                {
                    c = i;
                    break;
                }
            }
            if (c < 0)
            {
                continuar = true;
                for (int i = 0; i < cuentas.Count; i++)
                {
                    if (apertura)
                    {
                        continuar = cuentas[i].detalleCuenta.apertura;
                    }
                    if ((cuentas[i].detalleCuenta.producto[0].Equals('D') || cuentas[i].detalleCuenta.producto.Equals("ICB")) && continuar)
                    {
                        c = i;
                        break;
                    }
                }
            }
            if (c < 0)
            {
                continuar = true;
                if (apertura)
                {
                    continuar = cuentas[0].detalleCuenta.apertura;
                }
                if (continuar)
                {
                    c = 0;
                }
            }
            if (c < 0)
            {
                return null;
            }
            else
            {
                return cuentas[c];
            }
        }
        private void guardarResultadoAperCuenta(ref SeguimientoProcesos pasos, RespuestaCreacionCuenta respuesta, string moneda, string nombreCuenta, string monto, string indModalidad, string motivo, string producto, string fechaApertura, string plazo, bool tiempoMeses, string renovacion, string modoPagoInteres, string conCuenta, string cuentaAfiliada, string tasa, string penalidad, bool custodia, string dpfTipoCuenta, bool segundoPiso)
        {
            try
            {
                if (respuesta.success)
                {
                    DetalleCuenta datosCuenta = new DetalleCuenta();
                    datosCuenta.apertura = true;
                    datosCuenta.asignadoTarjeta = false;
                    datosCuenta.producto = producto;
                    datosCuenta.nroCuenta = respuesta.data.nroCuenta;
                    datosCuenta.moneda = moneda.Equals("303") ? "MNA" : "USD";
                    datosCuenta.nombreCuenta = nombreCuenta;
                    datosCuenta.monto = monto;
                    datosCuenta.indModalidad = indModalidad;
                    datosCuenta.motivoCuenta = motivo;
                    datosCuenta.fechaApertura = fechaApertura;
                    datosCuenta.plazo = plazo;
                    datosCuenta.tiempoMeses = tiempoMeses;
                    datosCuenta.renovacion = renovacion;
                    datosCuenta.modoPagoIntereses = modoPagoInteres;
                    datosCuenta.conCuenta = conCuenta;
                    datosCuenta.dpfCuentaAfiliada = cuentaAfiliada;
                    datosCuenta.tasa = tasa;
                    datosCuenta.penalidad = penalidad;
                    datosCuenta.custodia = custodia;
                    datosCuenta.dpfTipoCuenta = dpfTipoCuenta;
                    datosCuenta.segundoPiso = segundoPiso;
                    for (int i = 0; i < pasos.detalleCliente.Count; i++)
                    {
                        pasos.detalleCliente[i].detalleCuenta.Last().detalleCuenta = datosCuenta;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region SECCION: REPORTES
        public RespuestaReporte ReporteApertura(ReporteAperturaData datos, Sesion sesion)
        {
            RespuestaReporte response = new RespuestaReporte();
            try
            {
                string metodo = "Reportes/Apertura/reporte";
                if (string.IsNullOrEmpty(datos.tipoOperacion))
                {
                    datos.tipoOperacion = "";
                }
                if (string.IsNullOrEmpty(datos.usuario))
                {
                    datos.usuario = "";
                }
                ReporteApertura data = new ReporteApertura
                {
                    usuario = sesion.host,
                    consulta = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaReporte>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public RespuestaReporte ReporteTarjeta(ReporteTarjetaData datos, Sesion sesion)
        {
            RespuestaReporte response = new RespuestaReporte();
            try
            {
                string metodo = "Reportes/Tarjeta/reporte";
                if (string.IsNullOrEmpty(datos.tipoOperacion))
                {
                    datos.tipoOperacion = "";
                }
                if (string.IsNullOrEmpty(datos.usuario))
                {
                    datos.usuario = "";
                }
                ReporteTarjeta data = new ReporteTarjeta
                {
                    usuario = sesion.host,
                    consulta = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaReporte>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public RespuestaReporte ReporteStocks(Stock datos)
        {
            RespuestaReporte response = new RespuestaReporte();
            try
            {
                string metodo = "Reportes/Stocks/reporte";
                datos = LimpiarDatos.limpiarNulo<Stock>(datos);
                if (string.IsNullOrEmpty(datos.estadoTarjeta) || datos.estadoTarjeta.Equals("1"))
                {
                    datos.estadoTarjeta = "INACTIVA";
                }
                else
                {
                    datos.estadoTarjeta = "ACTIVA";
                }
                EnvioReporteStockTarjeta data = new EnvioReporteStockTarjeta
                {
                    data = datos
                };
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, ManagerJson.SerializarObjecto(data));
                response = _consumirAPI.consulta<RespuestaReporte>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {

                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public RespuestaReporte ImpresionContrato(SeguimientoProcesos pasos, Sesion sesion, int indice, bool original, bool copia, ref string operacion)
        {
            RespuestaReporte response = new RespuestaReporte();
            try
            {
                string metodo = "ContratoDpf/api/v1/Contrato/impresion";
                EnvioContratoData datos = new EnvioContratoData
                {
                    contrato = new ImpresionContratoDB(),
                    firmante = new ImpresionContratoFirmante(),
                    productos = new List<ImpresionContratoProducto>()
                };
                #region datosUsuario
                if (original && copia)
                {
                    //copia = false;
                }
                datos.contrato.fechaApertura = DateTime.Now.ToString("dd/MM/yyyy");
                datos.contrato.sucursal = sesion.host.sucursal;
                datos.contrato.agencia = sesion.host.agencia;
                datos.contrato.nroTarjeta = pasos.detalleCliente[indice].tarjeta;
                datos.contrato.apertura = ManagerValidation.validarProceoApertura(pasos.nombreProceso);
                datos.contrato.original = original;
                datos.contrato.copia = copia;
                if (!string.IsNullOrEmpty(datos.contrato.nroTarjeta))
                {
                    datos.contrato.tipoOperacion = ManagerValidation.validarOperacion(pasos.nombreProceso, pasos.detalleCliente[indice].codigoOperacion);
                }
                #endregion
                #region firmante
                string[] idC = ManagerValidation.obtenerIDC(pasos.detalleCliente[indice].idC, pasos.detalleCliente[indice].idT);
                datos.firmante.idcNumero = idC[0];
                datos.firmante.idcExtension = idC[1];
                datos.firmante.idcTipo = pasos.detalleCliente[indice].idT;
                datos.firmante.idcComplemento = "00";
                datos.firmante.localidad = pasos.detalleCliente[indice].localidadDescripcion;
                if (pasos.tipoPersona.Equals("N"))
                {
                    PersonaNatural persona = pasos.detalleCliente[indice].pn;
                    datos.firmante.paternoRazonSocial = persona.paterno;
                    datos.firmante.materno = persona.materno;
                    datos.firmante.nombres = persona.nombres;
                    datos.firmante.tipoCliente = persona.tipoCliente;
                    datos.firmante.direccionTexto = persona.direccion;
                    datos.firmante.telefono = persona.telefono;
                    datos.firmante.banca = "P";//persona.tipoBanca;
                }
                else
                {
                    PersonaJuridica persona = pasos.detalleCliente[indice].pj;
                    datos.firmante.paternoRazonSocial = persona.razonSocial;
                    datos.firmante.materno = string.Empty;
                    datos.firmante.nombres = string.Empty;
                    datos.firmante.tipoCliente = persona.tipoCliente;
                    datos.firmante.direccionTexto = persona.direccion;
                    datos.firmante.telefono = persona.telefono;
                    datos.firmante.banca = "E";//persona.tipoBanca;
                }
                #endregion
                var detalleCuenta = pasos.detalleCliente[indice].detalleCuenta;
                for (int i = 0; i < detalleCuenta.Count; i++)
                {
                    ImpresionContratoProducto producto = new ImpresionContratoProducto();
                    producto.producto = detalleCuenta[i].detalleCuenta.producto;
                    producto.moneda = detalleCuenta[i].detalleCuenta.moneda.Equals("MNA") ? "303" : "302";
                    producto.nombreCuenta = detalleCuenta[i].detalleCuenta.nombreCuenta;
                    producto.nroCuenta = detalleCuenta[i].detalleCuenta.nroCuenta;
                    producto.monto = string.IsNullOrEmpty(detalleCuenta[i].detalleCuenta.monto) ? -1M : decimal.Parse(detalleCuenta[i].detalleCuenta.monto, CultureInfo.InvariantCulture);
                    producto.clave = detalleCuenta[i].detalleCuenta.indModalidad + (detalleCuenta[i].detalleCuenta.custodia ? ",CUS" : string.Empty);
                    producto.plazo = detalleCuenta[i].detalleCuenta.plazo;
                    producto.unidadTiempo = detalleCuenta[i].detalleCuenta.tiempoMeses ? "1" : "0";
                    producto.fechaApertura = detalleCuenta[i].detalleCuenta.fechaApertura;
                    producto.renovacion = detalleCuenta[i].detalleCuenta.segundoPiso ? "0" : detalleCuenta[i].detalleCuenta.Equals("") ? "" : "1";
                    producto.pagoIntereses = detalleCuenta[i].detalleCuenta.conCuenta;
                    producto.otraCuenta = detalleCuenta[i].detalleCuenta.dpfCuentaAfiliada;
                    producto.tasa = detalleCuenta[i].detalleCuenta.tasa;
                    producto.penalidad = detalleCuenta[i].detalleCuenta.penalidad;
                    datos.productos.Add(producto);
                }
                EnvioContrato data = new();
                data.data = datos;
                this._operacion = ManagerOperation.GenerateOperation(data.operation);
                operacion = this._operacion;
                string jsonString = JsonSerializer.Serialize(data);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                response = _consumirAPI.consulta<RespuestaReporte>(metodo, data);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {

                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        #endregion

        #region SEGUROS
        public RegistrarSeguroResponse AfiliacionSeguro(RegistrarSeguroRequest datos)
        {
            RegistrarSeguroResponse response = new RegistrarSeguroResponse();
            try
            {
                string metodo = "OperacionesDb/api/Seguros/seguros/afiliar";
                _operacion = ManagerOperation.GenerateOperation(datos.operation);
                string jsonString = JsonSerializer.Serialize(datos);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                response = _consumirAPI.consulta<RegistrarSeguroResponse>(metodo, datos);
            }
            catch (Exception ex)
            {

                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }

            finally
            {

                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }
        public GeneraContratoResponse CertificadoSeguro(GeneraContratoRequest datos)
        {
            GeneraContratoResponse response = new GeneraContratoResponse();
            try
            {
                string metodo = "OperacionesDb/api/Seguros/certificado/generar";
                _operacion = ManagerOperation.GenerateOperation(datos.operation);
                string jsonString = JsonSerializer.Serialize(datos);
                Logger.Error("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                Logger.Debug("[{0}] ->  REQUEST: {1}", this._operacion, jsonString);
                response = _consumirAPI.consulta<GeneraContratoResponse>(metodo, datos);
            }
            catch (Exception ex)
            {

                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }

            finally
            {

                if (response.success)
                {
                    Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "REPORTE OK");
                }
                else
                {
                    Logger.Error("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
                }
            }
            return response;
        }

        public List<ConfiguracionSeguros> GetListSeguros(string producto)
        {
            List<ConfiguracionSeguros> Seguros = _configuracion.configuracionSeguros;
            List<ConfiguracionSeguros> SegurosCategoria = new();
            Seguros.ForEach(Seguro =>
            {
                if (Seguro.Estado)
                {
                    ConfiguracionSeguros newSeguros = new();
                    List<TipoSeguro> listaTipos = new();
                    Seguro.Tipo.ForEach(tipo =>
                    {
                        if (tipo.Producto == producto)
                        {
                            listaTipos.Add(tipo);
                        }
                    });
                    if (listaTipos.Count >= 1)
                    {
                        SegurosCategoria.Add(Seguro);
                    }
                }                
            });
            return SegurosCategoria;
        }
        #endregion
    }
}

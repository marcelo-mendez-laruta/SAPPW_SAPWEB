using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Sap.Models.Enviar;
using BCP.Framework.Common;
using BCP.Sap.Connectors;
using BCP.Framework.Logs;
using BCP.Sap.Models.Rebibir;
using BCP.Sap.Models.Enviar.Tarjeta;
using BCP.Sap.Models.Sap;
using BCP.Sap.Models.Rebibir.Tarjeta;
using BCP.Sap.Business;
using BCP.Sap.Models.Rebibir.SmartLink;
using BCP.Sap.Models.Configuracion;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly ISapBusiness _business;
        private readonly ILogger _logger;
        private string _operacion;
        public TarjetaController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }

        #region VISTAS
        public ActionResult Bloqueo(string tarjeta)
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    BloqueoDBData consulta = new BloqueoDBData();

                    var json = ManagerJson.SerializarObjecto(ConsultaTarjeta(tarjeta, 0,null).Value);
                    RespuestaConsultaTarjeta resultadoConsulta = ManagerJson.DeserializarObjecto<RespuestaConsultaTarjeta>(json);
                    if (resultadoConsulta.success)
                    {
                        string[] situacion = new string[] { "06", "03", "11", "12" };
                        if (!situacion.Contains(resultadoConsulta.data.situacion))
                        {
                            return RedirectToAction("SelBloqueo");
                        }
                        consulta.codBloqueo = resultadoConsulta.data.codigoBloqueo;
                        consulta.tarjeta = resultadoConsulta.data.tarjeta;
                        consulta.fechaVenTarj = resultadoConsulta.data.fechaExpiracion;
                        consulta.idcDB = resultadoConsulta.data.idc;
                        consulta.tidcDB = resultadoConsulta.data.tipoIDC;
                        consulta.nombre = resultadoConsulta.data.nombreTarjeta;
                    }
                    return View(model: consulta);
                }
            }
        }
        public ActionResult Cambio()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (!sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_MAN_PER_COM_T") && sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_CAM_RAP_TAR"))
                {
                    limpiarFlujos();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult CargaBloqueo()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    limpiarFlujos();
                    return View();
                }
            }
        }
        public ActionResult PinCosto()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    string tarjeta = pasos.detalleCliente.First().tarjeta;
                    return View(model: (new string[] { "12", string.IsNullOrEmpty(tarjeta) ? string.Empty : tarjeta }));
                }
            }
        }
        public ActionResult Prod()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_TAR_PRO"))
                {
                    limpiarFlujos();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Stocks()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_STO")) 
                {
                    limpiarFlujos();
                    Stock data = new Stock();
                    data.sucursal = sesion.host.sucursal;
                    data.agencia = sesion.host.agencia;
                    return View(model: data);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Tramite()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_PRO_TAR"))
                {
                    limpiarFlujos();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult SelBloqueo()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_COT_BLO_REG_BLO"))
                {
                    limpiarFlujos();

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Sat(bool consulta, bool tarjetaNueva, int indice, string tarjeta, string exclusiva)
        {
            Sesion sesion = new Sesion();
            int contadorSeguro = 0;
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (!sesion.autorizado)
                {
                    return RedirectToAction("Index", "Home");
                }
                string[] menu = sesion.usuario.politicas.Split('|');
                if (menu.Contains("SAPP_BO_TAR") && menu.Contains("SAPP_BO_TAR_MAN"))
                {                    
                    ConsultaTarjeta resultado = new ConsultaTarjeta();
                    string proceso = "Sat";
                    string tipoPersona = "N";
                    if (string.IsNullOrEmpty(exclusiva))
                    {
                        exclusiva = string.Empty;
                    }
                    try
                    {
                        if (consulta)
                        {
                            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                            proceso = pasos.nombreProceso;
                            if (!tarjetaNueva)
                            {
                                if (!string.IsNullOrEmpty(tarjeta))
                                {
                                    var json = ManagerJson.SerializarObjecto(ConsultaTarjeta(tarjeta, indice, exclusiva).Value);
                                    RespuestaConsultaTarjeta resultadoConsulta = ManagerJson.DeserializarObjecto<RespuestaConsultaTarjeta>(json);
                                    resultado = resultadoConsulta.data;
                                }
                                if (pasos.registro || pasos.nombreProceso.Equals("Entrega de Tarjeta"))
                                {
                                    #region DATOS PARA CONTRATO
                                    //SeguimientoProcesos 
                                    proceso = "Proceso" + proceso;
                                    if (pasos.nombreProceso.Equals("Apertura"))
                                    {
                                        resultado = this._business.asignarCuenta(resultado, pasos.detalleCliente[indice].detalleCuenta);
                                    }
                                    else if (pasos.nombreProceso.Equals("Entrega de Tarjeta"))
                                    {
                                        pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                                    }
                                    string adicional=string.Empty;
                                    if (!string.IsNullOrEmpty(tarjeta))
                                    {
                                        if (pasos.tipoPersona.Equals("N"))
                                        {
                                            adicional = pasos.detalleCliente[indice].pn.tipoCliente + "-" + pasos.detalleCliente[indice].pn.cic;
                                        }
                                        else
                                        {
                                            adicional = pasos.detalleCliente[indice].pj.tipoCliente + "-" + pasos.detalleCliente[indice].pj.cic;
                                        }
                                    }
                                    exclusiva = exclusiva + "-" + indice + "-" + adicional;
                                    #endregion
                                }
                            }
                            else
                            {
                                proceso = "Proceso" + pasos.nombreProceso + "TN";
                                resultado = this._business.sinTarjeta(resultado, pasos.detalleCliente[indice]);
                                if (pasos.nombreProceso.Equals("Apertura"))
                                {
                                    contadorSeguro = (from t in pasos.detalleCliente[indice].detalleCuenta where  t.cuentaProducto.producto[0].Equals('A') && !t.cuentaProducto.codigoModalidad.Equals("103") select t.detalleCuenta.nroCuenta).Count();
                                    resultado = this._business.asignarCuenta(resultado, pasos.detalleCliente[indice].detalleCuenta);
                                }
                                string adicional;
                                tipoPersona = pasos.tipoPersona;
                                if (pasos.tipoPersona.Equals("N"))
                                {
                                    adicional = pasos.detalleCliente[indice].pn.tipoCliente + "-" + pasos.detalleCliente[indice].pn.cic;
                                }
                                else
                                {
                                    adicional = pasos.detalleCliente[indice].pj.tipoCliente + "-" + pasos.detalleCliente[indice].pj.cic;
                                }
                                exclusiva = exclusiva + "-" + indice + "-" + adicional;
                            }
                        }
                        else
                        {
                            TempData["guid"] = false;//!string.IsNullOrEmpty(sesion.guid);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        TempData["usoPagina"] = proceso;
                        TempData["datos"] = exclusiva;
                    }
                    var res = resultado.idc;
                    ViewBag.tipoPersona = tipoPersona;
                    if (proceso.Equals("ProcesoAperturaTN") && contadorSeguro>0 && tipoPersona.Equals("N"))
                    {
                        List<ConfiguracionSeguros> Seguros = _business.GetListSeguros("Tarjetas de Debito");
                        ViewBag.ListaSeguros = Seguros;
                    }                    
                    return View(model: resultado);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        #endregion

        #region INTERMEDIO
        public ActionResult RegistroSat(string strSituacion, string iTarjeta)
        {
            if (string.IsNullOrEmpty(strSituacion))
            {
                strSituacion = string.Empty;
            }
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            int indice = int.Parse(iTarjeta[0].ToString());
            pasos.detalleCliente[indice].tarjeta = LimpiarDatos.credimas(iTarjeta.Substring(1));
            pasos.detalleCliente[indice].situacionBloqueo = strSituacion;
            HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
            if (pasos.nombreProceso.Equals("Cambio de Tarjeta"))
            {
                return RedirectToAction("PinCosto");
            }
            else if (pasos.nombreProceso.Equals("Entrega de Tarjeta"))
            {
                return RedirectToAction("ImpContrato", "Home");
            }
            else
            {
                return RedirectToAction("Paso6", "Registro");
            }
        }
        #endregion

        #region PETICIONES POST
        [HttpPost]
        public JsonResult ConsultaTarjeta(string tarjeta, int indice, string bancaExclusiva)
        {
            RespuestaConsultaTarjeta response = new RespuestaConsultaTarjeta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                SeguimientoProcesos pasos = new SeguimientoProcesos();
                if (HttpContext.Session.GetString("procesoApertura") == null)
                {
                    pasos.nombreProceso = "Sat";
                    pasos.detalleCliente = new List<DatosCliente>();
                    pasos.detalleCliente.Add(new DatosCliente { detalleCuenta = new List<AperturaCuenta>() });
                }
                else
                {
                    pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    pasos.detalleCliente[indice].tarjeta = "";
                    List<AperturaCuenta> cuentas = pasos.detalleCliente[indice].detalleCuenta;
                    if (cuentas.Count > 0)
                    {                        
                        List<AperturaCuenta> eliminados = cuentas.Where(x => x.detalleCuenta.apertura).ToList();
                        pasos.detalleCliente[indice].detalleCuenta = eliminados;
                    }
                }
                response = this._business.ConsultaTarjeta(sesion, tarjeta, bancaExclusiva,indice ,ref pasos);
                if (response.success)
                {                    
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }        
        [HttpPost]
        public JsonResult TarjetaStocks(StockConsulta datos)
        {
            RespuestaStockTarjeta response = new RespuestaStockTarjeta();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.TarjetaStocks(datos, sesion);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult OperacionTarjeta(Datos datos,bool afiliacion, string situacion, string bancaExclusiva, int indice)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.OperacionTarjeta(sesion, datos, afiliacion,situacion, bancaExclusiva,indice,ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        
        [HttpPost]
        public JsonResult CambioTarjeta(Cambio datos)
        {
            SapResponse response = new SapResponse();
            this._operacion=ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                response = this._business.CambioTarjeta(sesion,datos, ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult BloqueoTarjeta(Bloqueo datos)
        {
            RespuestaBloqueo response=new RespuestaBloqueo();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.BloqueoTarjeta(sesion, datos,ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult RegistroBloqueoDB(BloqueoDBData datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.RegistroBloqueoDB(sesion, datos);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult RecepcionTarjeta(Recepcion datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.RecepcionTarjeta(datos, sesion);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult ActualizacionTarjeta(ActualizacionData datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ActualizacionTarjeta(datos, sesion);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult EntregaTarjeta(Entrega datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.EntregaTarjeta(sesion, datos, true,ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura",ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult TarjetasRelacionadas(Relacion datos)
        {
            RespuestaRelacionTarjeta response = new RespuestaRelacionTarjeta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.TarjetasRelacionadas(datos,sesion);
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(response));
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult CambioTarjetaRapido(CambioRapido datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = new SeguimientoProcesos();
                Sesion host = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.CambioTarjetaRapido(host, datos,ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult OperacionBancaExclusiva(BancaExclusivaData datos,string tipo)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.BancaExclusiva(sesion, datos, tipo,ref pasos);
                if (response.success)
                {
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
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
            return Json(response);
        }
        #endregion

        #region REPORTES

        [HttpPost]
        public string RegistrarContrato20190()
        {
            bool exito=false;
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (HttpContext.Session.GetString("procesoApertura") != null)
                {
                    SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    if (!string.IsNullOrEmpty(sesion.guid))
                    {
                        exito = this._business.F20190(pasos, 0, sesion);
                    }
                    else
                    {
                        exito = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "FIN GENERACION FORMULARIO 20190");
            }
            return exito?"OK":"NO";
        }

        [HttpPost]
        public ActionResult RegistrarContrato(bool original, bool copia)
        {
            string nombre = string.Empty;
            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            if (HttpContext.Session.GetString("procesoApertura")!=null)
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                RespuestaReporte reporte = this._business.ImpresionContrato(pasos, sesion, 0, original, copia, ref nombre);
                if (reporte.success)
                {
                    byte[] bytes = System.Convert.FromBase64String(reporte.data.pdf);
                    return File(bytes, "application/pdf", nombre + ".pdf");
                }
                if (pasos.nombreProceso.Equals("Sat"))
                {
                    return RedirectToAction("Sat", "Tarjeta");
                }
            }
            return RedirectToAction("Index", "Home");
        }
       
        [HttpPost]
        public IActionResult ReporteStocks(Stock datos)
        {
            RespuestaReporte respuesta = this._business.ReporteStocks(datos);
            if (respuesta.success)
            {
                byte[] bytes = System.Convert.FromBase64String(respuesta.data.pdf);
                return File(bytes, "application/pdf", "stocks.pdf");
            }
            return RedirectToAction("Reportes", "Home");
        }
        #endregion

        #region PIN PAD 
        [HttpPost]
        public JsonResult LeerTarjeta()
        {
            string[] tarjeta = BCP.Sap.DataAccess.PinPad.consumirTarjeta();
            if (tarjeta[0] != null && tarjeta[0].Equals("00"))
            {
                tarjeta[1] = LimpiarDatos.credimas(tarjeta[1]);
            }
            return Json(tarjeta);
        }
        [HttpPost]
        public JsonResult LeerPin(string tarjeta)
        {
            string[] pin = BCP.Sap.DataAccess.PinPad.consumirPin(tarjeta);
            return Json(pin);
        }
        #endregion

        #region FLUJO
        [HttpPost]
        public string LimpiarSat(string indice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("procesoApertura")))
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                if (!string.IsNullOrEmpty(indice))
                {
                    pasos.detalleCliente[int.Parse(indice)].tarjeta = "";
                    List<AperturaCuenta> cuentas = pasos.detalleCliente[int.Parse(indice)].detalleCuenta;
                    List<AperturaCuenta> eliminados = cuentas.Where(x => x.detalleCuenta.apertura).ToList();
                    pasos.detalleCliente[int.Parse(indice)].detalleCuenta = eliminados;
                }               
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
            }
            return "ok";
        }
        private void limpiarFlujos()
        {
            try
            {
                if (HttpContext.Session.GetString("procesoApertura") != null)
                {
                    Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                    sesion.host.guid = sesion.guid;
                    HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion));
                    HttpContext.Session.Remove("procesoApertura");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
        }
        #endregion
    }
}

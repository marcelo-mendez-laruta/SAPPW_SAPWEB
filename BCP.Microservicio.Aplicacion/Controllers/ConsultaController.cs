using System;
using System.Collections.Generic;
using BCP.Sap.Connectors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Sap.Models.Enviar;
using BCP.Framework.Common;
using BCP.Sap.Models.Rebibir;
using BCP.Framework.Logs;
using BCP.Sap.Models.Enviar.Tarjeta;
using BCP.Sap.Models.Sap;
using BCP.Sap.Models.Rebibir.Tarjeta;
using BCP.Sap.Business;
using BCP.Sap.Models.Enviar.ProcesoApertura;
using System.Linq;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;
        private string _operacion;
        public ConsultaController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }

        #region TARJETA        
        public ActionResult EntregaTarjeta()
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
                    string[] menu = sesion.usuario.politicas.Split('|');
                    if (!menu.Contains("SAPP_BO_TAR_MAN_PER_COM_T") && menu.Contains("SAPP_BO_TAR") && menu.Contains("SAPP_BO_TAR_ENT_ELI"))
                    {
                        string nombreProceso = "Entrega de Tarjeta";
                        SeguimientoProcesos pasos = new SeguimientoProcesos
                        {
                            nombreProceso = nombreProceso,
                            detalleCliente = new List<DatosCliente>(),                            
                        };
                        pasos.detalleCliente.Add(new DatosCliente { detalleCuenta = new List<AperturaCuenta>() });
                        HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                        return RedirectToAction("AlfabeticaTarjeta");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }            
        }
        public ActionResult AlfabeticaTarjeta()
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
                    string usoPagina = "";
                    if (HttpContext.Session.GetString("procesoApertura") != null)
                    {
                        SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                        if (pasos.detalleCliente.Count > 0)
                        {
                            int indice = 0;
                            if (pasos.nombreProceso.Equals("Apertura"))
                            {
                                try
                                {
                                    indice = int.Parse(Request.Form["indice"].ToString());
                                }
                                catch (Exception ex) { }
                            }
                            string nombre = "";
                            if (!(pasos.nombreProceso.Equals("Entrega de Tarjeta") || pasos.nombreProceso.Equals("ConsultaTarjeta")))
                            {
                                if (pasos.tipoPersona.Equals("J"))
                                {
                                    nombre = pasos.detalleCliente[indice].pj.razonSocial;
                                }
                                else
                                {
                                    nombre = pasos.detalleCliente[indice].pn.paterno + " " + pasos.detalleCliente[indice].pn.materno + " " + pasos.detalleCliente[indice].pn.nombres;
                                }
                            }
                            TempData["nombre"] = indice.ToString() + "-" + nombre;
                            usoPagina = "Proceso" + pasos.nombreProceso;
                        }
                        else
                        {
                            usoPagina = pasos.nombreProceso;
                        }
                    }
                    else
                    {
                        limpiarFlujos();
                        usoPagina = "ConsultaTarjeta";
                        SeguimientoProcesos pasos = new SeguimientoProcesos
                        {
                            nombreProceso = usoPagina,
                            detalleCliente = new List<DatosCliente>()
                        };
                        pasos.detalleCliente.Add(new DatosCliente { detalleCuenta = new List<AperturaCuenta>() });
                        HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                    }
                    TempData["usoPagina"] = usoPagina;
                    return View();
                }
            }           
        }

        [HttpPost]
        public JsonResult CAT(string nombre)
        {
            RespuestaBusquedaTarjeta response = new RespuestaBusquedaTarjeta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.BusquedaTarjeta(sesion, nombre);
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

        #region ALS 
        public ActionResult AlfabeticaClienteALS()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CON_ALS"))
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
        public ActionResult InfoProductoALS(AlsInformeCrediticioRequest credito)
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
                    AlsInformeCrediticioItem data = new AlsInformeCrediticioItem();
                    if (credito != null)
                    {
                        if (credito.codigoCredito != null)
                        {
                            if (!credito.codigoCredito.Equals(""))
                            {
                                var json = ManagerJson.SerializarObjecto(ConsultaInformeCrediticioALS(credito).Value);
                                RecibirALSInformeCrediticio respuesta = ManagerJson.DeserializarObjecto<RecibirALSInformeCrediticio>(json);
                                if (respuesta != null)
                                {
                                    if (respuesta.success)
                                    {
                                        if (respuesta.data.total > 0)
                                        {
                                            if (credito.item < respuesta.data.total)
                                                data = respuesta.data.rows[0];
                                            data.nombres = credito.nombre;
                                            return View(model: data);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult ProductoClienteALS(string customer)
        {
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("autorizado").Equals("no"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                limpiarFlujos();
                ALSProductoClienteResponseData data = new ALSProductoClienteResponseData();
                if (customer != null)
                {
                    if (!customer.Equals(""))
                    {
                        var json = ManagerJson.SerializarObjecto(ConsultaProductoClienteALS(new ALSProductoClienteRequestData { customer = customer }).Value);
                        RecibirALSProductoCliente respuesta = ManagerJson.DeserializarObjecto<RecibirALSProductoCliente>(json);
                        if (respuesta != null)
                        {
                            if (respuesta.success)
                            {
                                data = respuesta.data;
                            }
                        }
                        data.customer = customer;
                    }
                }
                return View(model: data);
            }
        }
        [HttpPost]
        public JsonResult ConsultaAlfabeticaALS(ALSConsultaAlfabeticaRequestData datos)
        {
            RecibirALSConsultaAlfabetica response = new RecibirALSConsultaAlfabetica();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaAlfabeticaALS(sesion, datos);
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
        public JsonResult ConsultaProductoClienteALS(ALSProductoClienteRequestData datos)
        {
            RecibirALSProductoCliente response = new RecibirALSProductoCliente();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaProductoClienteALS(sesion, datos);
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
        public JsonResult ConsultaInformeCrediticioALS(AlsInformeCrediticioRequest datos)
        {
            RecibirALSInformeCrediticio response = new RecibirALSInformeCrediticio();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaInformeCrediticioALS(sesion, datos);
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

        #region CLIENTE
        public ActionResult AlfabeticaCliente()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CLI_CON_ALF"))
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
        [HttpPost]
        public JsonResult ConsultaAlfabetica(string cliente,bool completo,string idc,string idt,string ide)
        {
            RespuestaConsultaAlfabetica response = new RespuestaConsultaAlfabetica();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.dataConAlfCli(sesion.host, cliente, completo, idc, idt, ide);
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

        #region BLOQUEO
        public ActionResult Bloqueo()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_COT_BLO_CON_BLO"))
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
        [HttpPost]
        public JsonResult ConsultaBloqueos(BloqueoDBDataCondicional datos)
        {
            RespuestaConsultaBloqueo response = new RespuestaConsultaBloqueo();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaBloqueoBD(datos,sesion.host);
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

        #region GUID
        [HttpPost]
        public JsonResult GUID(string tipo)
        {
            RespuestaGUID DatosGUID = new RespuestaGUID();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion2 = new();
                bool validacion = false;
                string[] pn = { "P", "Q", "S", "Y" };
                string[] pj = { "J", "K", "R", "V", "W", "Y", "Z" };
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                sesion2 = this._business.pGUID(sesion);
                DatosGUID = sesion.datosGUID;
                HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion2));
                if (tipo.Equals("J"))
                {
                    validacion = pj.Contains(DatosGUID.data.idcTipo);
                }
                else
                {
                    validacion = pn.Contains(DatosGUID.data.idcTipo);
                }
                if (!validacion)
                {
                    DatosGUID = new RespuestaGUID();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializarObjecto(DatosGUID));
            }
            return Json(DatosGUID);
        }
        #endregion

        #region FLUJO
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

        [HttpPost]
        public string limpiarPasoAnterior()
        {
            if (HttpContext.Session.GetString("procesoApertura") != null)
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                if (pasos.detalleCliente.Count > 1)
                {
                    pasos.detalleCliente.RemoveAt(pasos.detalleCliente.Count - 1);
                    return "ok";
                }
            }
            return "no";
        }
        #endregion
    }

}

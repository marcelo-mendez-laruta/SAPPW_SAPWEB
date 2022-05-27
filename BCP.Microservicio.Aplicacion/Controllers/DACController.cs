using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Business;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Enviar.Producto;
using BCP.Sap.Models.Rebibir;
using BCP.Sap.Models.Sap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class DACController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;
        private string _operacion;

        public DACController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }

        #region VISTAS
        public ActionResult CuentasMancomunadas()
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
        public ActionResult DatAdiCli()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CLI_DAT_ADI"))
                {
                    //string[] parametros = { "AddDirecciones", "EditDirecciones", "AddRelaciones", "EditRelaciones", "OLSSAImprimir(btImprimir)", "CuentasMancomunadas" };
                    limpiarFlujos();
                    RespuestaParametro response = this._business.obtenerParametro();
                    List<List<SelectListItem>> listaTotal = DataParametro.selectParametro(response);
                    TempData["guid"] = !string.IsNullOrEmpty(sesion.guid);
                    return View(listaTotal);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult EditDireccion()
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
        public ActionResult Email()
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
        public ActionResult RelClientes()
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
        #endregion

        #region AEP
        [HttpPost]
        public JsonResult BusquedaAepPNJ(string id, string tid,string fecha)
        {
            RespuestaConsultaAep response=new RespuestaConsultaAep();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.BusquedaAepPNJ(sesion, id, tid, fecha);
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
        public JsonResult OperacionesAepPNJ(string id, string tid, string fecha, DatosAep datos,bool registro)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.OperacionesAepPNJ(sesion,id,tid,fecha,datos,registro);
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
        public JsonResult EliminarAepPNJ(string id, string tid, string fecha)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.EliminarAepPNJ(sesion,id,tid,fecha);
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

        #region EMP
        [HttpPost]
        public JsonResult BusquedaEmpleadorPNJ(string id, string tid)
        {
            RespuestaEmpleadorBusqueda response=new RespuestaEmpleadorBusqueda();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.BusquedaEmpPNJ(sesion, id, tid);
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
        public JsonResult OperacionesEmpleadorPNJ(string id, string tid, string eid, string etid, DatosEmpleador datos, bool registro)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    string[] idCEmp = ManagerValidation.obtenerIDC(eid, etid);
                    if (!idCEmp.Contains("?"))
                    {
                        Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                        response = this._business.OperacionesEmpPNJ(sesion, idC, tid, idCEmp, etid, datos, registro);
                    }
                    else
                    {
                        response = new SapResponse
                        {
                            message = "ERROR EN FORMATO IDC DEL EMPLEADOR",
                            state = "88"
                        };
                    }
                }
                else
                {
                    response = new SapResponse
                    {
                        message = "ERROR EN FORMATO IDC",
                        state = "88"
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
            return Json(response);
        }
        #endregion

        #region DIRECCION
        [HttpPost]
        public JsonResult BusquedaDireccionPNJ(string id, string tid)
        {
            RespuestaConsultaDireccion response = new RespuestaConsultaDireccion();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                    response = this._business.BusquedaDireccionPNJ(usuario, idC, tid);
                }
                else
                {
                     response = new RespuestaConsultaDireccion
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
            return Json(response);
        }       
        [HttpPost]
        public JsonResult OperacionesDireccionPNJ(string id, string tid, DatosDireccion datos , bool registro)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);

                if (!idC.Contains("?"))
                {
                    ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                    response = this._business.OperacionesDireccionPNJ(usuario, idC, tid, datos, registro);
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult ModCorrespondencia(string id, string tid, int direccion)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);

                if (!idC.Contains("?"))
                {
                    ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                    response = this._business.ModificarCorrespondencia(usuario, idC, tid, direccion);
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
            return Json(response);
        }

        [HttpPost]
        public string ConCorrespondencia(string id, string tid)
        {
            string response = string.Empty;
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                string[] pn = { "P", "Q", "S", "Y" };
                if (pn.Contains(tid))
                {
                    RespuestaPersonaNatural respuesta = this._business.ConsultaClientePN(usuario, idC, tid,false,false);
                    if (respuesta.success)
                    {
                        response = respuesta.data.correspondencia;
                    }
                }
                else
                {
                    RespuestaPersonaJuridica respuesta = this._business.ConsultaClientePJ(usuario, idC, tid);
                    if (respuesta.success)
                    {
                        response = respuesta.data.correspondencia;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            finally
            {
                Logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, "CORRESPONDENCIA: "+response);
            }
            return (response);
        }

        [HttpPost]
        public JsonResult ConsultaCliPro(ProductoCliente datos)
        {
            RespuestaConsultaProductoCliente response = new RespuestaConsultaProductoCliente();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion= ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaCliPro(sesion, datos);
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

        #region REC
        [HttpPost]
        public JsonResult BusquedaRecPNJ(string id, string tid)
        {
            RespuestaConsultaRec response = new RespuestaConsultaRec();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                if (!idC.Contains("?"))
                {
                    ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                    response = this._business.ConsultaRecPNJ(usuario, idC, tid);
                }
                else
                {
                     response = new RespuestaConsultaRec
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult OperacionesRecPNJ(string id, string tid, DatosRec datos, bool registro)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);

                if (!idC.Contains("?"))
                {
                    ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                    response = this._business.OperacionesRecPNJ(usuario, idC, tid, datos, registro);
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
            return Json(response);
        }
        [HttpPost]
        public JsonResult EliminarRecPNJ(string id, string tid, string idRel,string tidRel,string codRelacion)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.EliminarRecPNJ(sesion, id, tid, idRel,tidRel, codRelacion);
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
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BCP.Framework.Common;
using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Rebibir;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BCP.Framework.Logs;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Sap;
using BCP.Sap.Business;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ISapBusiness _business;
        private readonly ILogger _logger;
        private string _operacion;
        public PersonaController(ILogger logger,ISapBusiness business)
        {
            this. _logger = logger;
            this._business = business;
        }

        #region VISTAS
        public ActionResult Natural()
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
                else if(sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CLI_PER_NAT"))
                {
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
        public ActionResult Juridica()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CLI_PER_JUR"))
                {
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
    #endregion

        #region CONSULTA
        [HttpPost]
        public JsonResult ConsultaPNJ(string id, string tid,string tipoCliente)
        {
            string[] idC = ManagerValidation.obtenerIDC(id, tid);
            if (!idC.Contains("?"))
            {
                ParametrosHost usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                if (tipoCliente.Equals("J"))
                {
                    RespuestaPersonaJuridica consulta = this._business.ConsultaClientePJ(usuario, idC, tid);
                    return Json(consulta);
                }
                else
                {
                    RespuestaPersonaNatural consulta = this._business.ConsultaClientePN(usuario, idC, tid,false,false);
                    return Json(consulta);
                }                
            }
            else
            {
                SapResponse resultado = new SapResponse
                {
                    state = "88",
                    message = "FORMATO DE IDC INVALIDO"
                };
                return Json(resultado);
            }
        }
        #endregion

        #region MODIFICAR DATOS
        [HttpPost]
        public JsonResult ModificarIdcPNJ(string id, string tid, string Nid, string Ntid, string tipo)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ModificarIdcPNJ(sesion, id, tid, Nid, Ntid, tipo);
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
        public JsonResult ModificarNombresPNJ(string id, string tid, string datos, string tipoPersona)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ModificarNombresPNJ(sesion,id, tid, datos, tipoPersona);
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
        public JsonResult ModificarPNJ(string id, string tid, ModificacionDataPN pn, ModificacionDataPJ pj, string tipo)
        {
            SapResponse respuesta =new SapResponse();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                respuesta = this._business.ModificarPNJ(sesion, id, tid, pn, pj, tipo);
            }
            catch(Exception )
            {
                throw;
            }
            return Json(respuesta);
        }
        #endregion

        #region CREAR CLIENTE
        [HttpPost]
        public JsonResult CrearPNJ(string id, string tid, string cliente,string tipoPersona,string complemento)
        {
            RespuestaCreacionCliente respuesta=new RespuestaCreacionCliente();
            try
            {
                string[] idC = ManagerValidation.obtenerIDC(id, tid);
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));

                if (tipoPersona.Equals("j"))
                {
                    CreacionClientePJ data = ManagerJson.DeserializarObjecto<CreacionClientePJ>(cliente);                    
                    data.registroCompleto = true;
                    respuesta = this._business.CrearClientePJ(sesion.host, idC, tid, data);
                }
                else
                {
                    CreacionClientePN data = ManagerJson.DeserializarObjecto<CreacionClientePN>(cliente);
                    respuesta = this._business.CrearClientePN(sesion.host, idC, tid, data);
                    if (respuesta.success)
                    {
                        RegistroINFOCLIENTE infocliente= ManagerJson.DeserializarObjecto<RegistroINFOCLIENTE>(complemento);
                        infocliente.cic = respuesta.data.cic;
                        SapResponse respuestaRegistro = this._business.RegistrarINFOCLIENTE(idC, tid, infocliente, sesion.usuario.usuario);
                        if (!respuestaRegistro.success)
                        {
                            respuesta.message = respuestaRegistro.message;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return Json(respuesta);
        }
        #endregion

        #region FUNCIONARIO DE NEGOCIOS
        [HttpPost]
        public JsonResult CambioFunNeg(string id, string tid, FunNeg funNeg, string tipoCliente,string oficina)
        {
            RespuestaCambioFunNeg respuesta = new RespuestaCambioFunNeg();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                sesion.host.sucursal = oficina[0].ToString() + "01";
                sesion.host.agencia = oficina;
                respuesta = this._business.CambioFunNeg(sesion, id, tid, funNeg, tipoCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(respuesta);
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

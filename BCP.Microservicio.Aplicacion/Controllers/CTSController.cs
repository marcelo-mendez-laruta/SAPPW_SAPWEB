using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Framework.Common;
using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Models.Rebibir;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Framework.Logs;
using Newtonsoft.Json;
using BCP.Sap.Models.Sap;
using BCP.Sap.Business;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class CTSController : Controller
    {
        // GET: CTS
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;
        private string _operacion;
        public CTSController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }
        public ActionResult Apertura()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_PRO_APE_PRO_APE_CTS"))
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
        public ActionResult Empresa()
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
                    TempData["guid"] = !string.IsNullOrEmpty(sesion.guid);
                    return View();
                }
            }
        }
        public ActionResult Empleado()
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
                    TempData["guid"] = !string.IsNullOrEmpty(sesion.guid);
                    return View();
                }
            }
        }
       
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
    }
}

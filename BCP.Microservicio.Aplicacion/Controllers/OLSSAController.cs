using BCP.Framework.Common;
using BCP.Sap.Models.Rebibir;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Framework.Logs;
using BCP.Sap.Models.Sap;
using BCP.Sap.Business;
using System;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class OLSSAController : Controller
    {
        // GET: OLSSAController
        private readonly ISapBusiness _business;
        private readonly ILogger _logger;

        public OLSSAController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }
        public ActionResult ImpDir()
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
        public ActionResult CuentasAsoc()
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
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", ManagerOperation.GenerateOperation(), ex.Message, ManagerJson.SerializarObjecto(ex));
            }
        }
    }
    
}

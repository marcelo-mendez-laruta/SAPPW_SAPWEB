using BCP.Framework.Common;
using BCP.Sap.Models.Rebibir;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Framework.Logs;
using BCP.Sap.Models.Sap;
using BCP.Sap.Business;
using System;
using System.Linq;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class VPController : Controller
    {
        // GET: VP
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;

        public VPController(ILogger logger,ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }
        public ActionResult ConsultaAlfabetica()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CON_VIS_PLU"))
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
        public ActionResult InfoProducto()
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
        public ActionResult ProductoPorCliente()
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

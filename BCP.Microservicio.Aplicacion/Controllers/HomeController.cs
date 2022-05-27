using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Microservicio.Aplicacion.Models;
using BCP.Framework.Logs;
using System.Diagnostics;
using BCP.Sap.Connectors;
using BCP.Framework.Common;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Rebibir;
using BCP.Sap.Models.Enviar.Reporte;
using BCP.Sap.Business;
using BCP.Sap.Models.Sap;
using BCP.Sap.Models.Configuracion;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;
        private string _operacion;
        public HomeController(ILogger logger, ISapBusiness business)
        {
            this._business = business;
            this._logger = logger;
        }

        public ActionResult RedireccionarProcesos(string vista)
        {
            try
            {
                limpiarFlujos();
                string[] uri = vista.Split('|');
                return RedirectToAction(uri[0], uri[1]);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public string Autenticacion(Login login)
        {
            string mensaje = "error";
            try
            {
                string contrasena = string.Empty;
                RespuestaSegurinet respuesta = this._business.Login(login, out contrasena);
                login.contrasena = contrasena;
                if (respuesta.success)
                {
                    if (respuesta.data.conAccesso)
                    {
                        bool exito = false;
                        AdecuacionConfiguracionEstaciones rconf = this._business.DatosEstacion(login.usuario, ref exito);
                        if (exito)
                        {
                            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                            sesion.autorizado = true;
                            sesion.usuario.nombre = respuesta.data.nombre;
                            //sesion.usuario.email = respuesta.data.email;
                            //sesion.usuario.cargo = respuesta.data.cargo;
                            sesion.usuario.politicas = respuesta.data.politicas;
                            sesion.host = rconf.host;
                            sesion.estacion = rconf.slk;
                            sesion.host.guid = sesion.guid;
                            sesion.host.ipOrigen = HttpContext.Connection.RemoteIpAddress.ToString();
                            sesion.credenciales = login;
                            HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion));
                            mensaje = respuesta.message;
                            /*var cookies = HttpContext.Request.Cookies[".AspNetCore.Session"];
                            CookieOptions cookieOptions = new CookieOptions();
                            cookieOptions.Expires = DateTime.Now.AddHours(3);
                            HttpContext.Response.Cookies.Append(".AspNetCore.Session", cookies, cookieOptions);*/

                        }
                        else
                        {
                            mensaje = rconf.message;
                        }
                    }
                }
                else
                {
                    mensaje = respuesta.message;
                    if (mensaje.Equals("DEBE CAMBIAR SU CONTRASEÑA ACTUAL."))
                    {
                        Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                        sesion.credenciales = login;
                        HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion));
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {

            }
            return mensaje;
        }
        public ActionResult Index(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                limpiarFlujos();
            }
            else if(HttpContext.Session.GetString("autorizado") != null)
            {
                HttpContext.Session.Clear();
            }
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                sesion.guid = guid;
                string usuario = HttpContext.User.Identity.Name;
                string matricula = LimpiarDatos.limpiarMatricula(usuario);
                sesion.usuario = new DatosUsurio { usuario = usuario };
                RespuestaUsuario respuesta = this._business.ActiveDirectory(matricula);
                if (respuesta.success)
                {
                    RespuestaSegurinet politicas = this._business.LoginUsuario(matricula);
                    if (politicas.success)
                    {
                        if (politicas.data.conAccesso)
                        {
                            bool exito = false;
                            if (!string.IsNullOrEmpty(politicas.data.usuario))
                            {
                                matricula = politicas.data.usuario;
                            }
                            AdecuacionConfiguracionEstaciones rconf = this._business.DatosEstacion(matricula, ref exito);
                            if (exito)
                            {
                                sesion.autorizado = true;
                                sesion.usuario.usuario = sesion.usuario.usuario + " - " + respuesta.data.nombre;
                                sesion.usuario.nombre = respuesta.data.nombre;
                                sesion.usuario.email = respuesta.data.email;
                                sesion.usuario.cargo = respuesta.data.cargo;
                                sesion.usuario.politicas = politicas.data.politicas;
                                sesion.host = rconf.host;
                                sesion.estacion = rconf.slk;
                                sesion.host.guid = sesion.guid;
                                sesion.host.ipOrigen = HttpContext.Connection.RemoteIpAddress.ToString();
                                sesion.credenciales = new Login { usuario = matricula };
                            }
                        }
                    }
                }
                else
                {
                    //return Unauthorized(); =>se debe mandar a login?
                }
                HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion));
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            }
            return View(model: sesion.autorizado);
        }
        public ActionResult About()
        {
            limpiarFlujos();
            return View();
        }
        public ActionResult Manual()
        {
            limpiarFlujos();
            return View();
        }
        public ActionResult Movi()
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
                    string[] politicas = sesion.usuario.politicas.Split('|');
                    if (politicas.Contains("SAPP_BO_PRO_CON_INF_CTA"))
                    {
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult SoporteTecnico()
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
        public ActionResult ReportesBloqueo()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_TAR_COT_BLO_REP_BLO"))
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
        public ActionResult Permiso()
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
        public ActionResult SelectTablas()
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
        public ActionResult ViewLogsBloqueos()
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
        public ActionResult ProdClie()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CLI_PRO_CLI"))
                {
                    limpiarFlujos();
                    TempData["guid"] = !string.IsNullOrEmpty(sesion.guid);
                    return View();
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
                else
                {
                    limpiarFlujos();
                    return View();
                }
            }
        }
        public ActionResult Invr()
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
                    string[] politicas = sesion.usuario.politicas.Split('|');
                    if (politicas.Contains("SAPP_BO_PRO_CON_INF_CTA"))
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
        }
        public ActionResult PegarCuentas()
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
                    return View(model:string.Empty);
                }
            }
        }
        public ActionResult Config()
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
                    Sesion model = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                    return View(model: model);
                }
            }
        }
        public ActionResult NegDetalle()
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
        public ActionResult PrnQry()
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
        public ActionResult ArchNeg()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_CON_ARC_NEG"))
                {
                    limpiarFlujos();
                    TempData["guid"] = !string.IsNullOrEmpty(sesion.guid);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Reniec()
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
                    // var imgScr = String.Format("data:image/gif;base64,{0}",usuario.usuario.foto);
                    //ViewData["Title"] = "Reniec: ";
                    //return View(model:imgScr);
                    return View();
                }
            }
        }
        public ActionResult ImpContrato()
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
                    if (HttpContext.Session.GetString("procesoApertura") != null)
                    {
                        SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                        if (!pasos.nombreProceso.Equals("Mantenimiento de Ahorros"))
                        {
                            string[] cuentas = { "", "", "", "" };
                            var detalleCuenta = pasos.detalleCliente.First().detalleCuenta;
                            for (int i = 0; i < detalleCuenta.Count; i++)
                            {
                                bool incluir = true;
                                if (ManagerValidation.validarProceoApertura(pasos.nombreProceso))
                                {
                                    incluir = detalleCuenta[i].detalleCuenta.asignadoTarjeta && detalleCuenta[i].detalleCuenta.apertura;
                                }
                                if (incluir)
                                {
                                    cuentas[LimpiarDatos.indiceCuenta(detalleCuenta[i].detalleCuenta.nroCuenta)] = detalleCuenta[i].detalleCuenta.nroCuenta;
                                }
                            }
                            bool pn = pasos.tipoPersona.ToUpper().Equals("N");
                            string nombre = string.Empty;
                            for (int i = 0; i < pasos.detalleCliente.Count; i++)
                            {
                                switch (pasos.nombreProceso)
                                {
                                    case "Apertura":
                                        nombre = pn ? pasos.detalleCliente[i].pn.paterno + " " + pasos.detalleCliente[i].pn.materno + " " + pasos.detalleCliente[i].pn.nombres : pasos.detalleCliente[i].pj.razonSocial;
                                        this._business.SLCuenta(cuentas, pasos.detalleCliente[i].tarjeta, nombre);
                                        break;
                                    case "Afiliacion":
                                        this._business.SLActualizacion(cuentas, pasos.detalleCliente[i].tarjeta);
                                        break;
                                    case "Cambio de Tarjeta":
                                        this._business.SLCambio(cuentas, pasos.detalleCliente[i].tarjetaAnterior, pasos.detalleCliente[i].tarjeta);
                                        break;
                                }
                                if (!string.IsNullOrEmpty(sesion.guid))
                                {
                                    this._business.F20190(pasos, i, sesion);
                                }
                            }
                        }
                        return View(model: pasos.detalleCliente);
                    }
                    return View(model: new List<DatosCliente>());
                }
            }
        }
        public ActionResult BcaExc()
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
        public ActionResult Reportes()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_REP"))
                {
                    limpiarFlujos();
                    return View(model: sesion.credenciales.usuario);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult MantAhorro()
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
        [HttpPost]
        public JsonResult ConsultaPegarCuentas(int indice)
        {
            var datos = HttpContext.Session.GetString("procesoApertura");
            if (datos != null)
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(datos);
                Dictionary<string, object> principal = new Dictionary<string, object>();
                Dictionary<string, object> secundario = new Dictionary<string, object>();
                List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
                //principal.Add("idDocumento", pasos.detalleCliente[indice].idC);
                //principal.Add("tipoDocumento", pasos.detalleCliente[indice].idT);
                var z = (from t in pasos.detalleCliente[indice].detalleCuenta where t.detalleCuenta.apertura && !(t.detalleCuenta.producto[0]=='D' || t.detalleCuenta.producto.Equals("CUE")) select t).ToList();
                foreach (var i in z)
                {
                    secundario.Clear();
                    secundario.Add("tipoCuenta", i.cuentaProducto.producto);
                    secundario.Add("numeroCuenta", i.detalleCuenta.nroCuenta);
                    secundario.Add("moneda", i.detalleCuenta.moneda);
                    lista.Add(secundario.ToDictionary(p => p.Key, q => q.Value));
                }
                principal.Add("cuenta", lista);
                return Json(principal);
            }
            return Json("");
        }

        [HttpPost]
        public JsonResult ConsultaProCli(string id, string tid, string ini, string fin)
        {
            RespuestaConsultaProducto response = new RespuestaConsultaProducto();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaProCli(id, tid, ini, fin, sesion);
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
        public JsonResult RelacionarDirProd(EnvioRegistroProductoDireccionData datos)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.RelacionarDirProd(datos, sesion);
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
        public JsonResult AN(string id, string tid, string tipo, string paterno, string materno, string nombres)
        {
            RespuestaRegistro response = new RespuestaRegistro();
            try
            {
                response = this._business.ArchivoNegativo(id, tid, tipo, paterno, materno, nombres);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult ConsultaMoviProductos(string tipo, Cuenta nCuenta)
        {
            object response = null;
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaMoviProductos(tipo, nCuenta, sesion);
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
                DatosGUID = sesion2.datosGUID;
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

        [HttpPost]
        public ActionResult ModificarCajaAhorro()
        {
            return RedirectToAction("ImpContrato");
        }
        [HttpPost]
        public string CuentaComercial(string cuentaExtra, string moneda)
        {
            string cuenta = string.Empty;
            try
            {
                cuenta = ManagerHost.CuentaComercial(cuentaExtra, moneda);
            }
            catch (Exception ex)
            {
                cuenta = string.Empty;
            }
            return cuenta;
        }
        [HttpPost]
        public string ObtenerIDPC(string cuenta)
        {
            int id = 0;
            try
            {
                id = ManagerHost.IdPegarCuentas(cuenta);
            }
            catch (Exception ex)
            {
                id = 0;
            }
            return id.ToString();
        }
        [HttpPost]
        public string ClaveSegurinet(string contrasena)
        {
            string mensaje = string.Empty;
            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            SapResponse respuesta = this._business.ClaveSegurinet(contrasena, sesion);
            if (respuesta.success)
            {
                mensaje = "OK";
                HttpContext.Session.Remove("autorizado");
            }
            else
            {
                mensaje = respuesta.message;
            }
            return mensaje;
        }
        /*public ActionResult Print()
        {
            string url = _configuration.variablesConfiguration("url", 0) + "Reportes/api/v1/Tarjeta/consulta/pn";
            ReporteTarjeta data = new ReporteTarjeta
            {

                usuario = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host,
                consulta = new ReporteTarjetaData
                {
                    usuario = "",
                    fecha = "",
                    tipoOperacion = ""
                }
            };
            RespuestaReporte respuesta = ConsumirAPI.consulta<RespuestaReporte>(url, data);
            if (respuesta.success)
            {
                byte[] bytes = System.Convert.FromBase64String(respuesta.data.pdf);
                return File(bytes, "application/pdf");
            }
            return RedirectToAction("Reportes", "Home");

        }*/
        [HttpPost]
        public ActionResult ReporteTarjeta(ReporteTarjetaData datos)
        {
            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            RespuestaReporte respuesta = this._business.ReporteTarjeta(datos, sesion);
            if (respuesta.success)
            {
                byte[] bytes = System.Convert.FromBase64String(respuesta.data.pdf);
                return File(bytes, "application/pdf", "tarjetas.pdf");
            }
            return RedirectToAction("Reportes", "Home");
        }
        [HttpPost]
        public ActionResult ReporteApertura(ReporteAperturaData datos)
        {
            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            RespuestaReporte respuesta = this._business.ReporteApertura(datos, sesion);
            if (respuesta.success)
            {
                byte[] bytes = System.Convert.FromBase64String(respuesta.data.pdf);
                return File(bytes, "application/pdf", "apertura.pdf");
            }
            return RedirectToAction("Reportes", "Home");
        }
        [HttpPost]
        public ActionResult ImprimirContrato(int firmante,bool original,bool copia)
        {
            Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            string nombre = string.Empty;
            RespuestaReporte respuesta = this._business.ImpresionContrato(pasos, sesion, firmante, original, copia, ref nombre);//temporal
            if (respuesta.success)
            {
                byte[] bytes = System.Convert.FromBase64String(respuesta.data.pdf);
                return File(bytes, "application/pdf", nombre + ".pdf");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CambiarClave()
        {
            Sesion sesion = new Sesion();
            if (HttpContext.Session.GetString("autorizado") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (sesion.credenciales.contrasena.Equals(""))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(model: sesion.credenciales.usuario);
                }
            }
        }
        public ActionResult Salir()
        {
            try
            {
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                Logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializarObjecto(ex));
            }
            return RedirectToAction("Index", "Home");
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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Microservicio.Aplicacion.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BCP.Sap.Connectors;
using BCP.Framework.Common;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Enviar.ProcesoApertura;
using BCP.Sap.Models.Rebibir;
using BCP.Framework.Logs;
using BCP.Sap.Models.Enviar.Cliente;
using BCP.Sap.Models.Enviar.Producto;
using System.Globalization;
using BCP.Sap.Models.Sap;
using BCP.Sap.Business;
using BCP.Sap.Models.Configuracion;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    //[Authorize(Roles = "Administrators")]
    public class RegistroController : Controller
    {
        // GET: RegistroController
        private readonly ILogger _logger;
        private readonly ISapBusiness _business;
        private string _operacion;
        public RegistroController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }
        public ActionResult AHS()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_PRO_MAN_PRO_AHS_MAN_CTA"))
                {
                    TempData["procesoInicial"] = "Mantenimiento de Ahorros";
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult ICB()
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
                else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_PRO_MAN_PRO_ING_CUS_CER_BCA"))
                {
                    TempData["procesoInicial"] = "Ingreso a Custodia DPF";
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Index()
        {
            string proceso = "Apertura";
            if (TempData["procesoInicial"] != null)
            {
                proceso = TempData["procesoInicial"].ToString();
                return View(model: proceso);
            }
            else
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
                    else if (sesion.usuario.politicas.Split('|').Contains("SAPP_BO_PRO_APE_PRO"))
                    {                       
                        /*if (HttpContext.Session.GetString("procesoApertura") != null)
                        {
                            limpiarFlujos();
                        }*/
                        return View(model: proceso);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }           
        }
        public ActionResult Afiliacion()
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
                    if (!menu.Contains("SAPP_BO_TAR_MAN_PER_COM_T") && menu.Contains("SAPP_BO_TAR")&&menu.Contains("SAPP_BO_TAR_AFI_DES_PRO"))
                    {
                        TempData["procesoInicial"] = "Afiliacion";
                        return RedirectToAction("Index");
                    }
                    else {
                        return RedirectToAction("Index", "Home");
                    }
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
                else
                {
                    string[] menu = sesion.usuario.politicas.Split('|');
                    if (!menu.Contains("SAPP_BO_TAR_MAN_PER_COM_T") && menu.Contains("SAPP_BO_TAR") &&menu.Contains("SAPP_BO_TAR_CAM_TAR"))
                    {
                        TempData["procesoInicial"] = "Cambio de Tarjeta";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult RegistroPaso1(SeguimientoProcesos proceso)
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
                if (proceso == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    proceso.registro=!(proceso.nombreProceso.Equals("Mantenimiento de Ahorros") || proceso.nombreProceso.Equals("Ingreso a Custodia DPF"));
                    Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                    if (string.IsNullOrEmpty(sesion.guid))
                    {
                        sesion.host.guid = Guid.NewGuid().ToString();
                    }
                    proceso.detalleCliente = new List<DatosCliente>();
                    HttpContext.Session.SetString("autorizado", ManagerJson.SerializarObjecto(sesion));
                    string flujo = "0";
                    if(HttpContext.Session.GetString("procesoApertura") != null)
                    {
                        SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                        if(pasos.tipoPersona.Equals(proceso.tipoPersona))
                        {
                            if (pasos.nMancomuna <= proceso.nMancomuna) 
                            {
                                pasos.nMancomuna = proceso.nMancomuna;
                                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                                flujo = "1";
                            }
                            else
                            {
                                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(proceso));
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(proceso));
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(proceso));
                    }
                    TempData["flujo"] = flujo;
                    return RedirectToAction("Paso2");
                }
            }
        }
        public ActionResult Paso2()
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
                if (HttpContext.Session.GetString("procesoApertura") == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string flechas = "0";
                    if (TempData["flujo"] != null)
                    {
                        flechas = TempData["flujo"].ToString();
                    }
                    SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    int procesoAPaso2 = 0; 
                    int flujo = clienteFlujo(pasos);
                    if (flujo == -1)
                    {
                        procesoAPaso2 = pasos.detalleCliente.Count;
                    }
                    else
                    {
                        procesoAPaso2 = flujo;
                    }
                    TempData["proceso"] = pasos.nombreProceso + "-" + pasos.tipoPersona + "-" + (!string.IsNullOrEmpty(sesion.guid) && pasos.detalleCliente.Count()==0 ? "S" : "N") + (procesoAPaso2 + 1).ToString();
                    TempData["navegacion"] = flechas;
                    RespuestaParametro response = this._business.obtenerParametro();
                    List<List<SelectListItem>> listaTotal = DataParametro.selectParametro(response);
                    return View(listaTotal);
                }
            }
        }
        [HttpPost]
        public string ModificarDireccionP2(string direccion, string localidad, string telefono)
        {
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            int flujo = clienteFlujo(pasos);
            DatosCliente cliente;
            if (flujo == -1)
            {
                if (pasos.tipoPersona.ToUpper().Equals("N"))
                {
                    pasos.detalleCliente.Last().pn.direccion = direccion;
                    pasos.detalleCliente.Last().pn.localidad = localidad;
                    pasos.detalleCliente.Last().pn.telefono = telefono;
                }
                else
                {
                    pasos.detalleCliente.Last().pj.direccion = direccion;
                    pasos.detalleCliente.Last().pj.localidad = localidad;
                    pasos.detalleCliente.Last().pj.telefono = telefono;
                }
            }
            else
            {
                if (pasos.tipoPersona.ToUpper().Equals("N"))
                {
                    pasos.detalleCliente[flujo].pn.direccion=direccion;
                    pasos.detalleCliente[flujo].pn.localidad = localidad;
                    pasos.detalleCliente[flujo].pn.telefono = telefono;
                }
                else
                {
                    pasos.detalleCliente[flujo].pj.direccion = direccion;
                    pasos.detalleCliente[flujo].pj.localidad = localidad;
                    pasos.detalleCliente[flujo].pj.telefono = telefono;
                }
            }
            HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
            return "OK";
        }
        [HttpPost]
        public string RegistroPaso2(string localidad,string adicional,string filtroCuentas,string parametros, string modificarData,string modificarDataE)
        {
            string url = "0";
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                int flujo = clienteFlujo(pasos);
                #region modificar DPN
                if (!string.IsNullOrEmpty(parametros))
                {
                    SapResponse respuesta;                    
                    Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                    DatosCliente cliente;
                    if (flujo == -1)
                    {
                        cliente = pasos.detalleCliente.Last();
                    }
                    else
                    {
                        cliente = pasos.detalleCliente[flujo];
                    }
                    string[] ids = ManagerValidation.obtenerIDC(cliente.idC, cliente.idT);
                    PersonaNatural modificar = ManagerJson.DeserializarObjecto<PersonaNatural>(parametros);                   
                    #region REC
                    if (modificar.situacionLaboral.Equals("DEP") || modificar.situacionLaboral.Equals("EST") || modificar.situacionLaboral.Equals("JUB"))
                    {
                        if ((string.IsNullOrEmpty(cliente.pn.ruc) && string.IsNullOrEmpty(cliente.pn.nombreEmpresa)) && !string.IsNullOrEmpty(modificar.ruc) && !string.IsNullOrEmpty(modificar.nombreEmpresa))
                        {
                            DateTime hoy = DateTime.Now;
                            DateTime vencimiento = hoy.AddYears(1);
                            while (vencimiento.DayOfWeek == DayOfWeek.Saturday || vencimiento.DayOfWeek == DayOfWeek.Sunday)
                            {
                                vencimiento = vencimiento.AddDays(1);
                            }
                            DatosRec dataRec = new DatosRec
                            {
                                idcPersona = modificar.ruc,
                                tipoDocumentoPersona = "R",
                                fechaInicio = hoy.ToString("dd/MM/yyyy"),
                                fechaValor = "",
                                fechaVerificacion = hoy.ToString("dd/MM/yyyy"),
                                fechaTermino = hoy.ToString("dd/MM/yyyy"),
                                nombre = modificar.nombreEmpresa,
                                vigente = "S",
                                codigoRelacion = "PE",
                                codMoneda = "",
                                codigoAsociado = "",
                                valorAsociado = ""
                            };
                            SapResponse respuestaRec = this._business.OperacionesRecPNJ(sesion.host, ids, cliente.idT, dataRec, true);
                            if (!respuestaRec.success)
                            {
                                modificar.ruc = null;
                                modificar.nombreEmpresa = null;
                            }
                        }
                    }
                    #endregion
                    #region AGREGAR CORREO
                    if (modificarDataE.Equals("1"))
                    {
                        if (!string.IsNullOrEmpty(modificar.celular))
                        {
                            if (string.IsNullOrEmpty(modificar.mail))
                            {
                                modificar.mail = "SIN E@MAIL";
                            }
                            DatosDireccion correo = new DatosDireccion
                            {
                                codLocalidad = "0000",
                                tipoDireccion = "M",
                                direccionComprimida = modificar.mail,
                                estado = "C",
                                telefono = modificar.celular
                            };
                            SapResponse respuestacorreo = this._business.OperacionesDireccionPNJ(sesion.host, ids, cliente.idT, correo, true);
                            if (!respuestacorreo.success)
                            {
                                modificar.mail = null;
                                modificar.celular= null;
                            }
                        }
                    }
                    #endregion                   
                    if (modificarData.Equals("1"))
                    {
                        LimpiarDatos.combinarDatos<PersonaNatural>(cliente.pn, modificar);
                        if (!string.IsNullOrEmpty(cliente.pn.gradoInstruccion))
                        {
                            cliente.pn.gradoInstruccion = "NDI";
                        }
                        if (cliente.pn.negocioPropio.Equals("S") && !string.IsNullOrEmpty(cliente.pn.nombreComercial))
                        {
                            cliente.pn.tipoCuenta = "I";
                        }
                        else
                        {
                            cliente.pn.negocioPropio = "N";
                            cliente.pn.tipoCuenta= "P";
                            cliente.pn.nombreComercial = string.Empty;
                        }
                        ModificacionDataPN modificarDatosPaso2 = ManagerJson.DeserializarObjecto<ModificacionDataPN>(ManagerJson.SerializarObjecto(cliente.pn));
                        modificarDatosPaso2.domicilio = cliente.pn.direccion;
                        respuesta = this._business.ModificarPNJ(sesion, cliente.idC, cliente.idT, modificarDatosPaso2, null, "n");
                        if (respuesta.success)
                        {
                            if (flujo == -1)
                            {
                                pasos.detalleCliente[pasos.detalleCliente.Count - 1] = cliente;
                            }
                            else
                            {
                                pasos.detalleCliente[flujo] = cliente;
                            }
                        }
                    }
                }
                #endregion
                #region OBTENER URL
                if (flujo == -1)
                {
                    pasos.detalleCliente.Last().localidadDescripcion = string.IsNullOrEmpty(localidad)?"":localidad;
                    pasos.detalleCliente.Last().estadoCivilDescripcion = adicional;
                    pasos.detalleCliente.Last().filtroAN = !filtroCuentas.Equals("0");
                    pasos.detalleCliente.Last().detalleCuenta = new List<AperturaCuenta>();
                    if (pasos.nombreProceso != "Mantenimiento de Ahorros")
                    {
                        if (pasos.nMancomuna != pasos.detalleCliente.Count)
                        {
                            url = Url.Action("Paso2", "Registro");
                        }
                        else if (pasos.nombreProceso.Equals("Apertura"))
                        {
                            url = Url.Action("Paso3", "Registro");
                        }
                        else if (pasos.nombreProceso.Equals("Afiliacion") || pasos.nombreProceso.Equals("Cambio de Tarjeta"))
                        {
                            url = Url.Action("AlfabeticaTarjeta", "Consulta");
                        }
                        else if (pasos.nombreProceso.Equals("Ingreso a Custodia DPF"))
                        {
                            url = Url.Action("Paso4", "Registro");
                        }
                    }
                    else
                    {
                        url = Url.Action("MantAhorro", "Home");
                    }
                }
                else
                {
                    pasos.detalleCliente[flujo].flujo = false;
                    pasos.detalleCliente[flujo].localidadDescripcion = string.IsNullOrEmpty(localidad) ? "" : localidad;
                    pasos.detalleCliente[flujo].estadoCivilDescripcion = adicional;
                    pasos.detalleCliente[flujo].filtroAN = !filtroCuentas.Equals("0");
                    pasos.detalleCliente[flujo].detalleCuenta = new List<AperturaCuenta>();
                    int flujo1 = clienteFlujo(pasos);
                    if (flujo1 == -1)
                    {
                        if (!pasos.nombreProceso.Equals("Mantenimiento de Ahorros"))
                        {
                            if (pasos.nMancomuna != pasos.detalleCliente.Count)
                            {
                                url = Url.Action("Paso2", "Registro");
                            }
                            else if (pasos.nombreProceso.Equals("Apertura"))
                            {
                                url = Url.Action("Paso3", "Registro");
                            }
                            else if (pasos.nombreProceso.Equals("Afiliacion") || pasos.nombreProceso.Equals("Cambio de Tarjeta"))
                            {
                                url = url = Url.Action("AlfabeticaTarjeta", "Consulta");
                            }
                            else if (pasos.nombreProceso.Equals("Ingreso a Custodia DPF"))
                            {
                                url = Url.Action("Paso4", "Registro");
                            }
                        }
                        else
                        {
                            url = Url.Action("MantAhorro", "Home");
                        }
                    }
                    else
                    {
                        url = Url.Action("Paso2Flujo", "Registro");
                    }
                }
                if (pasos.nMancomuna > 1 && !url.Contains("Paso2"))
                {
                    pasos.detalleCliente = OrdenarClienteExclusivo(pasos.detalleCliente, pasos.tipoPersona);
                }
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                #endregion
            }
            catch (Exception)
            {
                //return RedirectToAction("Index","Home");
            }
            return url;
        }
        private List<DatosCliente> OrdenarClienteExclusivo(List<DatosCliente> anterior,string tipoPersona)
        {
            List<DatosCliente> ordenado = new List<DatosCliente>();
            ordenado.Add(null);
            bool exclusivo = false;
            for(int i = 0; i < anterior.Count; i++)
            {
                if (tipoPersona.Equals("N"))
                {
                    if(!exclusivo&&(anterior[i].pn.tipoCliente.Equals("CX")|| anterior[i].pn.tipoCliente.Equals("VP")))
                    {
                        ordenado[0] = anterior[i];
                        exclusivo = true;
                    }
                    else
                    {
                        ordenado.Add(anterior[i]);
                    }
                }
                else
                {
                    if (!exclusivo && (anterior[i].pj.tipoCliente.Equals("CX") || anterior[i].pj.tipoCliente.Equals("VP")))
                    {
                        ordenado[0] = anterior[i];
                        exclusivo = true;
                    }
                    else
                    {
                        ordenado.Add(anterior[i]);
                    }
                }
            }
            if (!exclusivo)
            {
                ordenado.RemoveAt(0);
            }
            return ordenado;
        }
        public ActionResult Paso2Flujo()
        {
            TempData["flujo"] = "1";
            return RedirectToAction("Paso2");
        }
        public ActionResult Paso3()
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
                    List<TipoCuenta> producto = new List<TipoCuenta>();
                    if (HttpContext.Session.GetString("procesoApertura") != null)
                    {
                        SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                        string tipoP = pasos.tipoPersona;
                        if (!pasos.tipoPersona.Equals("J"))
                        {
                            int edad = 999;
                            int aux = 0;
                            for(int i=0;i< pasos.detalleCliente.Count; i++) 
                            {
                                aux = int.Parse(LimpiarDatos.edad(pasos.detalleCliente[i].pn.fechaNacimiento));
                                if (aux < edad)
                                {
                                    edad = aux;
                                }
                            }
                            TempData["edad"] = edad.ToString();
                            tipoP = tipoP + pasos.nMancomuna;
                        }
                        TempData["tipoPersona"] = tipoP;
                        RespuestaTipoCuenta resultado = this._business.obtenerListaCuentas(pasos.tipoPersona);
                        HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                        string[] politicas = sesion.usuario.politicas.Split('|');
                        if (resultado.success)
                        {
                            string tipoDocumento = pasos.detalleCliente.First().idT;
                            bool ArchNeg = pasos.detalleCliente.First().filtroAN;
                            string[] permitidas = this._business.cuentasAN();
                            for (int i = 0; i < resultado.data.Count; i++)
                            {
                                if (tipoDocumento.Equals("W"))
                                {
                                    if (resultado.data[i].codigo.Equals("FIS"))
                                    {
                                        if (politicas.Contains(resultado.data[i].comando))
                                        {
                                            producto.Add(resultado.data[i]);
                                        }
                                    }
                                }
                                else if (ArchNeg)
                                {
                                    if (permitidas.Contains(resultado.data[i].codigo))
                                    {
                                        if (politicas.Contains(resultado.data[i].comando))
                                        {
                                            producto.Add(resultado.data[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!resultado.data[i].codigo.Equals("FIS"))
                                    {
                                        if (politicas.Contains(resultado.data[i].comando))
                                        {
                                            producto.Add(resultado.data[i]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (producto.Count == 0)
                    {
                        producto.Add(new TipoCuenta());
                    }
                    return View(model: producto);
                }
            }
        }
        public ActionResult Paso4(ProductoModalidad cuentaProducto)
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
                    if (pasos.nombreProceso.Equals("Ingreso a Custodia DPF"))
                    {
                        cuentaProducto = new ProductoModalidad
                        {
                            producto = "ICB",
                            idModalidad = "",
                            codigoProducto = "",
                            codigoModalidad = ""
                        };
                        //string tipo = Request.Form["LVCuenta"].ToString();
                        //string modalidad = Request.Form["LVModalidadCue"].ToString();
                    }
                    AperturaCuenta cuenta=new AperturaCuenta { cuentaProducto = cuentaProducto };
                    for(int i = 0; i < pasos.detalleCliente.Count; i++)
                    {
                        pasos.detalleCliente[i].detalleCuenta.Add(cuenta);
                    }
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                    if (pasos.tipoPersona.Equals("J"))
                    {
                        return View("Paso4J", pasos);
                    }
                    else
                    {
                        return View("Paso4N", pasos);
                    }
                }
            }
        }
        public ActionResult Paso5()
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
                    string persona = pasos.tipoPersona;
                    TempData["TipoPersona"] = persona;
                    if (persona.ToUpper().Equals("N"))
                    {
                        int crCuenta = (from t in pasos.detalleCliente.First().detalleCuenta where t.cuentaProducto.producto[0].Equals('A') && !t.cuentaProducto.codigoModalidad.Equals("103") select t.detalleCuenta.nroCuenta).Count();
                        if (crCuenta>0)
                        {
                            List<ConfiguracionSeguros> Seguros = _business.GetListSeguros("Cuenta de Ahorro");
                            ViewBag.ListaSeguros = Seguros;
                            ViewBag.idC = pasos.detalleCliente.First().idC;
                            ViewBag.Idt = pasos.detalleCliente.First().idT;
                        }
                    }
                    return View(pasos.detalleCliente.First().detalleCuenta);
                }
            }
        }
        [HttpPost]
        public JsonResult RegistroFiltro()
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion =ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.RegistroFiltro(pasos, sesion);
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
        public JsonResult AperturaProductoAhorro(CajaAhorro ahorro)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.AperturaProductoAhorro(ahorro,ref pasos, sesion);
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
        public JsonResult AperturaProductoCorriente(CuentaCorriente corriente)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.AperturaProductoCorriente(corriente, ref pasos, sesion);
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
        public JsonResult AperturaProductoDPF(DPF dpf)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.AperturaProductoDPF(dpf, ref pasos, sesion);
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
        public JsonResult AperturaProductoCustodia(Custodia custodia)
        {
            RespuestaCreacionCuenta response = new RespuestaCreacionCuenta();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.AperturaProductoCustodia(custodia, ref pasos, sesion);
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
        
        public ActionResult Paso6()
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
                    try
                    {
                        string recursion = Request.Form["nuevaCuenta"].ToString();
                        if (recursion.Equals("S"))
                        {
                            return RedirectToAction("Paso3");
                        }
                        else
                        {
                            string tarjeta = Request.Form["asignarTarjeta"].ToString();
                            if (tarjeta.Equals("no"))
                            {
                                return RedirectToAction("ImpContrato", "Home");
                            }
                        }
                    }
                    catch (Exception ex) { }
                    int c = 0;

                    SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    string modalidad = "";
                    if (pasos.nombreProceso.Equals("Apertura"))
                    {
                        modalidad = pasos.detalleCliente.Last().detalleCuenta.Last().detalleCuenta.indModalidad;
                    }
                    for (int i = 0; i < pasos.detalleCliente.Count; i++)
                    {
                        if (i > 0 && modalidad.Contains("MC"))
                        {

                        }
                        else if(string.IsNullOrEmpty(pasos.detalleCliente[i].tarjeta))
                        {
                            c++;
                        }
                    }
                    if (c == 0) 
                    {
                        return RedirectToAction("ImpContrato", "Home");
                    }
                    else
                    {
                        TempData["TipoPersona"] = pasos.tipoPersona + "-" + modalidad;
                        return View(pasos.detalleCliente);
                    }
                }
            }
        }        
        [HttpPost]
        public JsonResult ConsultaProCli()
        {
            RespuestaConsultaProducto response = new RespuestaConsultaProducto();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaProCli(pasos.detalleCliente.First().idC, pasos.detalleCliente.First().idT, "", "", sesion);
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
        public JsonResult ConsultaPNJ(string id, string tid,bool primeraConsulta)
        {
            string[] idC = ManagerValidation.obtenerIDC(id, tid);
            if (!idC.Contains("?"))
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                bool encontrado = false;
                for (int i = 0; i < pasos.detalleCliente.Count(); i++)
                {
                    if (pasos.detalleCliente[i].idC.Equals(idC[0] + idC[1]))
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (!encontrado) 
                {
                    bool apertura = ManagerValidation.validarProceoApertura(pasos.nombreProceso);
                    Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                    if (pasos.tipoPersona.Equals("J"))
                    {
                        RespuestaPersonaJuridica respuesta = this._business.ConsultaClientePJ(sesion.host,idC,tid);
                        string mensaje = respuesta.message;
                        if (respuesta.success)
                        {
                            bool continuar = true;
                            if (apertura)
                            {
                                SapResponse respuestaFN = this._business.ConsultaFunNeg(sesion.host, respuesta.data.funcionarioNegocios);
                                if (respuestaFN.success)
                                {
                                    bool modificaTC = false;
                                    continuar = ManagerValidation.validarTipoClienteApertura(respuesta.data.tipoCliente, sesion.usuario.politicas.Split('|'), ref mensaje, ref modificaTC);
                                    if (modificaTC)
                                    {
                                        SapResponse respuestaTC = this._business.ModificarTipoClientePNJ(sesion.host, id, tid, pasos.tipoPersona.ToLower(), "CL");
                                        continuar = respuestaTC.success;
                                        respuesta.data.tipoCliente = "CL";
                                    }
                                }
                                else
                                {
                                    continuar = false;
                                    mensaje = respuestaFN.message;
                                }
                            }
                            if (continuar) 
                            { 
                                int flujo = clienteFlujo(pasos);
                                if (flujo == -1)
                                {
                                    if (primeraConsulta)
                                    {
                                        pasos.detalleCliente.Add(new DatosCliente { idC = idC[0] + idC[1], idT = tid, pj = respuesta.data });
                                    }
                                    else
                                    {
                                        if (pasos.detalleCliente.Count > 0)
                                        {
                                            pasos.detalleCliente.Last().idC = idC[0] + idC[1];
                                            pasos.detalleCliente.Last().idT = tid;
                                            pasos.detalleCliente.Last().pj = respuesta.data;
                                        }
                                        else
                                        {
                                            pasos.detalleCliente.Add(new DatosCliente { idC = idC[0] + idC[1], idT = tid, pj = respuesta.data });
                                        }
                                    }
                                }
                                else
                                {
                                    pasos.detalleCliente[flujo].idC = idC[0] + idC[1];
                                    pasos.detalleCliente[flujo].idT = tid;
                                    pasos.detalleCliente[flujo].pj = respuesta.data;
                                }
                                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                            }
                            else
                            {
                                respuesta = new RespuestaPersonaJuridica
                                {
                                    state = "40",
                                    message = mensaje
                                };
                            }
                        }
                        return Json(respuesta);
                    }
                    else
                    {                        
                        RespuestaPersonaNatural respuesta = this._business.ConsultaClientePN(sesion.host, idC, tid,true,apertura);
                        string mensaje = respuesta.message;
                        if (respuesta.success)
                        {
                            bool continuar = true;
                            string cambio = this._business.QR();
                            if (!(respuesta.data.cambioQR && tid.ToUpper().Equals("XX") &&cambio.Equals("S")))
                            {
                                respuesta.data.cambioQR = false;
                            }
                            if (apertura) 
                            {
                                SapResponse respuestaFN = this._business.ConsultaFunNeg(sesion.host, respuesta.data.funcionarioNegocios);
                                if (respuestaFN.success)
                                {
                                    bool modificarTC = false;
                                    continuar = ManagerValidation.validarTipoClienteApertura(respuesta.data.tipoCliente, sesion.usuario.politicas.Split('|'), ref mensaje, ref modificarTC);
                                    if (modificarTC)
                                    {
                                        SapResponse respuestaTC = this._business.ModificarTipoClientePNJ(sesion.host, id, tid, pasos.tipoPersona.ToLower(), "CL");
                                        continuar = respuestaTC.success;
                                        respuesta.data.tipoCliente = "CL";
                                    }
                                }
                                else
                                {
                                    continuar = false;
                                    mensaje = respuestaFN.message;
                                }
                            }
                            if (continuar)
                            {
                                int flujo = clienteFlujo(pasos);
                                if (flujo == -1)
                                {
                                    if (primeraConsulta)
                                    {
                                        pasos.detalleCliente.Add(new DatosCliente { idC = idC[0] + idC[1], idT = tid, pn = respuesta.data });
                                    }
                                    else
                                    {
                                        if (pasos.detalleCliente.Count > 0)
                                        {
                                            pasos.detalleCliente.Last().idC = idC[0] + idC[1];
                                            pasos.detalleCliente.Last().idT = tid;
                                            pasos.detalleCliente.Last().pn = respuesta.data;
                                        }
                                        else
                                        {
                                            pasos.detalleCliente.Add(new DatosCliente { idC = idC[0] + idC[1], idT = tid, pn = respuesta.data });
                                        }
                                    }
                                }
                                else
                                {
                                    pasos.detalleCliente[flujo].idC = idC[0] + idC[1];
                                    pasos.detalleCliente[flujo].idT = tid;
                                    pasos.detalleCliente[flujo].pn = respuesta.data;
                                }
                                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                            }
                            else
                            {
                                respuesta = new RespuestaPersonaNatural
                                {
                                    state = "40",
                                    message = mensaje
                                };
                            }
                        }
                        return Json(respuesta);
                    }                    
                }
                else
                {

                    SapResponse resultado = new SapResponse
                    {
                        state = "77",
                        message = "CLIENTE YA FUE REGISTRADO PARA EL PROCESO"
                    };
                    return Json(resultado);
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
        [HttpPost]
        public string VoBoSupervisor(Login login)
        {
            string mensaje = "EL USUARIO NO TIENE LOS PERMISOS NECESARIOS";
            try
            {
                if (this._business.VoBoSupervisor(login))
                {
                    mensaje = "OK";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return mensaje;
        }
        private int clienteFlujo(SeguimientoProcesos pasos)
        {
            int c = -1;
            for (int i = 0; i < pasos.detalleCliente.Count; i++)
            {
                if (pasos.detalleCliente[i].flujo)
                {
                    c = i;
                    break;
                }
            }
            return c;
        }
        [HttpPost]
        public JsonResult CrearPNJ(string id, string tid,string cliente,string tipoPersona,string complemento)
        {
            string[] idC = ManagerValidation.obtenerIDC(id, tid);
            RespuestaCreacionCliente respuesta;
            if (!idC.Contains("?"))
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                if (tipoPersona.ToLower().Equals("j"))
                {
                    CreacionClientePJ data = ManagerJson.DeserializarObjecto<CreacionClientePJ>(cliente);
                    if(tid.Equals("Y") && data.finSocial.Equals("SF"))
                    {
                        data.tipoCliente = "CT";
                    }
                    else
                    {
                        data.tipoCliente = "CL";
                    }
                    data.negocioPropio = "N";
                    respuesta = this._business.CrearClientePJ(sesion.host, idC,  tid, data);
                }
                else
                {
                    CreacionClientePN data = ManagerJson.DeserializarObjecto<CreacionClientePN>(cliente);
                    respuesta = this._business.CrearClientePN(sesion.host, idC, tid, data);
                    if (respuesta.success)
                    {
                        RegistroINFOCLIENTE infocliente = ManagerJson.DeserializarObjecto<RegistroINFOCLIENTE>(complemento);
                        infocliente.cic = respuesta.data.cic;
                        SapResponse respuestaRegistro = this._business.RegistrarINFOCLIENTE(idC, tid, infocliente, sesion.usuario.usuario);
                        if (!respuestaRegistro.success)
                        {
                            respuesta.message = respuestaRegistro.message;
                        }
                    }
                }
                if (respuesta.success)
                {

                    SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                    int flujo = clienteFlujo(pasos);
                    if (pasos.tipoPersona.Equals("J"))
                    {
                        PersonaJuridica dataCliente = ManagerJson.DeserializarObjecto<PersonaJuridica>(cliente);
                        dataCliente.cic = respuesta.data.cic;
                        dataCliente.sucursal = sesion.host.agencia;
                        dataCliente.oficina = sesion.host.agencia;
                        dataCliente.tipoBanca = "P";
                        dataCliente.funcionarioNegocios = "B000001";                        
                        if (tid.Equals("Y") && dataCliente.finSocial.Equals("SF"))
                        {
                            dataCliente.tipoCliente = "CT";
                        }
                        else
                        {
                            dataCliente.tipoCliente = "CL";
                        }
                        if(flujo == -1)
                        {
                            pasos.detalleCliente.Add(new DatosCliente());
                            pasos.detalleCliente.Last().idC = idC[0] + idC[1];
                            pasos.detalleCliente.Last().idT = tid;

                            pasos.detalleCliente.Last().pj = dataCliente;
                        }
                        else
                        {
                            pasos.detalleCliente[flujo].idC = idC[0] + idC[1];
                            pasos.detalleCliente[flujo].idT = tid;
                            pasos.detalleCliente[flujo].pj = dataCliente;
                        }
                    }
                    else
                    {
                        PersonaNatural dataCliente = ManagerJson.DeserializarObjecto<PersonaNatural>(cliente);
                        dataCliente.cic = respuesta.data.cic;
                        dataCliente.sucursal = sesion.host.agencia;
                        dataCliente.oficina = sesion.host.agencia;
                        dataCliente.tipoBanca = "P";
                        dataCliente.tipoCliente = "CL";
                        //dataCliente.fechaUltActualizacion = DateTime.Now.ToString("dd/MM/yyyy");
                        if (dataCliente.negocioPropio.Equals("S"))
                        {
                            dataCliente.funcionarioNegocios = "B000006";
                        }
                        else
                        {
                            dataCliente.funcionarioNegocios = "B000001";
                        }
                        if (flujo == -1)
                        {
                            pasos.detalleCliente.Add(new DatosCliente());
                            pasos.detalleCliente.Last().idC = idC[0] + idC[1];
                            pasos.detalleCliente.Last().idT = tid;

                            pasos.detalleCliente.Last().pn = dataCliente;
                        }
                        else
                        {
                            pasos.detalleCliente[flujo].idC = idC[0] + idC[1];
                            pasos.detalleCliente[flujo].idT = tid;
                            pasos.detalleCliente[flujo].pn = dataCliente;
                        }
                    }
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                }
                
            }
            else
            {
                respuesta = new RespuestaCreacionCliente
                {
                    state = "77",
                    message = "FORMATO DE IDC INVALIDO"
                };
            }
            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult ConsultaFacta(string id, string tid, string cic)
        {
            RespuestaConsultaFatca response=new RespuestaConsultaFatca();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.ConsultaFacta(id, tid, cic, sesion);
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
        public JsonResult CreacionFacta(string id, string tid, Fatca facta)
        {
            SapResponse response =new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                response = this._business.CreacionFacta(id, tid, facta, sesion);
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
        public JsonResult ConsultaAlfabetica(string cliente, bool completo, string idc, string idt, string ide)
        {
            RespuestaConsultaAlfabetica response=new RespuestaConsultaAlfabetica();
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
        [HttpPost]
        public string ArchivoNegativo(string id, string tid, string paterno, string materno,string nombres, string tipoPersona)
        {
            string response = "0";
            try
            {
                Sesion sesion = sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                string[] politicas = sesion.usuario.politicas.Split('|');
                if (politicas.Contains("SAPP_BO_CON_ARC_NEG")) 
                {
                    RespuestaRegistro resultado = this._business.ArchivoNegativo(id, tid, tipoPersona, paterno, materno, nombres);
                    if (resultado.success)
                    {
                        if (resultado.data.encontrado)
                        {
                            if (resultado.data.codigo > 0)
                            {
                                if (!politicas.Contains("SAPP_BO_PRO_APE_CLI_NEG"))
                                {
                                    string[] permitidas = this._business.cuentasAN();
                                    if (permitidas.Length> 0)
                                    {
                                        response = "2";
                                    }
                                    else
                                    {
                                        response = "1";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        response = "1";
                    }
                }
            }
            catch(Exception ex)
            {
                response = "1";
            }
            finally
            {

            }
            return response;
        }
        [HttpPost]
        public JsonResult ANEC(string id, string tid, string paterno, string materno, string nombres,string tipoPersona,string operacion)
        {
            RespuestaRegistro resultado = new RespuestaRegistro();
            if (operacion.Equals("AN"))
            {
                resultado = this._business.ArchivoNegativo(id, tid, tipoPersona,paterno,materno, nombres);
            }
            else
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                resultado = this._business.EnameChecker(sesion, paterno, materno, nombres);
            }           
            if (resultado.success)
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                int flujo = clienteFlujo(pasos);
                if (flujo == -1)
                {
                    if (operacion.Equals("AN"))
                    {
                        if (pasos.detalleCliente.Count > 0)
                        {
                            if (pasos.detalleCliente.Last().localidadDescripcion==null) 
                            {
                                pasos.detalleCliente.Last().AN = resultado.data.encontrado;
                            }                            
                        }
                    }
                    else
                    {
                        pasos.detalleCliente.Last().EC = resultado.data.encontrado;
                    }
                }
                else
                {
                    if (operacion.Equals("AN"))
                    {
                        pasos.detalleCliente[flujo].AN = resultado.data.encontrado;
                    }
                    else
                    {
                        pasos.detalleCliente[flujo].EC = resultado.data.encontrado;
                    }
                }
                HttpContext.Session.SetString("procesoApertura",ManagerJson.SerializarObjecto(pasos));
            }
            return Json(resultado);
        }           
        [HttpPost]
        public JsonResult CrearRecPN(string id, string tid, string nit, string nombreEmpresa)
        {
            SapResponse response = new SapResponse();
            this._operacion = ManagerOperation.GenerateOperation();
            try
            {
                string[] ids = ManagerValidation.obtenerIDC(id, tid);
                string[] ide = ManagerValidation.obtenerIDC(nit, "R");
                DateTime hoy = DateTime.Now;
                DatosRec data= new DatosRec
                    {
                    idcPersona = ide[0],
                        tipoDocumentoPersona = "R",
                        extensionPersona = ide[1],
                        fechaInicio = hoy.ToString("dd/MM/yyyy"),
                        fechaValor = "",
                        fechaVerificacion = hoy.ToString("dd/MM/yyyy"),
                        fechaTermino = hoy.ToString("dd/MM/yyyy"),
                        nombre = nombreEmpresa,
                        vigente = "S",
                        codigoRelacion = "PE",
                        codMoneda = "",
                        codigoAsociado = "",
                        valorAsociado = ""
                    };
                ParametrosHost host = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado")).host;
                response = this._business.OperacionesRecPNJ(host,ids,tid,data,true);
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
        public JsonResult ListaModalidad(string valor)
        {
            RespuestaProducto response = new RespuestaProducto();
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                response = this._business.ListaModalidad(valor, pasos);
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            return Json(response);
        }
        public ActionResult LimpiarPaso1()
        {
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            int indice = pasos.detalleCliente.Count;
            string proceso = pasos.nombreProceso;
            if (indice == 0)
            {
                limpiarFlujos();
                TempData["procesoInicial"] = proceso;
                return RedirectToAction("Index");
            }
            else if (pasos.nMancomuna == 1)
            {
                pasos.detalleCliente[0].flujo = true;
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                TempData["procesoInicial"] = proceso;
                return RedirectToAction("Index");
            }
            else
            {
                string redireccionA = "Paso2";
                pasos.detalleCliente[indice - 1].flujo = true;
                if (pasos.detalleCliente[indice - 1].localidadDescripcion==null)
                {
                    try
                    {
                        TempData["flujo"] = "1";
                        pasos.detalleCliente[indice - 2].flujo = true;
                    }
                    catch (Exception)
                    {
                        TempData["procesoInicial"] = proceso;
                        redireccionA = "Index";
                    }
                }
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));                
                return RedirectToAction(redireccionA);
            }
        }
        [HttpPost]
        public JsonResult RecargarDatosGuardados()
        {
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            int c = clienteFlujo(pasos);
            return Json(pasos.detalleCliente[c]);
        }
        [HttpPost]
        public string LimpiarCuentas()
        {
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            if (pasos.detalleCliente.Count > 0)
            {
                int indice = pasos.detalleCliente.Last().detalleCuenta.Count;
                if (indice == 0)
                {
                    var iP2 = pasos.detalleCliente.Count();
                    pasos.detalleCliente.RemoveAt(iP2 - 1);
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                }
            }
            return "ok";
        }

        public ActionResult LimpiarPaso3()
        {
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            if (pasos.detalleCliente.Last().detalleCuenta.Count==0)
            {
                pasos.detalleCliente.Last().flujo=true;
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                TempData["flujo"] = "1";
                return RedirectToAction("Paso2");
            }
            else
            {
                return RedirectToAction("Paso5");
            }
        }

        [HttpPost]
        public string LimpiarPaso4()
        {
            string mensaje = "1";
            try
            {
                SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                for(int i = 0; i < pasos.detalleCliente.Count; i++)
                {
                    pasos.detalleCliente[i].detalleCuenta.RemoveAt(pasos.detalleCliente[i].detalleCuenta.Count - 1);
                }                
                HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
            }
            catch (Exception)
            {
                mensaje = "0";
            }
           return mensaje;
        }
        [HttpPost]
        public JsonResult ActualizarFunNeg(string indicador)
        {
            RespuestaCambioFunNeg respuesta=new RespuestaCambioFunNeg();
            SeguimientoProcesos pasos = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
            DatosCliente cliente;
            int flujo = clienteFlujo(pasos);
            if (flujo == -1)
            {
                cliente = pasos.detalleCliente.Last();
            }
            else
            {
                cliente = pasos.detalleCliente[flujo];
            }
            string funNegocios = "";
            string banca = "";
            bool consulta = false;
            string sucursal = "";
            string agencia="";
            if (pasos.tipoPersona.ToUpper().Equals("N"))
            {
                funNegocios = cliente.pn.funcionarioNegocios;
                if(string.IsNullOrEmpty(funNegocios) || funNegocios.Equals("B000002")|| funNegocios.Equals("B000003"))
                {
                    if (indicador.Equals("S"))
                    {
                        funNegocios= "B000006";
                        cliente.pn.tipoCuenta = "I";
                    }
                    else
                    {
                        funNegocios = "B000001";
                        cliente.pn.tipoCuenta = "P";
                    }
                    banca= cliente.pn.tipoBanca;
                    cliente.pn.funcionarioNegocios = funNegocios;
                    sucursal = cliente.pn.sucursal[0].ToString() + "01";
                    agencia = cliente.pn.sucursal;
                    consulta = true;
                }
            }
            else
            {
                funNegocios = cliente.pj.funcionarioNegocios;
                if (string.IsNullOrEmpty(funNegocios) || funNegocios.Equals("B000002") || funNegocios.Equals("B000003"))
                {
                    if (indicador.Equals("FL"))
                    {
                        funNegocios = "B000006";
                    }
                    else
                    {
                        funNegocios = "B000007";
                    }
                    banca = cliente.pj.tipoBanca;
                    cliente.pj.funcionarioNegocios = funNegocios;
                    sucursal = cliente.pj.sucursal[0].ToString() + "01";
                    agencia = cliente.pj.sucursal;
                    consulta = true;
                }
            }
            if (consulta)
            {
                Sesion sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
                sesion.host.sucursal = sucursal;
                sesion.host.agencia = agencia;
                FunNeg cambioFunData = new FunNeg
                { 
                    funcionarioNegocios = funNegocios,
                    banca = banca
                };
                respuesta = this._business.CambioFunNeg(sesion, cliente.idC, cliente.idT, cambioFunData, pasos.tipoPersona.ToLower());
                if (respuesta.success)
                {
                    if (flujo == -1)
                    {
                        pasos.detalleCliente[pasos.detalleCliente.Count - 1] = cliente;
                    }
                    else
                    {
                        pasos.detalleCliente[flujo] = cliente;
                    }
                    HttpContext.Session.SetString("procesoApertura", ManagerJson.SerializarObjecto(pasos));
                }
            }
            return Json(respuesta);
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

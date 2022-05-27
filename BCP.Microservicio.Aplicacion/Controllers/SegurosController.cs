using BCP.Framework.Common;
using BCP.Framework.Logs;
using BCP.Sap.Business;
using BCP.Sap.Models.Sap;
using BCP.Sap.Models.Seguros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.Microservicio.Aplicacion.Controllers
{
    public class SegurosController : Controller
    {
        private readonly ISapBusiness _business;
        private readonly ILogger _logger;
        private string _operacion;
        public SegurosController(ILogger logger, ISapBusiness business)
        {
            this._logger = logger;
            this._business = business;
        }

        [HttpPost]
        public JsonResult AfiliarSeguro(string codigoSeguro, string cuenta, string idc, string tipo, string extension, string complemento)
        {

            #region Afiliar a Seguro
            RegistrarSeguroRequest datos = new();
            var sesion = ManagerJson.DeserializarObjecto<Sesion>(HttpContext.Session.GetString("autorizado"));
            datos.Idc = idc;
            datos.Tipo = tipo;
            datos.Extension = extension;
            datos.Complemento = complemento;
            datos.Cuenta = cuenta;
            datos.IdProducto = codigoSeguro;
            datos.Sucursal = sesion.host.sucursal;
            datos.Agencia = sesion.host.agencia;
            try 
            {
                SeguimientoProcesos proceso = ManagerJson.DeserializarObjecto<SeguimientoProcesos>(HttpContext.Session.GetString("procesoApertura"));
                datos.Profesion = proceso.detalleCliente.First().pn.profesion;
            }
            catch (Exception) { }
            UserCredenciales newCredentials = new();
            newCredentials.Ip = sesion.host.ipOrigen;
            newCredentials.Usuario = sesion.usuario.usuario.Split("\\")[1];
            datos.UserCredenciales = newCredentials;
            RegistrarSeguroResponse RespuestaAfiliacion = _business.AfiliacionSeguro(datos);
            #endregion
            return Json(RespuestaAfiliacion);
        }
        public string CertificadoSeguro(GeneraContratoRequest datos)
        {
            GeneraContratoResponse generaContratoResponse = new();
            generaContratoResponse = _business.CertificadoSeguro(datos);
            if (generaContratoResponse.success)
                return generaContratoResponse.data.PDF;
            else
                return null;
        }
    }
}

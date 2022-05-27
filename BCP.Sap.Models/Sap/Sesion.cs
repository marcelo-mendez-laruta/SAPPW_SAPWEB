using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Rebibir;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class Sesion
    {
        public bool autorizado { get; set; }
        public DatosUsurio usuario { get; set; }
        public string guid { get; set; }
        public ParametrosHost host { get; set; }
        public ParametrosSLK estacion { get; set; }
        public Login credenciales { get; set; }
        public RespuestaGUID datosGUID { get; set; }
    }   
}

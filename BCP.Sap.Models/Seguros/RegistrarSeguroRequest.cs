using BCP.Sap.Models.Comunes;
using BCP.Sap.Models.Enviar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Seguros
{
    public class RegistrarSeguroRequest: EnvioOperacion
    {
        public string Idc { get; set; } //CI 6767294
        public string Tipo { get; set; } //Ejemplo: Q
        public string Extension { get; set; }//LP
        public string Complemento { get; set; }//Ejemplo: 00
        public string Cuenta { get; set; }//Tarjeta o cuenta
        public string IdProducto { get; set; }//Codigo de producto
        public string Agencia { get; set; }
        public string Sucursal { get; set; }
        public string Profesion { get; set; }//temporal
        public UserCredenciales UserCredenciales { get; set; }
    }
    public class UserCredenciales
    {
        public string Usuario { get; set; }
        public string Ip { get; set; }
    }
}

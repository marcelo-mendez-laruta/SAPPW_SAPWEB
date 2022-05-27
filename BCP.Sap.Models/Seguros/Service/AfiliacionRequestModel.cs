using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Seguros.Service
{
    public class AfiliacionRequestModel
    {
        public Persona Persona { get; set; }
        public string Sucursal { get; set; }
        public string Agencia { get; set; }
        public string Cuenta { get; set; }
        public string IdProducto { get; set; }
        public UserCredenciales UserCredenciales { get; set; }
    }
    public class Persona
    {
        public string CELULAR {set;get;}

        public string CI {set;get;}

        public string CIC {set;get;}

        public string COMPLEMENTO {set;get;}

        public string CORREO {set;get;}

        public string DIRECCION {set;get;}

        public string ESTADO_CIVIL {set;get;}

        public string EXTENSION {set;get;}

        public string FECHA_NACIMIENTO {set;get;}

        public string IMAGEN {set;get;}

        public string MATERNO {set;get;}

        public string NACIONALIDAD {set;get;}

        public string NOMBRE {set;get;}

        public string NUMERO_DIRECCION {set;get;}

        public string OCUPACION {set;get;}

        public string PATERNO {set;get;}

        public string PROFESION {set;get;}

        public string SEXO {set;get;}

        public string TELEFONO {set;get;}

        public string TIPO_DIRECCION {set;get;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Sap.Models.Rebibir;
namespace BCP.Sap.Models.Sap
{
    public class RespuestaPersonaNatural : SapResponse
    {
        public PersonaNatural data { get; set; }
    }
    public class PersonaNatural:Persona
    {
        public string paterno { get; set; }//
        public string materno { get; set; }//
        public string nombres { get; set; }//
        public string gradoInstruccion { get; set; }//
        public string sexo { get; set; }//
        public string situacionLaboral { get; set; }//
        public string estadoCivil { get; set; }//
        public string profesion { get; set; }//
        public string fechaNacimiento { get; set; }//
        public string nacionalidad { get; set; }//
        public string segmento { get; set; }//
        public string tipoCuenta { get; set; }//        
        public string indicadorVivienda { get; set; }//
        public string nombreEmpresa { get; set; }
        public string mail { get; set; }
        public string celular { get; set; }
        public string ruc { get; set; }
        public string negocioPropio { get; set; }
        public bool cambioQR { get; set; }
        //public string paisNacimiento { get; set; }//
        //public string segundaNacionalidad { get; set; }//
        //public string fiscales { get; set; }//
        //public string pais_Fiscales { get; set; }//
        //public string tiN_Fiscales { get; set; }//
        //public string ssN_Fiscales { get; set; }//
        //public string paisResidencia { get; set; }//
        //public string complemento { get; set; }//
    }
}

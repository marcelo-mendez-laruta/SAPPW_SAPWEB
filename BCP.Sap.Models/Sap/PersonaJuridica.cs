using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Sap.Models.Rebibir;
namespace BCP.Sap.Models.Sap
{
    public class RespuestaPersonaJuridica : SapResponse
    {
        public PersonaJuridica data { get; set; }
    }
    public class PersonaJuridica : Persona
    {
        public string finSocial { get; set; }
        public string fechaConstitucion { get; set; }
        public string categoria { get; set; }
        public string naturalezaJuridica { get; set; }
        public string razonSocial { get; set; }
        public string sucursalAgencia { get; set; }
        public string tipoEmpresa { get; set; }
    }
}

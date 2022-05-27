using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Sap
{
    public class Persona
    {
        public string residente { get; set; }
        public string nombreComercial { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string ciiu { get; set; }
        public string telefono { get; set; }
        public string oficina { get; set; }
        public string tipoCliente { get; set; }
        public string funcionarioNegocios { get; set; }
        public string tipoBanca { get; set; }
        public string registroMercantil { get; set; }
        public string magnitudEmpresa { get; set; }
        public string nit { get; set; }
        public string estadoNIT { get; set; }
        public string codigoSBS { get; set; }
        public string cic { get; set; }
        public string correspondencia { get; set; }
        public string segmentoBanca { get; set; }
        public string sucursal { get; set; }

        public string idc { get; set; }
        public string extension { get; set; }
        public string tipoDocumento { get; set; }
        public string fechaUltActualizacion { get; set; }//
    }
}

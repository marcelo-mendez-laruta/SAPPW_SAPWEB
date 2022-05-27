using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Sap
{
    public class Parametro
    {
        public string grupo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public bool dependencia { get; set; }
        public string codigoNivelSuperior { get; set; }
    }
}

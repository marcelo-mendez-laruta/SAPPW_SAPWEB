using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionSeguros
    {
        public string Nombre { get; set; }
        public List<TipoSeguro> Tipo { get; set; }
        public bool Estado { get; set; }
        public bool Checked { set; get; }
    }
    public class TipoSeguro
    {
        public string Periodo { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
    }
}

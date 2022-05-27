using BCP.Sap.Models.Enviar.ProcesoApertura;
using BCP.Sap.Models.Rebibir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Cache
{
    public class ListaModalidadModel
    {
        public RespuestaProducto response { get; set; }
        public EnvioTipoProducto request { get; set; }
    }
    public class ArrayListaModalidadModel
    {
        public List<ListaModalidadModel> lista { get; set; }    
    }
}

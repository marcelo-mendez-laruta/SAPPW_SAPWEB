using BCP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Rebibir.Tarjeta
{
    public class RespuestaBusquedaTarjeta : SapResponse
    {
        public int total { get; set; }
        public List<BusquedaTarjeta> data { get; set; }
    }
    public class BusquedaTarjeta
    {
        private string nroCredimas1;
        private string nombre1;
        private string situacion1;
        private string clienteExclusivo1;

        public string nroCredimas { get => nroCredimas1; set => nroCredimas1 = LimpiarDatos.credimas(value); }
        public string nombre { get => nombre1; set => nombre1 = value[0].Equals('?')?value.Substring(1):value; }
        public string situacion { get=>situacion1; set=>situacion1=ManagerHost.situacionTarjeta(value); }
        public string clienteExclusivo { get => clienteExclusivo1; set => clienteExclusivo1 = ManagerHost.ex(value); }
    }
}

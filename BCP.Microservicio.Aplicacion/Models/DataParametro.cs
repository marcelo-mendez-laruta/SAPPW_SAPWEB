using BCP.Microservicio.Aplicacion.Models;
using BCP.Sap.Connectors;
using BCP.Sap.Models.Enviar;
using BCP.Sap.Models.Rebibir;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.Microservicio.Aplicacion.Models
{
    public class DataParametro
    {
        public static List<List<SelectListItem>> selectParametro(RespuestaParametro resultado)
        {
            List<List<SelectListItem>> listaTotal=new List<List<SelectListItem>>(); 
            if (resultado != null)
            {
                if (resultado.success)
                {
                    var agrupacion = from grupo in resultado.data
                                     group grupo by grupo.grupo into agrupado
                                     orderby agrupado.Key
                                     select agrupado;

                    List<SelectListItem> lista = new List<SelectListItem>();
                    int c = 0;
                    foreach (var i in agrupacion)
                    {
                        foreach (var item in i)
                        {
                            lista.Add(new SelectListItem { Value = item.codigo.Trim(), Text = item.descripcion.Trim() });
                            //System.Diagnostics.Debug.WriteLine(item.grupo);
                            //break;
                        }
                        listaTotal.Add(lista.ToList());
                        lista.Clear();
                    }
                }
            }
            if (listaTotal.Count < 23)
            {
                while (listaTotal.Count < 23)
                {
                    listaTotal.Add(new List<SelectListItem>());
                }
            }
            return listaTotal;
        }
    }
}

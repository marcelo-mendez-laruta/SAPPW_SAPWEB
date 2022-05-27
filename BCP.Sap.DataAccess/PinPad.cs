using BCP.Framework.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BCP.Sap.DataAccess
{
    public class PinPad
    {
        private static async Task<string> consumir(string url)
        {
            string responseContent = string.Empty;
            try
            {
                Uri baseURL = new Uri(url);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(baseURL.ToString());
                
                if (response.IsSuccessStatusCode)
                {
                    responseContent = await response.Content.ReadAsStringAsync();

                }
            }
            catch(Exception ex)
            {

            }
            return responseContent;

        }
        private static string[] consulta(string url)
        {
            string[] respuesta = new string[2];
            string resultado = string.Empty;
            Task.Run(async () =>
            {
                resultado = await consumir(url);
            }).Wait();
            if (!resultado.Equals(""))
            {
                resultado = ManagerJson.DeserializarObjecto<string>(resultado);
                respuesta = resultado.Split(',');
            }
            return respuesta;
        }

        public static string[] consumirTarjeta()
        {
            string url = "http://127.0.0.1:9991/Values/GetTarjeta/1";
            return consulta(url);
        }
        public static string[] consumirPin(string tarjeta)
        {
            string url = "http://127.0.0.1:9991/Values/GetPin/";
            if (tarjeta != null &&  tarjeta.Replace("-","").Length==16)
            {
                url = url + tarjeta.Replace("-", "");
                return consulta(url);
            }
            else
            {
                return new string[2];
            }
        }
    }
}

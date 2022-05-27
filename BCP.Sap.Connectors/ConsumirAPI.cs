using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using BCP.Framework.Common;
using BCP.Sap.Models.Configuracion;

namespace BCP.Sap.Connectors
{
    public class ConsumirAPI
    {
        private ConfiguracionApi _credencialesAPI;
        private string _url;
        public ConsumirAPI(ConfiguracionApi redencialesAPI,string url)
        {
            this._credencialesAPI = redencialesAPI;
            this._url = url;
        }

        public T consulta<T>(string metodo, object request)
        {
            T response;
            try
            {
                string endpoint = this._url+metodo;            
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
                httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    string contrasena= LimpiarDatos.EnDecryp(this._credencialesAPI.contrasena, false);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this._credencialesAPI.usuario + ":" + contrasena)));
                    httpClient.DefaultRequestHeaders.Add("Channel", this._credencialesAPI.canal);
                    httpClient.DefaultRequestHeaders.Add("PublicToken", this._credencialesAPI.token);
                    httpClient.DefaultRequestHeaders.Add("AppUserId", this._credencialesAPI.usuarioApp);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Correlation-Id", ManagerOperation.GenerateOperation());
                    httpClient.DefaultRequestHeaders.Add("api-version", "1.0");
                    var json = ManagerJson.SerializarObjecto(request);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = httpClient.PostAsync(new Uri(endpoint), content).Result;
                    var resultContent = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        response = ManagerJson.DeserializarObjecto<T>(resultContent);
                    }
                    else
                    {
                        response = ManagerJson.DeserializarObjecto<T>("{\"operation\":\"" + DateTime.Now.ToString("yyyyMMddhhmmss") + "\"  ,\"state\": \"" + (int)result.StatusCode + "\",\"message\": \"" + result.ReasonPhrase + "\"}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}

using Newtonsoft.Json;

namespace BCP.Framework.Common
{
    public class ManagerJson
    {
        public static T DeserializarObjecto<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string SerializarObjecto(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
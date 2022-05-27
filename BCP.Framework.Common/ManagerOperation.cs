using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Common
{
    public class ManagerOperation
    {
        public static string GenerateOperation(string request)
        {
            string response = DateTime.Now.ToString("yyyyMMddhhmmssff");
            try
            {
                if (!string.IsNullOrEmpty(request))
                    response = request;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static string GenerateOperationV2(string request)
        {
            string response = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                if (!string.IsNullOrEmpty(request))
                    response = request;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static string GenerateOperation()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmssff");
        }

        public static string Fecha_Hora()
        {
            return System.DateTime.Now.ToString("yyyyMMdd_hhmmss");
        }

        public static string Fecha112()
        {
            return System.DateTime.Now.ToString("yyyyMMdd");
        }
    }
}
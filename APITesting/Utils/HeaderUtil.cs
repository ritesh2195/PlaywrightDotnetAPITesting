using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Utils
{
    public class HeaderUtil
    {
        public static Dictionary<string, string> GetHeader()
        {
            string email = "riteshranjanmishra938@gmail.com";

            string apiToken = "ATATT3xFfGF0h1Hbqr0czDumi50xvGPMoY9OJN1vm3MQDjeECWmOMDRK_DpJGSPCVNRVxpCnELW5EWherErwOsK_lsXkGeVu8Et8OnQE8y6E0XjRsjJX1LYNh5gAaeEH3ROsT0pzWK5ok2sQDbk1Ybt5kV6TK6GX01IBSxYZnlPp6cquJA_gDh4=7C92F60E";

            string token = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{apiToken}"));

            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");

            headers.Add("Authorization", token);

            return headers;
        }
    }
}

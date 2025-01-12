using EcommercePlaywrightAutomation.Config;
using Microsoft.Playwright;
using System.Text;

namespace APITesting.Driver
{
    public class PlaywrightAPIDriver
    {

        public static ThreadLocal<IAPIRequestContext> apiLocalDriver = new ThreadLocal<IAPIRequestContext>();

        public async Task<IAPIRequestContext> InitializePlaywrightAPIDriver()
        {
            var playwright = await Playwright.CreateAsync();

            apiLocalDriver.Value = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = ConfigReader.ReadConfig().BaseUrl,
                ExtraHTTPHeaders = GetHeader()
            });

            return GetDriver();
        }

        public static IAPIRequestContext GetDriver()
        {
            return apiLocalDriver.Value!;
        }

        public static Dictionary<string, string> GetHeader()
        {

            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");

            headers.Add("Authorization", GetAuthBasicToken());

            return headers;
        }

        public static string GetAuthBasicToken()
        {
            string email = ConfigReader.ReadConfig().Email;

            string apiKey = ConfigReader.ReadConfig().ApiKey;

            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{apiKey}"));
        }
    }
}

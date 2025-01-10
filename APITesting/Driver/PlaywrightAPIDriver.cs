using APITesting.Utils;
using EcommercePlaywrightAutomation.Config;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITesting.Driver
{
    public class PlaywrightAPIDriver
    {

       public static ThreadLocal<IAPIRequestContext> apiLocalDriver = new ThreadLocal<IAPIRequestContext>();

        public async Task<IAPIRequestContext> InitializePlaywrightAPIDriver()
        {
            var configData = ConfigReader.ReadConfig();

            string token = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configData.Email}:{configData.ApiKey}"));

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var playwright = await Playwright.CreateAsync();

            apiLocalDriver.Value =  await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = configData.BaseUrl,
                ExtraHTTPHeaders = HeaderUtil.GetHeader()
            });

            return GetDriver();
        }

        public static IAPIRequestContext GetDriver()
        {
            return apiLocalDriver.Value!;
        }
    }
}

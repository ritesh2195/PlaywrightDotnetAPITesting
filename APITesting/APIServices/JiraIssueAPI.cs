using APITesting.Constant;
using APITesting.Utils;
using EcommercePlaywrightAutomation.Config;
using Microsoft.Playwright;
using System.Text;

namespace APITesting.APIServices
{
    public class JiraIssueAPI
    {

        private IPlaywright? _playwright;
        private IAPIRequestContext? _apiRequestContext;

        public async Task InitializeIssueAPIAsync()
        {
            var configData = ConfigReader.ReadConfig();

            string token = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configData.Email}:{configData.ApiKey}"));

            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");

            headers.Add("Authorization", token);

            _playwright = await Playwright.CreateAsync();

            _apiRequestContext = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = configData.BaseUrl,
                ExtraHTTPHeaders = HeaderUtil.GetHeader()
            });
        }

        public async Task<IAPIResponse> GetIssueDetailsAsync(string issueId)
        {
            return await _apiRequestContext.GetAsync($"{JiraEndPointConstants.IssueEndPoint}/{issueId}");
        }

        public async Task<IAPIResponse> CreateIssueAsync(Object payload)
        {
            return await _apiRequestContext.PostAsync($"{JiraEndPointConstants.IssueEndPoint}/", new APIRequestContextOptions()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"User-Agent","abc" }
                },
                DataObject = payload
            });
        }

        public async Task<IAPIResponse> DeleteIssueAsync(string issueId)
        {
            return await _apiRequestContext.DeleteAsync($"{JiraEndPointConstants.IssueEndPoint}/{issueId}");
        }
    }
}

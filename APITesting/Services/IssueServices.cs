using APITesting.Constant;
using APITesting.Utils;
using EcommercePlaywrightAutomation.Config;
using Microsoft.Playwright;
using System.Text;

namespace APITesting.APIServices
{
    public class IssueServices
    {
        private readonly Task<IAPIRequestContext> _apiRequestContext;

        public IssueServices()
        {
            _apiRequestContext = Task.Run(InitializeIssueAPIAsync);

        }

        private async Task<IAPIRequestContext> InitializeIssueAPIAsync()
        {
            var configData = ConfigReader.ReadConfig();

            string token = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configData.Email}:{configData.ApiKey}"));

            Dictionary<string, string> headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");

            headers.Add("Authorization", token);

            var playwright = await Playwright.CreateAsync();

            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = configData.BaseUrl,
                ExtraHTTPHeaders = HeaderUtil.GetHeader()
            });
        }

        public async Task<IAPIResponse> GetIssueDetailsAsync(string key)
        {
            return await _apiRequestContext.Result.GetAsync($"{EndPointConstants.IssueEndPoint}/{key}");
        }

        public async Task<IAPIResponse> CreateIssueAsync(Object payload)
        {
            return await _apiRequestContext.Result.PostAsync($"{EndPointConstants.IssueEndPoint}/", new APIRequestContextOptions()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"User-Agent","abc" }
                },
                DataObject = payload
            });
        }

        public async Task<IAPIResponse> DeleteIssueAsync(string key)
        {
            return await _apiRequestContext.Result.DeleteAsync($"{EndPointConstants.IssueEndPoint}/{key}");
        }

        public async Task<IAPIResponse> AddCommentOnIssue(string issueId, Object payload)
        {
            return await _apiRequestContext.Result.PostAsync($"{EndPointConstants.IssueEndPoint}/{issueId}/comment",
                new APIRequestContextOptions
                {
                    DataObject = payload
                });
        }
    }
}

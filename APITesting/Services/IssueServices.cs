using APITesting.Constant;
using Microsoft.Playwright;

namespace APITesting.APIServices
{
    public class IssueServices
    {

        private readonly IAPIRequestContext _requestContext;

        public IssueServices(IAPIRequestContext requestContext)
        {
            _requestContext = requestContext;

        }

        public async Task<IAPIResponse> GetIssueDetailsAsync(string key)
        {
            return await _requestContext.GetAsync($"{EndPointConstants.IssueEndPoint}/{key}");
        }

        public async Task<IAPIResponse> CreateIssueAsync(Object payload)
        {
            return await _requestContext.PostAsync($"{EndPointConstants.IssueEndPoint}/", new APIRequestContextOptions()
            {
                DataObject = payload
            });
        }

        public async Task<IAPIResponse> DeleteIssueAsync(string key)
        {
            return await _requestContext.DeleteAsync($"{EndPointConstants.IssueEndPoint}/{key}");
        }

        public async Task<IAPIResponse> AddCommentOnIssue(string issueId, Object payload)
        {
            return await _requestContext.PostAsync($"{EndPointConstants.IssueEndPoint}/{issueId}/comment",
                new APIRequestContextOptions
                {
                    DataObject = payload
                });
        }

        public async Task<IAPIResponse> AddAttachmentOnIssue(string issueId, string filePath)
        {

            var multipart = _requestContext.CreateFormData();

            multipart.Append("file", new FilePayload()
            {
                Name = Path.GetFileName(filePath),
                MimeType = "image/jpeg",
                Buffer = File.ReadAllBytes(filePath)
            });

            return await _requestContext.PostAsync($"{EndPointConstants.IssueEndPoint}/{issueId}/attachments",
                new APIRequestContextOptions
                {
                    Headers = new Dictionary<string, string>()
                {
                    {"X-Atlassian-Token","nocheck" }
                },
                    Multipart = multipart

                });
        }
    }
}

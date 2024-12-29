using APITesting.Models;
using Microsoft.Playwright;
using static APITesting.Models.JiraIssueModel;

namespace APITesting.Tests
{
    public class CreateIssueTest : BaseTest
    {

        [Test]
        public async Task CreateNewIssue()
        {
            var issuePayload = new JiraIssueModel
            {
                Fields = new IssueFields
                {
                    Project = new Project { Key = "RP" },
                    Summary = faker.Random.Words(12),
                    Description = faker.Random.Words(20),
                    Issuetype = new IssueType { Name = "Bug" },
                    Priority = new Priority { Name = "High" }
                }
            };

            IAPIResponse createIssueResponse = await issueServices.CreateIssueAsync(issuePayload);

            Assert.That(createIssueResponse.Status, Is.EqualTo(201));

            string createIssueId = (await createIssueResponse.JsonAsync()).Value.GetProperty("id").ToString();

            IAPIResponse getIssueResponse = await issueServices.GetIssueDetailsAsync(createIssueId);

            Assert.That(getIssueResponse.Status, Is.EqualTo(200));

            var getIssueJsonResponse = await getIssueResponse.JsonAsync();

            Assert.That(createIssueId, Is.EqualTo(getIssueJsonResponse.Value.GetProperty("id").ToString()));

            Assert.That(issuePayload.Fields.Description, Is.EqualTo(getIssueJsonResponse.Value.GetProperty("fields").GetProperty("description").ToString()));

            Assert.That((await issueServices.DeleteIssueAsync(createIssueId)).Status, Is.EqualTo(204));
        }
    }
}

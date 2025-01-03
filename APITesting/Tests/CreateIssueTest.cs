using APITesting.Models;
using Microsoft.Playwright;
using static APITesting.Models.JiraIssueModel;

namespace APITesting.Tests
{
    public class CreateIssueTest : BaseTest
    {
        private static string? createIssueKey;
        private static string? createIssueId;

        [Test]
        [Order(0)]
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

            createIssueKey = (await createIssueResponse.JsonAsync()).Value.GetProperty("key").ToString();

            createIssueId = (await createIssueResponse.JsonAsync()).Value.GetProperty("id").ToString();

            IAPIResponse getIssueResponse = await issueServices.GetIssueDetailsAsync(createIssueKey);

            Assert.That(getIssueResponse.Status, Is.EqualTo(200));

            var getIssueJsonResponse = await getIssueResponse.JsonAsync();

            Assert.That(createIssueKey, Is.EqualTo(getIssueJsonResponse.Value.GetProperty("key").ToString()));

            Assert.That(issuePayload.Fields.Description, Is.EqualTo(getIssueJsonResponse.Value.GetProperty("fields").GetProperty("description").ToString()));
        }

        [Test]
        [Order(1)]
        public async Task AddCommentTest()
        {
            var commentResponse = await issueServices.AddCommentOnIssue(createIssueId, new IssueComment { Body = faker.Random.Words(5) });

            Assert.That(commentResponse.Status,Is.EqualTo(201));
        }

        [Test]
        [Order(2)]
        public async Task DeleteIssueTest()
        {
            Assert.That((await issueServices.DeleteIssueAsync(createIssueKey)).Status, Is.EqualTo(204));
        }
    }
}

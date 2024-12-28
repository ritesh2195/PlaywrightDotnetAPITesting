using APITesting.APIServices;
using Bogus;

namespace APITesting.Tests
{
    public class BaseTest
    {
        public JiraIssueAPI issueAPI;
        public Faker faker;

        [SetUp]
        public async Task SetUp()
        {
            issueAPI = new JiraIssueAPI();

            await issueAPI.InitializeIssueAPIAsync();

            faker = new Faker();
        }
    }
}

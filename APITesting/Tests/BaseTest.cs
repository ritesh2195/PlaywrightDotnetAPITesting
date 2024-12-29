using APITesting.APIServices;
using APITesting.Utils;
using AventStack.ExtentReports;
using Bogus;

namespace APITesting.Tests
{
    public class BaseTest
    {
        public IssueServices issueServices;
        public Faker faker;

        [SetUp]
        public async Task SetUp()
        {
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports", $"{TestContext.CurrentContext.Test.Name}_Report.html");

            reportPath = Path.GetFullPath(reportPath);

            ExtentReportUtil.InitializeReport(reportPath);

            ExtentReportUtil.CreateTest(TestContext.CurrentContext.Test.Name);

            issueServices = new IssueServices();

            await issueServices.InitializeIssueAPIAsync();

            faker = new Faker();
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportUtil.FlushReport();
        }
    }
}

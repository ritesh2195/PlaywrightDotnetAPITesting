using APITesting.APIServices;
using APITesting.Driver;
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

            faker = new Faker();

            var driver = new PlaywrightAPIDriver();

            await driver.InitializePlaywrightAPIDriver();

            issueServices = new IssueServices(PlaywrightAPIDriver.GetDriver());
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportUtil.FlushReport();
        }
    }
}

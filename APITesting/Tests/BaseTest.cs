using APITesting.APIServices;
using APITesting.Driver;
using APITesting.Services;
using APITesting.Utils;
using Bogus;
using Microsoft.Playwright;

namespace APITesting.Tests
{
    public class BaseTest
    {
        protected IssueServices issueServices;
        protected UserService userService;
        private IAPIRequestContext _requestContext;
        public Faker faker;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports", "TestSuite_Report.html");

            reportPath = Path.GetFullPath(reportPath);

            ExtentReportUtil.InitializeReport(reportPath);
        }

        [SetUp]
        public async Task SetUp()
        {
            // Initialize Extent report for the current test
            ExtentReportUtil.CreateTest(TestContext.CurrentContext.Test.Name);

            // Initialize Faker for test data
            faker = new Faker();

            // Initialize Playwright API Driver
            var driver = new PlaywrightAPIDriver();
            _requestContext = await driver.InitializePlaywrightAPIDriver();

            // Initialize API Services
            issueServices = new IssueServices(PlaywrightAPIDriver.GetDriver());
            userService = new UserService(PlaywrightAPIDriver.GetDriver());
        }

        [TearDown]
        public async Task TearDown()
        {
            // Dispose of the Playwright API Request Context
            if (_requestContext != null)
            {
                await _requestContext.DisposeAsync();

                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var message = TestContext.CurrentContext.Result.Message;

                switch (status)
                {
                    case NUnit.Framework.Interfaces.TestStatus.Passed:
                        ExtentReportUtil.LogPass("Test passed successfully.");
                        break;

                    case NUnit.Framework.Interfaces.TestStatus.Failed:
                        ExtentReportUtil.LogFail($"Test failed: {message}");
                        break;

                    default:
                        ExtentReportUtil.LogFail("Test did not complete as expected.");
                        break;
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Flush the Extent report to ensure all results are written
            ExtentReportUtil.FlushReport();
        }
    }
}

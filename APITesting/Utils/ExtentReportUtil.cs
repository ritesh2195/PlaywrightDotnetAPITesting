using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace APITesting.Utils
{
    public static class ExtentReportUtil
    {
        private static ExtentReports? extent;

        public static void InitializeReport(string reportPath)
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                var spark = new ExtentSparkReporter(reportPath);
                extent.AttachReporter(spark);
            }
        }

        public static ExtentTest CreateTest(string testName)
        {
            if (extent == null)
            {
                throw new InvalidOperationException("ExtentReports has not been initialized.");
            }

            return extent.CreateTest(testName);
        }

        public static void FlushReport()
        {
            extent?.Flush();
        }
    }
}

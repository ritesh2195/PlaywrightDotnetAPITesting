using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace APITesting.Utils
{
    public static class ExtentReportUtil
    {
        private static ExtentReports? _extentReport;
        private static ExtentTest? _extentTest;

        public static void InitializeReport(string reportPath)
        {
            if (_extentReport == null)
            {
                _extentReport = new ExtentReports();
                var spark = new ExtentSparkReporter(reportPath);
                _extentReport.AttachReporter(spark);
            }
        }

        public static ExtentTest CreateTest(string testName)
        {
            if (_extentReport == null)
            {
                throw new InvalidOperationException("ExtentReports has not been initialized.");
            }

            _extentTest =_extentReport.CreateTest(testName);

            return _extentTest;
        }

        public static void FlushReport()
        {
            _extentReport?.Flush();
        }

        public static void LogPass(string details)
        {
            if (_extentTest == null)
            {
                throw new InvalidOperationException("No active test. Use StartTest to begin a test.");
            }

            _extentTest.Pass(details);
        }

        public static void LogFail(string details)
        {
            if (_extentTest == null)
            {
                throw new InvalidOperationException("No active test. Use StartTest to begin a test.");
            }

            _extentTest.Fail(details);
        }
    }
}

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;

namespace MR_Automation
{
    [SetUpFixture]
    public class BaseClass
    {
        string currentTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string reportFileName = TestConstants.GetConfigKeyValue("ReportFileName");
        string reportFilePath = TestConstants.GetConfigKeyValue("ReportFilePath");

        [OneTimeSetUp]
        public void ExtentStart()
        {
            // Create instance for web driver
            TestConstants.Driver = new ChromeDriver();

            // Maximize the browser window
            TestConstants.Driver.Manage().Window.Maximize();
            
            TestConstants.Extent = new ExtentReports();
            
            string spath = $"{reportFilePath}\\{reportFileName}{currentTimeStamp}.html";
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, spath);
            string reportPath = Path.GetFullPath(sFile);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            TestConstants.Extent.AttachReporter(htmlReporter);
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            TestConstants.Extent.Flush();

            // Creat a copy of the package file 'index.html' with the specified file name and the timestamp
            File.Copy($"{reportFilePath}\\index.html", $"{reportFilePath}\\{reportFileName}{currentTimeStamp}.html");

            try
            {
                // Close the browser
                if (TestConstants.Driver != null)
                {
                    TestConstants.Driver.Quit();
                    // Dispose of the driver
                    TestConstants.Driver.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error closing browser: " + e.Message);
                throw; // Rethrow the exception
            }
        }
    }
}
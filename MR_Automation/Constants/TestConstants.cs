using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace MR_Automation
{
    public static class TestConstants
    {
        public static ChromeDriver Driver;
        //TODO: This needs to be read from config file
        public static string BaseURL = GetConfigKeyValue("BaseURL");
        public static ExtentReports Extent;
        public static ExtentTest LogTest;
        public static WebDriverWait Wait;

        public static string ProjectName { get; set; }
        public static List<string> UploadedProjectFiles { get; set; } = new List<string>();
        public static void NavigateToWebPage(string _partialurl)
        {
            // Assign the url
            var url = $"{BaseURL + _partialurl}";

            // Navigate to the site URL
            Driver.Navigate().GoToUrl(url);
        }

        public static string GetConfigKeyValue(string key)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                if (value == null)
                {
                    // Key not found in configuration
                    return string.Empty;
                }
                return value;
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine($"Error reading configuration file: {ex.Message}");
                LogTest.Log(Status.Fail, $"Error reading configuration file: {ex.Message}");
                return null;
            }
        }
    }
}

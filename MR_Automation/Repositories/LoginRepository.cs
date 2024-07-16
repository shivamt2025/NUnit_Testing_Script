using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace MR_Automation
{
    public class LoginRepository
    {
        internal string _usernameElementId = "outlined-basic";
        internal string _passwordElementId = "outlined-adornment-password";
        internal string _xPathForErrorPrompt = "//div[@class='mt-2']/span";
        internal string _errorText = "Invalid Username/Password";
        internal string _xPathForWelcomeText = "//div[@class='border-r']//span";


        public void LoginSteps(string username, string password)
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify login functionality with valid credentials.").Info("Verify login functionality with valid credentials.");

            // Calling NavigateToWebPage method
            TestConstants.NavigateToWebPage("login");
            TestConstants.LogTest.Log(Status.Info, "Application login page opened successfully.");

            TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Find username element and assign value
            TestConstants.Driver.FindElement(By.Id(_usernameElementId)).SendKeys(TestConstants.GetConfigKeyValue("username"));
            TestConstants.LogTest.Log(Status.Info, "Username entered.");

            // Find password element and assign value
            TestConstants.Driver.FindElement(By.Id(_passwordElementId)).SendKeys(TestConstants.GetConfigKeyValue("password"));
            TestConstants.LogTest.Log(Status.Info, "Password entered.");

            // Find login button
            IWebElement loginButton = TestConstants.Driver.FindElement(By.Id(":r2:"));

            if (loginButton != null)
            {
                // Perform click operation on the login button
                loginButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Login button clicked.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Login button is not visible.");
            }
            
        }


        public void LoginWithValidCredentials()
        {
            string username = TestConstants.GetConfigKeyValue("Username");
            string password = TestConstants.GetConfigKeyValue("Password");

            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify login functionality with valid credentials.")
                                              .Info("Verify login functionality with valid credentials.");

            // Navigate to the login page
            TestConstants.NavigateToWebPage("login");
            TestConstants.LogTest.Log(Status.Info, "Application login page opened successfully.");

            TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Find username element and assign value
            TestConstants.Driver.FindElement(By.Id(_usernameElementId)).SendKeys(username);
            TestConstants.LogTest.Log(Status.Info, "Username entered.");

            // Find password element and assign value
            TestConstants.Driver.FindElement(By.Id(_passwordElementId)).SendKeys(password);
            TestConstants.LogTest.Log(Status.Info, "Password entered.");

            // Find login button
            IWebElement loginButton = TestConstants.Driver.FindElement(By.Id(":r2:"));

            if (loginButton != null)
            {
                // Perform click operation on the login button
                loginButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Login button clicked.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Login button is not visible.");
            }
        }
    }
}

using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace MR_Automation
{

    [TestFixture, Order(1)]
    public class LoginTest : LoginRepository
    {
        [Test, Order(1)]
        public void LoginWithValidCredentialsTest()
        {
            // Calling the LoginSteps method from LoginRepository
            LoginSteps("Username", "Password");

            // Define WebDriverWait with a timeout of 50 seconds
            WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(50));

            // Wait until the element with text Welcome is visible               
            IWebElement homePageText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForWelcomeText)));
            TestConstants.LogTest.Log(Status.Info, "Application home page visible.");



            System.Threading.Thread.Sleep(2000);



            //Get the current url 
            string actualUrl = TestConstants.Driver.Url;

            //Assign the expected url to compare with the current url
            string expectedUrl = $"{TestConstants.BaseURL}dashboard/projects";

            //Verify the actual and expected url condition
            if (!string.IsNullOrEmpty(actualUrl) && actualUrl.Equals(expectedUrl))
            {
                TestConstants.LogTest.Log(Status.Pass, "Application user login successful.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Application user login unsuccessful.");
            }
        }
/*
        //[Test]
        public void LoginWithInvalidCredentialsTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify login functionality with invalid credentials.").Info("Verify login functionality with invalid credentials.");

            // Calling the LoginSteps method from LoginRepository
            LoginSteps("InvalidUsername", "InvalidPassword");

            //Find the error message displayed on the login page
            IWebElement loginErrorText = TestConstants.Driver.FindElement(By.XPath(_xPathForErrorPrompt));

            //Verify if the expected error message matches with the displayed error message
            if (loginErrorText != null && !string.IsNullOrEmpty(loginErrorText.Text) && loginErrorText.Text == _errorText)
            {
                TestConstants.LogTest.Log(Status.Info, _errorText);
            }
            else
            {
                TestConstants.LogTest.Log(Status.Pass, "Application user login successful.");
            }
        }

        */
     }
}

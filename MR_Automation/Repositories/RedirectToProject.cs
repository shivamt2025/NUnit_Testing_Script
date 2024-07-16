using AventStack.ExtentReports;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Automation.Repositories
{
    public class RedirectToProject : LoginRepository
    {
        #region login
        internal string _cssSelectorForSearchBar = "input.w-full.h-12.pl-10";
        internal string _xpathForAvailableForInternalReview = "//div[span='Available for internal review']";
        internal string _xpathForEditFunctionality = "//span[text()='Edit']";
        internal string _xpathForEntitiesButton = "//div[contains(@class, 'bg-[#9747FF]') and text()='Entities']";
        #endregion

        #region apply_changes
        internal string _cssForProceedButton = "button.border.w-48.p-2.rounded[style*='border: 1px solid red']";
        internal string _xpathForApplyChanges = "//button[contains(@class, 'bg-[#9747FF]') and text()='Apply Changes']";
        #endregion
        public void redirect()
        {
           IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
            IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
           homeIcon.Click();
           TestConstants.LogTest.Log(Status.Info, "home button clicked");


            TestConstants.Driver.Navigate().Refresh();
            TestConstants.LogTest.Log(Status.Info, "Page refreshed");

           

            IWebElement searchInput = TestConstants.Driver.FindElement(By.CssSelector(_cssSelectorForSearchBar));
            searchInput.SendKeys(Keys.Control + "a");
           searchInput.SendKeys(Keys.Backspace);



            searchInput = TestConstants.Driver.FindElement(By.CssSelector(_cssSelectorForSearchBar));
            string project = TestConstants.GetConfigKeyValue("ProjectName");
            searchInput.SendKeys(project);
            TestConstants.LogTest.Log(Status.Info, "Entered search string");
            Thread.Sleep(1000);

            searchInput.SendKeys(Keys.Enter);
            TestConstants.LogTest.Log(Status.Info, "Clicked the search input.");


            IWebElement divElement = TestConstants.Driver.FindElement(By.XPath(_xpathForAvailableForInternalReview));
            divElement.Click();
            TestConstants.LogTest.Log(Status.Info, "Project available for internal review");

            IWebElement editButton = TestConstants.Driver.FindElement(By.XPath(_xpathForEditFunctionality));
            editButton.Click();
            TestConstants.LogTest.Log(Status.Info, "Edit Functionality Opened");

            Thread.Sleep(2000);

            //IWebElement entitiesButton = TestConstants.Driver.FindElement(By.XPath(_xpathForEntitiesButton));
            //entitiesButton.Click();
        }

        public void redirect2()
        {
            //LoginWithValidCredentials();
            IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
            IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
            homeIcon.Click();
            TestConstants.LogTest.Log(Status.Info, "home button clicked");

            string project = TestConstants.GetConfigKeyValue("ProjectName2");
            Console.WriteLine(project);

            IWebElement searchInput = TestConstants.Driver.FindElement(By.CssSelector(_cssSelectorForSearchBar));
            searchInput.SendKeys(Keys.Control + "a"); 
            searchInput.SendKeys(Keys.Backspace); 


            Thread.Sleep(1000);
            searchInput.SendKeys(project);
            TestConstants.LogTest.Log(Status.Info, "Entered search string");
            Thread.Sleep(1000);

            searchInput.SendKeys(Keys.Enter);
            TestConstants.LogTest.Log(Status.Info, "Clicked the search input.");


            IWebElement divElement = TestConstants.Driver.FindElement(By.XPath(_xpathForAvailableForInternalReview));
            divElement.Click();
            TestConstants.LogTest.Log(Status.Info, "Project available for internal review");

            IWebElement editButton = TestConstants.Driver.FindElement(By.XPath(_xpathForEditFunctionality));
            editButton.Click();
            TestConstants.LogTest.Log(Status.Info, "Edit Functionality Opened");

            Thread.Sleep(2000);

        }

        public void apply_changes()
        {
            IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath(_xpathForApplyChanges));
            applyChangesButton.Click();

            IWebElement proceedButton = TestConstants.Driver.FindElement(By.CssSelector(_cssForProceedButton));
            proceedButton.Click();

            Thread.Sleep(2000);
        }


        public void reset()
        {
            IWebElement resetButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Reset']"));
            resetButton.Click();

            IWebElement proceedButton = TestConstants.Driver.FindElement(By.CssSelector(_cssForProceedButton));
            proceedButton.Click();

            Thread.Sleep(2000);
        }
    }
}

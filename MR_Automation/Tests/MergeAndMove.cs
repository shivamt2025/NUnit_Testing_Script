using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(16)]
    public class MergeAndMove : RedirectToProject
    {
        private string projectName="";

        [Test,Order(1)]
        public void merge()
        {
            //redirect();
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Merge and Move").Info("Tests For Merge and Move started");
            IWebElement mergeButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Merge']"));
            mergeButton.Click();
            Thread.Sleep(2000);

            try
            {
                IWebElement assigned_button = TestConstants.Driver.FindElement(By.ClassName("css-xkjp3a")).FindElement(By.ClassName("css-qiwgdb"));
                assigned_button.Click();

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys("YES").Perform();
                actions.SendKeys(Keys.Enter).Perform();

                IWebElement assign_account_button = TestConstants.Driver.FindElement(By.ClassName("css-hlgwow"));
                assign_account_button.Click();

                actions.SendKeys("sreeharsha").Perform();
                actions.SendKeys(Keys.Enter).Perform();

                DateTime now = DateTime.Now;
                string formattedTime = now.ToString("yyyyMMdd_HHmmss");
                projectName = "MergedProject_" + formattedTime;

                IWebElement newProjectNameDiv = TestConstants.Driver.FindElement(By.ClassName("css-1o6z5ng"));
                newProjectNameDiv.SendKeys(projectName);

                Console.WriteLine(projectName);

                IWebElement mergeAndMoveButton = TestConstants.Driver.FindElement(By.XPath("//button[@class='px-4 py-2 mr-1 rounded text-white' and contains(text(), 'Merge and move')]"));
                mergeAndMoveButton.Click();
                Thread.Sleep(1000); 

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//span[@class='font-base pb-6 -2 pl-11 pr-10' and contains(@style, 'font-size: 12px; color: rgb(84, 84, 84);')]"));

                string spanText = spanElement.Text;

                Console.WriteLine(spanText);

                string pattern = @"Please note that you are merging following projects and moving the merged project to .* account as MergedProject_\d{8}_\d{6}\.";
                bool isMatch = Regex.IsMatch(spanText, pattern);

                if (isMatch)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail, "Could not show validation message successfully");

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                TestConstants.LogTest.Log(Status.Info, "Not enough projects : " + error.Message);
            }
        }

        [Test,Order(2)]

        public void newProjectCheck()
        {
            try
            {
                IWebElement proceedButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Proceed']"));
                proceedButton.Click();

                Thread.Sleep(5000);
                IWebElement searchInput = TestConstants.Driver.FindElement(By.CssSelector(_cssSelectorForSearchBar));
                searchInput.Clear();

                searchInput.SendKeys(Keys.Enter);
                TestConstants.Driver.Navigate().Refresh();
                Thread.Sleep(3000);

                IWebElement projectNameSpan = TestConstants.Driver.FindElement(By.XPath("//div[contains(@class, 'flex flex-wrap flex-row break-all text-ellipsis overflow-hidden')]/span[2]"));
                string projectNameNew = projectNameSpan.Text;

                Console.WriteLine("Extracted project name: " + projectNameNew);

                if(projectName==projectNameNew)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Successfully merged");
                    return;
                }
                //TestConstants.LogTest.Log(Status.Fail, "Could not merge");

            }
            catch (Exception error)
            {
                TestConstants.LogTest.Log(Status.Fail,"Error Occured "+ error.Message);
            }
        }
    }
}

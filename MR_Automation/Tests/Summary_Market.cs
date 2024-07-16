using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(14)]
    public class Summary_Market: RedirectToProject
    {
        [Test,Order(7)]

        public void ui_changes()
        {
            try
            {
                //redirect();

                //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[4]")).Click();
                //IWebElement summariesElement = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Summaries')]"));
                //summariesElement.Click();

                TestConstants.LogTest = TestConstants.Extent.CreateTest("Summaries Market ").Info("Tests For Summaries Market started");
                Thread.Sleep(1000);

                IWebElement marketSummaryButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Market Summary']"));
                marketSummaryButton.Click();

                

                Random rnd = new Random();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("message"));
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"message\"]")).Clear();
                string temp = rnd.Next().ToString();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"message\"]")).SendKeys(temp);
                Console.WriteLine(temp);
                Thread.Sleep(1000);

                apply_changes();
                Thread.Sleep(3000);
                IWebElement textareaElementAfter = TestConstants.Driver.FindElement(By.Id("message"));
                string temp2 = textareaElementAfter.GetAttribute("value"); 
                Console.WriteLine(temp2);
                Console.WriteLine(temp2==temp);

                if ((string)temp == (string)temp2)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Changes successfully reflected on UI");
                    return;
                }
                else
                    TestConstants.LogTest.Log(Status.Fail, "Could not reflect changes to UI");
            }

            catch (Exception e)
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }
    }
}

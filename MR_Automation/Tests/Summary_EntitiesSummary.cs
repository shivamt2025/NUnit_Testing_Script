using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MR_Automation
{
    [TestFixture,Order(12)]
    public class Summary_EntitiesSummary:RedirectToProject
    {
        [Test,Order(3)]

        public void ui_popup()
        {
            try
            {
                Thread.Sleep(1000);
                //redirect();
                TestConstants.LogTest = TestConstants.Extent.CreateTest("Summaries Entities ").Info("Tests For Summaries Entities started");
                //IWebElement summariesElement = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Summaries')]"));
                //summariesElement.Click();

                IWebElement entitiesSummaryButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Entities Summary']"));
                entitiesSummaryButton.Click();
                Thread.Sleep(4000);
                ReadOnlyCollection<IWebElement> rowelements = TestConstants.Driver.FindElements(By.CssSelector(".MuiTableRow-root.css-xkjp3a"));
                rowelements[1].FindElement(By.XPath("//div[text()='see Sentences']\r\n")).Click();

                Random rnd = new Random();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                textareaElement.Clear();
                string temp = rnd.Next().ToString();
                textareaElement.SendKeys(temp);


                Actions actions = new Actions(TestConstants.Driver);
                int x = 0;
                int y = 0;
                actions.MoveByOffset(x, y).Click().Perform();
                apply_changes();

                IWebElement textareaElementUI = TestConstants.Driver.FindElement(By.Id("message"));
                string textFromTextarea = textareaElementUI.GetAttribute("value");
                Console.WriteLine(textFromTextarea);

                if (temp == textFromTextarea)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Changes successfully reflected from pop up to UI");
                    return;
                }
                else
                    TestConstants.LogTest.Log(Status.Fail, "Could not reflect changes from pop up to UI");
            }
            catch (Exception e)
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test, Order(4)]


        public void popup_ui()
        {
            try
            {
                //redirect();
                //IWebElement summariesElement = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Summaries')]"));
                //summariesElement.Click();

                //IWebElement entitiesSummaryButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Entities Summary']"));
                //entitiesSummaryButton.Click();
                Thread.Sleep(4000);

                ReadOnlyCollection<IWebElement> rowelements = TestConstants.Driver.FindElements(By.CssSelector(".MuiTableRow-root.css-xkjp3a"));

                IWebElement textareaElementUI = TestConstants.Driver.FindElement(By.Id("message"));
                textareaElementUI.Clear();
                string textFromTextarea = textareaElementUI.GetAttribute("value");

                Random rnd = new Random();
                string temp = rnd.Next().ToString();
                textareaElementUI.SendKeys(temp);

                apply_changes();
                Thread.Sleep(2000);
                

                rowelements[1].FindElement(By.XPath("//div[text()='see Sentences']\r\n")).Click();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                string test = textareaElement.Text;

                Actions actions = new Actions(TestConstants.Driver);
                int x = 0;
                int y = 0;
                actions.MoveByOffset(x, y).Click().Perform();

                if (test == temp)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Changes successfully reflected from UI to pop up");
                    return;
                }
                else
                    TestConstants.LogTest.Log(Status.Fail, "Could not reflect changes from UI to pop up");

            }
            catch (Exception e)
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }
    }
}

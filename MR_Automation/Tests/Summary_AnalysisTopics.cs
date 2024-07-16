using AventStack.ExtentReports;
using Microsoft.Extensions.Options;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(11)]
    public class Summary_AnalysisTopics: RedirectToProject
    {
        [Test,Order(1)]

        public void ui_popup()
        {
            try
            {
                redirect2();
                TestConstants.LogTest = TestConstants.Extent.CreateTest("Summaries Analysis Topics").Info("Tests For Summaries Analysis Topics started");

                IWebElement summariesElement = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Summaries')]"));
                summariesElement.Click();
                ReadOnlyCollection<IWebElement> rowelements = TestConstants.Driver.FindElements(By.CssSelector(".MuiTableRow-root.css-xkjp3a"));
                rowelements[1].FindElement(By.XPath("//div[text()='see Sentences']\r\n")).Click();

                Random rnd = new Random();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                textareaElement.Clear();
                Thread.Sleep(1000);
                string temp = rnd.Next().ToString();
                textareaElement.SendKeys(temp);
                Console.WriteLine(temp);
                
                Actions actions = new Actions(TestConstants.Driver);
                int x = 200;
                int y = 300; 
                actions.MoveByOffset(x, y).Click().Perform();
                apply_changes();
                Thread.Sleep(1000);

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

        [Test,Order(2)]

        public void popup_ui()
        {
            try
            {
                //redirect();
                //IWebElement summariesElement = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Summaries')]"));
                //summariesElement.Click();
                ReadOnlyCollection<IWebElement> rowelements = TestConstants.Driver.FindElements(By.CssSelector(".MuiTableRow-root.css-xkjp3a"));

                IWebElement textareaElementUI = TestConstants.Driver.FindElement(By.Id("message"));
                textareaElementUI.Clear();
                string textFromTextarea = textareaElementUI.GetAttribute("value");

                Random rnd = new Random();
                string temp = rnd.Next().ToString();
                textareaElementUI.SendKeys(temp);

                Thread.Sleep(2000);
                apply_changes();

                rowelements[1].FindElement(By.XPath("//div[text()='see Sentences']\r\n")).Click();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                string test=textareaElement.Text;
                Actions actions = new Actions(TestConstants.Driver);
                int x = 0;
                int y = 0;
                actions.MoveByOffset(x, y).Click().Perform();

                if (test==temp)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Changes successfully reflected from UI to pop up");
                    Thread.Sleep(1000);
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

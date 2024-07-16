using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(15)]
    public class ReportEmailTest:RedirectToProject
    {
        [Test,Order(1)]

        public void ui_popup()
        {
            try
            {
               //edirect();
                TestConstants.LogTest = TestConstants.Extent.CreateTest("Report").Info("Tests For Reports started");
                IWebElement reportAndEmailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Report and Email']"));
                reportAndEmailButton.Click();

                IWebElement seeSentencesButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='See Sentences']"));
                seeSentencesButton.Click();

                Random rnd = new Random();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                textareaElement.Clear();
                string temp = rnd.Next().ToString();
                textareaElement.SendKeys(temp);
                Console.WriteLine(temp);

                Actions actions = new Actions(TestConstants.Driver);
                int x = 0;
                int y = 0;
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
                TestConstants.LogTest.Log(Status.Info, e.Message);
            }
        }

        [Test,Order(2)]

        public void popup_ui()
        {
            try
            {
                //redirect();
                //IWebElement reportAndEmailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Report and Email']"));
                //reportAndEmailButton.Click();

                IWebElement textareaElementUI = TestConstants.Driver.FindElement(By.Id("message"));
                textareaElementUI.Clear();
                string textFromTextarea = textareaElementUI.GetAttribute("value");

                Random rnd = new Random();
                string temp = rnd.Next().ToString();
                textareaElementUI.SendKeys(temp);

                Thread.Sleep(2000);
                apply_changes();


                IWebElement seeSentencesButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='See Sentences']"));
                seeSentencesButton.Click();

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
            catch(Exception e)
            {
                TestConstants.LogTest.Log (Status.Info, e.Message);
            }
        }

        [Test,Order(3)]

        public void UI_POPUP()
        {
            try
            {
                TestConstants.LogTest = TestConstants.Extent.CreateTest("Email").Info("Tests For Email started");
                //redirect();
                //IWebElement reportAndEmailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Report and Email']"));
                //reportAndEmailButton.Click();

                IWebElement emailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Email']"));
                emailButton.Click();

                IWebElement seeSentencesButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='See Sentences']"));
                seeSentencesButton.Click();

                Random rnd = new Random();
                IWebElement textareaElement = TestConstants.Driver.FindElement(By.Id("editedSummary"));
                textareaElement.Clear();
                string temp = rnd.Next().ToString();
                textareaElement.SendKeys(temp);
                Console.WriteLine(temp);

                Actions actions = new Actions(TestConstants.Driver);
                int x = 0;
                int y = 0;
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
                TestConstants.LogTest.Log(Status.Info, e.Message);
            }
        }

        [Test,Order(4)]

        public void POPUP_UI()
        {
            try
            {
                //redirect();
                //IWebElement reportAndEmailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Report and Email']"));
                //reportAndEmailButton.Click();

                //IWebElement emailButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='Email']"));
                //emailButton.Click();

                IWebElement textareaElementUI = TestConstants.Driver.FindElement(By.Id("message"));
                Thread.Sleep(1000);
                textareaElementUI.Clear();
                string textFromTextarea = textareaElementUI.GetAttribute("value");

                Random rnd = new Random();
                string temp = rnd.Next().ToString();
                textareaElementUI.SendKeys(temp);

                Thread.Sleep(2000);
                apply_changes();


                IWebElement seeSentencesButton = TestConstants.Driver.FindElement(By.XPath("//div[text()='See Sentences']"));
                seeSentencesButton.Click();

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
                TestConstants.LogTest.Log(Status.Info, e.Message);
            }
        }
    }
}

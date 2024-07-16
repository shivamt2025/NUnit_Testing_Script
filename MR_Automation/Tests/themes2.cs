using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter.Configuration;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(10)]
    public class themes2: RedirectToProject
    {
        [Test,Order(34)]

        public void delete_test()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Theme Sentences").Info("Tests For Theme Sentences started");
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();


            TestConstants.LogTest.Log(Status.Info, "deletion test started");
            try
            {
                string sentence = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[4]/div")).Text;
                TestConstants.LogTest.Log(Status.Info, "deleted sentence"+sentence);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "delete button selected");
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");
                string sentence_after = "";
                try
                {
                    sentence_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[4]/div")).Text;
                }
                catch
                {
                    TestConstants.LogTest.Log(Status.Pass, "Successfully deleted");
                }

                if(sentence==sentence_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not delete the sentence");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Successfully deleted");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(35)]

        public void change_theme()
        {
            TestConstants.LogTest.Log(Status.Info, "staretd the changing of theme of a sentence started");
            try
            {
                string sentence = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[4]/div")).Text;
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                TestConstants.LogTest.Log(Status.Info, "sentence is "+sentence);
                TestConstants.LogTest.Log(Status.Info, "theme is"+theme);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[8]/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                string sentence_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[4]/div")).Text;
                string theme_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;

                if(sentence==sentence_after && theme==theme_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Either no more themes or could not change the theme");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Successfully changed theme");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements : ");
            }
        }

        [Test,Order(36)]

        public void multiple_changes()
        {
            TestConstants.LogTest.Log(Status.Info, "test for multiple changes started");
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();

            try
            {
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[8]/div")).Click();
                TestConstants.LogTest.Log(Status.Info, "user defined button clicked");
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "changed the theme");
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "delete button clicked");
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;

                string x = $"The sentence is deleted and will not be moved to theme \"{theme}\". Do you wish to Apply changes?";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "trying to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelling the changes");

                //bool isValid = Regex.IsMatch(validation_message, x);
                if (validation_message==x)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for multiple changes shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Either not enough to merge or could not show validation message");

            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(37)]

        public void switching_home()
        {
            TestConstants.LogTest.Log(Status.Info, "test for switching to home started");
            try
            {
                reset();
            }
            catch { }
            try
            {
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "delete button clicked");
                string x = "You are leaving this tab. Any changes made will not be saved";

                IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
                IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
                homeIcon.Click();
                TestConstants.LogTest.Log(Status.Info, "home button clicked");

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//span[@class='font-base pb-6 -2 pl-11 pr-7' and contains(@style, 'font-size: 12px; color: rgb(84, 84, 84);')]"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);

                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the changes");


                bool isValid = Regex.IsMatch(validation_message, x);
                if (isValid)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for switching to home shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Could not show validation message for switching to home");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(38)]

        public void switching_tab()
        {
            TestConstants.LogTest.Log(Status.Info, "test for switching tab started");
            try
            {
                reset();
            }
            catch { }
            //redirect();
            


            try
            {
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the delete button");
                string x = "You are leaving the tab. Do you want to apply changes and continue? If not, the changes will not be saved.";

                IWebElement themesMasterButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[1]"));
                themesMasterButton.Click();

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//*[@id[starts-with(., 'popup-')]]/div/span\r\n"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);
                Thread.Sleep(1000);

                IWebElement continueButton = TestConstants.Driver.FindElement(By.XPath("//button[contains(text(), 'Continue anyway')]"));
                continueButton.Click();
                TestConstants.LogTest.Log(Status.Info, "accepting the prompt to change tabs");


                bool isValid = validation_message == x;
                if (isValid)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for switching to tab shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Could not show validation message for switching to tab");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }
    }
}

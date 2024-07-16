using AventStack.ExtentReports;
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
    [TestFixture,Order(9)]
    public class themes1:RedirectToProject
    {
        [Test,Order(23)]

        public void rename()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Theme Master").Info("Tests For Theme Master started");
            TestConstants.LogTest.Log(Status.Info, "test for renaming started");
            //redirect();
            TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renaming it to"+renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                if(theme ==renamedText)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Chnaged the theme successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail,"Could not change the theme successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(24)]

        public void merge()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            TestConstants.LogTest.Log(Status.Info, "test for merging started");
            try
            {
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[5]/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "new theme selected");

                try
                {
                    apply_changes();
                    TestConstants.LogTest.Log(Status.Info, "applied changes");
                }
                catch
                {
                    TestConstants.LogTest.Log(Status.Info, "Not enough elements to merge");
                }

                string theme_after= TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;

                if(theme!=theme_after)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Merged successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Could not merge");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(25)]

        public void delete()
        {
            TestConstants.LogTest.Log(Status.Info, "test for deletion started");
            try
            {
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");
                string theme_after = "";
                try
                {
                    theme_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                }
                catch
                {
                    TestConstants.LogTest.Log(Status.Pass, "Deleted the theme successfully");
                }

                if(theme==theme_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not delete the theme");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Deleted the theme successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test, Order(26)]

        public void change_theme()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            TestConstants.LogTest.Log(Status.Info, "test for changing theme started");

            try
            {
                string sentiment = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[7]")).Text;
                string theme = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[8]/div/div/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                if (sentiment == "Positive")
                {
                    actions.SendKeys("Negative").Perform();
                }
                else if (sentiment == "Negative")
                {
                    actions.SendKeys("Positive").Perform();
                }
                else if (sentiment == "Overall")
                {
                    actions.SendKeys("Positive").Perform();
                }

                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "selecetd the new theme");

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                string sentiment_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[7]")).Text;
                string theme_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[2]")).Text;

                if(sentiment==sentiment_after && theme==theme_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not change the sentiment");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Changed the sentiment successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(27)]

        public void user_defined()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            TestConstants.LogTest.Log(Status.Info, "test for user defined property started");

            try
            {
                IWebElement userdefined_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[9]/div/input"));

                bool isCheckedBefore = userdefined_box.GetAttribute("checked") == "true";
                userdefined_box.Click();
                TestConstants.LogTest.Log(Status.Info, "userdefined box is clicked");
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                IWebElement userdefined_box2 = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[9]/div/input"));
                bool isCheckedAfter = userdefined_box2.GetAttribute("checked") == "true";

                if (isCheckedAfter == isCheckedBefore)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not change user-defined property ");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "User-defined property changed succesfully");
            }
            catch (Exception e)
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements : " + e.Message);
            }
        }

        [Test,Order(28)]

        public void rename_delete()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[3]")).Click();
            TestConstants.LogTest.Log(Status.Info, "test for rename delete started");

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renamed to "+renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the delete button");

                string x = @"^The theme ""[^""]+"" is deleted and renaming will not have any effect\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "trying to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the changes");

                bool isValid = Regex.IsMatch(validation_message, x);
                if (isValid)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for rename and delete shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Could not show validation message for rename and delete");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test, Order(29)]

        public void rename_merge()
        {
            TestConstants.LogTest.Log(Status.Info, "test for rename merge started");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renamed text :"+ renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[5]/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "new topic for merge selected");
                string x = @"^The theme ""[^""]+"" is merged with ""[^""]+"" and renaming will not have any effect\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "trying to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();

                TestConstants.LogTest.Log(Status.Info, "clicked the cancel button");

                bool isValid = Regex.IsMatch(validation_message, x);
                if (isValid)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for merge and rename shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Either not enough to merge or could not show validation message for merge and rename");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test, Order(30)]

        public void merge_delete()
        {
            TestConstants.LogTest.Log(Status.Info, "started the merge delete test");
            try
            {
                reset();
            }
            catch { }

            try
            {
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[5]/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "selected the merge option");
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the delete button");

                string x = @"^The theme ""[^""]+"" is deleted therefore will not be merged with ""[^""]+"". Do you wish to Apply changes\?$";

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


                bool isValid = Regex.IsMatch(validation_message, x);
                if (isValid)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Validation message for merge and delete shown successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Info, "Could not show validation message for merge and delete");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test, Order(31)]

        public void multiple_changes()
        {
            TestConstants.LogTest.Log(Status.Info, "test for multiple changes started");
            try
            {
                reset();
            }
            catch { }


            try
            {
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[5]/div")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();

                TestConstants.LogTest.Log(Status.Info, "merge selected");
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "delete box selected");
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "new renamed text is"+renamedText);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);

                string x= @"^The theme ""[^""]+"" is deleted therefore will not be merged with ""[^""]+"" or renamed\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "trying to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the changes");


                bool isValid = Regex.IsMatch(validation_message, x);
                if (isValid)
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

        [Test,Order(32)]

        public void switching_home()
        {
            TestConstants.LogTest.Log(Status.Info, "started the switching to home test");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "renamed an element");

                IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
                IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
                homeIcon.Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the home button");

                string x = "You are leaving this tab. Any changes made will not be saved";

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//span[@class='font-base pb-6 -2 pl-11 pr-7' and contains(@style, 'font-size: 12px; color: rgb(84, 84, 84);')]"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);

                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the switching to home action");


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

        [Test, Order(33)]

        public void switching_tab()
        {
            TestConstants.LogTest.Log(Status.Info, "started the switching tab test");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/table/tbody/tr[1]/td[3]/input")).SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "renamed an element");
                string x = "You are leaving the tab. Do you want to apply changes and continue? If not, the changes will not be saved.";

                IWebElement themesSentencesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]"));
                themesSentencesButton.Click();

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//*[@id[starts-with(., 'popup-')]]/div/span\r\n"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);
                Thread.Sleep(1000);

                IWebElement continueButton = TestConstants.Driver.FindElement(By.XPath("//button[contains(text(), 'Continue anyway')]"));
                continueButton.Click();

                TestConstants.LogTest.Log(Status.Info, "continued to switch the tab");

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

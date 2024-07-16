using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(7)]
    public class entities2 : RedirectToProject
    {
        [Test,Order(10)]
        public void rename()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Entity Master").Info("Tests For Entity Master started");
            TestConstants.LogTest.Log(Status.Info, "test for rename started");

            try
            {
                string anchor = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "Renaming to "+ renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "Applied changes");

                string anchor_after= TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;

                if(renamedText==anchor_after)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Successfully changed the anchor");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail, "Could not change the anchor");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(11)]

        public void delete()
        {
            TestConstants.LogTest.Log(Status.Info, "test for deletion started");
            try
            {
                string anchor = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;
                Console.WriteLine(anchor);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[8]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the delete button");

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                string anchor_after = "";

                try
                {
                    anchor_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;
                    Console.WriteLine(anchor_after);
                }
                catch {
                    TestConstants.LogTest.Log(Status.Pass, "Deleted the anchor successfully");
                    return;
                }

                if(anchor==anchor_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not delete the anchor");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Deleted the anchor successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(12)]

        public void checkbox()
        {
            TestConstants.LogTest.Log(Status.Info, "checkbox test started");
            try
            {
                IWebElement checkbox = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[9]/div/input"));
                bool isCheckedBefore = checkbox.GetAttribute("checked") == "true";
                checkbox.Click();
                TestConstants.LogTest.Log(Status.Info, "checkbox clicked");
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                IWebElement checkbox2 = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[9]/div/input"));
                bool isCheckedAfter = checkbox2.GetAttribute("checked") == "true";

                if (isCheckedAfter == isCheckedBefore)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not change user-defined property ");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "User-defined property changed succesfully");

            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elemenst");
            }
        }

        [Test,Order(13)]

        public void merge()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();
            TestConstants.LogTest.Log(Status.Info, "test for merge started");
            try
            {
                IWebElement drop = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[6]/div/div/div/div"));
                drop.Click();
                Thread.Sleep(1000);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();

                string type = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                string anchor = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;

                try
                {
                    apply_changes();
                    TestConstants.LogTest.Log(Status.Info, "applied changes");
                }
                catch
                {
                    TestConstants.LogTest.Log(Status.Info, "Not enough anchors in entity type");
                }

                string type_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                string anchor_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;

                if(type== type_after && anchor==anchor_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not merge");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Successfully merged");
            }
            catch 
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(14)]

        public void move()
        {
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();
            TestConstants.LogTest.Log(Status.Info, "starting the move test");
            try
            {
                
                IWebElement drop = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[7]/div/div/div/div"));
                drop.Click();
                Thread.Sleep(1000);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();

                string type = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                string anchor = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;

                try
                {
                    apply_changes();
                    TestConstants.LogTest.Log(Status.Info, "applied changes");
                }
                catch
                {
                    TestConstants.LogTest.Log(Status.Info, "Not enough anchors in entity type");
                }

                string type_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                string anchor_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;

                if (type == type_after && anchor == anchor_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not move");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Successfully moved");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(15)]

        public void rename_delete()
        {
            TestConstants.LogTest.Log(Status.Info, "test for rename delete staretd");
            //redirect();
            //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[2]")).Click();
            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renaming to "+ renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[8]/div/input")).Click();

                string x = @"^The entity anchor ""[^""]+"" is deleted and renaming will not have any effect\. Do you wish to Apply changes\?$";

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

        [Test,Order(16)]

        public void merge_rename()
        {
            TestConstants.LogTest.Log(Status.Info, "test for merge rename started");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renaming to "+renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);

                IWebElement drop = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[6]/div/div/div/div"));
                drop.Click();
                Thread.Sleep(1000);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();

                string x = @"^The entity anchor ""[^""]+"" is merged with ""[^""]+"" and renaming will not have any effect\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "tried to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the changes");


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
                TestConstants.LogTest.Log(Status.Info, "Not enough elements to merge");
            }
        }


        [Test,Order(17)]

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
                IWebElement drop = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[6]/div/div/div/div"));
                drop.Click();
                Thread.Sleep(1000);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[8]/div/input")).Click();
                TestConstants.LogTest.Log(Status.Info, "delete button clicked");

                string x = @"^The entity anchor ""[^""]+"" is deleted therefore will not be merged with ""[^""]+"". Do you wish to Apply changes\?$";

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

        [Test,Order(18)]

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
                IWebElement drop = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[6]/div/div/div/div"));
                drop.Click();
                Thread.Sleep(1000);
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown");

                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[8]/div/input")).Click();

                string x = @"^The entity anchor ""[^""]+"" is deleted therefore will not be merged with ""[^""]+"" or renamed. Do you wish to Apply changes\?$";

                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renaming to "+renamedText);

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "delete button clicked");

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "trying to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "canclled the changes");


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

        [Test, Order(19)]

        public void switching_home()
        {
            TestConstants.LogTest.Log(Status.Info, "test for switching to home staretd");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "renaming text");

                IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
                IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
                homeIcon.Click();
                TestConstants.LogTest.Log(Status.Info, "home icon is clicked");

                string x = "You are leaving this tab. Any changes made will not be saved";

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//span[@class='font-base pb-6 -2 pl-11 pr-7' and contains(@style, 'font-size: 12px; color: rgb(84, 84, 84);')]"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);

                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelling to switch to home");


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

        [Test,Order(20)]

        public void switching_tab()
        {
            TestConstants.LogTest.Log(Status.Info, "test for switching to tab started");
            try
            {
                reset();
            }
            catch { }

            try
            {
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                TestConstants.LogTest.Log(Status.Info, "renaming the text");

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[4]/input")).SendKeys(renamedText);

                string x = "You are leaving the tab. Do you want to apply changes and continue? If not, the changes will not be saved.";

                IWebElement entitySentencesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[1]/div[3]"));
                entitySentencesButton.Click();

                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//*[@id[starts-with(., 'popup-')]]/div/span\r\n"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);
                Thread.Sleep(1000);

                IWebElement continueButton = TestConstants.Driver.FindElement(By.XPath("//button[contains(text(), 'Continue anyway')]"));
                continueButton.Click();
                TestConstants.LogTest.Log(Status.Info, "accepting the switching tab prompt");


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

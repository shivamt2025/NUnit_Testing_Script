using AventStack.ExtentReports;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture, Order(6)]
    public class entities1 : RedirectToProject
    {
        [Test,Order(1)]

        public void edit()
        {
            //redirect();
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Entity Type Master").Info("Tests For Entity Type Master started");
            IWebElement entitiesButton = TestConstants.Driver.FindElement(By.XPath(_xpathForEntitiesButton));
            entitiesButton.Click();


            TestConstants.LogTest.Log(Status.Info, "Renaming test started");

            try
            {
                IWebElement rename_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[3]/input"));
                TestConstants.LogTest.Log(Status.Info, "Renaming box clicked");
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();

                rename_box.SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "New renamed text is :" + renamedText);

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "Applied changes");
                string check = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]")).Text;
                Console.WriteLine(check);
                if(check==renamedText)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Successfully changed the name of the entity");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail, "Could not rename the name of the entity");
            }
            catch (Exception e) 
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough entities");
            }
        }

        [Test,Order(2)]

        public void delete()
        {
            //redirect();
            TestConstants.LogTest.Log(Status.Info, "Deletion test started");
            try
            {
                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "Deletion box clicked");
                string entityName = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]")).Text;
                TestConstants.LogTest.Log(Status.Info, "Entity name is :"+entityName);
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "Applied changes");
                Thread.Sleep(2000);
                string entityNameAfter = "";
                try
                {
                    entityNameAfter = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]")).Text;
                }
                catch { }

                if (entityNameAfter == "")
                    TestConstants.LogTest.Log(Status.Pass, "Deleted the entity successfully");

                if(entityNameAfter==entityName)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not delete the entity");
                    return;
                }

                TestConstants.LogTest.Log(Status.Pass, "Deleted the entity successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements to delete");
            }
        }

        [Test,Order(3)]

        public void merge()
        {
            //redirect();
            TestConstants.LogTest.Log(Status.Info, "Merge box clicked");
            try
            {
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).Click();
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).SendKeys(Keys.Enter);
                TestConstants.LogTest.Log(Status.Info, "Merging the project to a differrnt one");
                string entityBefore = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                Console.WriteLine(entityBefore);
                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "Applied changes");

                string entityAfter = "";
                try
                {
                    entityAfter= TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                }
                catch { }
                Console.WriteLine(entityAfter);

                if(entityAfter == entityBefore)
                {
                    TestConstants.LogTest.Log(Status.Info, "Either not enough elements to merge or could not merge");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Merged Successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Either not enough elements to merge");
            }
        }

        [Test,Order(4)]

        public void multiple_changes()
        {
            //redirect();
            TestConstants.LogTest.Log(Status.Info, "Multiple changes test started");

            try
            {
                TestConstants.LogTest.Log(Status.Info, "Rename box clicked");
                IWebElement rename_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[3]/input"));
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                rename_box.SendKeys(renamedText);

                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "Deletion box clicked");

                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).Click();
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).SendKeys(Keys.Enter);
                TestConstants.LogTest.Log(Status.Info, "Merge box clicked");

                string x = $"The entity type \"[^\"]+\" is deleted and will not be merged. Do you wish to Apply changes?";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();


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

        [Test,Order(5)]

        public void merge_rename()
        {
            //redirect();
            TestConstants.LogTest.Log(Status.Info, "Merge rename test started");
            try
            {
                reset();
            }
            catch { }

            try
            {
                TestConstants.LogTest.Log(Status.Info, "Rename box clicked");
                IWebElement rename_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[3]/input"));
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                rename_box.SendKeys(renamedText);

                TestConstants.LogTest.Log(Status.Info, "Merge box clicked");
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).Click();
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).SendKeys(Keys.Enter);

                string x = @"^The entity type ""[^""]+"" is merged with ""[^""]+"" and renaming will not have any effect\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "Tried to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Clicked the cancel button");


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

        [Test,Order(6)]

        public void rename_delete()
        {
            TestConstants.LogTest.Log(Status.Info, "Started the rename delete test");
            try
            {
                reset();
            }
            catch { }


            try
            {

                IWebElement rename_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[3]/input"));
                Random rnd = new Random();
                string renamedText = rnd.Next().ToString();
                rename_box.SendKeys(renamedText);
                TestConstants.LogTest.Log(Status.Info, "Clicked the rename box");

                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "Clicked the delete box");

                string x = @"^The entity type ""[^""]+"" is deleted and renaming will not have any effect\. Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Cancelled the changes");


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
                TestConstants.LogTest.Log(Status.Info, "Not enough elements ");
            }


        }

        [Test,Order(7)]

        public void merge_delete()
        {
            TestConstants.LogTest.Log(Status.Info, "test for merge delete started");
            try
            {
                reset();
            }
            catch { }


            try
            {

                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).Click();
                TestConstants.Driver.FindElement(By.CssSelector(".css-b52kj1")).SendKeys(Keys.Enter);
                TestConstants.LogTest.Log(Status.Info, "merge box clicked");

                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "delete box clicked");

                string x = @"^The entity type ""[^""]+"" is deleted and will not be merged with ""[^""]+"". Do you wish to Apply changes\?$";

                IWebElement applyChangesButton = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div[2]/button[2]"));
                applyChangesButton.Click();
                Thread.Sleep(2000);
                TestConstants.LogTest.Log(Status.Info, "tried to apply changes");

                IWebElement messageElement = TestConstants.Driver.FindElement(By.CssSelector(".grid.grid-cols-8.gap-2.justify-center")).FindElement(By.CssSelector(".col-span-7"));
                string validation_message = messageElement.Text;
                Console.WriteLine(validation_message);
                IWebElement cancelButton = TestConstants.Driver.FindElement(By.XPath("//button[text()='Cancel']"));
                cancelButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the applied changes");


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

        [Test,Order(8)]

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

                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the delete box");

                IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
                IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
                homeIcon.Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the home icon");

                string x = "You are leaving this tab. Any changes made will not be saved";

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

        [Test,Order(9)]

        public void swiching_tab()
        {
            TestConstants.LogTest.Log(Status.Info, "switching to tab test started");
            try
            {
                reset();
            }
            catch { }

            try
            {
                IWebElement delete_box = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[4]/div/input"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "clicked the deleted box");
                string x = "You are leaving the tab. Do you want to apply changes and continue? If not, the changes will not be saved.";

                IWebElement entityMasterButton = TestConstants.Driver.FindElement(By.XPath("//div[contains(text(), 'Entity Master')]"));
                entityMasterButton.Click();

                
                IWebElement spanElement = TestConstants.Driver.FindElement(By.XPath("//*[@id[starts-with(., 'popup-')]]/div/span\r\n"));
                string validation_message = spanElement.Text;
                Console.WriteLine(validation_message);
                Thread.Sleep(1000);

                IWebElement continueButton = TestConstants.Driver.FindElement(By.XPath("//button[contains(text(), 'Continue anyway')]"));
                continueButton.Click();
                TestConstants.LogTest.Log(Status.Info, "cancelled the changes");


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

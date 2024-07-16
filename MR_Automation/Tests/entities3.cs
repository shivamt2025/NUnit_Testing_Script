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
    [TestFixture, Order(8)]
    public class entities3 : RedirectToProject
    {
        [Test, Order(21)]

        public void delete()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Entity Sentences").Info("Tests For Entity Sentences started");
            TestConstants.LogTest.Log(Status.Info, "test for deletion staretd");
            try
            {
                IWebElement delete_box = TestConstants.Driver.FindElement(By.ClassName("css-xkjp3a")).FindElement(By.ClassName("accent-[#9747FF]"));
                delete_box.Click();
                TestConstants.LogTest.Log(Status.Info, "delete button clicked");
                string sentence_before = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[5]/div")).Text;
                Console.WriteLine(sentence_before);

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied changes");

                string sentence_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[5]/div")).Text;
                Console.WriteLine(sentence_after);

                if(sentence_before != sentence_after)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Deleted the sentence successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail, "Could not delete the sentence successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }

        [Test,Order(22)]

        public void anchor_change()
        {
            TestConstants.LogTest.Log(Status.Info, "test for anchor change started");
            try
            {
                string anchor_before = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;
                string sentence_before = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[5]")).Text;

                Console.WriteLine(anchor_before);
                Console.WriteLine(sentence_before);

                TestConstants.Driver.FindElement(By.ClassName("css-19qne77")).Click();
                Actions actions = new Actions(TestConstants.Driver);
                actions.SendKeys(Keys.Enter).Perform();
                TestConstants.LogTest.Log(Status.Info, "clicked the dropdown and selected the anchor");

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "applied the changes");

                string anchor_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]")).Text;
                string sentence_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[5]")).Text;

                if(anchor_before==anchor_after && sentence_before==sentence_after)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Could not change the anchor or there is only 1 sentence");
                    return;
                }
                TestConstants.LogTest.Log(Status.Pass, "Changed the anchor successfully");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }

        }




    }
}

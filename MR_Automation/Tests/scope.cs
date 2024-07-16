using AventStack.ExtentReports;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using MR_Automation.Repositories;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RazorEngine.Compilation.ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Automation
{
    [TestFixture,Order(5)]
    public class scope_a :RedirectToProject
    {
        [Test,Order(1)]
        public void topics()
        {
            try
            {
                redirect();
                //TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div[1]/div[1]")).Click();
                ReadOnlyCollection<IWebElement> topics = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[1]/div[2]")).FindElements(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div"));
                Console.WriteLine(topics.Count);

                Random rnd = new Random();
                string topic = rnd.Next().ToString();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"standard-basic\"]")).SendKeys(topic);
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"standard-basic\"]")).SendKeys(Keys.Enter);
                TestConstants.LogTest.Log(Status.Info, "Added a new project");

                apply_changes();
                TestConstants.LogTest.Log(Status.Info, "Applied Changes");

                //*[@id="root"]/div/div[2]/div[2]/div/div[1]/div[2]

                ReadOnlyCollection<IWebElement> topics_after = TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[1]/div[2]")).FindElements(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[1]/div[2]/div"));
                if (topics_after.Count == topics.Count + 1)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Added the new topic successfully");
                    return;
                }
                TestConstants.LogTest.Log(Status.Fail, "Could not add the new topic");
            }
            catch(Exception e)
            {
                TestConstants.LogTest.Log(Status.Info, e.Message);
            }
        }

        [Test,Order(2)]

        public void attribute()
        {
            try
            {
                try
                {
                    TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/thead/tr/th[3]/div/svg")).Click();
                    TestConstants.LogTest.Log(Status.Pass, "Deletion done of the existing attribute");
                }
                catch { }

                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[4]/div")).Click();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/thead/tr/th[3]/div/div/input")).SendKeys("Location");
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[4]/div")).Click();
                TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/thead/tr/th[4]/div/div/input")).SendKeys("Age");


                ReadOnlyCollection<IWebElement> elements= TestConstants.Driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody")).FindElements(By.TagName("tr"));
                Console.WriteLine(elements.Count);



                //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[1]/td[3]/div/input
                //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[2]/td[3]/div/input

                //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[1]/td[4]/div/input

                IWebElement tableBody = TestConstants.Driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody"));

               
                IList<IWebElement> rows = tableBody.FindElements(By.TagName("tr"));

              
                string[] values = new string[rows.Count];

        
                for (int i = 0; i < rows.Count; i++)
                {
                    
                    string xpathColumn3 = $"//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[{i + 1}]/td[3]/div/input"; 
                    IWebElement inputElementColumn3 = TestConstants.Driver.FindElement(By.XPath(xpathColumn3));
                    string xpathColumn4 = $"//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[{i + 1}]/td[4]/div/input";
                    IWebElement inputElementColumn4 = TestConstants.Driver.FindElement(By.XPath(xpathColumn4));
                    string valueToInput = $"Value {i + 1}";
                    inputElementColumn3.SendKeys(valueToInput);
                    inputElementColumn4.SendKeys(valueToInput);
                    values[i] = valueToInput;
                }

                apply_changes();

                IWebElement tableBody2 = TestConstants.Driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody"));

                IList<IWebElement> rows2 = tableBody2.FindElements(By.TagName("tr"));

                for (int i = 0; i < rows2.Count; i++)
                {
                    
                    string xpathColumn3 = $"//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[{i + 1}]/td[3]/div/input";

       
                    IWebElement inputElementColumn3 = TestConstants.Driver.FindElement(By.XPath(xpathColumn3));

                    string xpathColumn4 = $"//*[@id='root']/div/div[2]/div[2]/div/div[2]/div[3]/div/table/tbody/tr[{i + 1}]/td[4]/div/input";

                    IWebElement inputElementColumn4 = TestConstants.Driver.FindElement(By.XPath(xpathColumn4));

                    string valueInColumn3 = inputElementColumn3.GetAttribute("value");

                    string valueInColumn4 = inputElementColumn4.GetAttribute("value");

                    if (valueInColumn3 != values[i])
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Value mismatch in third column at row {i + 1}: Expected {values[i]}, Found {valueInColumn3}");
                        return;
                    }

                    if (valueInColumn4 != values[i])
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Value mismatch in fourth column at row {i + 1}: Expected {values[i]}, Found {valueInColumn4}");
                        return;
                    }
                }

                TestConstants.LogTest.Log(Status.Pass, "All values validated successfully.");
            }
            catch
            {
                TestConstants.LogTest.Log(Status.Info, "Not enough elements");
            }
        }
    }


}

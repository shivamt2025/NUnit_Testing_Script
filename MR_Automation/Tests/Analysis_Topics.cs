using AventStack.ExtentReports;
using ExcelDataReader;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office.CustomUI;

//12.04 30 june
namespace MR_Automation
{
    [TestFixture, Order(4)]
    public class AnalysisTest : AnalysisRepository
    {
        private readonly string _excelFilePath = "C:\\React Projects\\Internship\\mr-automation\\mr-automation\\MR_Automation\\MR_Automation\\TestFiles.\\Analysis_Topics_Test (1).xlsx";
        private ExcelHelper _excelHelper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _excelHelper = new ExcelHelper(_excelFilePath);
            
        }
        [Test, Order(1)]
        public void OpenProject()
        {

            TestConstants.LogTest = TestConstants.Extent.CreateTest("Search Project").Info("Starting test for searching project.");

            IWebElement upper_tab = TestConstants.Driver.FindElement(By.ClassName("css-pi4zcm")).FindElement(By.CssSelector("div.flex.gap-4.justify-end.items-center"));
            IWebElement homeIcon = upper_tab.FindElements(By.TagName("svg"))[1];
            homeIcon.Click();
            TestConstants.LogTest.Log(Status.Info, "home button clicked");

            IWebElement searchInput = TestConstants.Driver.FindElement(By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.page-header.flex.justify-between.items-center > div > input"));
            searchInput.SendKeys(Keys.Control + "a");
            searchInput.SendKeys(Keys.Backspace);


            // Perform search for "Snacking"
            PerformSearch("QC_FlowTest_05-06_Test-v10");
            System.Threading.Thread.Sleep(2000);


            try
            {


                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20));


                IWebElement projectElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForProject)));
                projectElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Project selected.");
                System.Threading.Thread.Sleep(3000);

                // Wait for the review button and click it
                IWebElement reviewButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForReviewButton)));
                reviewButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Review button clicked.");
                System.Threading.Thread.Sleep(3000);

                // Wait for the "Review" message in the top bar
                IWebElement reviewMessageElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForReviewMessage)));
                TestConstants.LogTest.Log(Status.Info, "Review message found in the top bar.");
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);

            }


        }



            [Test, Order(2)]
        public void SearchAndAnalyzeTopic1()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest(" Validate Topic 1").Info("Starting test to validate Topic 1.");

            
        

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20)); // Increased wait time

                // Wait for the project to be clickable and select it
               

                // Wait for the analysis topics element to be clickable and click it to open the dropdown menu
                IWebElement analysisTopicsElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathFor1)));
                analysisTopicsElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Analysis topics text clicked.");
                System.Threading.Thread.Sleep(2000);
                // Fetch the total number of analysis topics from the UI
                int totalTopicsCount = ExtractDigits(analysisTopicsElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Total topics count from UI: {totalTopicsCount}");
                string TopicName = ExtractWord(analysisTopicsElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Topic Name from UI: {TopicName}");
                // Build the XPath expression using the topicName
                string xpathExpression = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer')]";

                // Wait until the dropdown items are visible
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathExpression)));
                // Fetch all dropdown items using a more precise XPath
                string xpathExpression1 = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer') and contains(@class, 'rounded-md')]";

                // Find the elements using the constructed XPath expression
                var topics = TestConstants.Driver.FindElements(By.XPath(xpathExpression1));

                int actualTopicsCount = topics.Count;
                TestConstants.LogTest.Log(Status.Info, $"Dropdown contains {actualTopicsCount} topics.");

                // Verify the actual topics count with the total count displayed on the UI
                if (actualTopicsCount == totalTopicsCount)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual topics count matches the displayed total count: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual topics count ({actualTopicsCount}) does not match the displayed total count: {totalTopicsCount}");
                }

                int totalTopicsCountFromExcel = _excelHelper.GetTotalTopicsCount();
                TestConstants.LogTest.Log(Status.Info, $"Total topics count from Excel: {totalTopicsCountFromExcel}");

                // Verify the actual topics count with the total count from the Excel file
                if (actualTopicsCount == totalTopicsCountFromExcel)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual topics count matches the count from the Excel file: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual topics count ({actualTopicsCount}) does not match the count from the Excel file: {totalTopicsCountFromExcel}");
                }


                System.Threading.Thread.Sleep(2000); // 2000 milliseconds = 2 seconds


                Dictionary<string, int> topicCountsFromExcel = _excelHelper.GetTopicCountsFromExcel();

                foreach (var topic in topics)
                {
                    try
                    {
                        // Wait for the topic name element to be present and get the topic name
                        var topicNameElement = wait.Until(d => topic.FindElement(By.XPath(".//div[@class='truncate overflow-hidden']")));
                        string topicName = topicNameElement.GetAttribute("aria-label");

                        if (string.IsNullOrEmpty(topicName))
                        {
                            TestConstants.LogTest.Log(Status.Warning, "Topic name is empty or null.");
                            continue;
                        }

                        // Wait for the topic count element to be present and get the topic count text
                        var topicCountTextElement = wait.Until(d => topic.FindElement(By.XPath(".//span[contains(@class, 'flex-grow-0')]")));
                        string topicCountText = topicCountTextElement.Text;

                        if (string.IsNullOrEmpty(topicCountText))
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Count text for topic '{topicName}' is empty or null.");
                            continue;
                        }

                        // Fetch topic count using regular expression
                        int topicCount = ExtractDigits(topicCountText);

                        // Log the topic name and count
                        TestConstants.LogTest.Log(Status.Info, $"Topic: {topicName}, Count: {topicCount}");

                        if (topicCountsFromExcel.TryGetValue(topicName, out int expectedCount))
                        {
                            if (topicCount == expectedCount)
                            {
                                TestConstants.LogTest.Log(Status.Pass, $"Topic '{topicName}' count matches the expected count from Excel: {topicCount}");
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Fail, $"Topic '{topicName}' count ({topicCount}) does not match the expected count from Excel: {expectedCount}");
                            }
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Topic '{topicName}' not found in the Excel file.");
                        }
                    }
                    catch (NoSuchElementException ex)
                    {
                        TestConstants.LogTest.Log(Status.Warning, $"Element not found: {ex.Message}");
                    }
                    catch (FormatException ex)
                    {
                        TestConstants.LogTest.Log(Status.Warning, $"Failed to parse count for topic: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        TestConstants.LogTest.Log(Status.Error, $"Error processing element: {ex.Message}");
                    }
                }

                
                VerifyResponseCount();
           

            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }

       


       
        
        [Test, Order(3)]
        public void SearchAndAnalyzeTopic2()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Validate Topic 2").Info("Starting test to validate Topic 2.");

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20)); // Increased wait time

                
                IWebElement themesElement = TestConstants.Driver.FindElement(By.XPath(_xPathFor2));
                
                themesElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Themes dropdown clicked.");


                

                // Rest of the code for themes validation
                // ...
                // Fetch the total number of analysis topics from the UI
                int totalTopicsCount = ExtractDigits(themesElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Total topics count from UI: {totalTopicsCount}");

                string TopicName = ExtractWord(themesElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Topic Name from UI: {TopicName}");

               
                // Build the XPath expression using the topicName
                string xpathExpression = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer')]";

                // Wait until the dropdown items are visible
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathExpression)));
                // Fetch all dropdown items using a more precise XPath
                string xpathExpression1 = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer') and contains(@class, 'rounded-md')]";

                // Find the elements using the constructed XPath expression
                var topics = TestConstants.Driver.FindElements(By.XPath(xpathExpression1));
                int actualTopicsCount = topics.Count;
                TestConstants.LogTest.Log(Status.Info, $"Dropdown contains {actualTopicsCount} topics.");

                // Verify the actual topics count with the total count displayed on the UI
                if (actualTopicsCount == totalTopicsCount)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual topics count matches the displayed total count: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual topics count ({actualTopicsCount}) does not match the displayed total count: {totalTopicsCount}");
                }


                int totalThemesCountFromExcel = _excelHelper.GetTotalThemesCount();
                TestConstants.LogTest.Log(Status.Info, $"Total themes count from Excel: {totalThemesCountFromExcel}");

                // Verify the actual themes count with the total count from the Excel file
                if (actualTopicsCount == totalThemesCountFromExcel)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual themes count matches the count from the Excel file: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual themes count ({actualTopicsCount}) does not match the count from the Excel file: {totalThemesCountFromExcel}");
                }

                System.Threading.Thread.Sleep(2000); // 2000 milliseconds = 2 seconds

                IWebElement viewByElement = TestConstants.Driver.FindElement(By.XPath(_xPathForViewByElement));
                viewByElement.Click();
                TestConstants.LogTest.Log(Status.Info, "View By element clicked.");
                
                // Wait for the "Transcripts" option to be clickable
              
                IWebElement attributesOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAttributesOption)));

                // Click on "Transcripts" from the dropdown menu
                attributesOption.Click();

                System.Threading.Thread.Sleep(2000);

                Dictionary<string, int> themesCountsFromExcel = _excelHelper.GetThemesCountsFromExcel();

                foreach (var topic in topics)
                {
                    try
                    {
                        // Wait for the theme name element to be present and get the theme name
                        var themeNameElement = wait.Until(d => topic.FindElement(By.XPath(".//div[@class='truncate overflow-hidden']")));
                        string themeName = themeNameElement.GetAttribute("aria-label");

                        if (string.IsNullOrEmpty(themeName))
                        {
                            TestConstants.LogTest.Log(Status.Warning, "Theme name is empty or null.");
                            continue;
                        }

                        // Wait for the theme count element to be present and get the theme count text
                        var themeCountTextElement = wait.Until(d => topic.FindElement(By.XPath(".//span[contains(@class, 'flex-grow-0')]")));
                        string themeCountText = themeCountTextElement.Text;

                        if (string.IsNullOrEmpty(themeCountText))
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Count text for theme '{themeName}' is empty or null.");
                            continue;
                        }

                        // Fetch theme count using regular expression
                        int themeCount = ExtractDigits(themeCountText);

                        // Log the theme name and count
                        TestConstants.LogTest.Log(Status.Info, $"Theme: {themeName}, Count: {themeCount}");

                        if (themesCountsFromExcel.TryGetValue(themeName, out int expectedCount))
                        {
                            if (themeCount == expectedCount)
                            {
                                TestConstants.LogTest.Log(Status.Pass, $"Theme '{themeName}' count matches the expected count from Excel: {themeCount}");
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Fail, $"Theme '{themeName}' count ({themeCount}) does not match the expected count from Excel: {expectedCount}");
                            }
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Theme '{themeName}' not found in the Excel file.");
                        }
                    }
                    catch (NoSuchElementException ex)
                    {
                        TestConstants.LogTest.Log(Status.Error, $"Element not found: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        TestConstants.LogTest.Log(Status.Error, $"An error occurred: {ex.Message}");
                    }
                }









                VerifyResponseCount1();
                
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }

        [Test, Order(4)]
        public void SearchAndAnalyzeTopic3()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Validate Topic 3").Info("Starting test to validate topic  3.");

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20)); // Increased wait time


                IWebElement themesElement = TestConstants.Driver.FindElement(By.XPath(_xPathFor3));

                themesElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Themes dropdown clicked.");




                // Rest of the code for themes validation
                // ...
                // Fetch the total number of analysis topics from the UI
                int totalTopicsCount = ExtractDigits(themesElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Total topics count from UI: {totalTopicsCount}");

                string TopicName = ExtractWord(themesElement.Text);
                TestConstants.LogTest.Log(Status.Info, $"Topic Name from UI: {TopicName}");


                // Build the XPath expression using the topicName
                string xpathExpression = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer')]";

                // Wait until the dropdown items are visible
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathExpression)));
                // Fetch all dropdown items using a more precise XPath
                string xpathExpression1 = $"//div[@id='{TopicName}']//div[contains(@class, 'cursor-pointer') and contains(@class, 'rounded-md')]";

                // Find the elements using the constructed XPath expression
                var topics = TestConstants.Driver.FindElements(By.XPath(xpathExpression1));
                int actualTopicsCount = topics.Count;
                TestConstants.LogTest.Log(Status.Info, $"Dropdown contains {actualTopicsCount} topics.");

                // Verify the actual topics count with the total count displayed on the UI
                if (actualTopicsCount == totalTopicsCount)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual topics count matches the displayed total count: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual topics count ({actualTopicsCount}) does not match the displayed total count: {totalTopicsCount}");
                }


                int totalThemesCountFromExcel = _excelHelper.GetTotalSnacksCount();
                TestConstants.LogTest.Log(Status.Info, $"Total themes count from Excel: {totalThemesCountFromExcel}");

                // Verify the actual themes count with the total count from the Excel file
                if (actualTopicsCount == totalThemesCountFromExcel)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Actual themes count matches the count from the Excel file: {actualTopicsCount}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Actual themes count ({actualTopicsCount}) does not match the count from the Excel file: {totalThemesCountFromExcel}");
                }

                System.Threading.Thread.Sleep(2000); // 2000 milliseconds = 2 seconds

                IWebElement viewByElement = TestConstants.Driver.FindElement(By.XPath(_xPathForViewByElement));
                viewByElement.Click();
                TestConstants.LogTest.Log(Status.Info, "View By element clicked.");

                // Wait for the "Transcripts" option to be clickable

                IWebElement attributesOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAttributesOption)));

                // Click on "Transcripts" from the dropdown menu
                attributesOption.Click();

                System.Threading.Thread.Sleep(2000);

                Dictionary<string, int> themesCountsFromExcel = _excelHelper.GetSnacksCountsFromExcel();

                foreach (var topic in topics)
                {
                    try
                    {
                        // Wait for the theme name element to be present and get the theme name
                        var themeNameElement = wait.Until(d => topic.FindElement(By.XPath(".//div[@class='truncate overflow-hidden']")));
                        string themeName = themeNameElement.GetAttribute("aria-label");

                        if (string.IsNullOrEmpty(themeName))
                        {
                            TestConstants.LogTest.Log(Status.Warning, "Theme name is empty or null.");
                            continue;
                        }

                        // Wait for the theme count element to be present and get the theme count text
                        var themeCountTextElement = wait.Until(d => topic.FindElement(By.XPath(".//span[contains(@class, 'flex-grow-0')]")));
                        string themeCountText = themeCountTextElement.Text;

                        if (string.IsNullOrEmpty(themeCountText))
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Count text for theme '{themeName}' is empty or null.");
                            continue;
                        }

                        // Fetch theme count using regular expression
                        int themeCount = ExtractDigits(themeCountText);

                        // Log the theme name and count
                        TestConstants.LogTest.Log(Status.Info, $"Theme: {themeName}, Count: {themeCount}");

                        if (themesCountsFromExcel.TryGetValue(themeName, out int expectedCount))
                        {
                            if (themeCount == expectedCount)
                            {
                                TestConstants.LogTest.Log(Status.Pass, $"Theme '{themeName}' count matches the expected count from Excel: {themeCount}");
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Fail, $"Theme '{themeName}' count ({themeCount}) does not match the expected count from Excel: {expectedCount}");
                            }
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Warning, $"Theme '{themeName}' not found in the Excel file.");
                        }
                    }
                    catch (NoSuchElementException ex)
                    {
                        TestConstants.LogTest.Log(Status.Error, $"Element not found: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        TestConstants.LogTest.Log(Status.Error, $"An error occurred: {ex.Message}");
                    }
                }









                VerifyResponseCount1();

            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }





       [Test, Order(5)]
        public void ValidateCompare()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Validate Compare Page").Info("Starting test for various compare page functionalities.");

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20)); // Increased wait time

                // Navigate to the configure page
                IWebElement comparePageLink = TestConstants.Driver.FindElement(By.XPath(_xPathForComparePage));
                comparePageLink.Click();
                TestConstants.LogTest.Log(Status.Info, "Navigated to compare page.");
                System.Threading.Thread.Sleep(6000);

                By svgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[60px\\].p-5 > div > svg");
                IWebElement viewButton = wait.Until(ExpectedConditions.ElementExists(svgLocator));
                Actions actions = new Actions(TestConstants.Driver);
                actions.MoveToElement(viewButton).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);

                // Click on saved views
                IWebElement savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);

               IWebElement hoverElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForHover)));

             // Use Actions to hover over the element

           actions.MoveToElement(hoverElement).Perform();
               


                By ViewVizsvgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > div.main.w-full.mt-2.flex.flex-col.justify-start.gap-1.overflow-y-auto > div > div > div.absolute.bottom-0.right-0.text-sm.mr-3 > button > svg");
                IWebElement viewViz = wait.Until(ExpectedConditions.ElementExists(ViewVizsvgLocator));
                Actions vwactions = new Actions(TestConstants.Driver);
                vwactions.MoveToElement(viewViz).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " View Viz button clicked using Actions class.");
               

                System.Threading.Thread.Sleep(3000);

                IWebElement leftFilterElement = TestConstants.Driver.FindElement(By.XPath(_xPathForLeftFilter));
                string leftFilterText = leftFilterElement.Text.Trim();

                // Extract right filter text
                IWebElement rightFilterElement = TestConstants.Driver.FindElement(By.XPath(_xPathForRightFilter));
                string rightFilterText = rightFilterElement.Text.Trim();


                leftFilterText = string.Join(", ", leftFilterText.Split(',').Select(s => s.Trim()));
                rightFilterText = string.Join(", ", rightFilterText.Split(',').Select(s => s.Trim()));

                TestConstants.LogTest.Log(Status.Info, "Left and Right Filter text extracted.");


                savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);




                // Wait for and extract the first text from the pop-up
                IWebElement firstSavedViewText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForFirstSavedViewText)));
                string firstSavedText = firstSavedViewText.Text;
                TestConstants.LogTest.Log(Status.Info, "First saved view's text extracted: " + firstSavedText);

                // Parse the extracted text
                string[] parts = firstSavedText.Split(new string[] { " vs " }, StringSplitOptions.None);
                string left_text = parts[0].Trim();
                string right_text = parts[1].Trim();

            

                left_text = string.Join(", ", left_text.Split(',').Select(s => s.Trim()));
                right_text = string.Join(", ", right_text.Split(',').Select(s => s.Trim()));
              
                TestConstants.LogTest.Log(Status.Info, $"Before: {right_text}");
                string toRemove = "view viz";
                right_text = right_text.TrimEnd();
                if (right_text.EndsWith(toRemove))
                {
                    right_text = right_text.Substring(0, right_text.Length - toRemove.Length).TrimEnd();
                }
                TestConstants.LogTest.Log(Status.Info, $"After: {right_text}");

                // Your additional code for comparison or further processing can go here


                By EditsvgLocator = By.CssSelector(_cssSelForEditButton);
                IWebElement edit = wait.Until(ExpectedConditions.ElementToBeClickable(EditsvgLocator));
                Actions editactions = new Actions(TestConstants.Driver);
                editactions.MoveToElement(edit).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Edit clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);



                editactions.SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Enter)
           .Perform();
                TestConstants.LogTest.Log(Status.Info, "Backspace pressed 4 times and Enter key pressed.");


                By DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                IWebElement delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                Actions delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);
               


                By svgLocator1 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                IWebElement viewButton1 = wait.Until(ExpectedConditions.ElementExists(svgLocator1));
                Actions actions1 = new Actions(TestConstants.Driver);
                actions1.MoveToElement(viewButton1).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");


                // Validate left filter text
                if (left_text == leftFilterText)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Left filter text matches successfully.Expected: {left_text}, Actual: {leftFilterText} ");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Left filter text does not match. Expected: {left_text}, Actual: {leftFilterText}");
                }

                // Validate right filter text
                if (right_text == rightFilterText)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Right filter text matches successfully.  Expected: {right_text}, Actual: {rightFilterText}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Right filter text does not match. Expected: {right_text}, Actual: {rightFilterText}");
                }






                IWebElement dropdown1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAreaDropdown)));
                dropdown1.Click();
                TestConstants.LogTest.Log(Status.Info, "Areas Dropdown opened: ");

                var area_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:r5\\:"));

                var areasubClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int areasubclassCount = areasubClasses.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of subclasses in area dropdown: {areasubclassCount}");

                // Click on the second element if it exists
                if (areasubclassCount >= 2)
                {
                    areasubClasses[1].Click();
                    TestConstants.LogTest.Log(Status.Info, "Unchecked the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2  subclasses.");
                }

                List<string> remainingItemsText = new List<string>();
                for (int i = 2; i < areasubClasses.Count; i++)
                {


                    remainingItemsText.Add(areasubClasses[i].Text.Trim());

                }

                // Join the texts into a comma-separated string
                string remainingItemsTextStr = string.Join(", ", remainingItemsText);
                TestConstants.LogTest.Log(Status.Info, $"Checked items are: {remainingItemsTextStr}");



                System.Threading.Thread.Sleep(3000);

                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Areas Dropdown closed.");
                System.Threading.Thread.Sleep(3000);







                IWebElement dropdown21 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForLoc1Dropdown)));
                dropdown21.Click();
                TestConstants.LogTest.Log(Status.Info, "Location  Dropdown 1 opened: ");

                var loc1_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:rd\\:"));

                var loc1subClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int loc1subclassCount = loc1subClasses.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of  subclasses in locaation dropdown: {loc1subclassCount}");


                // Click on the second element if it exists
                if (loc1subclassCount >= 2)
                {
                    loc1subClasses[2].Click();
                    TestConstants.LogTest.Log(Status.Info, "Unchecked  the second option.");
                    loc1subClasses[6].Click();
                    TestConstants.LogTest.Log(Status.Info, "Unchecked  the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2 identical subclasses.");
                }

                System.Threading.Thread.Sleep(2000); // Adjust this time as needed




                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Location  Dropdown 1 closed.");
                System.Threading.Thread.Sleep(3000);


                


                svgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[60px\\].p-5 > div > svg");
                viewButton = wait.Until(ExpectedConditions.ElementExists(svgLocator));
                actions = new Actions(TestConstants.Driver);
                actions.MoveToElement(viewButton).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);

                // Click on saved views
                savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);



                IWebElement leftFilterElement1 = TestConstants.Driver.FindElement(By.XPath(_xPathForLeftFilter));
                string leftFilterText1 = leftFilterElement1.Text.Trim();

                // Extract right filter text
                IWebElement rightFilterElement1 = TestConstants.Driver.FindElement(By.XPath(_xPathForRightFilter));
                string rightFilterText1 = rightFilterElement1.Text.Trim();


                leftFilterText1 = string.Join(", ", leftFilterText1.Split(',').Select(s => s.Trim()));
                rightFilterText1 = string.Join(", ", rightFilterText1.Split(',').Select(s => s.Trim()));



                IWebElement firstSavedViewText1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForFirstSavedViewText)));
                string firstSavedText1 = firstSavedViewText1.Text;
                TestConstants.LogTest.Log(Status.Info, "First saved view text extracted: " + firstSavedText1);

                // Parse the extracted text
                //string[] parts = firstSavedText.Split(new string[] { " vs ", ":" }, StringSplitOptions.None);

                string[] parts1 = firstSavedText1.Split(new string[] { " vs ", ":" }, StringSplitOptions.None);
                string left_text1 = parts1[0].Trim();

                string right_text1 = parts1[1].Trim();


                string main_topics1 = parts1[2].Trim();



                left_text1 = string.Join(", ", left_text1.Split(',').Select(s => s.Trim()));
                right_text1 = string.Join(", ", right_text1.Split(',').Select(s => s.Trim()));
                main_topics1 = string.Join(", ", main_topics1.Split(',').Select(s => s.Trim()));







                // Validate left filter text
                if (left_text1 == leftFilterText1)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Left filter text matches successfully.Expected: {left_text1}, Actual: {leftFilterText1} ");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Left filter text does not match. Expected: {left_text1}, Actual: {leftFilterText1}");
                }

                // Validate right filter text
                if (right_text1 == rightFilterText1)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Right filter text matches successfully.  Expected: {right_text1}, Actual: {rightFilterText1}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Right filter text does not match. Expected: {right_text1}, Actual: {rightFilterText1}");
                }

                // Log main topics (this is just to log it, not validating as per the requirement)
                TestConstants.LogTest.Log(Status.Info, "Subtopics: " + main_topics1);

                // Function to sort a comma-separated string
                string SortCommaSeparatedString(string input)
                {
                    var sorted = input.Split(',').Select(s => s.Trim()).OrderBy(s => s).ToList();
                    return string.Join(", ", sorted);
                }

                // Sort the comma-separated strings for comparison
                string sortedMainTopics = SortCommaSeparatedString(main_topics1);
                string sortedRemainingItems = SortCommaSeparatedString(remainingItemsTextStr);

                // Log the sorted strings for debugging
                TestConstants.LogTest.Log(Status.Info, $"Sorted sub topics: {sortedMainTopics}");
                TestConstants.LogTest.Log(Status.Info, $"Sorted checked optiions are: {sortedRemainingItems}");

                // Validate sorted main topics and remaining items text
                if (sortedMainTopics == sortedRemainingItems)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Right filter text matches successfully.  Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Right filter text does not match. Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }






                System.Threading.Thread.Sleep(5000);


                IWebElement hoverElement1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForHover)));

                // Use Actions to hover over the element

                actions.MoveToElement(hoverElement1).Perform();

                DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);

                By svgLocator2 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                IWebElement viewButton2 = wait.Until(ExpectedConditions.ElementExists(svgLocator2));
                Actions actions2 = new Actions(TestConstants.Driver);
                actions1.MoveToElement(viewButton2).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");








                //#@$^^$^^^$^&^^%%$^&%%^$%&^%$^&*^%$#$%^&%$#%^%$#%^&*^%$#%^&*(^%$#%^&%$^&*(^%$#%^&*(&^%$#@$%^&%$#%^&*^%$^&%$#@$%^&*^%%#$@!#$%^&*()_*&^%$#@!#$%^&*()_*&^%$#@!#$%^&*()_(*&^%$#@!$%^&*()*&^%$#@@$%^&*()









                IWebElement analysisTopicsElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathFor1)));
                analysisTopicsElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Themes clicked.");


                System.Threading.Thread.Sleep(5000);
                // Extract text from top-left location
                IWebElement identifier = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[1]/div[1]")));
                string identifierText = identifier.Text;
                TestConstants.LogTest.Log(Status.Info, "Identifier text extracted: " + identifierText);







                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAreaDropdown)));
                dropdown1.Click();
                TestConstants.LogTest.Log(Status.Info, "Area Dropdown opened: ");




                area_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:r5\\:"));

                areasubClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                areasubclassCount = areasubClasses.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of  subclasses in area dropdown: {areasubclassCount}");

                // Click on the second element if it exists
                if (areasubclassCount >= 2)
                {
                    areasubClasses[1].Click();
                    TestConstants.LogTest.Log(Status.Info, " Unchecked the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2  options.");
                }
                remainingItemsText = new List<string>();
                for (int i = 2; i < areasubClasses.Count; i++)
                {


                    remainingItemsText.Add(areasubClasses[i].Text.Trim());

                }

                // Join the texts into a comma-separated string
                remainingItemsTextStr = string.Join(", ", remainingItemsText);
                TestConstants.LogTest.Log(Status.Info, $"Checked options are: {remainingItemsTextStr}");



                System.Threading.Thread.Sleep(3000);

                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Areas Dropdown closed.");
                System.Threading.Thread.Sleep(3000);


                dropdown21 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForLoc1Dropdown)));
                dropdown21.Click();
                TestConstants.LogTest.Log(Status.Info, "Location  Dropdown 1 opened: ");

                loc1_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:rd\\:"));

                loc1subClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                loc1subclassCount = loc1subClasses.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of  subclasses in location dropdown: {loc1subclassCount}");

                // Click on the second element if it exists
                if (loc1subclassCount >= 2)
                {
                    loc1subClasses[2].Click();
                    TestConstants.LogTest.Log(Status.Info, "Unchecked on the second option.");
                    loc1subClasses[6].Click();
                    TestConstants.LogTest.Log(Status.Info, "Unchecked on the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2  options.");
                }

                System.Threading.Thread.Sleep(2000); // Adjust this time as needed




                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Location  Dropdown 1 closed.");
                System.Threading.Thread.Sleep(3000);


               


                svgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[60px\\].p-5 > div > svg");
                viewButton = wait.Until(ExpectedConditions.ElementExists(svgLocator));
                actions = new Actions(TestConstants.Driver);
                actions.MoveToElement(viewButton).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);

                // Click on saved views
                savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);



                leftFilterElement1 = TestConstants.Driver.FindElement(By.XPath(_xPathForLeftFilter));
                leftFilterText1 = leftFilterElement1.Text.Trim();

                // Extract right filter text
                rightFilterElement1 = TestConstants.Driver.FindElement(By.XPath(_xPathForRightFilter));
                rightFilterText1 = rightFilterElement1.Text.Trim();


                leftFilterText1 = string.Join(", ", leftFilterText1.Split(',').Select(s => s.Trim()));
                rightFilterText1 = string.Join(", ", rightFilterText1.Split(',').Select(s => s.Trim()));



                firstSavedViewText1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForFirstSavedViewText)));
                firstSavedText1 = firstSavedViewText1.Text;
                TestConstants.LogTest.Log(Status.Info, "First saved view text extracted: " + firstSavedText1);

                // Parse the extracted text
                //string[] parts = firstSavedText.Split(new string[] { " vs ", ":" }, StringSplitOptions.None);

                parts1 = firstSavedText1.Split(new string[] { " vs ", ":" }, StringSplitOptions.None);
                left_text1 = parts1[0].Trim();

                right_text1 = parts1[1].Trim();


                main_topics1 = parts1[2].Trim();



                left_text1 = string.Join(", ", left_text1.Split(',').Select(s => s.Trim()));
                right_text1 = string.Join(", ", right_text1.Split(',').Select(s => s.Trim()));
                main_topics1 = string.Join(", ", main_topics1.Split(',').Select(s => s.Trim()));







                // Validate left filter text
                sortedMainTopics = SortCommaSeparatedString(leftFilterText1);
                sortedRemainingItems = SortCommaSeparatedString(left_text1);

                // Log the sorted strings for debugging
                TestConstants.LogTest.Log(Status.Info, $"Sorted sub topics: {sortedMainTopics}");
                TestConstants.LogTest.Log(Status.Info, $"Sorted checked items text: {sortedRemainingItems}");

                // Validate sorted main topics and remaining items text
                if (sortedMainTopics == sortedRemainingItems)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Right filter text matches successfully.  Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Right filter text does not match. Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }


                sortedMainTopics = SortCommaSeparatedString(rightFilterText1);
                sortedRemainingItems = SortCommaSeparatedString(right_text1);

                // Log the sorted strings for debugging
                TestConstants.LogTest.Log(Status.Info, $"Sorted sub topics: {sortedMainTopics}");
                TestConstants.LogTest.Log(Status.Info, $"Sorted checked items text: {sortedRemainingItems}");

                // Validate sorted main topics and remaining items text
                if (sortedMainTopics == sortedRemainingItems)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Right filter text matches successfully.  Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Right filter text does not match. Expected: {sortedMainTopics}, Actual: {sortedRemainingItems}");
                }

                // Log main topics (this is just to log it, not validating as per the requirement)
                TestConstants.LogTest.Log(Status.Info, "Sub topics: " + main_topics1);

                string[] identifierParts = identifierText.Split(':');
                string identifierRightText = identifierParts.Length > 1 ? identifierParts[1].Trim() : identifierText.Trim();

                // Concatenate remainingItemsTextStr, "within", and the right part of identifierText with spaces
                string expectedText = $"{remainingItemsTextStr} within {identifierRightText}";

                // Check if main_topics1 matches the concatenated string
                if (main_topics1 == expectedText)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Sub Topics match successfully. Expected: {main_topics1}, Actual: {expectedText}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Sub Topics do not match. Expected: {main_topics1}, Actual: {expectedText}");
                }



                System.Threading.Thread.Sleep(5000);

                hoverElement1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForHover)));

                // Use Actions to hover over the element

                actions.MoveToElement(hoverElement1).Perform();

                DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                 delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                 delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);


                svgLocator2 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                viewButton2 = wait.Until(ExpectedConditions.ElementExists(svgLocator2));
                actions2 = new Actions(TestConstants.Driver);
                actions1.MoveToElement(viewButton2).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");






                IWebElement dropdown32 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForLoc2Dropdown)));
                dropdown32.Click();
                TestConstants.LogTest.Log(Status.Info, "Profile Dropdown 2 opened: ");

                var pro2_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:red\\:"));

                var pro2subClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int pro2subclassCount = pro2subClasses.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of  subclasses in profile dropdown 2: {pro2subclassCount}");

                // Click on the second element if it exists
                if (pro2subclassCount >= 2)
                {
                    pro2subClasses[1].Click();
                    TestConstants.LogTest.Log(Status.Info, "Clicked on the All option.");
                    pro2subClasses[5].Click();
                    TestConstants.LogTest.Log(Status.Info, "Clicked on the All option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2 options.");
                }

                System.Threading.Thread.Sleep(2000); // Adjust this time as needed




                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Profile  Dropdown 2 closed.");
                System.Threading.Thread.Sleep(3000);





                IWebElement Errmsg = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[2]/span")));
                string ErrmsgText = Errmsg.Text;
                TestConstants.LogTest.Log(Status.Info, ErrmsgText);


            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }






        [Test, Order(6)]
        public void ValidateCloud()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Validate Cloud Page").Info("Starting test for various cloud page functionalities.");

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(20)); // Increased wait time

                // Navigate to the configure page
                IWebElement configurePageLink = TestConstants.Driver.FindElement(By.XPath(_xPathForCloudPage));
                configurePageLink.Click();
                TestConstants.LogTest.Log(Status.Info, "Navigated to cloud page.");

                System.Threading.Thread.Sleep(8000);

                // Click on view button
                By svgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[100px\\].p-5 > div.flex.items-center.justify-between > div.flex > svg");
                IWebElement viewButton = wait.Until(ExpectedConditions.ElementExists(svgLocator));
                Actions actions = new Actions(TestConstants.Driver);
                actions.MoveToElement(viewButton).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);


                // Click on saved views
                IWebElement savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);

                // Wait for and extract the first text from the pop-up
                // Extract text from the first saved view element
                IWebElement firstSavedViewText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div")));
                string firstSavedText = firstSavedViewText.Text;
                TestConstants.LogTest.Log(Status.Info, "First saved view text extracted: '" + firstSavedText + "'");

                // Extract text from the top-left location
                IWebElement topLeftTextElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForTopLeftText)));
                string topLeftText = topLeftTextElement.Text;
                TestConstants.LogTest.Log(Status.Info, "Filter Value extracted: '" + topLeftText);

                // Clean up both strings by removing unnecessary characters and trimming whitespace
                string CleanText(string text)
                {
                    // Normalize spaces and remove trailing whitespace
                    return string.Join(" ", text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                }

                string cleanedFirstSavedText = CleanText(firstSavedText);
                string cleanedTopLeftText = CleanText(topLeftText);
                string toRemove = ": Sub-topics";
                cleanedTopLeftText = cleanedTopLeftText.TrimEnd();
                if (cleanedTopLeftText.EndsWith(toRemove))
                {
                    cleanedTopLeftText = cleanedTopLeftText.Substring(0, cleanedTopLeftText.Length - toRemove.Length).TrimEnd();
                }



                // Log detailed information about the cleaned strings for comparison
                TestConstants.LogTest.Log(Status.Info, $"Before comparison - Saved Text: '{cleanedTopLeftText}' (Length: {cleanedTopLeftText.Length}), CleanedFirstSavedText: '{cleanedFirstSavedText}' (Length: {cleanedFirstSavedText.Length})");

                // Validate the texts match
                if (cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Left filter text matches successfully. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Left filter text does not match. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }

                // Additional diagnostic logging if there's a mismatch
                if (!cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    for (int i = 0; i < Math.Min(cleanedTopLeftText.Length, cleanedFirstSavedText.Length); i++)
                    {
                        if (cleanedTopLeftText[i] != cleanedFirstSavedText[i])
                        {
                            TestConstants.LogTest.Log(Status.Info, $"Mismatch at position {i}: Expected '{cleanedTopLeftText[i]}', Actual '{cleanedFirstSavedText[i]}'");
                        }
                    }

                    if (cleanedTopLeftText.Length != cleanedFirstSavedText.Length)
                    {
                        TestConstants.LogTest.Log(Status.Info, $"Length mismatch: Expected length {cleanedTopLeftText.Length}, Actual length {cleanedFirstSavedText.Length}");
                    }
                }


                IWebElement hoverElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div")));
              
                // Use Actions to hover over the element

                actions.MoveToElement(hoverElement).Perform();
                //Wait  for view viz button to be clickable 18 june
                By ViewVizsvgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > div.main.w-full.mt-2.flex.flex-col.justify-start.gap-1.overflow-y-auto > div > div > div.absolute.bottom-0.right-0.text-sm.mr-3 > button > svg");
                IWebElement viewViz = wait.Until(ExpectedConditions.ElementExists(ViewVizsvgLocator));
                Actions vwactions = new Actions(TestConstants.Driver);
                vwactions.MoveToElement(viewViz).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " View Viz button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);


                // Extract text from top-left location
                topLeftTextElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForTopLeftText)));
                topLeftText = topLeftTextElement.Text;
                TestConstants.LogTest.Log(Status.Info, "Filter text extracted: " + topLeftText);


                savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked.");
                System.Threading.Thread.Sleep(5000);


                By EditsvgLocator = By.CssSelector(_cssSelForEditButton);
                IWebElement edit = wait.Until(ExpectedConditions.ElementToBeClickable(EditsvgLocator));
                Actions editactions = new Actions(TestConstants.Driver);
                editactions.MoveToElement(edit).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Edit clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);



                editactions.SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Backspace)
           .SendKeys(Keys.Enter)
           .Perform();
                TestConstants.LogTest.Log(Status.Info, "Backspace pressed 4 times and Enter key pressed.");










                By DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                IWebElement delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                Actions delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);


                By svgLocator1 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                IWebElement viewButton1 = wait.Until(ExpectedConditions.ElementExists(svgLocator1));
                Actions actions1 = new Actions(TestConstants.Driver);
                actions1.MoveToElement(viewButton1).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");


                // Interact with dropdown menus: uncheck "All" and select random options
                IWebElement dropdown1 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAreaDropdown)));
                dropdown1.Click();
                TestConstants.LogTest.Log(Status.Info, "Areas Dropdown opened: ");

                var area_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:r5\\:"));

                var areasubClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int areasubclassCount = areasubClasses.Count;
                TestConstants.LogTest.Log(Status.Info,$"Number of subclasses in area dropdownn {areasubclassCount}");

                // Click on the second element if it exists
                if (areasubclassCount >= 2)
                {
                    areasubClasses[1].Click();
                    TestConstants.LogTest.Log(Status.Info,"Clicked on the second element.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info,"There are less than 2 identical subclasses.");
                }



                    System.Threading.Thread.Sleep(3000);

                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Areas Dropdown closed.");
                System.Threading.Thread.Sleep(3000);


                IWebElement dropdown2 = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForLocationDropdown)));
                dropdown2.Click();

                TestConstants.LogTest.Log(Status.Info, "Location Dropdown opened: ");

                

                var loc_dropdown = TestConstants.Driver.FindElements(By.CssSelector("#\\:rk\\:"));

                var locsubClasses = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int locsubclassCount = locsubClasses.Count;
                TestConstants.LogTest.Log(Status.Info,$"Number of subclasses in location dropdown: {locsubclassCount}");

                // Click on the second element if it exists
                if (locsubclassCount >= 2)
                {
                    locsubClasses[1].Click();
                    TestConstants.LogTest.Log(Status.Info,"Clicked on the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info,"There are less than 2 options.");
                }

                System.Threading.Thread.Sleep(2000); // Adjust this time as needed


                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Location Dropdown closed.");
                System.Threading.Thread.Sleep(3000);

               
                // Click on save view button
                //By svgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[100px\\].p-5 > div.flex.items-center.justify-between > div.flex > svg");
                By saveViewSvgLocator = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[100px\\].p-5 > div.flex.items-center.justify-between > div.flex > svg");
                IWebElement saveViewButton = wait.Until(ExpectedConditions.ElementExists(saveViewSvgLocator));
                actions.MoveToElement(saveViewButton).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);


                // Click on saved views again
                savedViewsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked again.");

                // Wait for and extract the new first text from the pop-up
                IWebElement newFirstSavedViewText = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div")));
                string newFirstSavedText = newFirstSavedViewText.Text;
                TestConstants.LogTest.Log(Status.Info, "New first saved view text extracted: " + newFirstSavedText);

                // Extract text from top-left location again
                topLeftTextElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForTopLeftText)));
                string newTopLeftText = topLeftTextElement.Text;
                TestConstants.LogTest.Log(Status.Info, "Top-left text extracted again: " + newTopLeftText);
                

                 cleanedFirstSavedText = CleanText(newFirstSavedText);
                 cleanedTopLeftText = CleanText(newTopLeftText);

                // Log detailed information about the cleaned strings for comparison
                TestConstants.LogTest.Log(Status.Info, $"Before comparison - Saved Text: '{cleanedTopLeftText}' (Length: {cleanedTopLeftText.Length}), CleanedFirstSavedText: '{cleanedFirstSavedText}' (Length: {cleanedFirstSavedText.Length})");

                // Validate the texts match
                if (cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Left filter text matches successfully. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Left filter text does not match. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }

                // Additional diagnostic logging if there's a mismatch
                if (!cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    for (int i = 0; i < Math.Min(cleanedTopLeftText.Length, cleanedFirstSavedText.Length); i++)
                    {
                        if (cleanedTopLeftText[i] != cleanedFirstSavedText[i])
                        {
                            TestConstants.LogTest.Log(Status.Info, $"Mismatch at position {i}: Expected '{cleanedTopLeftText[i]}', Actual '{cleanedFirstSavedText[i]}'");
                        }
                    }

                    if (cleanedTopLeftText.Length != cleanedFirstSavedText.Length)
                    {
                        TestConstants.LogTest.Log(Status.Info, $"Length mismatch: Expected length {cleanedTopLeftText.Length}, Actual length {cleanedFirstSavedText.Length}");
                    }
                }



                hoverElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div")));

                // Use Actions to hover over the element

                actions.MoveToElement(hoverElement).Perform();

                 DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                 delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                 delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);


                svgLocator1 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                 viewButton1 = wait.Until(ExpectedConditions.ElementExists(svgLocator1));
               
                actions1.MoveToElement(viewButton1).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");




                IWebElement analysisTopicsElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathFor1)));
                analysisTopicsElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Themes clicked.");


                System.Threading.Thread.Sleep(5000);                                              
                // Extract text from top-left location
                IWebElement identifier = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[1]/div[1]")));
                string identifierText = identifier.Text;
                TestConstants.LogTest.Log(Status.Info, "Filter text extracted: " + identifierText);



                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAreaDropdown)));
                dropdown1.Click();
                TestConstants.LogTest.Log(Status.Info, "Area Dropdown opened: ");



                var area_dropdown_new = TestConstants.Driver.FindElements(By.CssSelector("#\\:r5\\:"));

                var areasubClasses_new = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int areasubclassCount_new = areasubClasses_new.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of  subclasses in area dropdown: {areasubclassCount_new}");


                // Click on the second element if it exists
                if (areasubclassCount_new >= 2)
                {
                    areasubClasses_new[1].Click();
                    TestConstants.LogTest.Log(Status.Info, "Clicked on the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2 options.");
                }

                System.Threading.Thread.Sleep(3000);

                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Area Dropdown closed.");
                System.Threading.Thread.Sleep(3000);


                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForLocationDropdown)));
                dropdown2.Click();

                TestConstants.LogTest.Log(Status.Info, "Location Dropdown opened: ");


                var loc_dropdown_new = TestConstants.Driver.FindElements(By.CssSelector("#\\:rk\\:"));

                var locsubClasses_new = TestConstants.Driver.FindElements(By.CssSelector("li"));


                int locsubclassCount_new = locsubClasses_new.Count;
                TestConstants.LogTest.Log(Status.Info, $"Number of subclasses in location dropdown: {locsubclassCount_new}");

                // Click on the second element if it exists
                if (locsubclassCount_new >= 2)
                {
                    locsubClasses_new[1].Click();
                    TestConstants.LogTest.Log(Status.Info, "Clicked on the second option.");
                    locsubClasses_new[6].Click();
                    TestConstants.LogTest.Log(Status.Info, "Clicked on the second option.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "There are less than 2 options.");
                }

                System.Threading.Thread.Sleep(2000); // Adjust this time as needed

                //TestConstants.LogTest.Log(Status.Info, "Uncliked UK");


                actions.MoveByOffset(-10, -10).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Location Dropdown closed.");
                System.Threading.Thread.Sleep(3000);


                // Click on save view button
                By saveViewSvgLocator1 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.h-full.w-full > div > div.h-\\[100px\\].p-5 > div.flex.items-center.justify-between > div.flex > svg");
                IWebElement saveViewButton1 = wait.Until(ExpectedConditions.ElementExists(saveViewSvgLocator1));
                actions.MoveToElement(saveViewButton1).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Save View button clicked using Actions class.");
                System.Threading.Thread.Sleep(5000);





                // Click on saved views again
                IWebElement savedViewsButton1= wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForSavedView)));
                savedViewsButton1.Click();
                TestConstants.LogTest.Log(Status.Info, "Saved Views button clicked again.");

                // Wait for and extract the new first text from the pop-up
                IWebElement newFirstSavedViewText1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForFirstSavedViewText)));
                 string newFirstSavedText1 = newFirstSavedViewText1.Text;
                TestConstants.LogTest.Log(Status.Info, "New first saved view text extracted: " + newFirstSavedText1);

                // Extract text from top-left location again
                IWebElement topLeftTextElement1 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForTopLeftText)));
                string newTopLeftText1 = topLeftTextElement1.Text;
                TestConstants.LogTest.Log(Status.Info, "Top-left text extracted again: " + newTopLeftText1);

                // Validate the new texts match
                cleanedFirstSavedText = CleanText(newFirstSavedText);
                cleanedTopLeftText = CleanText(newTopLeftText);

                // Log detailed information about the cleaned strings for comparison
                TestConstants.LogTest.Log(Status.Info, $"Before comparison - Saved Text: '{cleanedTopLeftText}' (Length: {cleanedTopLeftText.Length}), CleanedFirstSavedText: '{cleanedFirstSavedText}' (Length: {cleanedFirstSavedText.Length})");

                // Validate the texts match
                if (cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Left filter text matches successfully. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Left filter text does not match. Expected: '{cleanedTopLeftText}', Actual: '{cleanedFirstSavedText}'");
                }

                // Additional diagnostic logging if there's a mismatch
                if (!cleanedTopLeftText.Equals(cleanedFirstSavedText, StringComparison.Ordinal))
                {
                    for (int i = 0; i < Math.Min(cleanedTopLeftText.Length, cleanedFirstSavedText.Length); i++)
                    {
                        if (cleanedTopLeftText[i] != cleanedFirstSavedText[i])
                        {
                            TestConstants.LogTest.Log(Status.Info, $"Mismatch at position {i}: Expected '{cleanedTopLeftText[i]}', Actual '{cleanedFirstSavedText[i]}'");
                        }
                    }

                    if (cleanedTopLeftText.Length != cleanedFirstSavedText.Length)
                    {
                        TestConstants.LogTest.Log(Status.Info, $"Length mismatch: Expected length {cleanedTopLeftText.Length}, Actual length {cleanedFirstSavedText.Length}");
                    }
                }

                hoverElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div")));

                // Use Actions to hover over the element

                actions.MoveToElement(hoverElement).Perform();

                 DeletesvgLocator = By.CssSelector(_cssSelForDeleteButton);
                 delete = wait.Until(ExpectedConditions.ElementToBeClickable(DeletesvgLocator));
                 delactions = new Actions(TestConstants.Driver);
                delactions.MoveToElement(delete).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, " Delete button clicked using Actions class.");


                System.Threading.Thread.Sleep(3000);


                svgLocator1 = By.CssSelector("#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > button > svg");
                viewButton1 = wait.Until(ExpectedConditions.ElementExists(svgLocator1));
               
                actions1.MoveToElement(viewButton1).Click().Perform();
                TestConstants.LogTest.Log(Status.Info, "Saved view pop up closed");



            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }

        [Test, Order(7)]
        public void GenerateAndDownloadPPT()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Generate and Download PPT").Info("Starting test for generating and downloading PPT.");

            try
            {
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(180)); // Increased wait time

                // Navigate to the configure page
                IWebElement configurePageLink = TestConstants.Driver.FindElement(By.XPath(_xPathForConfigurePage));
                configurePageLink.Click();
                TestConstants.LogTest.Log(Status.Info, "Navigated to configure page.");

                System.Threading.Thread.Sleep(5000);

                // Wait for the generate PPT button and click it
                IWebElement generatePPTButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForGeneratePPTButton)));
                generatePPTButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Generate PPT button clicked.");

                // Wait for the popup to be visible
                IWebElement reportNameField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForReportNameField)));
                IWebElement reportDescriptionField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForReportDescriptionField)));

                // Enter report name with date and time
                string reportName = "FlowTest_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                reportNameField.SendKeys(reportName);
                TestConstants.LogTest.Log(Status.Info, $"Report name entered: {reportName}");

                // Enter report description
                string reportDescription = "Testing the Generation of power point for demo project in Ezythemes";
                reportDescriptionField.SendKeys(reportDescription);
                TestConstants.LogTest.Log(Status.Info, "Report description entered.");

                // Click on generate PowerPoint button
                IWebElement generatePowerPointButton = TestConstants.Driver.FindElement(By.XPath(_xPathForGeneratePowerPointButton));
                generatePowerPointButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Generate PowerPoint button clicked.");

                System.Threading.Thread.Sleep(8000);

                // Wait for the "Generating... Please Wait" text to disappear
                By generatingTextXPath = By.XPath("//*[@id='root']/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p[2]");
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(generatingTextXPath));
                TestConstants.LogTest.Log(Status.Info, "Report generation completed.");

                // Click on the generated report link
                IWebElement generatedReportLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='root']/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p")));
                generatedReportLink.Click();
                TestConstants.LogTest.Log(Status.Info, "Clicked on the report link to download.");


                System.Threading.Thread.Sleep(8000);

                // Verify the file is downloaded in the local storage (Downloads folder)
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string downloadedFile = Path.Combine(downloadsPath, reportName + ".pptx");

                if (File.Exists(downloadedFile))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"File downloaded successfully: {downloadedFile}");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"File not found in the downloads folder: {downloadedFile}");
                }
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }





        private int FetchResponseCount()
        {
            // Assuming there is an element that displays the response count
            IWebElement responseCountElement = TestConstants.Driver.FindElement(By.XPath(_xPathForResponseCount));
            return ExtractDigits(responseCountElement.Text);
        }
        private int ExtractDigits(string input)
        {
            var match = Regex.Match(input, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }
        private static string ExtractWord(string input)
        {
            var match = Regex.Match(input, @"\b[A-Za-z]+\b");
            return match.Success ? match.Value : string.Empty;
        }
        public void VerifyResponseCount()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify Response Count").Info("Starting test to verify response count.");

            try
            {
                // Ensure "Top Picks" button is switched off
                IWebElement topPicksButton = TestConstants.Driver.FindElement(By.XPath(_xPathForTopPicksButton));
                topPicksButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Top Picks button switched off.");

                System.Threading.Thread.Sleep(2000);

                // Fetch the digit in front of word "response"
                int totalResponseCount = FetchResponseCount();

                // Click on "View By" element to open the dropdown menu
                IWebElement viewByElement = TestConstants.Driver.FindElement(By.XPath(_xPathForViewByElement));
                viewByElement.Click();
                TestConstants.LogTest.Log(Status.Info, "View By element clicked.");

                // Wait for the "Transcripts" option to be clickable
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(10));
                IWebElement transcriptsOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForTranscriptsOption)));

                // Click on "Transcripts" from the dropdown menu
                transcriptsOption.Click();
                TestConstants.LogTest.Log(Status.Info, "Transcripts option selected.");

                // Wait for some time to ensure the UI updates
                System.Threading.Thread.Sleep(2000); // Adjust this time as needed

                // Click on "Go To" element
                IWebElement goToElement = TestConstants.Driver.FindElement(By.XPath(_xPathForGoToElement));
                goToElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Go To element clicked.");

                // Fetch and sum up response counts for each option except "All"
                int sumOfResponseCounts = 0;
                var optionsCount = _xPathsForOptions.Count();
                for (int i = 0; i < optionsCount; i++)
                {
                    IWebElement optionElement = TestConstants.Driver.FindElement(By.XPath(_xPathsForOptions[i]));
                    optionElement.Click();
                    // Wait for some time
                    System.Threading.Thread.Sleep(2000); // Adjust this time as needed
                    int responseCount = FetchResponseCount();
                    sumOfResponseCounts += responseCount;
                    TestConstants.LogTest.Log(Status.Info, $"Response count for transcript ({i + 1}) selected: {responseCount}");

                    if (i < optionsCount - 1)
                    {
                        goToElement.Click();
                    }
                }

                // Verify the sum of response counts equals the initial total response count
                if (sumOfResponseCounts == totalResponseCount)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Sum of response counts: ({sumOfResponseCounts}) matches the initial total response count: ({totalResponseCount}).");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Sum of response counts ({sumOfResponseCounts}) does not match the initial total response count ({totalResponseCount}).");
                }
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }
        public void RetryAction(Action action, int retryCount = 5, int delayMilliseconds = 1000)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    action();
                    break; // If action succeeds, exit the loop
                }
                catch (StaleElementReferenceException)
                {
                    if (i == retryCount - 1)
                        throw; // If this was the last retry, rethrow the exception

                    System.Threading.Thread.Sleep(delayMilliseconds); // Wait before retrying
                }
            }
        }
        public void VerifyResponseCount1()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify Response Count").Info("Starting test to verify response count.");

            try
            {
                IWebElement viewByElement = TestConstants.Driver.FindElement(By.XPath(_xPathForViewByElement));
                viewByElement.Click();
                TestConstants.LogTest.Log(Status.Info, "View By element clicked.");

                // Wait for the "Transcripts" option to be clickable
                WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(10));
                IWebElement attributesOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForAttributesOption)));

                // Click on "Transcripts" from the dropdown menu
                attributesOption.Click();
                TestConstants.LogTest.Log(Status.Info, "Attributes option selected.");

                // Wait for some time to ensure the UI updates
                System.Threading.Thread.Sleep(2000); // Adjust this time as needed





                // Fetch the digit in front of word "response"
                int totalResponseCount = FetchResponseCount();

                // Click on "View By" element to open the dropdown menu
                
                viewByElement.Click();
                TestConstants.LogTest.Log(Status.Info, "View By element clicked.");

                // Wait for the "Transcripts" option to be clickable
                
                IWebElement transcriptsOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xPathForTranscriptsOption)));

                // Click on "Transcripts" from the dropdown menu
                transcriptsOption.Click();
                TestConstants.LogTest.Log(Status.Info, "Transcripts option selected.");

                // Wait for some time to ensure the UI updates
                System.Threading.Thread.Sleep(2000); // Adjust this time as needed

                // Click on "Go To" element
                IWebElement goToElement = TestConstants.Driver.FindElement(By.XPath(_xPathForGoToElement));
                goToElement.Click();
                TestConstants.LogTest.Log(Status.Info, "Go To element clicked.");

                // Fetch and sum up response counts for each option except "All"
                int sumOfResponseCounts = 0;
                var optionsCount = _xPathsForOptions.Count();
                for (int i = 0; i < optionsCount; i++)
                {
                    IWebElement optionElement = TestConstants.Driver.FindElement(By.XPath(_xPathsForOptions[i]));
                    optionElement.Click();
                    // Wait for some time
                    System.Threading.Thread.Sleep(2000); // Adjust this time as needed
                    int responseCount = FetchResponseCount();
                    sumOfResponseCounts += responseCount;
                    TestConstants.LogTest.Log(Status.Info, $"Response count for transcript ({i + 1}) selected: {responseCount}");

                    if (i < optionsCount - 1)
                    {
                        goToElement.Click();
                    }
                }

                // Verify the sum of response counts equals the initial total response count
                if (sumOfResponseCounts == totalResponseCount)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Sum of response counts: ({sumOfResponseCounts}) matches the initial total response count: ({totalResponseCount}).");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Sum of response counts ({sumOfResponseCounts}) does not match the initial total response count ({totalResponseCount}).");
                }
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, "An error occurred: " + ex.Message);
            }
        }

    }
} 













using AventStack.ExtentReports;
using OpenQA.Selenium;


namespace MR_Automation
{
    [TestFixture, Order(2)]
    public class HomeTest : HomeRepository
    {
        [Test, Order(1)]
        public void HomeDisplayElementTest()
        {
            // Calling LoginSteps method
            //var loginRepo = new LoginRepository();
            //loginRepo.LoginSteps("Username", "Password");
            

            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of home page header, navigation and project list section").Info("Test - Verify the display of home page header, navigation and project list section.");

            // Calling DashboardSteps method
            DashboardSteps();

            // Test - Find the home page header element
            IWebElement headerTextElement = TestConstants.Driver.FindElement(By.XPath(_xPathForHeaderText));

            // Verify if the header section is displayed
            if (headerTextElement != null && !string.IsNullOrEmpty(headerTextElement.Text) && headerTextElement.Text == _headerText)
            {
                TestConstants.LogTest.Log(Status.Pass, "Header section is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Header section is not visible on the home page.");
            }

            // Test - Find the home page navigation section element
            IWebElement navigationTextElement = TestConstants.Driver.FindElement(By.XPath(_xPathForNavigationText));

            // Verify if the left side navigation is displayed
            if (navigationTextElement != null && !string.IsNullOrEmpty(navigationTextElement.Text) && navigationTextElement.Text == _navigationText1)
            {
                TestConstants.LogTest.Log(Status.Pass, "Navigation section is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Navigation section is not visible on the home page.");
            }

            // Test - Find the home page project list element
            IWebElement projectListElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectList));

            // Verify if the project list is displayed
            if (projectListElement != null && projectListElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Project list is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project list is not visible on the home page.");
            }
           
            // Test - Find the home page logo
            IWebElement logoElement = TestConstants.Driver.FindElement(By.XPath(_xPathForLogo));

            // Verify if the logo is displayed
            if (logoElement != null && logoElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Logo is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Logo is not visible on the home page.");
            }

            // Test - Find the calendar icon on the home page
            IWebElement calendarElement = TestConstants.Driver.FindElement(By.XPath(_xPathForCalendarIcon));

            // Verify if the calendar icon is displayed
            if (calendarElement != null && calendarElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Calendar icon is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Calendar icon is not visible on the home page.");
            }

            // Test - Find the home icon on the home page
            IWebElement homeElement = TestConstants.Driver.FindElement(By.XPath(_xPathForHomeIcon));

            // Verify if the home icon is displayed
            if (homeElement != null && homeElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Home icon is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Home icon is not visible on the home page.");
            }

            // Test - Find the profile icon on the home page       
            IWebElement profileElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProfileIcon));

            // Verify if the profile icon is displayed
            if (profileElement != null && profileElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Profile icon is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Profile icon is not visible on the home page.");
            }
	       
            // Test - Find the create project button on the home page
            IWebElement createProjectButtonElement = TestConstants.Driver.FindElement(By.XPath(_xPathForHomePageCreateProjectButton));

            // Verify if the create project button is displayed
            bool isCreateProjectButtonDisplayed = createProjectButtonElement.Displayed;

            if (createProjectButtonElement != null && createProjectButtonElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Create project button is visible on the home page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Create project button is not visible on the home page.");
            }
        }

        [Test, Order(2)]
        public async Task HomeFilterAndSortingDropdownTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of home page filter by and sort by dropdown list").Info("Test - Verify the display of home page filter by and sort by dropdown list.");

            // Test: Verify the filter by dropdown list on the home page
            // Create expected filter by dropdown list :: This will contain expected dropdown values
            List<string> expectedFilterByItems = new List<string>
            {
                "All Projects",
                "Project setup",
                "Validating",
                "Processing",
                "Payment required",
                "Transcript issue",
                "Review",
                "Reprocessing"
            };

            IWebElement filterByDropdownElement;

            try
            {
                // Find the filter by dropdown element on the home page
                filterByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForFilterByElement));

                if (filterByDropdownElement != null)
                {
                    TestConstants.LogTest.Log(Status.Info, "Filter by dropdown is visible on the home page.");

                    // Perform click operation on the filter by element to open the dropdown list
                    filterByDropdownElement.Click();
                }
            }
            catch (StaleElementReferenceException)
            {
                // Relocate the filter by dropdown
                filterByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForFilterByElement));

                if (filterByDropdownElement != null)
                {
                    TestConstants.LogTest.Log(Status.Info, "Filter by dropdown is visible on the home page.");

                    // Retry click operation on the filter by element to open the dropdown list
                    filterByDropdownElement.Click();
                }
                else 
                {
                    TestConstants.LogTest.Log(Status.Fail, "Filter by dropdown is not visible on the home page.");
                }
            }

            await Task.Delay(2000);

            // Find the each dropdown list element and store them all in a list
            IList<IWebElement> filterByList = TestConstants.Driver.FindElements(By.XPath(_xPathForFilterByListElement)); 

            if (filterByList.Count != 0 )
            {
                // Define a list to store the text of each filter by list item
                List<string> filterByListText = new List<string>();

                foreach (IWebElement listitem in filterByList)
                {
                    // Get the text of the current list item
                    string filterByListItemText = listitem.Text;

                    // Add the text to the list
                    filterByListText.Add(filterByListItemText);

                    // Print the list item text to the console
                    Console.WriteLine(filterByListItemText);
                }

                // Click on first list element to close the dropdown
                IWebElement firstFilterElement = TestConstants.Driver.FindElement(By.XPath(_xPathForFilterByListElement));
                firstFilterElement.Click();

                // Verify the filter by dropdown contains expected number of values
                if (filterByList.Count == expectedFilterByItems.Count)
                {
                    Console.WriteLine($"Filter by dropdown contains {filterByList.Count} items as expected");
                    TestConstants.LogTest.Log(Status.Pass, $"Filter by dropdown contains {filterByList.Count} items as expected.");

                    for (int i = 0; i < expectedFilterByItems.Count; i++)
                    {
                        if (expectedFilterByItems[i].Equals(filterByListText[i], StringComparison.CurrentCultureIgnoreCase))
                        {
                            TestConstants.LogTest.Log(Status.Pass, $"Filter by dropdown contains '{filterByListText[i]}' as expected.");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, $"Filter by dropdown does not contains '{expectedFilterByItems[i]}' as expected, but found '{filterByListText[i]}' instead.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Filter by dropdown do not contains {expectedFilterByItems.Count} items as expected, but contains {filterByList.Count} items instead.");
                    TestConstants.LogTest.Log(Status.Fail, $"Filter by dropdown do not contains {expectedFilterByItems.Count} items as expected, but contains {filterByList.Count} items instead.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Filter by list has no items.");
            } 
            
            // Test: Verify the sort by dropdown list on the home page
            // Create expected sort by dropdown list :: This will contain expected dropdown values
            List<string> expectedSortByValues = new List<string>
            {
                "Last modified",
                "Newest first",
                "Oldest first"
            };

            IWebElement sortByDropdownElement;

            try
            {
                // Find the sort by dropdown element on the home page
                sortByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForSortByElement));

                if (sortByDropdownElement != null)
                {
                    TestConstants.LogTest.Log(Status.Info, "Sort by dropdown is visible on the home page.");

                    // Perform click operation on the sort by element to open the dropdown list
                    sortByDropdownElement.Click(); 
                }
            }
            catch (StaleElementReferenceException)
            {
                // Relocate the sort by dropdown
                sortByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForSortByElement));

                if (sortByDropdownElement != null)
                {
                    TestConstants.LogTest.Log(Status.Info, "Sort by dropdown is visible on the home page.");

                    // Retry click operation on the sort by element to open the dropdown list
                    sortByDropdownElement.Click();
                }
            }

            await Task.Delay(2000);

            // Find the each dropdown list element and store them all in a list
            IList<IWebElement> sortByList = TestConstants.Driver.FindElements(By.XPath(_xPathForSortByListElement));
            
            if (sortByList.Count != 0)
            {
                // Define a list to store the text of each sort by list item
                List<string> sortByListText = new List<string>();

                foreach (IWebElement listitem in sortByList)
                {
                    // Get the text of the current list item
                    string sortByListItemText = listitem.Text;

                    // Add the text to the list
                    sortByListText.Add(sortByListItemText);

                    // Print the list item text to the console
                    Console.WriteLine(sortByListItemText);
                }

                // Click on first list element to close the dropdown
                IWebElement firstSortElement = TestConstants.Driver.FindElement(By.XPath(_xPathForSortByListElement));
                firstSortElement.Click();

                // Verify the sort by dropdown contains expected number of values
                if (sortByList.Count == expectedSortByValues.Count)
                {
                    Console.WriteLine($"Sort by dropdown contains {sortByList.Count} items as expected");
                    TestConstants.LogTest.Log(Status.Pass, $"Sort by dropdown contains {sortByList.Count} items as expected.");

                    for (int i = 0; i < expectedSortByValues.Count; i++)
                    {
                        if (expectedSortByValues[i].Equals(sortByListText[i], StringComparison.OrdinalIgnoreCase))
                        {
                            TestConstants.LogTest.Log(Status.Pass, $"Sort by dropdown contains '{sortByListText[i]}' as expected.");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, $"Sort by dropdown does not contains '{expectedSortByValues[i]}' as expected, but found '{sortByListText[i]}' instead.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Sort by dropdown do not contains {sortByList.Count} items as expected, but contains {sortByList.Count} items instead.");
                    TestConstants.LogTest.Log(Status.Fail, $"Sort by dropdown do not contains {sortByList.Count} items as expected, but contains {sortByList.Count} items instead.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Sort by list has no items.");
            }
        }

        [Test, Order(3)]
        public async Task HomePaginationTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the home page projects list pagination").Info("Test - Verify the home page projects list pagination.");

            // Test: Find the home page pagination element
            IWebElement paginationElement = TestConstants.Driver.FindElement(By.XPath(_xPathForPaginationElement));

            if(paginationElement != null)
            {
                // Execute JavaScript to scroll up to the cancel button
                IWebElement flag = paginationElement;
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", flag);

                await Task.Delay(2000);

                // Find count of the projects displayed on the page
                var projectList = TestConstants.Driver.FindElements(By.XPath(_xPathForProjectList)).Count();

                if (!paginationElement.Displayed && projectList != 0 && projectList <= 9)
                {
                    TestConstants.LogTest.Log(Status.Fail, $"{projectList} project(s) are visible without any pagination.");
                }
                else if (paginationElement.Displayed && projectList >= 9)
                {
                    // Locate next page button and click it
                    IWebElement nextPageElement = paginationElement.FindElement(By.XPath(_xPathForSecondPageElement));
                    nextPageElement.Click();

                    // Find number of items on the second page
                    var itemsOnNextPage = TestConstants.Driver.FindElements(By.XPath(_xPathForProjectList)).Count();

                    // Verify if there are any items on the next page
                    //Assert.Greater(itemsOnNextPage, 0, "No projects found on the next page.");
                    if (itemsOnNextPage > 0)
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Project list is visible with pagination.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Pass, "No projects found on the page.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Pagination is not visible on the home page.");
            }  
        }
    }
}

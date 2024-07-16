
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace MR_Automation
{
    public class ReviewRepository
    {
        internal string _xPathForFilterByElement = "(//div[contains(@role,'combobox')])[2]";
        internal string _xPathForReviewListElement = "//ul[@role='listbox']/li[@data-value='Review']";
        internal string _xPathForProjectUnderReviewStage = "//div[@class='flex flex-col justify-start items-start']//div[contains(@class, 'cursor-pointer') and contains(text(),'Review')]";
        internal string _xPathForSearchInputBox = "//div[@class = 'flex items-center border-2 bg-white rounded-lg ']//input";

        internal string _xPathForDisplayedProjectUnderReviewStage = "//div[@class = 'flex justify-start items-center gap-3']//span[@class = 'font-medium  text-black']";
        internal string _projectUnderReviewStage = TestConstants.GetConfigKeyValue("ReviewProjectName");
        internal string _xPathForProjectScopeIcon = "//div[@class='cursor-pointer']//*[name()='svg'][@data-testid='SummarizeTwoToneIcon']";
        internal string _xPathForLogo = "//div[@class='flex gap-2 justify-start items-center ']//img[@alt='logo']";
        internal string _xPathForCalendarIcon = "//div[@class='flex gap-4 justify-end items-center']//*[name()='svg' and @class='MuiSvgIcon-root MuiSvgIcon-fontSizeMedium cursor-pointer css-vubbuv']";
        internal string _xPathForHomeIcon = "//div[@class='cursor-pointer']//div";
        internal string _xPathForProfileIcon = "//div[@class='border-black border-2 rounded-full h-8 w-8 flex justify-center items-center cursor-pointer pt-0.5 bg-transparent text-black']";
        internal string _xPathForReviewPageTitle = "//div[@class='font-semibold flex gap-2 items-center']//span[1]";
        internal string _xPathForReviewPageNoteIcon = "//div[@class='absolute right-4']//*[name()='svg']";

        internal string _xPathForAnalyzeBySection = "//div[@class='w-full bg-white rounded-md shadow-lg']";
        internal string _xPathForAnalyzeBySectionHeader = ".//span[contains(text(), 'Analyze by')]";
        internal string _xPathForAnalyzeByEntityTypes = "//div[@class='h-full']//div[contains(@class,'MuiPaper-root MuiPaper-elevation MuiPaper-rounded')]";
        internal string _xPathForAnalyzeByEntityTypeValuesList = "//div[contains(@class,'MuiCollapse-root MuiCollapse-vertical')]//div[@class='innerdivedit overflow-y-auto pb-2 ']//div[@class='w-full flex justify-between gap-2 items-center rounded-md  py-2 px-1']";
        internal string _xPathForAnalyzeByEntityTypeValue = ".//div[@class='truncate overflow-hidden']";
        internal string _xPathForAnalyzeByEntityTypeValueCount = ".//span[contains(@class, 'flex-grow-0 flex-shrink-1')]";
        internal string _xPathForAnalyzeByEntityTypeValueEditIcon = ".//div[@class='flex items-center']//button//*[name()='svg']//*[name()='path'][@d='M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z']";
        internal string _xPathForAnalyzeByEntityTypeValueHideIcon = ".//div[@class='flex items-center']//*[name()='svg']//*[name()='path'][@d='M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z']";

        internal string _xPathForSummarySectionHeader = "//span[@class = 'text-lg font-semibold' and contains(text(), 'Summary')]";
        internal string _xPathForSummarySectionDownloadLink = "//span[@class = 'font-medium cursor-pointer' and contains(text(), 'Report')]";
        internal string _xPathForSummarySectionDivs = "//div[@class='flex flex-col justify-start ']//div[contains(@class, 'MuiPaper-root MuiPaper-elevation MuiPaper-rounded ')]";
        internal string _xPathForSummarySectionDivsHeader = ".//div[@class='MuiButtonBase-root MuiAccordionSummary-root Mui-expanded MuiAccordionSummary-gutters css-1iji0d4']//div//p";
        internal string _xPathForSummarySectionDivExpandButton = ".//div[@class='MuiAccordionSummary-expandIconWrapper Mui-expanded css-1fx8m19']";

        internal string _xPathForResponsesHeader = "//div[@class='flex justify-between items-center flex-1']//span";
        internal string _xPathForTopPicksToggle = "//div[@class='flex justify-start items-center mb-2 mr-1']//span[contains(@class,'css-199y1ve')]";

        internal string _xPathForIntersectionBubble = "//div[@class='text-[15px] font-semibold']";
        internal string _xPathForIntersectionBubbleHeader = "//div[@class='text-lg font-bold']";
        internal string _xPathForIntersectionBubbleDropDown = "//div[@class='MuiFormControl-root css-1cj10mq']//div[@role='combobox']";
        internal string _xPathForIntersectionBubbleDropDownList = "//div[contains(@class,'MuiPaper-root MuiPaper-elevation MuiPaper-rounded')]//ul";
        internal string _xPathForIntersectionBubbleList = "//div[@class='mt-3 rounded-md max-h-[84%] w-full px-3 overflow-auto']/div/div[@class='font-semibold flex justify-between mt-[1.4rem] mb-4 pl-3 pr-1.5']/div[1]";
        internal string _xPathForIntersectionBubbleBlankList = "//div[@class='col-span-4 w-full h-full flex justify-center items-center text-md font-semibold text-gray-500']//p";

        internal string _xPathForPreviewIcon = "//*[name()='svg'][@class='text-gray-400 cursor-pointer mt-1']";
        internal string _xPathForPreviewSection = "//div[@class='text-lg font-semibold pl-1 pt-1']";
        internal string _xPathForPreviewCancelIcon = "//*[name()='svg'][@data-testid='CancelRoundedIcon']";

        internal string _xPathForViewByFilter = "//div[@class='flex justify-start items-center mt-2']//div[@class='MuiFormControl-root css-1qwwdw9']";
        internal string _xPathForViewByFilterLabel = ".//label";
        internal string _xPathForAttributesFilter = "//div[@class='flex justify-start items-center mt-2']//div[@class='flex items-center mr-8']";
        internal string _xPathForAttributesFilterLabel = ".//div//label";

        internal string _xPathForAnalyzeByEditPopup = "//div[@class='flex flex-col justify-start gap-3']";
        internal string _xPathForAnalyzeByEditPopupInputBox = ".//input[@class='MuiInputBase-input MuiInput-input css-mnn31']";
        internal string _xPathForAnalyzeByEditPopupCancelButton = ".//button[contains(text(),'Cancel')]";
        internal string _xPathForAnalyzeByEditPopupSubmitButton = ".//button[contains(text(),'Submit')]";

        // Method to filter by review stage
        public async Task FilterUnderReviewStageProjects()
        {
            // Find the filter by dropdown element on the home page
            IWebElement filterByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForFilterByElement));

            try
            {
                // Find the filter by dropdown element on the home page
                filterByDropdownElement = TestConstants.Driver.FindElement(By.XPath(_xPathForFilterByElement));

                if (filterByDropdownElement != null)
                {
                    TestConstants.LogTest.Log(Status.Info, "Filter by dropdown is visible on the home page.");

                    // Perform click operation on the filter by element to open the dropdown list
                    filterByDropdownElement.Click();
                    await Task.Delay(1000);

                    IWebElement reviewFilterElement = TestConstants.Driver.FindElement(By.XPath(_xPathForReviewListElement));
                    if (reviewFilterElement != null)
                    {
                        reviewFilterElement.Click();
                        await Task.Delay(2000);
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Review option is not visible im the filter by dropdown.");
                    }
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
                    await Task.Delay(1000);


                    IWebElement reviewFilterElement = TestConstants.Driver.FindElement(By.XPath(_xPathForReviewListElement));
                    if (reviewFilterElement != null)
                    {
                        reviewFilterElement.Click();
                        await Task.Delay(2000);
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Review option is not visible im the filter by dropdown.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Filter by dropdown is not visible on the home page.");
                }
            }
        }

        // Method to search and open a project in the review stage
        public async Task SearchAndOpenProjectUnderReviewStage()
        {
            // Find the first project list element on the home page
            IWebElement searchInputBox = TestConstants.Driver.FindElement(By.XPath(_xPathForSearchInputBox));
            if (searchInputBox != null)
            {
                searchInputBox.SendKeys(_projectUnderReviewStage);
                searchInputBox.SendKeys(Keys.Enter);
                await Task.Delay(2000);

                try
                {
                    // Wait until the project with review state is visible         
                    IWebElement reviewButton = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectUnderReviewStage));

                    if (reviewButton != null)
                    {
                        TestConstants.LogTest.Log(Status.Info, "Review button is visible.");

                        reviewButton.Click();
                        TestConstants.LogTest.Log(Status.Info, "Review button clicked.");

                        IWebElement reviewPageTitle = TestConstants.Driver.FindElement(By.XPath(_xPathForReviewPageTitle));
                        if (reviewPageTitle != null && reviewPageTitle.Displayed)
                        {
                            TestConstants.LogTest.Log(Status.Pass, "Navigated to Review page successfully.");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, "Navigation to Review page unsuccessful.");
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Review button is not visible.");
                    }
                }
                catch (WebDriverTimeoutException ex)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Timed out waiting for review button to be visible. " + ex.Message);
                }
                catch (Exception ex)
                {
                    TestConstants.LogTest.Log(Status.Fail, ex.Message);
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Search input box is not visible.");
            }
        }

        // Method to get count and text separately
        public static (string text, int count) ExtractTextAndCount(IWebElement input)
        {
            // Regular expression pattern to match the text and count separately
            string pattern = @"^(.*)\s*\((\d+)\)\s*$";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            string inputText = input.Text;
            Match match = regex.Match(inputText);

            if (match.Success)
            {
                string text = match.Groups[1].Value.Trim();
                int count = int.Parse(match.Groups[2].Value);
                return (text, count);
            }
            else
            {
                throw new ArgumentException("Input string format is incorrect.");
            }
        }

        // Method to compare the string in the format (X of X)
        public static (string firstCount, string secondCount) CompareNumbersInStringFormat(string input)
        {
            // Define the regex pattern to match the format (X of X)
            string pattern = @"\((\d+) of (\d+)\)";

            // Match the pattern against the input text
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // Extract the numbers
                string firstCount = match.Groups[1].Value;
                string secondCount = match.Groups[2].Value;

                return (firstCount, secondCount);
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Input count format do not matches with expected format (X of X)");

                return (null, null);
            }

        }

        // Method to get entity types list
        public async Task<List<IWebElement>> GetEntityTypeList()
        {
            await Task.Delay(5000);
            IList<IWebElement> analyzeByEntityTypes = TestConstants.Driver.FindElements(By.XPath(_xPathForAnalyzeByEntityTypes));

            if (analyzeByEntityTypes.Count != 0)
            {
                // Initialize a list to store visible entity types
                IList<IWebElement> entityTypes = new List<IWebElement>();

                // Iterate through each value
                foreach (var value in analyzeByEntityTypes)
                {
                    // Verify if the element is displayed
                    if (value.Displayed)
                    {
                        entityTypes.Add(value);

                        // Wait
                        Thread.Sleep(1000);
                    }
                }
                return (List<IWebElement>)entityTypes;
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, $"Entity types are not visible under Analyze By section.");
                return new List<IWebElement>();
            }
        }

        // Method to get entity type values list
        public async Task<List<IWebElement>> GetEntityTypeValues(int i)
        {
            int parentIndex = i;

            // Created the full XPath for the entity type value list elements of the current entity type
            string fullXPath = $"{_xPathForAnalyzeByEntityTypes}[{parentIndex}]{_xPathForAnalyzeByEntityTypeValuesList}";

            // Find the entity type value list elements using the created full XPath
            IList<IWebElement> analyzeByEntityTypeValueList = TestConstants.Driver.FindElements(By.XPath(fullXPath));

            if (analyzeByEntityTypeValueList.Count != 0)
            {
                // Initialize a list to store entity types values
                IList<IWebElement> entityTypeValues = new List<IWebElement>();

                // Iterate through each value
                foreach (var value in analyzeByEntityTypeValueList)
                {
                    // Verify if the element is displayed
                    if (value.Displayed)
                    {
                        entityTypeValues.Add(value);

                        // Wait
                        Thread.Sleep(1000);
                    }
                }
                return (List<IWebElement>)entityTypeValues;
            }
            else
            {
                TestConstants.LogTest.Log(Status.Info, "Entity type values are not visible under entity type.");
                return new List<IWebElement>();
            }
        }
    }
}

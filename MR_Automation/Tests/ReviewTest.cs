using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MR_Automation
{
    [TestFixture, Order(3)]
    public class ReviewTest : ReviewRepository
    {
        [Test, Order(1)]
        public async Task ReviewProjectPageDisplayTest()
        {
            // Calling LoginSteps method
            //var loginRepo = new LoginRepository();
            //loginRepo.LoginSteps("Username", "Password");

            // Calling DashboardSteps method
            //var homeRepo = new HomeRepository();
            //homeRepo.DashboardSteps();

            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of logo, project name, project scope, calendar icon, home icon, profile icon, page title and note icon on the review page").Info("Test - Verify display of logo, project name, project scope, calendar icon, home icon, profile icon, page title and note icon on the review page.");

            await FilterUnderReviewStageProjects();

            await SearchAndOpenProjectUnderReviewStage();

            // Test - Find the review page logo on the review page
            IWebElement logoElement = TestConstants.Driver.FindElement(By.XPath(_xPathForLogo));

            // Verify if the logo is displayed
            if (logoElement != null && logoElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Logo is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Logo is not visible on the review page.");
            }

            // Test - Find the project name on the review page
            IWebElement displayedProjectUnderReviewStage = TestConstants.Driver.FindElement(By.XPath(_xPathForDisplayedProjectUnderReviewStage));

            string projectNameDisplayed = displayedProjectUnderReviewStage.Text;

            // Verify if the project name is displayed
            if (displayedProjectUnderReviewStage != null && displayedProjectUnderReviewStage.Displayed )
            {
                TestConstants.LogTest.Log(Status.Pass, $"Project name \'{projectNameDisplayed}\' is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project name is not visible on the review page.");
            }

            // Test - Find the project scope icon on the review page
            IWebElement projectScopeIcon = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectScopeIcon));

            // Verify if the project scope icon is displayed
            if (projectScopeIcon != null && projectScopeIcon.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Project scope icon is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project scope icon is not visible on the review page.");
            }

            // Test - Find the calendar icon on the review page
            IWebElement calendarElement = TestConstants.Driver.FindElement(By.XPath(_xPathForCalendarIcon));

            // Verify if the calendar icon is displayed
            if (calendarElement != null && calendarElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Calendar icon is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Calendar icon is not visible on the review page.");
            }

            // Test - Find the home icon on the review page
            IWebElement homeElement = TestConstants.Driver.FindElement(By.XPath(_xPathForHomeIcon));

            // Verify if the home icon is displayed
            if (homeElement != null && homeElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Home icon is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Home icon is not visible on the review page.");
            }

            // Test - Find the profile icon on the review page       
            IWebElement profileElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProfileIcon));

            // Verify if the profile icon is displayed
            if (profileElement != null && profileElement.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Profile icon is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Profile icon is not visible on the review page.");
            }

            // Test - Find the page title on the review page       
            IWebElement reviewPageTitle = TestConstants.Driver.FindElement(By.XPath(_xPathForReviewPageTitle));
            string pageTitle = reviewPageTitle.Text;

            // Verify if the page title is displayed
            if (reviewPageTitle != null && reviewPageTitle.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, $"Page title \'{pageTitle}\' is visible on the page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Page title is not visible on the page.");
            }

            // Test - Find the note icon on the review page       
            IWebElement noteIcon = TestConstants.Driver.FindElement(By.XPath(_xPathForReviewPageNoteIcon));

            // Verify if the note icon is displayed
            if (noteIcon != null && noteIcon.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Note icon is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Note icon is not visible on the review page.");
            }
        }

        [Test, Order(2)]
        public void SummarySectionsDisplayTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Summary section header, entity types header").Info("Test - Verify display of Summary section header, entity types header.");

            // Test - Verify the display of Summary section header
            IWebElement summarySectionHeader = TestConstants.Driver.FindElement(By.XPath(_xPathForSummarySectionHeader));

            if (summarySectionHeader != null && summarySectionHeader.Displayed)
            {
                string summarySectionHeaderText = summarySectionHeader.Text;
                TestConstants.LogTest.Log(Status.Pass, $"\'{summarySectionHeaderText}\' section header is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, $"Summary section header is not visible on the review page.");
            }

            // Test - Verify the display of Summary sections and if expanded by default.

            var summarySectionDivs = TestConstants.Driver.FindElements(By.XPath(_xPathForSummarySectionDivs));

            for (int i = 0; i < summarySectionDivs.Count; i++)
            {
                var div = summarySectionDivs[i];

                IWebElement summarySectionDivsHeader = div.FindElement(By.XPath(_xPathForSummarySectionDivsHeader));

                if (summarySectionDivsHeader != null)
                {
                    TestConstants.LogTest.Log(Status.Info, $"Summary section {i + 1}:\'{summarySectionDivsHeader.Text}\' is visible.");

                    var expandIcon = div.FindElement(By.XPath(_xPathForSummarySectionDivExpandButton));

                    if (expandIcon != null)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"Summary section {i + 1}: '{summarySectionDivsHeader.Text}' is expanded by default.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Summary section {i + 1}: '{summarySectionDivsHeader.Text}' is not expanded by default.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Summary section {i + 1} is not visible.");
                }
            }

            // Test - Verify the display of Summary section download link
            IWebElement summarySectionDownloadLink = TestConstants.Driver.FindElement(By.XPath(_xPathForSummarySectionDownloadLink));

            if (summarySectionDownloadLink != null && summarySectionDownloadLink.Displayed)
            {
                string summarySectionDownloadLinkText = summarySectionDownloadLink.Text;
                TestConstants.LogTest.Log(Status.Pass, $"\'{summarySectionDownloadLinkText}\' section download link is visible on the review page.");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, $"Summary section download link is not visible on the review page.");
            }

        }

        [Test, Order(3)]
        public async Task AnalyzeBySectionDisplayTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Analyze by section header, entity types, and entity type values count").Info("Test - Verify display of Analyze by section header, entity types, and entity type values count.");

            // Test - Verify if the analyze by section name is displayed
            IWebElement analyzeBySection = TestConstants.Driver.FindElement(By.XPath(_xPathForAnalyzeBySection));

            if (analyzeBySection != null)
            {
                IWebElement analyzeBySectionHeader = analyzeBySection.FindElement(By.XPath(_xPathForAnalyzeBySectionHeader));

                if (analyzeBySectionHeader != null && analyzeBySectionHeader.Displayed)
                {
                    string analyzeBySectionHeaderText = analyzeBySectionHeader.Text;
                    TestConstants.LogTest.Log(Status.Pass, $"\'{analyzeBySectionHeaderText}\' section header visible on the review page.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Analyze By section header is not visible on the review page.");
                }

                await Task.Delay(2000);

                IList<IWebElement> analyzeByEntityTypes = await GetEntityTypeList();

                if (analyzeByEntityTypes.Count != 0)
                {
                    int i = 1;
                    foreach (var entityType in analyzeByEntityTypes)
                    {
                        var output = ExtractTextAndCount(entityType);

                        entityType.Click();

                        TestConstants.LogTest.Log(Status.Pass, $"Visible Entity Type: '{output.text}' with count '{output.count}'");

                        await Task.Delay(2000);

                        // Find the entity type value list elements using the created full XPath
                        IList<IWebElement> analyzeByEntityTypeValueList = await GetEntityTypeValues(i);

                        if (analyzeByEntityTypeValueList.Count != 0 && analyzeByEntityTypeValueList.Count == output.count)
                        {
                            TestConstants.LogTest.Log(Status.Pass, $"'{output.text} header count {output.count} matches with the visible list item count {analyzeByEntityTypeValueList.Count}'");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, $"Values under {entityType} are not visible.");
                        }
                        i++;
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Analyze By section entity types are not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Analyze By section is not visible on the review page.");
            }
        }

        [Test, Order(4)]
        public async Task EntityTypeValuesDisplayTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Analyze by entity type values and count").Info("Verify display of Analyze by entity type values and count.");

            IList<IWebElement> analyzeByEntityTypes = await GetEntityTypeList();

            if (analyzeByEntityTypes.Count != 0)
            {
                int i = 1;
                foreach (var entityType in analyzeByEntityTypes)
                {
                    entityType.Click();
                    await Task.Delay(2000);

                    // Get the entity type value list from using method
                    List<IWebElement> entityTypeValues = await GetEntityTypeValues(i);

                    if (entityTypeValues.Count != 0)
                    {
                        for (int count = 0; count <= 1; count++)
                        {
                            if (entityTypeValues.Count >= count)
                            {
                                entityTypeValues[count].Click();
                                await Task.Delay(2000);

                                if (entityTypeValues[count] != null)
                                {
                                    TestConstants.LogTest.Log(Status.Pass, $"Visible entity type value: \'{entityTypeValues[count].Text}\'");
                                    try
                                    {
                                        IWebElement hideIcon = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueHideIcon));
                                        if (hideIcon != null)
                                        {
                                            TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: Hide icon is visible.");
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: Hide icon is not visible.");
                                        }

                                        IWebElement editIcon = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueEditIcon));
                                        if (editIcon != null)
                                        {
                                            TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: Edit icon is visible.");

                                            IWebElement entityValueCountElement = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueCount));
                                            if (entityValueCountElement != null)
                                            {
                                                var entityValueCount = CompareNumbersInStringFormat(entityValueCountElement.Text);

                                                if (entityValueCount.firstCount == entityValueCount.secondCount)
                                                {
                                                    TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: first count: {entityValueCount.firstCount} matches with the second count: {entityValueCount.secondCount}");
                                                }
                                                else
                                                {
                                                    TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: first count: {entityValueCount.firstCount} do not matches with the second count: {entityValueCount.secondCount}");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: Edit icon is not visible.");
                                        }
                                    }
                                    catch (NoSuchElementException ex)
                                    {
                                        TestConstants.Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(120));

                                        entityTypeValues[count].Click();
                                        await Task.Delay(2000);

                                        IWebElement hideIcon = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueHideIcon));
                                        if (hideIcon != null)
                                        {
                                            TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: Hide icon is visible.");
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: Hide icon is not visible.");
                                        }

                                        IWebElement editIcon = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueEditIcon));
                                        if (editIcon != null)
                                        {
                                            TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: Edit icon is visible.");
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: Edit icon is not visible.");
                                        }

                                        IWebElement entityValueCountElement = entityTypeValues[count].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueCount));
                                        if (entityValueCountElement != null)
                                        {
                                            var entityValueCount = CompareNumbersInStringFormat(entityValueCountElement.Text);

                                            if (entityValueCount.firstCount == entityValueCount.secondCount)
                                            {
                                                TestConstants.LogTest.Log(Status.Pass, $"{entityTypeValues[count].Text}: first count: {entityValueCount.firstCount} matches with the second count: {entityValueCount.secondCount}");
                                            }
                                            else
                                            {
                                                TestConstants.LogTest.Log(Status.Fail, $"{entityTypeValues[count].Text}: first count: {entityValueCount.firstCount} do not matches with the second count: {entityValueCount.secondCount}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    TestConstants.LogTest.Log(Status.Fail, "Entity type value is not visible.");
                                }
                            }
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Values under {entityType} are not visible.");
                    }
                    i++;
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Analyze By section entity types are not visible.");
            }
        }

        [Test, Order(5)]
        public async Task IntersectionBubbleDisplayTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of intersection bubble and its elements").Info("Verify display of intersection bubble and its elements.");

            IList<IWebElement> analyzeByEntityTypes = await GetEntityTypeList();

            if (analyzeByEntityTypes.Count != 0)
            {
                int i = 1;
                foreach (var entityType in analyzeByEntityTypes)
                {
                    entityType.Click();
                    await Task.Delay(2000);

                    // Get the entity type value list from using method
                    List<IWebElement> entityTypeValues = await GetEntityTypeValues(i);

                    if (entityTypeValues.Count != 0)
                    {
                        entityTypeValues[0].Click();
                        await Task.Delay(2000);

                        IWebElement entityTypeValue = entityTypeValues[0].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValue));

                        // Define WebDriverWait with a timeout of 30 seconds
                        TestConstants.Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(30));

                        IWebElement intersectionBubble = TestConstants.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForIntersectionBubble)));

                        if (intersectionBubble != null)
                        {
                            TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section is visible for '{entityTypeValue.Text}' with text: " + intersectionBubble.Text);

                            IWebElement intersectionBubbleHeader = TestConstants.Driver.FindElement(By.XPath(_xPathForIntersectionBubbleHeader));
                            if (intersectionBubbleHeader != null)
                            {
                                TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section header is visible as: '{intersectionBubbleHeader.Text}'");
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Fail, $"Intersection Bubble section header is not visible.");
                            }

                            IList<IWebElement> intersectionBubbleList = TestConstants.Driver.FindElements(By.XPath(_xPathForIntersectionBubbleList));

                            if (intersectionBubbleList.Count != 0)
                            {
                                TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section list is visible.");

                                var bubbleText = ExtractTextAndCount(intersectionBubble);

                                if (intersectionBubbleList.Count == bubbleText.count)
                                {
                                    TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section list items count {intersectionBubbleList.Count} matches with the count on the bubble header {bubbleText.count}.");
                                }
                                else
                                {
                                    TestConstants.LogTest.Log(Status.Fail, $"Intersection Bubble section list items count {intersectionBubbleList.Count} do not matches with the count on the bubble header {bubbleText.count}.");
                                }
                            }

                            IWebElement intersectionBubbleDropDown = TestConstants.Driver.FindElement(By.XPath(_xPathForIntersectionBubbleDropDown));

                            if (intersectionBubbleDropDown != null)
                            {
                                TestConstants.LogTest.Log(Status.Pass, "Intersection Bubble section drop down is visible.");

                                string intersectionBubbleDropDownAttribute = intersectionBubbleDropDown.GetAttribute("class");

                                if (intersectionBubbleDropDownAttribute.Contains("Mui-disabled"))
                                {
                                    TestConstants.LogTest.Log(Status.Info, "Intersection Bubble section drop down is disabled.");

                                    IWebElement intersectionBubbleBlankList = TestConstants.Driver.FindElement(By.XPath(_xPathForIntersectionBubbleBlankList));

                                    if (intersectionBubbleBlankList != null)
                                    {
                                        TestConstants.LogTest.Log(Status.Pass, $"For {entityTypeValue.Text}: {intersectionBubbleBlankList.Text}");
                                    }
                                    else
                                    {
                                        TestConstants.LogTest.Log(Status.Info, $"For {entityTypeValue.Text}: no text visible.");
                                    }
                                }
                                else
                                {
                                    TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section drop down is enabled.");

                                    intersectionBubbleDropDown.Click();
                                    IWebElement intersectionBubbleDropDownList = TestConstants.Driver.FindElement(By.XPath(_xPathForIntersectionBubbleDropDownList));

                                    if (intersectionBubbleDropDownList != null)
                                    {
                                        TestConstants.LogTest.Log(Status.Pass, $"Intersection Bubble section drop down list is visible.");

                                        Actions action = new Actions(TestConstants.Driver);
                                        action.SendKeys(Keys.Escape).Perform();
                                    }
                                    else
                                    {
                                        TestConstants.LogTest.Log(Status.Fail, $"Intersection Bubble section drop down list is not visible.");
                                    }
                                }
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Fail, $"Intersection Bubble section drop down is not visible.");
                            }
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, $"Intersection Bubble section is not visible for '{entityTypeValue.Text}'");
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Values under {entityType} are not visible.");
                    }
                    i++;
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, $"Analyze By section entity types are not visible.");
            }

        }

        [Test, Order(6)]
        public async Task ResponseCountTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Response section and count").Info("Verify display of Response section and count.");

            IList<IWebElement> analyzeByEntityTypes = await GetEntityTypeList();

            if (analyzeByEntityTypes.Count != 0)
            {
                int i = 1;
                foreach (var entityType in analyzeByEntityTypes)
                {
                    entityType.Click();
                    await Task.Delay(2000);

                    // Get the entity type value list from using method
                    List<IWebElement> entityTypeValues = await GetEntityTypeValues(i);

                    if (entityTypeValues.Count != 0)
                    {
                        if (entityTypeValues[0] != null)
                        {
                            entityTypeValues[0].Click();
                            await Task.Delay(2000);

                            // Define WebDriverWait with a timeout of 60 seconds
                            TestConstants.Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(60));

                            TestConstants.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForResponsesHeader)));

                            IWebElement topPicksToggle = TestConstants.Driver.FindElement(By.XPath(_xPathForTopPicksToggle));
                            if (topPicksToggle != null)
                            {
                                string togglClassAttribute = topPicksToggle.GetAttribute("class");
                                if (togglClassAttribute.Contains("Switch-checked"))
                                {
                                    TestConstants.LogTest.Log(Status.Info, "Top picks toggle is ON by default.");

                                    topPicksToggle.Click();
                                    TestConstants.LogTest.Log(Status.Info, "Top picks toggle clicked and turned OFF.");

                                    await Task.Delay(3000);

                                    IWebElement responseHeader = TestConstants.Driver.FindElement(By.XPath(_xPathForResponsesHeader));

                                    if (responseHeader != null)
                                    {
                                        TestConstants.LogTest.Log(Status.Pass, "Response header is visible.");

                                        var responseHeaderText = ExtractTextAndCount(responseHeader);

                                        IWebElement entityValueCountElement = entityTypeValues[0].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueCount));
                                        if (entityValueCountElement != null)
                                        {
                                            var entityValueCount = CompareNumbersInStringFormat(entityValueCountElement.Text);
                                            int baseCount = int.Parse(entityValueCount.secondCount);

                                            if (responseHeaderText.count == baseCount)
                                            {
                                                TestConstants.LogTest.Log(Status.Pass, $"Entity type value count {entityValueCount.secondCount} matches with the response count{responseHeaderText.count}");
                                            }
                                            else
                                            {
                                                TestConstants.LogTest.Log(Status.Fail, $"Entity type value count {entityValueCount.secondCount} do not match with the response count{responseHeaderText.count}");
                                            }
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, "Entity type value count is not visible.");
                                        }
                                    }
                                    else
                                    {
                                        TestConstants.LogTest.Log(Status.Fail, "Response header is not visible.");
                                    }
                                }
                                else
                                {
                                    TestConstants.LogTest.Log(Status.Info, "Top picks toggle is  OFF.");

                                    IWebElement responseHeader = TestConstants.Driver.FindElement(By.XPath(_xPathForResponsesHeader));

                                    if (responseHeader != null)
                                    {
                                        TestConstants.LogTest.Log(Status.Pass, "Response header is visible.");

                                        var responseHeaderText = ExtractTextAndCount(responseHeader);

                                        IWebElement entityValueCountElement = entityTypeValues[0].FindElement(By.XPath(_xPathForAnalyzeByEntityTypeValueCount));
                                        if (entityValueCountElement != null)
                                        {
                                            var entityValueCount = CompareNumbersInStringFormat(entityValueCountElement.Text);
                                            int baseCount = int.Parse(entityValueCount.secondCount);

                                            if (responseHeaderText.count == baseCount)
                                            {
                                                TestConstants.LogTest.Log(Status.Pass, $"Entity type value count {entityValueCount.secondCount} matches with the response count {responseHeaderText.count}.");
                                            }
                                            else
                                            {
                                                TestConstants.LogTest.Log(Status.Fail, $"Entity type value count {entityValueCount.secondCount} do not match with the response count {responseHeaderText.count}.");
                                            }
                                        }
                                        else
                                        {
                                            TestConstants.LogTest.Log(Status.Fail, "Entity type value count is not visible.");
                                        }
                                    }
                                    else
                                    {
                                        TestConstants.LogTest.Log(Status.Fail, "Response header is not visible.");
                                    }
                                }
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Info, "Top picks toggle is OFF by default.");
                            }
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, "Entity type value is not visible.");
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Values under {entityType} are not visible.");
                    }
                    i++;
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Analyze By section entity types are not visible.");
            }
        }

        [Test, Order(7)]
        public async Task PreviewSectionDisplayTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Preview section and count").Info("Verify display of Preview section and count.");

            IWebElement previewEditIcon = TestConstants.Driver.FindElement(By.XPath(_xPathForPreviewIcon));

            if (previewEditIcon != null && previewEditIcon.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Preview icon is visible.");

                previewEditIcon.Click();
                await Task.Delay(3000);

                IWebElement transcriptPreviewSection = TestConstants.Driver.FindElement(By.XPath(_xPathForPreviewSection));
                if (transcriptPreviewSection != null && transcriptPreviewSection.Displayed)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Preview section is visible.");

                    await Task.Delay(5000);

                    IWebElement previewCancleButton = TestConstants.Driver.FindElement(By.XPath(_xPathForPreviewCancelIcon));
                    if (previewCancleButton != null && previewCancleButton.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Preview cancel button is visible.");

                        previewCancleButton.Click();
                        TestConstants.LogTest.Log(Status.Info, "Preview cancel button clicked.");

                        try
                        {
                            IWebElement transcriptPreviewSectionCheck = TestConstants.Driver.FindElement(By.XPath(_xPathForPreviewSection));

                            if (transcriptPreviewSectionCheck != null)
                            {
                                TestConstants.LogTest.Log(Status.Fail, "Preview section is not closed.");
                            }
                            else
                            {
                                TestConstants.LogTest.Log(Status.Pass, "Preview section is closed.");
                            }
                        }
                        catch (NoSuchElementException ex)
                        {

                            TestConstants.LogTest.Log(Status.Pass, "Preview section is closed.");
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Preview cancel button is not visible.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Preview section is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Edit icon is not visible.");
            }
        }

        [Test, Order(8)]
        public async Task ViewByFilterDisplayeTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of View By filter").Info("Verify display of View By filter.");

            IWebElement viewByFilter = TestConstants.Driver.FindElement(By.XPath(_xPathForViewByFilter));

            if (viewByFilter != null && viewByFilter.Displayed)
            {
                TestConstants.LogTest.Log(Status.Info, "View By filter is visible.");

                IWebElement viewByFilterLabel = viewByFilter.FindElement(By.XPath(_xPathForViewByFilterLabel));
                if (viewByFilterLabel != null)
                {
                    TestConstants.LogTest.Log(Status.Pass, "View By filter label is visible.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "View By filter label is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "View By filter is not visible.");
            }
        }

        [Test, Order(9)]
        public async Task AttributesFilterDisplayeTest()
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify display of Attributes filter").Info("Verify display of Attributes filter.");

            IWebElement attributesFilter = TestConstants.Driver.FindElement(By.XPath(_xPathForAttributesFilter));

            if (attributesFilter != null && attributesFilter.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Attributes filter is visible.");

                IWebElement viewByFilterLabel = attributesFilter.FindElement(By.XPath(_xPathForAttributesFilterLabel));
                if (viewByFilterLabel != null)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Attributes filter label is visible.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Attributes filter label is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Attributes filter is not visible.");
            }
        }

    }
}

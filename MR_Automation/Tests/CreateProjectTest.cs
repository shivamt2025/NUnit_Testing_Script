/* using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Data;
using System.Text.RegularExpressions;

namespace MR_Automation
{
    [TestFixture, Order(3)]
    public class CreateProjectWithAudioVideoFiles : CreateProjectRepository
    {
        [Test, Order(5)]
        public async Task CreateProjectPageAndStagesDisplayTest()
        {
            /// Calling LoginSteps method
            //var loginRepo = new LoginRepository();
            //loginRepo.LoginSteps("Username", "Password");

            // Test - Verify the display of create project page and project stages in the create project page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of create project page and project stages").Info("Test - Verify the display of create project page and project stages.");

            // Calling OpenCreateProjectPage method
            OpenCreateProjectPage();

            // Get the list of visible project creation stages
            List<string> visibleProjectStages = GetVisibleListItems(_xPathForProjectStages);

            await Task.Delay(2000);

            // Verify the visibility of create project page w.r.t. stage counts
            if (visibleProjectStages.Count != 0)
            {
                TestConstants.LogTest.Log(Status.Pass, $"Project stages visible are: {string.Join(", ", visibleProjectStages.Select(text => $"'{text}'"))}");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project stages are not visible.");
            }
        }

        [Test, Order(6)]
        public void DisableCreateProjectButtonTest()
        {
            // Test - Verify the disable create project button condition in the create project page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the disabled create project button condition").Info("Test - Verify the disable create project button condition in the create project page.");

            // Find the cancel button on the create project page
            var cancelButtonElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectCancelButton));

            if (cancelButtonElement != null)
            {
                // Execute JavaScript to scroll up to the cancel button
                IWebElement flag = cancelButtonElement;
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", flag);
            }
            else
            {
                TestConstants.LogTest.Log(Status.Info, "Cancel button is not visible.");
            }

            // Wait
            System.Threading.Thread.Sleep(3000);

            // Find the Create Project button on the page
            IWebElement createProjectButton = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectPageCreateProjectButton));

            if (createProjectButton != null)
            {
                if (!createProjectButton.Enabled)
                {
                    TestConstants.LogTest.Log(Status.Pass, "The create project button is visible but disabled as all the details are not filled.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "The create project button is visible but enabled even when all the details are not filled.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Create Project button is not visible.");
            }
        }

        [Test, Order(7)]
        public void ProjectAndCategoryNameInputTest()
        {
            // Test - Verify the functionality of the Project Name input box in the Create Project page.
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the project name input box functionality").Info("Test - Verify the project name input box functionality in the create project page.");

            TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Generate a unique project name based on the desired format
            string projectName = GenerateUniqueProjectName();

            // Find the project name input field and enter the generated name
            IWebElement projectNameInput = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectNameInputBox));
            if (projectNameInput != null)
            {
                TestConstants.LogTest.Log(Status.Info, "Project name input box is visible.");
                projectNameInput.SendKeys(projectName);

                // Retrieve the entered project name from the input field
                string enteredProjectName = projectNameInput.GetAttribute("value");

                // Verify the condition if enterd project name is not null or empty
                if (!string.IsNullOrEmpty(enteredProjectName))
                {
                    TestConstants.LogTest.Log(Status.Pass, "User is able to enter text \'" + enteredProjectName + "\' in the project name input box.");

                    // Storing the project name in a constant property
                    TestConstants.ProjectName = projectName;
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "User is not able to enter text in the project name input box.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project name input box is not visible.");
            }

            // Test - Verify the functionality of the Category Name input box in the Create Project page. 
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the category name input box functionality").Info("Test - Verify the category name input box functionality in the create project page.");

            // Generate a unique category name based on the desired format
            string categoryName = GenerateUniqueCategoryName();

            // Find the category name input field and enter the generated name
            IWebElement categoryNameInput = TestConstants.Driver.FindElement(By.XPath(_xPathForCategoryNameInputBox));
            if (categoryNameInput != null)
            {
                TestConstants.LogTest.Log(Status.Info, "Category name input box is visible.");
                categoryNameInput.SendKeys(categoryName);

                // Retrieve the entered category name text from the input field
                string enteredCategoryName = categoryNameInput.GetAttribute("value");

                // Verify the condition if enterd category name is not null or empty
                if (!string.IsNullOrEmpty(enteredCategoryName))
                {
                    TestConstants.LogTest.Log(Status.Pass, "User is able to enter text \'" + enteredCategoryName + "\' in the category name input box.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "User is not able to enter text in the category name input box.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Category name input box is not visible.");
            }
        }

        //[Test, Order(8)]
        public virtual void DefaultAudioVideoSelectionTest()
        {
            // Test - Verify the upload file radio button default selection in the create project page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the upload file radio button default selection").Info("Test - Verify the upload file radio button default selection in the create project page.");

            // Find the audio/video radio button element in the upload file section
            IWebElement avRadioButton = TestConstants.Driver.FindElement(By.XPath(_xPathForAudioVideoFilesRadioButton));

            // Verify if the Audio/Video files radio button is selected
            if (avRadioButton != null)
            {
                if (avRadioButton.Selected)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Audio/Video files radio button is selected by default.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Audio/Video files radio button is not selected by default.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Audio/Video files radio button is not visible.");
            }
        }

       // [Test, Order(9)]
        public void PricingMessageBeforeFileUploadDisplayTest()
        {
            // Test - Verify the display of the pricing message in the Estimated Price section before uploading the file
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of the pricing message before uploading the file").Info("Test - Verify the display of the pricing message in the Estimated Price section before uploading the file.");

            // Find the pricing message element 
            IWebElement beforePricingMessage = TestConstants.Driver.FindElement(By.XPath(_xPathForBeforePricingMessage));
            if (beforePricingMessage != null)
            {
                string beforePricingMessageText = beforePricingMessage.Text;
                if (!string.IsNullOrEmpty(beforePricingMessageText) && beforePricingMessageText == _expectedBeforePricingMessageText)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Pricing message matches with the expected message. Message:  " + beforePricingMessageText);
                }
                else if (!string.IsNullOrEmpty(beforePricingMessageText) && beforePricingMessageText != _expectedBeforePricingMessageText)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Pricing message do not matches with the expected message. Message:  " + beforePricingMessageText);
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Pricing message is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Pricing message is not visible.");
            }
        }

        [Test, Order(10)]
        public virtual void DeleteIconDisplayAndFunctionalityTest()
        {
            // Test - Verify the display and functionality of the delete icon in the upload file section.
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display and functionality of the delete icon").Info("Test - Verify the display and functionality of the delete icon in the upload file section.");

            // Calling UploadAudioFile method
            UploadSingleFile(_xPathForUploadedAudioFileDiv, _audioVideoFilesPath);

            // Once the upload is complete, find and click the delete icon
            IWebElement deleteButton = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));

            if (deleteButton != null && deleteButton.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Delete icon is visible.");

                deleteButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Delete button clicked.");

                // Wait for the delete button to disappear after clicking
                Thread.Sleep(3000);

                // Find and verify the visibility of the delete button again
                try
                {
                    IWebElement deleteButtonCheck = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));
                    if (deleteButtonCheck == null)
                    {
                        Console.WriteLine("File has been deleted successfully.");
                        TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                    }
                }
                catch (StaleElementReferenceException)
                {
                    // If the element reference is stale, try finding the element again
                    IWebElement deleteButtonCheck = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));
                    if (deleteButtonCheck == null)
                    {
                        Console.WriteLine("File has been deleted successfully.");
                        TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                    }
                }
                catch (NoSuchElementException)
                {
                    TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Delete icon is not visible.");

            }
        }

        [Test, Order(11)]
        public virtual void UploadFileSectionElementsDisplayTest()
        {
            // Test - Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section").Info("Test - Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section.");

            // Calling UploadAudioFile method
            UploadFilesWithElementsCheck(_xPathForUploadedAudioFileDiv, _audioVideoFilesPath);
        }

        [Test, Order(12)]
        public virtual void OrderDetailsPricingMessageDisplayTest()
        {
            // Test - Verify the display of the order details message above the Estimated Price section after uploading the audio/video file
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of the order details message after uploading the audio/video file").Info("Test - Verify the display of the order details message above the Estimated Price section after uploading the audio/video file.");

            // Find the pricing message element 
            IWebElement orderDetailsMessage = TestConstants.Driver.FindElement(By.XPath(_xPathForOrderDetailsMessage));
            if (orderDetailsMessage != null)
            {
                string orderDetailsMessageText = orderDetailsMessage.Text;

                // Remove dynamic file count from the visible message
                string orderDetailsMessageTextWithoutCount = Regex.Replace(orderDetailsMessageText, @"\d+", "[0]").Replace("\r\n", " ");

                if (!orderDetailsMessage.Displayed)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Order details message is not visible.");
                }
                else if (orderDetailsMessage.Displayed && !string.IsNullOrEmpty(orderDetailsMessageText) && _expectedOrderDetailsMessageForAVFilesText.Equals(orderDetailsMessageTextWithoutCount))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Order details message matches with the expected message. Message visible: \"{orderDetailsMessageText}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Order details message do not matches with the expected message. Message visible: \"{orderDetailsMessageText}\"");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Order details message is not visible.");
            }
        }

        [Test, Order(13)]
        public void CostSectionDisplayTest()
        {
            // Test - Verify the display of the cost section details
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of the cost section details").Info("Test - Verify the display of the cost section details.");

            // Find the 'estimated price' label and its amount
            IWebElement estimatedPrice = TestConstants.Driver.FindElement(By.XPath(_xPathForEstimatedPriceLabel));
            IWebElement estimatedPriceAmount = TestConstants.Driver.FindElement(By.XPath(_xPathForEstimatedPriceAmount));

            // Find the 'current balance' label and its amount
            IWebElement currentBalance = TestConstants.Driver.FindElement(By.XPath(_xPathForCurrentBalanceLabel));
            IWebElement currentBalanceAmount = TestConstants.Driver.FindElement(By.XPath(_xPathForCurrentBalanceAmount));

            // Verify the 'Estimated Price' label is displayed
            if (estimatedPrice != null)
            {
                if (!string.IsNullOrEmpty(estimatedPrice.Text) && estimatedPrice.Displayed && estimatedPriceAmount != null)
                {
                    TestConstants.LogTest.Log(Status.Info, $"Estimated price label is visible as: \"{estimatedPrice.Text}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "'Estimated Price' label is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "'Estimated Price' label is not visible.");
            }

            // Verify the estimated price amount is displayed
            if (true)
            {
                if (!string.IsNullOrEmpty(estimatedPriceAmount.Text) && estimatedPriceAmount.Displayed)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Estimated price amount is visible as: \"{estimatedPriceAmount.Text}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Estimated price amount is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Estimated price amount is not visible.");
            }

            // Verify the 'Current Balance' label is displayed
            if (currentBalance != null)
            {
                if (!string.IsNullOrEmpty(currentBalance.Text) && currentBalance.Displayed && currentBalanceAmount != null)
                {
                    TestConstants.LogTest.Log(Status.Info, $"Current balance label is visible as:  \"{currentBalance.Text}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Current Balance label is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "'Current Balance' label is not visible");
            }

            // Verify the current balance amount is displayed
            if (currentBalanceAmount != null)
            {
                if (!string.IsNullOrEmpty(currentBalanceAmount.Text) && currentBalanceAmount.Displayed)
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Current balance amount is visible as: \"{currentBalanceAmount.Text}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Current balance amount is not visible.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Current balance amount is not visible.");
            }

            // Convert values to numerical format for comparison
            decimal estimatedPrice1 = decimal.Parse(estimatedPriceAmount.Text.Trim('$'));
            decimal currentBalance1 = decimal.Parse(currentBalanceAmount.Text.Trim('$'));

            // Verify if current balance value is less than estimated price value
            if (currentBalance1 < estimatedPrice1)
            {
                TestConstants.LogTest.Log(Status.Info, "Current balanec is less than the estimated price.");

                // Find the 'payment required' label and its amount
                IWebElement paymentRequired = TestConstants.Driver.FindElement(By.XPath(_xPathForPaymentRequiredLabel));
                IWebElement paymentRequiredAmount = TestConstants.Driver.FindElement(By.XPath(_xPathForPaymentRequiredAmount));

                // Find the alert message element for 'Payment Required'
                IWebElement paymentRequiredAlert = TestConstants.Driver.FindElement(By.XPath(_xPathForPaymentRequiredAlert));

                // Verify the 'Payment Required' label is displayed
                if (paymentRequired != null)
                {
                    if (!string.IsNullOrEmpty(paymentRequired.Text) && paymentRequired.Displayed && paymentRequiredAmount != null)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"Payment required label is visible as: \"{paymentRequired.Text}\"");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "'Payment Required' label is not visible.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "'Payment Required' label is not visible.");
                }

                // Verify the payment required amount is displayed
                if (paymentRequiredAmount != null)
                {
                    if (!string.IsNullOrEmpty(paymentRequiredAmount.Text)! && paymentRequiredAmount.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"Payment required amount is visible as: \"{paymentRequiredAmount.Text}\"");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Payment required amount is not visible.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Payment required amount is not visible.");
                }

                // Verify the payment alert message display
                if (paymentRequiredAlert != null)
                {
                    // Remove dynamic amount from the visible alert message
                    string paymentRequiredAlertWithoutAmount = Regex.Replace(paymentRequiredAlert.Text, @"\d+", "[0]");

                    if (!string.IsNullOrEmpty(paymentRequiredAlert.Text) && paymentRequiredAlert.Displayed && _expectedPaymentAlertMessageText.Equals(paymentRequiredAlertWithoutAmount))
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Payment alert message is visible when current balance is less than estimated price.");
                        TestConstants.LogTest.Log(Status.Info, $"Payment alert message is visible as: \"{paymentRequiredAlert.Text}\"");
                    }
                    else if (!string.IsNullOrEmpty(paymentRequiredAlert.Text) && paymentRequiredAlert.Displayed && !_expectedPaymentAlertMessageText.Equals(paymentRequiredAlertWithoutAmount))
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Payment alert message is visible when current balance is less than estimated price.");
                        TestConstants.LogTest.Log(Status.Fail, $"Payment alert message do not match with the expected message. Visible message is: \"{paymentRequiredAlert.Text}\"");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Payment alert message is not visible.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "Current balanec is more than or equal to the estimated price.");
                }
            }
        }

        [Test, Order(14)]
        public async Task CreateProjectButtonFunctionalityTest()
        {
            // Test - Verify the create project button functionality to move to the configure page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the create project button functionality to move to the configure page", "Verify the create project button functionality to move to the configure page.");

            // Find the cancel button
            var cancelButtonElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectCancelButton));

            if (cancelButtonElement != null)
            {
                // Execute JavaScript to scroll upto the cancel button
                IWebElement flag = cancelButtonElement;
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", flag);
            }

            System.Threading.Thread.Sleep(3000);

            // Find the create project button 
            IWebElement createProjectButton = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectPageCreateProjectButton));

            // Verify if the create project button is enabled or disabled
            if (createProjectButton != null)
            {
                TestConstants.LogTest.Log(Status.Info, "Create project button is visible.");

                if (createProjectButton.Displayed && createProjectButton.Enabled)
                {
                    TestConstants.LogTest.Log(Status.Pass, "Create project button is enable as all the details are filled.");

                    // Perform click operation the create project button
                    createProjectButton.Click();
                    TestConstants.LogTest.Log(Status.Info, "Create project button clicked.");

                    //TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    await Task.Delay(2000);

                    //Get the current url 
                    string actualUrl = TestConstants.Driver.Url;

                    //Assign the expected url to compare with the current url
                    string expectedUrl = $"{TestConstants.BaseURL}createProject/configure_data/";

                    //Verify the actual and expected url condition
                    if (actualUrl != null && !string.IsNullOrEmpty(actualUrl) && actualUrl.Contains(expectedUrl))
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Navigation to configuration page successful.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Navigation to configuration page unsuccessful.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Create project button is disabled even when all the details are filled.");
                    TestConstants.LogTest.Log(Status.Fail, "Cannot perform click operation on the create project button.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Create project button is not visible.");
            }
        }

        [Test, Order(15)]
        public virtual void ConfigureData()
        {
            // Test - Verify the display of configure data page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of configure data page").Info("Verify the display of configure data page.");

            // Calling OpenCreateProjectPage method
            //OpenCreateProjectPage();

            // Calling CreateNewProject method
            //CreateNewProject();
            try
            {
                bool configureDataPageText = TestConstants.Driver.FindElement(By.XPath(_xPathForFilesUploadedHeader)).Displayed;
                TestConstants.LogTest.Log(configureDataPageText ? Status.Pass : Status.Fail, "Configure data page is visible.");

                // Find all elements with the specified CSS class
                IList<IWebElement> visibleFiles = TestConstants.Driver.FindElements(By.XPath(_xPathForVisibleUploadedFilesName));

                var listTestData = ReadDataFromExcel(_excelTestDataFilePathForAudioVideo);
                bool isCustomSegmentToggled = false;
                string customSegmentName = string.Empty;

                ICollection<IWebElement> locationInputs = TestConstants.Driver.FindElements(By.XPath(_xPathForLocationInput));
                ICollection<IWebElement> segmentInputs = TestConstants.Driver.FindElements(By.XPath(_xPathForCustomSegmentInput));

                var itemCount = 0;

                if (listTestData.IsCustomSegmentVisible)
                {
                    isCustomSegmentToggled = true;
                    // Find the custom segment toggle button
                    IWebElement customSegmentToggleButton = TestConstants.Driver.FindElement(By.XPath(_xPathForCustomSegmentToggleButton));

                    // Check if the custom segment toggle is currently selected (ON)
                    bool isToggleButtonSelected = customSegmentToggleButton.Selected;

                    if (!isToggleButtonSelected)
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Toggle button is disabled by default.");

                        // Perform a click on the toggle button to toggle its state
                        customSegmentToggleButton.Click();
                        Thread.Sleep(3000);

                        IWebElement segmentHeaderInput = TestConstants.Driver.FindElement(By.XPath(_xPathForCustomSegmentHeader));

                        segmentHeaderInput.Clear();
                        segmentHeaderInput.SendKeys(listTestData.CustomSegmentName);

                        customSegmentName = listTestData.CustomSegmentName;
                        TestConstants.LogTest.Log(Status.Pass, $"Custom Segment added: {customSegmentName}");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Toggle button is not disabled by default.");
                    }
                }

                foreach (var file in visibleFiles)
                {
                    var listelement = listTestData.TestDataSheets.FirstOrDefault(x => x.FileName == file.Text);
                    IWebElement locationInput = locationInputs.ElementAt(itemCount);
                    if (listelement?.Location != null)
                    {
                        locationInput.Clear();
                        locationInput.SendKeys(listelement.Location);
                        TestConstants.LogTest.Log(Status.Pass, $"Location '{listelement.Location}' added for '{listelement.FileName}'");
                    }
                    IWebElement segmentInput = segmentInputs.ElementAt(itemCount);
                    if (isCustomSegmentToggled && listelement?.CustomSegment != null)
                    {
                        segmentInput.Clear();
                        segmentInput.SendKeys(listelement.CustomSegment);

                        TestConstants.LogTest.Log(Status.Pass, $"{customSegmentName}: '{listelement.CustomSegment}' added for '{listelement.FileName}'");
                    }
                    itemCount++;
                }
                // Iterate through each element
                foreach (var visiblefile in visibleFiles)
                {
                    // Verify if the element is displayed
                    if (visiblefile.Displayed && TestConstants.UploadedProjectFiles.Any(x => x == visiblefile.Text))
                    {
                        // Get the text of the visible file
                        string fileName = visiblefile.Text;
                        Console.WriteLine(fileName);
                    }
                }
            }
            catch (NoSuchElementException)
            {
                TestConstants.LogTest.Log(Status.Fail, "Configure data page did not load.");
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, ex.Message);
            }
            try
            {
                // Read analysis topics from the excel file
                string[] analysisTopics = ReadAnalysisTopicsFromExcel(_excelTestDataFilePathForAudioVideo);
                // Read area of interests from the excel file
                Dictionary<string, List<string>> areaOfInterestData = ReadAreaOfInterestFromExcel(_excelTestDataFilePathForAudioVideo);
                // Read location from the excel file
                string[] location = ReadLocationsFromExcel(_excelTestDataFilePathForAudioVideo);
                // Read custom segment from the excel file
                string[] segment = ReadSegmentFromExcel(_excelTestDataFilePathForAudioVideo);
                //Read Uploaded files from the excel file
                //  string[] listedFiles = ReadFileNameFromExcel(_excelTestDataFilePath);
                // Populate Analysis topics
                HandleAnalysisTopic(analysisTopics);
                // Populate Area of Interest
                HandleAreaOfInterest(areaOfInterestData);
                // Populate Location
                //   HandleLocation(location);
                // Populate Custom Segment
                // HandleCustomSegment(segment);
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, $"An error occurred: {ex.Message}");
            }
        }

        [Test, Order(16)]
        public void ProcessOutputButtonFunctionalityTest()
        {
            // Test - Verify the functionality of the Process Output button on the configure data page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the functionality of the Process Output button on the configure data page").Info("Verify the functionality of the Process Output button on the configure data page.");

            // Find the cancel button on the configure data page
            var cancelButtonConfigurePage = TestConstants.Driver.FindElement(By.XPath(_xPathForConfigurePageCancelButton));

            if (cancelButtonConfigurePage != null)
            {
                // Execute JavaScript to scroll upto the cancel button
                IWebElement flag = cancelButtonConfigurePage;
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", flag);
            }
            else
            { }

            // Find the 'Process Output' button
            IWebElement processOutputButton = TestConstants.Driver.FindElement(By.XPath(_xPathForProcessOutputButton));

            // Verify if 'Process Output' button is enabled
            if (processOutputButton != null && processOutputButton.Enabled)
            {
                TestConstants.LogTest.Log(Status.Info, "Process Output button is enabled.");

                // Perform click operation on 'Process Output' button
                processOutputButton.Click();

                Thread.Sleep(2000);

                try
                {
                    IWebElement alertMessageForProcessOutput = TestConstants.Driver.FindElement(By.XPath(_xPathForProcessOutputAlert));
                    if (alertMessageForProcessOutput != null && alertMessageForProcessOutput.Text != null)
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Process Output button is not clickable due to missing data.");
                        TestConstants.LogTest.Log(Status.Fail, $"Alert message visible is: '{alertMessageForProcessOutput.Text}'");
                    }
                }
                catch (Exception ex)
                {
                    TestConstants.LogTest.Log(Status.Info, "Process Output button clicked.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Process Output button is disabled.");
            }

            TestConstants.Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(10));

            // Wait until 'Validating/Processing' element is visible
            TestConstants.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForProcessOutputPageText)));

            // Find the text element on the process output page
            IWebElement processOutputPageMessage = TestConstants.Driver.FindElement(By.XPath(_xPathForProcessOutputPageMessage));

            // Verify if the text is visible on the process page    
            if (processOutputPageMessage != null && !string.IsNullOrEmpty(processOutputPageMessage.Text))
            {
                TestConstants.LogTest.Log(Status.Pass, $"Process Output page is visible with the confirmation message: \"{processOutputPageMessage.Text}\"");
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Process Output page is not visible.");
            }
        }

        [Test, Order(17)]
        public void BackToHomeButtonFunctionalityTest()
        {
            // Test - Verify the functionality of the Back To Home button on the process output page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the functionality of the Back To Home button on the process output page").Info("Verify the functionality of the Back To Home button on the process output page.");

            // Find the 'Back To Home' button
            IWebElement backToHomeButtonElement = TestConstants.Driver.FindElement(By.XPath(_xPathForBackToHomeButton));

            if (backToHomeButtonElement != null)
            {
                // Execute JavaScript to scroll upto the 'Back To Home' button
                IWebElement flag = backToHomeButtonElement;
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", flag);

                // Verify if 'Back To Home' button is visible and enabled
                if (backToHomeButtonElement.Displayed && backToHomeButtonElement.Enabled)
                {
                    TestConstants.LogTest.Log(Status.Info, "Back to home page button is visible.");

                    // Perform click on the 'Back To Home' button
                    backToHomeButtonElement.Click();
                    TestConstants.LogTest.Log(Status.Info, "Back to home page button clicked.");

                    // Define WebDriverWait with a timeout of 10 seconds
                    WebDriverWait Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(10));

                    // Wait until the element with text Welcome is visible               
                    IWebElement homeWelcomeElement = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForHomePageWelcomeText)));

                    // Verify if the desired text is visible on the page
                    if (!string.IsNullOrEmpty(homeWelcomeElement.Text) && homeWelcomeElement.Text.Contains(_expectedHomePageWelcomeText))
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Application home page is visible.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Application home page is not visible.");
                    }
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Back to home page button is not enabled.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Back to home page button is not visible.");
            }
        }

        [Test, Order(18)]
        public void ProjectVisibilityCheck()
        {
            // Test - Verify the display of the created project on the top of the project list
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of the created project on the top of the project list").Info("Verify the display of the created project on the top of the project list.");

            // Find the first project list element on the home page
            IWebElement listedProjectName = TestConstants.Driver.FindElement(By.XPath(_xPathForFirstProjectListed));

            if (listedProjectName != null)
            {
                string listedProjectText = listedProjectName.Text;

                // Verify if entered project name matches with the visible first project name
                if (!string.IsNullOrEmpty(listedProjectText) && listedProjectText.Equals(TestConstants.ProjectName))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"The created project displayed at the top with the given project name: \"{listedProjectText}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "The created project not listed at the top of the home page.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Project list not found on the home page.");
            }
        }
    }

    [TestFixture, Order(5)]
    public class CreateProjectWithTranscriptFiles : CreateProjectWithAudioVideoFiles
    {
        [Test, Order(8)]
        public override void DefaultAudioVideoSelectionTest()
        {
            // Find the audio/video radio button element in the upload file section
            IWebElement rtRadioButton = TestConstants.Driver.FindElement(By.XPath(_xPathForResearchTranscriptsRadioButton));

            // Verify if the Audio/Video files radio button is selected
            if (rtRadioButton != null)
            {
                rtRadioButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Research Transcripts radio button clicked.");

                if (rtRadioButton.Selected)
                {
                    TestConstants.LogTest.Log(Status.Info, "Research Transcripts radio button is selected.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Info, "Research Transcripts radio button is already selected.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Research Transcripts radio button is not visible.");
            }
        }

        [Test, Order(10)]
        public override void DeleteIconDisplayAndFunctionalityTest()
        {
            // Test - Verify the display and functionality of the delete icon in the upload file section.
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display and functionality of the delete icon").Info("Test - Verify the display and functionality of the delete icon in the upload file section.");

            // Calling UploadAudioFile method
            UploadSingleFile(_xPathForUploadedTranscriptFileDiv, _transcriptFilesPath);

            // Once the upload is complete, find and click the delete icon
            IWebElement deleteButton = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));

            if (deleteButton != null && deleteButton.Displayed)
            {
                TestConstants.LogTest.Log(Status.Pass, "Delete icon is visible.");

                deleteButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Delete button clicked.");

                // Wait for the delete button to disappear after clicking
                Thread.Sleep(3000);

                // Find and verify the visibility of the delete button again
                try
                {
                    IWebElement deleteButtonCheck = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));
                    if (deleteButtonCheck == null)
                    {
                        Console.WriteLine("File has been deleted successfully.");
                        TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                    }
                }
                catch (StaleElementReferenceException)
                {
                    // If the element reference is stale, try finding the element again
                    IWebElement deleteButtonCheck = TestConstants.Driver.FindElement(By.XPath(_xPathForFileDeleteIcon));
                    if (deleteButtonCheck == null)
                    {
                        Console.WriteLine("File has been deleted successfully.");
                        TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                    }
                }
                catch (NoSuchElementException)
                {
                    TestConstants.LogTest.Log(Status.Pass, "File has been deleted successfully.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Delete icon is not visible.");

            }
        }

        [Test, Order(11)]
        public override void UploadFileSectionElementsDisplayTest()
        {
            // Test - Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section").Info("Test - Verify the display and location of the file name, file icon, progress bar, uploading message, delete icon in the upload file section.");

            //// Calling OpenCreateProjectPage method
            //OpenCreateProjectPage();

            // Calling UploadAudioFile method
            UploadFilesWithElementsCheck(_xPathForUploadedTranscriptFileDiv, _transcriptFilesPath);
        }

        [Test, Order(12)]
        public override void OrderDetailsPricingMessageDisplayTest()
        {
            // Test - Verify the display of the order details message above the Estimated Price section after uploading the transcript file
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of the order details message after uploading the transcript file").Info("Test - Verify the display of the order details message above the Estimated Price section after uploading the transcript file.");

            // Find the pricing message element 
            IWebElement orderDetailsTextFilesMessage = TestConstants.Driver.FindElement(By.XPath(_xPathForOrderDetailsMessage));
            if (orderDetailsTextFilesMessage != null)
            {
                string orderDetailsTextFilesMessageText = orderDetailsTextFilesMessage.Text;

                // Remove dynamic file count from the visible message
                string orderDetailsTextFilesMessageTextWithoutCount = Regex.Replace(orderDetailsTextFilesMessageText, @"\d+", "[0]").Replace("\r\n", " ");

                if (!orderDetailsTextFilesMessage.Displayed)
                {
                    TestConstants.LogTest.Log(Status.Fail, "Order details message is not visible.");

                }
                else if (orderDetailsTextFilesMessage.Displayed && !string.IsNullOrEmpty(orderDetailsTextFilesMessageText) && _expectedOrderDetailsMessageForTransciptFilesText.Equals(orderDetailsTextFilesMessageTextWithoutCount))
                {
                    TestConstants.LogTest.Log(Status.Pass, $"Order details message matches with the expected message. Message visible: \"{orderDetailsTextFilesMessage}\"");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, $"Order details message do not matches with the expected message. Message visible: \"{orderDetailsTextFilesMessage}\"");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Order details message is not visible.");


            }
        }

        [Test, Order(15)]
        public override void ConfigureData()
        {
            // Test - Verify the display of configure data page
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Verify the display of configure data page").Info("Verify the display of configure data page.");

            // Calling OpenCreateProjectPage method
            //OpenCreateProjectPage();

            // Calling CreateNewProject method
            //CreateNewProject();
            try
            {
                bool configureDataPageText = TestConstants.Driver.FindElement(By.XPath(_xPathForConfigurePageText)).Displayed;
                TestConstants.LogTest.Log(configureDataPageText ? Status.Pass : Status.Fail, "Configure data page is visible.");

                // Find all elements with the specified CSS class
                IList<IWebElement> visibleFiles = TestConstants.Driver.FindElements(By.XPath(_xPathForUploadedFileList));

                var listTestData = ReadDataFromExcel(_excelTestDataFilePathForTranscript);
                bool isCustomSegmentToggled = false;
                string customSegmentName = string.Empty;

                ICollection<IWebElement> locationInputs = TestConstants.Driver.FindElements(By.XPath(_xPathForLocationInput));
                ICollection<IWebElement> segmentInputs = TestConstants.Driver.FindElements(By.XPath(_xPathForCustomSegmentInput));


                var itemCount = 0;

                if (listTestData.IsCustomSegmentVisible)
                {
                    isCustomSegmentToggled = true;
                    // Find the custom segment toggle button
                    IWebElement customSegmentToggleButton = TestConstants.Driver.FindElement(By.XPath(_xPathForCustomSegmentToggleButton));

                    // Check if the custom segment toggle is currently selected (ON)
                    bool isToggleButtonSelected = customSegmentToggleButton.Selected;

                    if (!isToggleButtonSelected)
                    {
                        TestConstants.LogTest.Log(Status.Pass, "Custom Segment toggle is disabled by default.");

                        // Perform a click on the toggle button to toggle its state
                        customSegmentToggleButton.Click();
                        Thread.Sleep(3000);

                        IWebElement segmentHeaderInput = TestConstants.Driver.FindElement(By.XPath(_xPathForCustomSegmentHeader));

                        segmentHeaderInput.Clear();
                        segmentHeaderInput.SendKeys(listTestData.CustomSegmentName);

                        customSegmentName = listTestData.CustomSegmentName;
                        TestConstants.LogTest.Log(Status.Pass, $"Custom Segment added: {customSegmentName}");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Custom Segment toggle is not disabled by default.");
                    }
                }

                foreach (var file in visibleFiles)
                {
                    var listelement = listTestData.TestDataSheets.FirstOrDefault(x => x.FileName == file.Text);
                    IWebElement locationInput = locationInputs.ElementAt(itemCount);
                    if (listelement?.Location != null)
                    {
                        locationInput.Clear();
                        locationInput.SendKeys(listelement.Location);
                        TestConstants.LogTest.Log(Status.Pass, $"Location '{listelement.Location}' added for '{listelement.FileName}'");
                    }
                    IWebElement segmentInput = segmentInputs.ElementAt(itemCount);
                    if (isCustomSegmentToggled && listelement?.CustomSegment != null)
                    {
                        segmentInput.Clear();
                        segmentInput.SendKeys(listelement.CustomSegment);

                        TestConstants.LogTest.Log(Status.Pass, $"{customSegmentName}: '{listelement.CustomSegment}' added for '{listelement.FileName}'");
                    }
                    itemCount++;
                }
                // Iterate through each element
                foreach (var visiblefile in visibleFiles)
                {
                    // Verify if the element is displayed
                    if (visiblefile.Displayed && TestConstants.UploadedProjectFiles.Any(x => x == visiblefile.Text))
                    {
                        // Get the text of the visible file
                        string fileName = visiblefile.Text;
                        Console.WriteLine(fileName);
                    }
                }
            }
            catch (NoSuchElementException)
            {
                TestConstants.LogTest.Log(Status.Fail, "Configure data page did not load.");
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, ex.Message);
            }
            try
            {
                // Read analysis topics from the excel file
                string[] analysisTopics = ReadAnalysisTopicsFromExcel(_excelTestDataFilePathForTranscript);
                // Read area of interests from the excel file
                Dictionary<string, List<string>> areaOfInterestData = ReadAreaOfInterestFromExcel(_excelTestDataFilePathForTranscript);
                // Read location from the excel file
                string[] location = ReadLocationsFromExcel(_excelTestDataFilePathForTranscript);
                // Read custom segment from the excel file
                string[] segment = ReadSegmentFromExcel(_excelTestDataFilePathForTranscript);
                //Read Uploaded files from the excel file
                //  string[] listedFiles = ReadFileNameFromExcel(_excelTestDataFilePath);
                // Populate Analysis topics
                HandleAnalysisTopic(analysisTopics);
                // Populate Area of Interest
                HandleAreaOfInterest(areaOfInterestData);
                // Populate Location
                //   HandleLocation(location);
                // Populate Custom Segment
                // HandleCustomSegment(segment);
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, $"An error occurred: {ex.Message}");
            }
        }
    }
}


*/








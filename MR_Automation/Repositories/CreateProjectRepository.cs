using AventStack.ExtentReports;
using ExcelDataReader;
using MR_Automation.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Data;
using System.Text;

namespace MR_Automation
{
    public class CreateProjectRepository
    {
        #region Create Project Xpath Properties
        internal string _xPathForProjectStages = ".MuiStepLabel-label";
        internal string _xPathForHomePageCreateProjectButton = "//a//button[@type='button'][contains(@class, 'MuiButtonBase-root')]";
        internal string _xPathForProjectNameInputBox = "(//input[@id='outlined-basic'])[1]";
        internal string _xPathForCategoryNameInputBox = "(//input[@id='outlined-basic'])[2]";
        internal string _xPathForProjectCancelButton = "//button[normalize-space()='cancel']";
        internal string _xPathForProjectPageCreateProjectButton = "//div//button[@type='button'][contains(@class, 'MuiButtonBase-root')][2]";
        internal string _xPathForAudioVideoFilesRadioButton = "//div[@class='flex items-center gap-2']//input[contains(@value,'audio')]";
        internal string _xPathForResearchTranscriptsRadioButton = "//div[@class='flex items-center gap-2']//input[contains(@value,'transcripts')]";
        #endregion

        #region Upload File Xpath Properties
        internal string _xPathForUploadedTranscriptFileDiv = "//div[contains(@class, 'flex justify-start items-center  mt-3 gap-2')]";
        internal string _xPathForUploadedAudioFileDiv = "//div[contains(@class, 'flex justify-start items-center mt-3')]";
        internal string _xPathForUploadFileDivChildElements = ".//div[2]//div//span";
        internal string _xPathForBrowseFileButton = "label[for='file-upload']";
        internal string _xPathForFileUploadElement = "//input[@id='file-upload']";
        internal string _xPathForFileNameElement = ".//div[contains(@class, 'flex justify-between')]//span[1]";
        internal string _xPathForFilePercentageElement = ".//div[contains(@class, 'flex justify-between')]//span[2]";
        internal string _xPathForFileDeleteIcon = ".//div[3]//div//*[local-name()='svg'][@class='cursor-pointer']";
        internal string _xPathForFileIconElement = ".//img";
        internal string _xPathForBlueProgressBar = ".//div[@class='w-full bg-gray-200 h-[4px] dark:bg-blue-200']//div";
        internal string _xPathForGreenProgressBar = ".//span[@role='progressbar']//span";
        #endregion

        #region Pricing Section Xpath Properties
        internal string _xPathForBeforePricingMessage = "//span[@class='flex']";
        internal string _xPathForOrderDetailsMessage = "//div[@class='flex flex-col rounded-md border w-full shadow-md p-4 my-2 bg-white']//span[2]";
        internal string _xPathForEstimatedPriceLabel = "//div[@class='flex my-4 items-center ']//span[contains(text(), 'Estimated')]";
        internal string _xPathForEstimatedPriceAmount = "//div[@class='flex items-center ml-14 gap-2']//span";
        internal string _xPathForCurrentBalanceLabel = "//div[@class='flex my-4 items-center ']//span[contains(text(), 'Current')]";
        internal string _xPathForCurrentBalanceAmount = "//div[@class='flex my-4 items-center '][1]//span[@class='ml-14']";
        internal string _xPathForPaymentRequiredLabel = "//div[@class='flex my-4 items-center ']//span[contains(text(), 'Payment')]";
        internal string _xPathForPaymentRequiredAmount = "//div[@class='flex my-4 items-center '][2]//span[@class='ml-14']']";
        internal string _xPathForPaymentRequiredAlert = "//div[@class='flex gap-2 mt-4 items-center']//span[1]";
        #endregion

        #region Properties read from app config file
        internal string _excelTestDataFilePathForAudioVideo = TestConstants.GetConfigKeyValue("ExcelTestDataFilePathForAudioVideo");
        internal string _excelTestDataFilePathForTranscript = TestConstants.GetConfigKeyValue("ExcelTestDataFilePathForTranscript");
        internal string _audioVideoFilesPath = TestConstants.GetConfigKeyValue("AudioVideoFilesPath");
        internal string _transcriptFilesPath = TestConstants.GetConfigKeyValue("TranscriptFilesPath");
        internal string _expectedBeforePricingMessageText = TestConstants.GetConfigKeyValue("ExpectedBeforePricingMessageText");
        internal string _expectedOrderDetailsMessageForAVFilesText = TestConstants.GetConfigKeyValue("ExpectedOrderDetailsMessageForAVFilesText");
        internal string _expectedOrderDetailsMessageForTransciptFilesText = TestConstants.GetConfigKeyValue("ExpectedOrderDetailsMessageForTransciptFilesText");
        internal string _expectedPaymentAlertMessageText = TestConstants.GetConfigKeyValue("ExpectedPaymentAlertMessageText");
        #endregion

        #region Home Page Xpath Properties
        internal string _xPathForHomePageWelcomeText = "//div[@class='border-r']//span";
        internal string _expectedHomePageWelcomeText = "Welcome";
        #endregion

        #region Configure Date Page Xpath Properties
        internal string _xPathForFilesUploadedHeader = "//span[normalize-space()='Files Uploaded']";
        internal string _xPathForVisibleUploadedFilesName = "//div//span[@class='w-11/12 overflow-hidden text-ellipsis whitespace-nowrap']";
        internal string _xPathForLocationInput = "//input[@placeholder='Enter City/Country']";
        internal string _xPathForCustomSegmentInput = "//input[@placeholder='Enter Male/Loyalist/White collar']";
        internal string _xPathForCustomSegmentHeader = "(//input[@class='focus:outline-none py-1 border-2 rounded-md'])[1]";
        internal string _xPathForConfigurePageCancelButton = "//button[normalize-space()='Cancel']";
        internal string _xPathForProcessOutputButton = "//button[@class='w-full rounded-md']";
        internal string _xPathForProcessOutputAlert = "//div[@class='MuiAlert-message css-1xsto0d']";
        internal string _xPathForProcessOutputPageText = "//p[normalize-space()='Validating/Processing']";
        internal string _xPathForProcessOutputPageMessage = "//div[@class='flex gap-4']//div";
        internal string _xPathForBackToHomeButton = "(//div[@class='py-2 px-20 rounded-md'])[1]";
        internal string _xPathForFirstProjectListed = "(//div[contains(@class, 'flex-wrap') and contains(@class, 'flex-row')]//span[contains(@class, 'cursor-pointer')])[1]";
        internal string _xPathForConfigurePageText = "//span[normalize-space()='Files Uploaded']";
        internal string _xPathForUploadedFileList = "//div//span[@class='w-11/12 overflow-hidden text-ellipsis whitespace-nowrap']";
        internal string _xPathForAnalysisTopicFirstLine = "//input[@placeholder='Enter Analysis Topics here']";
        internal string _xPathForAnalysisTopicNextLine = "//input[@placeholder='Add more...']";
        internal string _xPathForAddAreaOfInterestButton = "//button[normalize-space()='Add Areas of Interest']";
        internal string _xPathForAreaOfInterestFirstValueInput = "//input[contains(@placeholder,'Specify at least 1 value')]";
        internal string _xPathForAreaOfInterestHeaderInput = "//input[@placeholder='Enter section name (e.g., Brand/Product/Services/Themes)']";
        internal string _xPathForAreaOfInterestNextValueInput = "//input[@placeholder='Add more']";
        internal string _xPathForCustomSegmentToggleButton = "//input[@type='checkbox']";
        #endregion


        public void OpenCreateProjectPage()
        {
            // Find and click the create project button element on the home page
            IWebElement createProjectHomeButton = TestConstants.Driver.FindElement(By.XPath(_xPathForHomePageCreateProjectButton));
            if (createProjectHomeButton != null)
            {
                createProjectHomeButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Create Project button on the home page is clicked.");

                //Get the current url 
                string actualUrl = TestConstants.Driver.Url;

                //Assign the expected url to compare with the current url
                string expectedUrl = $"{TestConstants.BaseURL}createProject/upload_file/";

                //Verify the actual and expected url condition
                if (actualUrl != null && !string.IsNullOrEmpty(actualUrl) && actualUrl.Contains(expectedUrl))
                {
                    TestConstants.LogTest.Log(Status.Pass, "Navigation to Create Project page successful.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "Navigation to Create Project page unsuccessful.");
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "Create Project button on the home page is not visible.");
            }
        }

        public async Task UploadMultipleFiles(string divPath, string spath)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, spath);
            string path = Path.GetFullPath(sFile);

            // Find the browse button in the upload file section
            IWebElement browseButton = TestConstants.Driver.FindElement(By.CssSelector(_xPathForBrowseFileButton));
            TestConstants.LogTest.Log(Status.Info, "Browse element is visible.");

            // Find the file upload input element
            IWebElement fileUploadInput = TestConstants.Driver.FindElement(By.XPath(_xPathForFileUploadElement));

            // Get all file paths in the specified directory
            string[] filePaths = System.IO.Directory.GetFiles(path);

            // Concatenate all file paths separated by newline character
            string allFilePaths = string.Join("\n", filePaths);

            // Send all file paths to the file upload input element
            fileUploadInput.SendKeys(allFilePaths);
            TestConstants.LogTest.Log(Status.Info, "File(s) uploading started.");

            // Define a list to store the files name
            List<string> uploadedFilesList = new List<string>();

            if (filePaths != null)
            {
                foreach (string filePath in filePaths)
                {
                    string uploadedFileName = Path.GetFileName(filePath);

                    // Add the text to the list
                    uploadedFilesList.Add(uploadedFileName);

                    // Print the list item text to the console
                    //Console.WriteLine(uploadedFileName);
                }
            }

            // Create a timer with a two second interval.
            //aTimer = new System.Timers.Timer(2000);
            //// Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += OnTimedEvent;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;

            // Find and store all the div elements in a list
            IList<IWebElement> divElements = TestConstants.Driver.FindElements(By.XPath(divPath));

            if (divElements.Count > 0)
            {
                foreach (var divElement in divElements)
                {
                    IList<IWebElement> childDivElements = divElement.FindElements(By.XPath(_xPathForUploadFileDivChildElements));
                    string visibleFileName = childDivElements[0].Text;

                    if (uploadedFilesList.Contains(visibleFileName))
                    {
                        WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(300));
                        wait.Until(ExpectedConditions.TextToBePresentInElement(childDivElements[1], "100%"));
                        TestConstants.LogTest.Log(Status.Pass, $"\"{visibleFileName}\" uploaded successfully.");

                        // Storing successfully uploaded filenames to the constant list property
                        TestConstants.UploadedProjectFiles.Add(visibleFileName);
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "Uploaded file did not match with the visible file name.");
                    }
                }
            }
            else
            {
                TestConstants.LogTest.Log(Status.Fail, "No files were uploaded.");
            }
        }

        public async Task UploadSingleFile(string divPath, string spath)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, spath);
            string path = Path.GetFullPath(sFile);

            // Find the browse button in the upload file section
            IWebElement browseButton = TestConstants.Driver.FindElement(By.CssSelector(_xPathForBrowseFileButton));
            TestConstants.LogTest.Log(Status.Info, "Browse element is visible.");

            // Get all file paths in the specified directory
            string[] filePaths = System.IO.Directory.GetFiles(path);
            string firstFilePath = string.Empty;

            try
            {
                // Verify the file upload completion w.r.t percentage value
                if (filePaths != null && filePaths.Length != 0)
                {
                    firstFilePath = filePaths[0];

                    // Find the file upload input element
                    IWebElement fileUploadInput = TestConstants.Driver.FindElement(By.XPath(_xPathForFileUploadElement));

                    fileUploadInput.SendKeys(firstFilePath);
                    TestConstants.LogTest.Log(Status.Info, "File uploading started.");
                }
                else
                {
                    TestConstants.LogTest.Log(Status.Fail, "File path not found.");
                }

                // Extract the file name from the file path
                string filename = Path.GetFileName(firstFilePath);
                Console.WriteLine("Filename = " + filename);

                // Find the upload file div element containing all the child elements
                IWebElement uploadFileElement = TestConstants.Driver.FindElement(By.XPath(divPath));

                // Find the file name element from the div
                IWebElement fileName = uploadFileElement.FindElement(By.XPath(_xPathForFileNameElement));
                string visibleFileName = fileName.Text;

                // Verify the file upload completion
                try
                {
                    if (visibleFileName != null && !string.IsNullOrEmpty(visibleFileName) && visibleFileName == filename)
                    {
                        TestConstants.LogTest.Log(Status.Info, $"Visible file name \"{visibleFileName}\" matches the with the uploaded file name.");
                        string actualPercentage = string.Empty;
                        string expectedPercentage = "100%";

                        do
                        {
                            IWebElement percentElement = uploadFileElement.FindElement(By.XPath(_xPathForFilePercentageElement));
                            actualPercentage = percentElement.Text;
                        }
                        while (actualPercentage != expectedPercentage);
                        TestConstants.LogTest.Log(Status.Pass, $"\"{visibleFileName}\" uploaded successfully.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Info, $"Visible file name \"{visibleFileName}\" did not match the with the uploaded file name.");
                    }
                }
                catch (Exception ex)
                {
                    TestConstants.LogTest.Log(Status.Fail, ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, ex.Message.ToString());
                throw;
            }
        }

        public async Task UploadFilesWithElementsCheck(string divPath, string spath)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, spath);
            string path = Path.GetFullPath(sFile);

            // Find the browse button in the upload file section
            IWebElement browseButton = TestConstants.Driver.FindElement(By.CssSelector(_xPathForBrowseFileButton));
            TestConstants.LogTest.Log(Status.Info, "Browse file element is visible.");

            // Get all file paths in the specified directory
            string[] filePaths = System.IO.Directory.GetFiles(path);

            string uploadedFileName = string.Empty;
            IWebElement uploadFileElement = null;

            try
            {
                // Verify the file upload completion w.r.t percentage value
                foreach (string filePath in filePaths)
                {
                    if (filePath != null && !string.IsNullOrEmpty(filePath))
                    {
                        uploadedFileName = Path.GetFileName(filePath);
                        Console.WriteLine("Filename = " + uploadedFileName);

                        // Find the file upload input element
                        IWebElement fileUploadInput = TestConstants.Driver.FindElement(By.XPath(_xPathForFileUploadElement));

                        fileUploadInput.SendKeys(filePath);
                        //TestConstants.LogTest.Log(Status.Info, "File uploading started.");

                        //TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                        // Find the upload file div element containing all the child elements
                        uploadFileElement = TestConstants.Driver.FindElement(By.XPath(divPath));

                        // Test - Verify the progress bar colour during ongoing upload

                        WebDriverWait wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(5));
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForBlueProgressBar)));

                        // Find the incomplete progress bar element
                        IWebElement blueProgressBar = uploadFileElement.FindElement(By.XPath(_xPathForBlueProgressBar));

                        if (blueProgressBar.Displayed)
                        {
                            TestConstants.LogTest.Log(Status.Pass, $"During upload, blue progress bar is visible for file \"{uploadedFileName}\".");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Fail, $"Blue Progress bar is not visible for file \"{uploadedFileName}\".");
                        }
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, "File path not found.");
                    }

                    // Find the file name element
                    IWebElement fileName = uploadFileElement.FindElement(By.XPath(_xPathForFileNameElement));
                    string visibleFileName = fileName.Text;

                    // Verify file upload completion
                    try
                    {
                        if (visibleFileName != null && !string.IsNullOrEmpty(visibleFileName) && visibleFileName == uploadedFileName)
                        {
                            string actualPercentage = string.Empty;
                            string expectedPercentage = "100%";

                            do
                            {
                                IWebElement percentElement = uploadFileElement.FindElement(By.XPath(_xPathForFilePercentageElement));
                                actualPercentage = percentElement.Text;
                            }
                            while (actualPercentage != expectedPercentage);

                            TestConstants.LogTest.Log(Status.Info, $"Visible file name \"{visibleFileName}\" matches the with the uploaded file name.");
                        }
                        else
                        {
                            TestConstants.LogTest.Log(Status.Info, $"Visible file name \"{visibleFileName}\" did not match the with the uploaded file name.");
                        }
                    }
                    catch (Exception ex)
                    {
                        TestConstants.LogTest.Log(Status.Fail, ex.Message.ToString());
                    }
                    TestConstants.LogTest.Log(Status.Pass, $"\"{visibleFileName}\" uploaded successfully.");

                    // Storing successfully uploaded filenames to the constant list property
                    TestConstants.UploadedProjectFiles.Add(visibleFileName);

                    TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    // Find the complete progress bar element
                    IWebElement greenProgressBar = uploadFileElement.FindElement(By.XPath(_xPathForGreenProgressBar));

                    // Test - Verify the progress bar color after upload completion

                    if (greenProgressBar.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"After upload completion, green progress bar is visible for file \"{visibleFileName}\".");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Green progress bar is not visible for file \"{visibleFileName}\".");
                    }

                    TestConstants.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    // Test - Verify the display and position of the file icon

                    // Find the file icon element
                    IWebElement fileIcon = uploadFileElement.FindElement(By.XPath(_xPathForFileIconElement));

                    if (!fileIcon.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Fail, "File icon is not visisble.");
                    }
                    else if (fileIcon.Displayed && fileIcon.Location.X < greenProgressBar.Location.X)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"File icon for file \"{visibleFileName}\" is visible on the left-hand side of the progress bar.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"File icon for file \"{visibleFileName}\" should be on the left-hand side of the progress bar.");
                    }

                    // Test - Verify the display and position of the file name
                    if (!fileName.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Fail, "File name is not visisble.");
                    }
                    else if (fileName.Displayed && !string.IsNullOrEmpty(fileName.Text) && fileName.Location.X > fileIcon.Location.X && fileName.Location.Y < greenProgressBar.Location.Y)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"File name \"{visibleFileName}\" is visible above the progress bar and right-hand side of the file icon.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"File name \"{visibleFileName}\" should be visible above progress bar and right-hand side of the file icon.");
                    }

                    // Find the delete icon element
                    IWebElement deleteIcon = uploadFileElement.FindElement(By.XPath(_xPathForFileDeleteIcon));

                    // Test - Verify the display and position of the delete icon
                    if (!deleteIcon.Displayed)
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Delete icon is not visisble for file \"{visibleFileName}\".");
                    }
                    else if (deleteIcon.Displayed && deleteIcon.Location.X > greenProgressBar.Location.X)
                    {
                        TestConstants.LogTest.Log(Status.Pass, $"Delete icon for file \"{visibleFileName}\" is visible on the left-hand side of the progress bar.");
                    }
                    else
                    {
                        TestConstants.LogTest.Log(Status.Fail, $"Delete icon for file \"{visibleFileName}\" should be on the left-hand side of the progress bar.");
                    }
                }
            }
            catch (Exception ex)
            {
                TestConstants.LogTest.Log(Status.Fail, ex.Message.ToString());
                throw;
            }
        }

        // Method: To create a new project
        public async Task CreateNewProject()
        {
            // Generate a unique project name based on the desired format
            string projectName = GenerateUniqueProjectName();

            // Find the project name input field and enter the generated name
            IWebElement projectNameInput = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectNameInputBox));
            projectNameInput.SendKeys(projectName);

            // Retrieve the entered project name from the input field
            string enteredProjectName = projectNameInput.GetAttribute("value");

            // Generate a unique category name based on the desired format
            string categoryName = GenerateUniqueCategoryName();

            // Find the category name input field and enter the generated name
            IWebElement categoryNameInput = TestConstants.Driver.FindElement(By.XPath(_xPathForCategoryNameInputBox));
            categoryNameInput.SendKeys(categoryName);

            UploadMultipleFiles(_xPathForUploadedAudioFileDiv, _audioVideoFilesPath);

            // Find the cancel button
            var cancelButtonElement = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectCancelButton));

            // Execute JavaScript to scroll upto the cancel button
            IWebElement flag = cancelButtonElement;
            IJavaScriptExecutor js = (IJavaScriptExecutor)TestConstants.Driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", flag);
            //System.Threading.Thread.Sleep(3000);

            // Find the create project button 
            IWebElement createProjectButton = TestConstants.Driver.FindElement(By.XPath(_xPathForProjectPageCreateProjectButton));

            // Verify if the create project button is enabled or disabled
            if (createProjectButton.Displayed && createProjectButton.Enabled)
            {
                // Perform click operation the create project button
                createProjectButton.Click();
                TestConstants.LogTest.Log(Status.Info, "Create project button clicked.");

                TestConstants.ProjectName = enteredProjectName;

                //await Task.Delay(2000);

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
                TestConstants.LogTest.Log(Status.Fail, "Create project button is not enabled.");
            }
        }

        // Method: To get the unique project name
        public static string GenerateUniqueProjectName()
        {
            // Generate the current date in the specified format
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy_HH_mm");

            // Generate a random string or number to ensure uniqueness
            string uniqueIdentifier = Guid.NewGuid().ToString().Substring(0, 5);

            // Combine the components to generate the project name
            string projectName = $"[TESTING]_WebFlow_{currentDate}";
            return projectName;
        }

        // Method: To get the random category name
        public string GenerateUniqueCategoryName()
        {
            // Define a list of category names
            string[] categoryNames = { "IT", "Education", "Shopping" };

            // Choose a random category name from the list
            Random rand = new Random();
            int index = rand.Next(categoryNames.Length);
            string categoryName = categoryNames[index];

            return categoryName;
        }

        // Method: To get the list of visible project stages
        public static List<string> GetVisibleListItems(string elementpath)
        {
            // Find all elements with the specified CSS class
            IList<IWebElement> listItemsCollection = TestConstants.Driver.FindElements(By.CssSelector(elementpath));

            if (listItemsCollection.Count != 0)
            {
                // Initialize a list to store visible texts
                List<string> visibleListItems = new List<string>();

                Console.WriteLine("Project stages visible are:");

                // Iterate through each element
                foreach (var visibleListItem in listItemsCollection)
                {
                    // Verify if the element is displayed
                    if (visibleListItem.Displayed)
                    {
                        // Get the text of the displayed element
                        string text = visibleListItem.Text;

                        // Add the text to the list of visible texts
                        visibleListItems.Add(text);

                        // Wait
                        Thread.Sleep(1000);

                        Console.WriteLine(text);
                    }
                }
                return visibleListItems;
            }
            else
            {
                TestConstants.LogTest.Log(Status.Info, "No items found in the list.");
                return new List<string>();
            }
        }

        public TestDataSheetHeader ReadDataFromExcel(string filePath)
        {
            var testDataSheetHeader = new TestDataSheetHeader();

            // Specify a custom encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var listTestDataSheet = new List<TestDataSheet>();
            // Create FileStream to read the Excel file
            using (System.IO.FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create reader for Excel file
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file into a DataSet
                    DataSet result = reader.AsDataSet();
                    // Get the table "ConfigureData" from the DataSet
                    DataTable table = result.Tables["ConfigureData"];
                    // Get the number of rows in the table
                    int rowCount = table.Rows.Count;

                    var customSegmentName = table.Rows[0][2].ToString();
                    if (!string.IsNullOrEmpty(customSegmentName))
                    {
                        var splittedCustomSegmentName = customSegmentName.Split(":")[1];
                        if (!string.IsNullOrEmpty(splittedCustomSegmentName))
                        {
                            testDataSheetHeader.CustomSegmentName = splittedCustomSegmentName;
                            testDataSheetHeader.IsCustomSegmentVisible = true;
                        }
                    }

                    // Initialize array to store Analysis Topics
                    string[] location = new string[rowCount - 1]; // Exclude header row
                    for (int i = 1; i < rowCount; i++) // Start from 1 to skip header row
                    {
                        var itemData = new TestDataSheet()
                        {
                            Id = i,
                            FileName = table.Rows[i][0].ToString(),
                            Location = table.Rows[i][1].ToString(),
                            CustomSegment = table.Rows[i][2].ToString()
                        };
                        if (!string.IsNullOrEmpty(itemData.Location) && !string.IsNullOrEmpty(itemData.FileName))
                            listTestDataSheet.Add(itemData);
                    }
                    testDataSheetHeader.TestDataSheets = listTestDataSheet;
                    return testDataSheetHeader;
                }
            }
        }

        public string[] ReadAnalysisTopicsFromExcel(string filePath)
        {
            // Specify a custom encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Create FileStream to read the Excel file
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create reader for Excel file
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file into a DataSet
                    DataSet result = reader.AsDataSet();

                    // Get the table "ConfigureData" from the DataSet
                    DataTable table = result.Tables["ConfigureData"];

                    // Get the number of rows in the table
                    int rowCount = table.Rows.Count;

                    // Initialize array to store Analysis Topics
                    string[] analysisTopics = new string[rowCount - 1]; // Exclude header row

                    // Read Analysis Topics from Excel
                    for (int i = 1; i < rowCount; i++) // Start from 1 to skip header row
                    {
                        analysisTopics[i - 1] = table.Rows[i][3].ToString(); // Assuming Analysis Topics are in column D (index 3)
                    }

                    return analysisTopics;
                }
            }
        }

        public void HandleAnalysisTopic(string[] analysisTopics)
        {
            bool firstLine = true; // Flag to track the first line
            foreach (string topic in analysisTopics)
            {
                // Find the input field for Question Buckets and enter data
                string xPathOfAnalysisTopicInput = firstLine ? _xPathForAnalysisTopicFirstLine : _xPathForAnalysisTopicNextLine;
                IWebElement questionBucketField = TestConstants.Driver.FindElement(By.XPath(xPathOfAnalysisTopicInput));
                questionBucketField.Clear();
                questionBucketField.SendKeys(topic);

                // Simulate pressing Enter key on keyboard
                questionBucketField.SendKeys(Keys.Enter);

                // Reset firstLine flag after the first line is entered
                firstLine = false;

                TestConstants.LogTest.Log(Status.Pass, $"Analysis Topic added: '{topic}'");
            }
        }

        public Dictionary<string, List<string>> ReadAreaOfInterestFromExcel(string filePath)
        {
            // Specify a custom encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Create FileStream to read the Excel file
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create reader for Excel file
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file into a DataSet
                    DataSet result = reader.AsDataSet();

                    // Get the table "ConfigureData" from the DataSet
                    DataTable table = result.Tables["ConfigureData"];

                    // Initialize a dictionary to store section names and values
                    Dictionary<string, List<string>> areaOfInterestData = new Dictionary<string, List<string>>();

                    // Loop through each column starting from column E
                    for (int i = 4; i < table.Columns.Count; i++)
                    {
                        string currentSectionName = null;

                        foreach (DataRow row in table.Rows)
                        {
                            string cellValue = row[i].ToString().Trim();

                            // Verify if it's a new section name
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                // Verify if it's a section name or a value

                                if (cellValue.StartsWith("AOI:"))
                                {
                                    // Trim "AOI: " from the section name
                                    currentSectionName = cellValue.Substring("AOI: ".Length);

                                    // Add the section name to the dictionary if it's not already present
                                    if (!areaOfInterestData.ContainsKey(currentSectionName))
                                    {
                                        areaOfInterestData[currentSectionName] = new List<string>();
                                    }
                                }
                                else
                                {
                                    // It's a section value, add it to the current section
                                    areaOfInterestData[currentSectionName].Add(cellValue);
                                }
                            }
                        }
                    }
                    return areaOfInterestData;
                }
            }
        }

        public void HandleAreaOfInterest(Dictionary<string, List<string>> areaOfInterestData)
        {
            // Click on +addAOI button to start entering AOI sections
            IWebElement addAOI = TestConstants.Driver.FindElement(By.XPath(_xPathForAddAreaOfInterestButton));
            addAOI.Click();

            foreach (var kvp in areaOfInterestData)
            {
                // Enter Section name from excel file
                string sectionName = kvp.Key;
                EnterSectionName(sectionName);

                // Enter first section value
                List<string> sectionValues = kvp.Value;
                EnterSectionValues(sectionValues);

                // Verify if there are more section values for the current section
                if (sectionValues.Count > 1)
                {
                    // Click on keyboard enter button after entering the first section value
                    PressEnterKey();

                    // Iterate through remaining section values
                    for (int i = 1; i < sectionValues.Count; i++)
                    {
                        // Enter section value at web element location //input[@placeholder='Add more']
                        string sectionValue = sectionValues[i];
                        EnterSectionValue(sectionValue);

                        // Click on keyboard enter button after entering each section value                        
                        PressEnterKey();
                    }
                    TestConstants.LogTest.Log(Status.Pass, $"Area of Interest added: '{sectionName}' with values: {string.Join(", ", sectionValues.Select(text => $"'{text}'"))}");
                }

                // Verify if there are more sections in the Excel file
                if (kvp.Key != areaOfInterestData.Last().Key)
                {
                    // Click on +addAOI button for next section
                    addAOI.Click();
                }
            }

            Console.WriteLine("All section data entered successfully.");
        }

        public void EnterSectionName(string sectionName)
        {
            Console.WriteLine("Entering section name: " + sectionName);
            IWebElement sectionNameField = TestConstants.Driver.FindElement(By.XPath(_xPathForAreaOfInterestHeaderInput));
            sectionNameField.Clear();
            sectionNameField.SendKeys(sectionName);
        }

        public void EnterSectionValues(List<string> sectionValues)
        {
            // Enter first section value at web element location //input[contains(@placeholder,'Specify at least 1 value')]
            string firstSectionValue = sectionValues[0];
            Console.WriteLine("Entering first section value: " + firstSectionValue);
            IWebElement sectionValueField = TestConstants.Driver.FindElement(By.XPath(_xPathForAreaOfInterestFirstValueInput));
            sectionValueField.Clear();
            sectionValueField.SendKeys(firstSectionValue);
            PressEnterKey();
        }

        public void EnterSectionValue(string sectionValue)
        {
            Console.WriteLine("Entering section value: " + sectionValue);
            // Find all input fields for section values and get the last one, assuming it's the one for adding more values
            IReadOnlyCollection<IWebElement> sectionValueFields = TestConstants.Driver.FindElements(By.XPath(_xPathForAreaOfInterestNextValueInput));
            IWebElement lastSectionValueField = sectionValueFields.Last();
            lastSectionValueField.Clear();
            lastSectionValueField.SendKeys(sectionValue);
        }

        public void PressEnterKey()
        {
            Actions action = new Actions(TestConstants.Driver);
            action.SendKeys(Keys.Enter).Perform();
        }

        public string[] ReadLocationsFromExcel(string filePath)
        {
            // Specify a custom encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Create FileStream to read the Excel file
            using (System.IO.FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create reader for Excel file
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file into a DataSet
                    DataSet result = reader.AsDataSet();

                    // Get the table "ConfigureData" from the DataSet
                    DataTable table = result.Tables["ConfigureData"];

                    // Get the number of rows in the table
                    int rowCount = table.Rows.Count;

                    // Initialize array to store Analysis Topics
                    string[] location = new string[rowCount - 1]; // Exclude header row

                    // Read Analysis Topics from Excel
                    for (int i = 1; i < rowCount; i++) // Start from 1 to skip header row
                    {
                        location[i - 1] = table.Rows[i][1].ToString(); // Assuming Analysis Topics are in column D (index 3)
                    }

                    return location;
                }
            }
        }

        public string[] ReadSegmentFromExcel(string filePath)
        {
            // Specify a custom encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Create FileStream to read the Excel file
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create reader for Excel file
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file into a DataSet
                    DataSet result = reader.AsDataSet();

                    // Get the table "ConfigureData" from the DataSet
                    DataTable table = result.Tables["ConfigureData"];

                    // Get the number of rows in the table
                    int rowCount = table.Rows.Count;

                    // Initialize array to store Analysis Topics
                    string[] segment = new string[rowCount - 1]; // Exclude header row

                    // Read Analysis Topics from Excel
                    for (int i = 1; i < rowCount; i++) // Start from 1 to skip header row
                    {
                        segment[i - 1] = table.Rows[i][2].ToString(); // Assuming Analysis Topics are in column D (index 3)
                    }

                    return segment;
                }
            }
        }

        public void VerifyToggleButton()
        {
            // Assuming 'toggleButton' is the WebElement representing the toggle button
            IWebElement customSegmentToggleButton = TestConstants.Driver.FindElement(By.XPath(_xPathForCustomSegmentToggleButton));

            // Check if the toggle button is disabled by default
            bool isToggleButtonEnabled = customSegmentToggleButton.Enabled;

            if (isToggleButtonEnabled == false)
            {

                TestConstants.LogTest.Log(Status.Pass, "Toggle button is disabled by default");
            }
            else

                TestConstants.LogTest.Log(Status.Fail, "Toggle button is not enabled by default");
        }
    }
}

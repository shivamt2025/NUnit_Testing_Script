using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MR_Automation
{
    public class HomeRepository
    {
        internal string _xPathForHeaderText = "//div[@class='flex gap-2 justify-start items-center']//span";
        internal string _headerText = "MR Transcripts Automation";
        internal string _xPathForHomePageWelcomeText = "//div[@class='border-r']//span";
        internal string _xPathForNavigationText = "//div[@class='flex justify-between flex-col']//div//span";
        internal string _navigationText1 = "Projects";
        internal string _xPathForProjectList = "//div[@class='bg-white rounded-md flex flex-col w-full h-full shadow-md']//span[@class='font-medium text-gray-400 ']";
        internal string _xPathForLogo = "//div[@class='flex gap-2 justify-start items-center']//img";
        internal string _xPathForCalendarIcon = "//*[local-name()='svg' and @class='MuiSvgIcon-root MuiSvgIcon-fontSizeMedium cursor-pointer css-vubbuv']/*[local-name()='path']";
        internal string _xPathForHomeIcon = "//div[@class='cursor-pointer']//div";
        internal string _xPathForProfileIcon = "//div[@class='border-black border-2 rounded-full h-8 w-8 flex justify-center items-center pt-0.5 cursor-pointer bg-transparent text-black']";
        internal string _xPathForHomePageCreateProjectButton = "//a//button[@type='button'][contains(@class, 'MuiButtonBase-root')]";
        internal string _xPathForFilterByElement = "(//div[contains(@role,'combobox')])[2]";
        internal string _xPathForFilterByListElement = "//ul[@role='listbox']/li";
        internal string _xPathForSortByElement = "(//div[contains(@role,'combobox')])[3]";
        internal string _xPathForSortByListElement = "//ul[@role='listbox']/li";
        internal string _xPathForPaginationElement = "//ul[@class='MuiPagination-ul css-nhb8h9']";
        internal string _xPathForSecondPageElement = ".//li//button[contains(@aria-label,'page 2')]";

        public void DashboardSteps() 
        {
            // Define WebDriverWait with a timeout of 10 seconds
            WebDriverWait Wait = new WebDriverWait(TestConstants.Driver, TimeSpan.FromSeconds(10));

            // Wait until the element with text Welcome is visible               
            IWebElement homeWelcomeElement = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(_xPathForHomePageWelcomeText)));
            TestConstants.LogTest.Log(Status.Info, "Application home page is visible.");
        }
    }
}

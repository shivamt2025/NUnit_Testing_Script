using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using OfficeOpenXml;


namespace MR_Automation
{
    public class AnalysisRepository
    {
        internal string _searchBarXPath = "//*[@id=\"root\"]/div/div[2]/div[1]/div/input";
        internal string _xPathForProject = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div";
        internal string _xPathForReviewButton = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[5]/div/div";
        internal string _xPathForReviewMessage = "//*[@id=\"root\"]/div/div[2]/div[1]";
        internal string _xPathFor1 = "//span[contains(@class, 'text-black-600') and contains(@style, 'font-weight: 700; font-size: 14px; font-family: Poppins, sans-serif;') and contains(text(), 'Themes')]\r\n";
        
        internal string _xPathFor2 = "//span[contains(@class, 'text-black-600') and contains(@style, 'font-weight: 700; font-size: 14px; font-family: Poppins, sans-serif;') and contains(text(), 'a')]\r\n";
        internal string _xPathFor3 = "//span[contains(@class, 'text-black-600') and contains(@style, 'font-weight: 700; font-size: 14px; font-family: Poppins, sans-serif;') and contains(text(), 'aaa')]\r\n";
        internal string _xPathForDropdownItems = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[1]/div/div[2]/div/div[1]/div[2]/div/div/div/div";
        internal string _xPathForTopPicksButton = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/div[2]/div/div/div[1]/div[2]/div[1]/div/span/input";
        internal string _xPathForViewByElement = "//*[@id=\"root\"]/div/div[2]/div[1]/div[2]/div/div[1]/div";
        internal string _xPathForTranscriptsOption = "//li[@data-value='Transcripts']\r\n";
        internal string _xPathForAttributesOption= "//li[@data-value='Attributes']\r\n";
        internal string _xPathForGoToElement = "//*[@id=\"root\"]/div/div[2]/div[1]/div[2]/div/div[2]/div/div/div";
        internal string[] _xPathsForOptions = {
          "//li[@data-value='5fc72233-eec1-4ad0-be8f-8eb495edbd3b']\r\n", // Option 1
            "//li[@data-value='c30289d9-156e-4671-95e7-1dc1833a6927']\r\n", // Option 2
            "//li[@data-value='da8fe7ea-f5c5-4363-aee2-a9b7444847e8']\r\n", // Option 3

            // Add more XPath for additional options as needed
        };
        internal string _xPathForResponseCount = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div[2]/div[2]/div/div/div[1]/div[1]/span";
        internal string _xPathForConfigurePage = "//*[@id=\"root\"]/div/div[1]/div/div[2]/div[6]/div[1]/div/div";
        internal string _xPathForGeneratePPTButton = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div/div/div[1]/div/div[3]";
        internal string _xPathForReportNameField = "/html/body/div[2]/div[3]/div/div/div/div/input";
        internal string _xPathForReportDescriptionField = "/html/body/div[2]/div[3]/div/div/div/div/textarea";
        internal string _xPathForGeneratePowerPointButton = "/html/body/div[2]/div[3]/div/div/div/div/div[7]/div[2]";
        internal string _xPathForReportList = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div";
        internal string _xPathForLatestReportLink = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p";

        //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]
        //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p[1]
        //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p[2]
        //*[@id="root"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]/p

        internal string _xPathForLatestReport = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div[2]/div/div[2]/div/div[1]/div[1]";

        internal string _xPathForCloudPage = "//*[@id=\"root\"]/div/div[1]/div/div[2]/div[5]";
        internal string _xPathForSaveViewButton = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div[1]/div[2]/svg/path";
        internal string _xPathForSavedView = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[1]/div[2]/button";


        

        internal string _xPathForFirstSavedViewText = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div";


        internal string _cssSelForDeleteButton = "#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > div.main.w-full.mt-2.flex.flex-col.justify-start.gap-1.overflow-y-auto > div > div > div.absolute.top-2.right-2.mr-3.ml-3.gap-2.flex.items-center > svg:nth-child(2)";

        internal string _cssSelForEditButton = "#root > div > div.main-section.grow.flex.flex-col > div.grow > div > div > div.col-span-7 > div.relative.bg-white.rounded-md.w-full.shadow-md > div.absolute.inset-y-0.right-16.mt-12.mr-12.z-3 > div > div.main.w-full.mt-2.flex.flex-col.justify-start.gap-1.overflow-y-auto > div > div > div.absolute.top-2.right-2.mr-3.ml-3.gap-2.flex.items-center > svg.MuiSvgIcon-root.MuiSvgIcon-fontSizeInherit.cursor-pointer.css-1cw4hi4";

        internal string _xPathForViewViz = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div/div[2]/button";
        

        internal string _xPathForHover = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/div[2]/div";


        internal string _xPathForCloseButton = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[2]/div/button";
        internal string _xPathForTopLeftText = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div[2]/div";



        internal string _xPathForAreaDropdown = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div[1]/div[1]/div/div";
                                                 

        internal string _xPathForLocationDropdown = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div[1]/div[2]/div/div[1]/div/div";
        //*[@id="root"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div/div[2]/div[1]/div/div


        internal string _xPathForProfileDropdown = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div[1]/div[2]/div/div[2]/div/div";


        internal string _xPathForOutsideElement = "//*[@id=\"root\"]/div/div[2]/div[1]";

        internal string _xPathForComparePage= "//*[@id=\"root\"]/div/div[1]/div/div[2]/div[4]";

        internal string _xPathForLeftFilter = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[2]/div/div[1]/table/thead/tr/th[2]/div";
                                               

        internal string _xPathForRightFilter = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[2]/div/div[1]/table/thead/tr/th[3]/div";

        internal string _xPathForLoc1Dropdown = "//*[@id=\"attribute-filter\"]";

        internal string _xPathForLPro1Dropdown = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div/div[2]/div[2]/div/div";

        internal string _xPathForLoc2Dropdown = "//*[@id=\"attribute-filter2\"]";

        internal string _xPathForPro2Dropdown = "//*[@id=\"root\"]/div/div[2]/div[2]/div/div/div[2]/div[1]/div[3]/div/div[1]/div/div[3]/div[2]/div/div";
        

        public void PerformSearch(string searchQuery)
        {
            TestConstants.LogTest = TestConstants.Extent.CreateTest("Review Button").Info("Starting further tests.");

            // Find search bar element and enter search query
            IWebElement searchBar = TestConstants.Driver.FindElement(By.XPath(_searchBarXPath));
            searchBar.SendKeys(searchQuery);
            searchBar.SendKeys(Keys.Enter);
            TestConstants.LogTest.Log(Status.Info, $"Search query '{searchQuery}' entered and search initiated.");
        }

       
    }
}

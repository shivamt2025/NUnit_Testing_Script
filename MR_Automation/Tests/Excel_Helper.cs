using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using AventStack.ExtentReports;
using ExcelDataReader;

namespace MR_Automation
{
    public class ExcelHelper
    {
        private readonly string _filePath;
        private DataTable _dataTable;

        public ExcelHelper(string filePath)
        {
            _filePath = filePath;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ReadExcelFile();
        }

        private void ReadExcelFile()
        {
            try
            {
                using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Use first row as column names
                            }
                        });

                        // Check if the table exists
                        if (result.Tables.Contains("Sheet1"))
                        {
                            _dataTable = result.Tables["Sheet1"];
                        }
                        else
                        {
                            throw new Exception("The table 'Sheet1' was not found in the Excel file.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and rethrow
                TestConstants.LogTest.Log(Status.Error, $"An error occurred while reading the Excel file: {ex.Message}");
                throw;
            }
        }

        public int GetTotalTopicsCount()
        {
            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Assuming the total count of analysis topics is in the first row, second column
            return int.Parse(_dataTable.Rows[0][1].ToString());
        }

        public Dictionary<string, int> GetTopicCountsFromExcel()
        {
            var topicCounts = new Dictionary<string, int>();

            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Start from the second row, as the first row contains the total count
            for (int i = 1; i < _dataTable.Rows.Count; i++)
            {
                string topicName = _dataTable.Rows[i][0]?.ToString();
                string topicCountString = _dataTable.Rows[i][1]?.ToString();

                

                if (int.TryParse(topicCountString, out int topicCount))
                {
                    topicCounts.Add(topicName, topicCount);
                }
                
            }

            return topicCounts;
        }

        public int GetTotalThemesCount()
        {
            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Assuming the total count of themes is in the first row, fourth column
            return int.Parse(_dataTable.Rows[0][3]?.ToString());
        }

        public Dictionary<string, int> GetThemesCountsFromExcel()
        {
            var themesCounts = new Dictionary<string, int>();

            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Start from the second row, as the first row contains the total count
            for (int i = 1; i < _dataTable.Rows.Count; i++)
            {
                string themeName = _dataTable.Rows[i][2]?.ToString();
                string themeCountString = _dataTable.Rows[i][3]?.ToString();

                

                if (int.TryParse(themeCountString, out int themeCount))
                {
                    themesCounts.Add(themeName, themeCount);
                }
               
            }

            return themesCounts;
        }

        public int GetTotalSnacksCount()
        {
            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Assuming the total count of themes is in the first row, sixth column
            return int.Parse(_dataTable.Rows[0][5]?.ToString());
        }

        public Dictionary<string, int> GetSnacksCountsFromExcel()
        {
            var snacksCounts = new Dictionary<string, int>();

            if (_dataTable == null)
                throw new Exception("Data table is not initialized.");

            // Start from the second row, as the first row contains the total count
            for (int i = 1; i < _dataTable.Rows.Count; i++)
            {
                string snackName = _dataTable.Rows[i][4]?.ToString();
                string snackCountString = _dataTable.Rows[i][5]?.ToString();

               

                if (int.TryParse(snackCountString, out int snackCount))
                {
                    snacksCounts.Add(snackName, snackCount);
                }
                
            }

            return snacksCounts;
        }
    }
}

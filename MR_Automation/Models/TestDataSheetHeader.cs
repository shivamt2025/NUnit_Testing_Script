namespace MR_Automation.Models
{
    public class TestDataSheetHeader
    {
        public string CustomSegmentName { get; set; }
        public bool IsCustomSegmentVisible { get; set;}
        public List<TestDataSheet> TestDataSheets { get; set;}
    }
}

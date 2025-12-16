namespace RFScheduling.Domain
{
    public class TestItem
    {
        public int TestItemId { get; set; }

        public string TestItemName { get; set; } = string.Empty;

        public string TestItemType {  get; set; } = string.Empty;

        public bool IsActive { get; set; } 

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

namespace RFScheduling.Domain
{
    public class ProjectTestItem
    {
        public int ProjectTestItemId { get; set; }

        public int ProjectId { get; set; }

        public int? TestItemId { get; set; } // Other可null

        public string? OtherTestItemText { get; set; }

        public string? TestItemType { get; set; }    

        public string Status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; }
    }
}

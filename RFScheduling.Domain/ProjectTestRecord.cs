namespace RFScheduling.Domain
{
    public class ProjectTestRecord
    {
        public int ProjectTestRecordId { get; set; }

        public int ProjectId { get; set; }

        public DateTime ActualStart { get; set; }

        public DateTime ActualEnd { get; set; }

        public decimal TotalDuration { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}

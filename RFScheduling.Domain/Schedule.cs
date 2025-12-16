namespace RFScheduling.Domain
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public int ProjectId { get; set; }

        public int ResourceId { get; set; }

        public int EngineerId { get; set; }

        public DateTime EstimatedStart { get; set; }

        public DateTime EstimateEnd { get; set; }

        public DateTime OriginalEstimatedStart { get; set; }

        public DateTime OriginalEstimatedEnd { get; set; }

        public string Status { get; set; } = string.Empty;

        public int CreatedBy { get; set; } 

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

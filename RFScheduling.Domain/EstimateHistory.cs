namespace RFScheduling.Domain
{
    public class EstimateHistory
    {
        public int EstimateHistoryId { get; set; }

        public int ScheduleId { get; set; }

        public DateTime? OldStart { get; set; }

        public DateTime? OldEnd { get; set; }

        public DateTime? NewStart { get; set; }

        public DateTime? NewEnd { get; set; }

        public string Reason { get; set; } = string.Empty;

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}

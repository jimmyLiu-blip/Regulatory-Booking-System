namespace RFScheduling.Domain
{
    public class ActualTestRecord
    {
        public int ActualTestRecordId { get; set; }

        public int ProjectTestItemId { get; set; }

        public DateTime? ActualStart { get; set; }

        public DateTime? ActualEnd { get; set; }

        public int TotalDuration { get; set; }

        public int PauseCount { get; set; }

        public DateTime? LastCalculatedDate { get; set; }
    }
}

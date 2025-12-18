namespace RFScheduling.Domain
{
    public class TestLog
    {
        public int TestLogId { get; set; }

        public int ScheduleId { get; set; }

        public string ActionType { get; set; } = string.Empty;

        public DateTime ActionTime { get; set; }

        public int UserId { get; set; }

        public string? Notes { get; set; }

        public bool IsModifiable { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

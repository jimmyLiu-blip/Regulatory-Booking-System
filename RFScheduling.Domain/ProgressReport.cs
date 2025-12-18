namespace RFScheduling.Domain
{
    public class ProgressReport
    {
        public int ProgressReportId { get; set; }

        // 對應哪一筆排程
        public int ScheduleId { get; set; }

        // InProgress / Completed / Fail
        public string ReportStatus { get; set; } = null!;

        // 回報內容（必填）
        public string ReportMessage { get; set; } = null!;

        // 回報人（工程師）
        public int ReportedBy { get; set; }

        // 回報時間
        public DateTime ReportedDate { get; set; }
    }
}

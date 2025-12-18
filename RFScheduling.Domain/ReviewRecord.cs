namespace RFScheduling.Domain
{
    public class ReviewRecord
    {
        public int ReviewRecordId { get; set; }

        // 案件
        public int ProjectId { get; set; }

        // 被審查的測項
        public int ProjectTestItemId { get; set; }

        // 第幾次送審（1,2,3...）
        public int ReviewRound { get; set; }

        // Approved / Returned
        public string ReviewResult { get; set; } = null!;

        // 審查意見
        public string? ReviewComment { get; set; }

        // 審查人（Reviewer）
        public int ReviewedBy { get; set; }

        // 審查時間
        public DateTime ReviewedDate { get; set; }

        // 送審當下時間快照
        public DateTime SubmittedAt { get; set; }
    }
}

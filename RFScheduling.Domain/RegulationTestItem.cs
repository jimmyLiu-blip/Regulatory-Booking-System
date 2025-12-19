namespace RFScheduling.Domain
{
    public class RegulationTestItem
    {
        public int RegulationTestItemId { get; set; }

        public int RegulationId { get; set; }

        public int TestItemId { get; set; }

        // 類型補充
        public string? RequirementType { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
    }
}

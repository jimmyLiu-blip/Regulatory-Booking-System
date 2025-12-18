namespace RFScheduling.Domain
{
    public class ProjectRegulation
    {
        public int ProjectRegulationId { get; set; }
        public int ProjectId { get; set; }              // SD: string ProjectId
        public int? RegulationId { get; set; }          // NULL = Other
        public string? OtherRegulationText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

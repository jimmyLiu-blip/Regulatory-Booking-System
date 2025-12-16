namespace RFScheduling.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public int RegulationId { get; set; }

        public int TestItemId { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int CreatedBy { get; set; } 

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}

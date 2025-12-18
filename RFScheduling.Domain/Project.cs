namespace RFScheduling.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string Priority { get; set; } = "P2";

        public string Status { get; set; } = "Pending";

        public int CreatedBy { get; set; } 

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}

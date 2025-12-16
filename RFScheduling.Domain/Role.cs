namespace RFScheduling.Domain
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; } 

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

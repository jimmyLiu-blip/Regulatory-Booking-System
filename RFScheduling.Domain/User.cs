namespace RFScheduling.Domain
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string Email { get; set; } = string.Empty ;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

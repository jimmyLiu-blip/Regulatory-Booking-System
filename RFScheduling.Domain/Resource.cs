namespace RFScheduling.Domain
{
    public class Resource
    {
        public int ResourceId { get; set; }

        public string ResourceName { get; set; } = string.Empty;
        
        public string Area { get; set; } = string.Empty;

        public string ResourceType {  get; set; } = string.Empty;
        
        public bool IsActive { get; set; } 

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

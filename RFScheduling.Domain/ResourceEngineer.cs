namespace RFScheduling.Domain
{
    public class ResourceEngineer
    {
        public int ResourceEngineerId { get; set; }

        public int ResourceId { get; set; }

        public int EngineerId { get; set; }      

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }
    }
}

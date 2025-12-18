namespace RFScheduling.Domain
{
    public class Regulation
    {
        public int RegulationId { get; set; }

        public string RegulationCode { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

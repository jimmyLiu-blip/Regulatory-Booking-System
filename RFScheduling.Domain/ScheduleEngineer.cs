namespace RFScheduling.Domain
{
    public class ScheduleEngineer
    {
        public int ScheduleEngineerId { get; set; }
        public int ScheduleId { get; set; }
        public int EngineerId { get; set; }      
        public DateTime AssignedDate { get; set; }
        public int AssignedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }

}

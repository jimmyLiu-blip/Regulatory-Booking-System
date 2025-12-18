namespace RFScheduling.Domain
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public int ProjectId { get; set; }           

        public int ProjectTestItemId { get; set; }     

        public int ResourceId { get; set; }

        public string ScheduleType { get; set; } = "Case"; 

        public DateTime? EstimatedStart { get; set; }     

        public DateTime? EstimatedEnd { get; set; }

        public DateTime? OriginalEstimatedStart { get; set; }

        public DateTime? OriginalEstimatedEnd { get; set; }

        public string Status { get; set; } = "InQueue";    

        public string? Notes { get; set; }                

        public int CreatedBy { get; set; }                 

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }      
        
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }               
    }
}

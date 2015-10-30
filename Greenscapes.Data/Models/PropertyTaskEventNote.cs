using Newtonsoft.Json;

namespace Greenscapes.Data.Models
{
    public class PropertyTaskEventNote : ModelBase
    {
        
        public string  Notes { get; set; }
    
        public ReviewStatusEnum ReviewStatus { get; set; }
       
      
        public int EventScheduleId
        {
            get;
            set;
        }
      
       [JsonIgnore]
       public EventSchedule EventSchedule { get; set; }
    }
}
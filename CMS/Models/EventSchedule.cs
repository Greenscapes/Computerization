using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class EventSchedule:ModelBase
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartTime { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public StatusEnum Status { get; set; }
        public int PropertyTaskId
        {
            get;
            set;
        }
       [JsonIgnore]
        public PropertyTask PropertyTask { get; set; }
    }
}
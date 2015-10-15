﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
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
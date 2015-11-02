﻿using System;

namespace CMS.Models
{
    public class ServiceTicket : ModelBase
    {
        public string TemplateName { get; set; }

        public string TemplateUrl { get; set; }

        public string ReferenceNumber { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public string JsonFields { get; set; }
        
        public string Notes { get; set; }

        public int Condition { get; set; }
    }
}
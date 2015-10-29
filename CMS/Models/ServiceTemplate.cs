using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class ServiceTemplate : ModelBase
    {
        public string Name { get; set; }

        public string Url { get; set; }
        
        public string JsonFields { get; set; }
        
        public virtual ICollection<ServiceTicket> Tickets
        {
            get;
            set;
        }

    }
}
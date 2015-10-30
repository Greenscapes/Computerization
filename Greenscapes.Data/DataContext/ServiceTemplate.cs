using System.Collections.Generic;

namespace Greenscapes.Data.DataContext
{
    public class ServiceTemplate 
    {
        public int Id { get; set; }

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
using System.Collections.Generic;

namespace CMS.Models
{
    public class PropertyTaskListType : TypeBase
    {
        public PropertyTaskListType()
        {
            PropertyTaskHeaders = new List<PropertyTaskHeader>();
        }

        public virtual ICollection<PropertyTaskHeader> PropertyTaskHeaders
        {
            get;
            set;
        }
    }
}
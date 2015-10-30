using System.Collections.Generic;

namespace Greenscapes.Data.Models
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
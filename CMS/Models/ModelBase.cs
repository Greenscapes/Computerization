using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CMS.Models
{
    public abstract class ModelBase
    {
        protected ModelBase()
        {
            CreatedDateTime = LastUpdatedDateTime = DateTime.UtcNow;
        }

        public int Id
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public DateTime LastUpdatedDateTime
        {
            get;
            set;
        }
    }
}
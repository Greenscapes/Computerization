using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Greenscapes.Data.DataContext
{
    public partial class Property
    {
        [NotMapped]
        public int TaskListId { get; set; }
    }
   
}
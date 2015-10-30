using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greenscapes.Data.DataContext
{
    public partial class Employee
    {      
        [NotMapped]
        public bool InCrew { get; set; }
    }
}
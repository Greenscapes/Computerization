using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Suffix { get; set; }

        public Boolean RequiresAccess { get; set; }

        public string AccessDetails { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class ServiceTemplateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public string JsonFields { get; set; }
    }
}
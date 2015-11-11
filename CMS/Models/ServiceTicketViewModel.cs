using Greenscapes.Data.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class ServiceTicketViewModel
    {
        public int Id { get; set; }

        public string TemplateName { get; set; }

        public string TemplateUrl { get; set; }

        public string PropertyName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
        
        public int ServiceTemplateId { get; set; }

        public int EventTaskListId { get; set; }

        public DateTime EventDate { get; set; }

        public string ReferenceNumber { get; set; }

        public DateTime? VisitFromTime { get; set; }

        public DateTime? VisitToTime { get; set; }

        public string JsonFields { get; set; }

        public int? ApprovedById { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string Notes { get; set; }

        public int? Condition { get; set; }

        public List<ServiceMemberViewModel> Members { get; set; }
    }
}
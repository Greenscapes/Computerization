using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class ManagerViewModel
    {
        public List<UnapprovedServiceTicketViewModel> UnapprovedTickets { get; set; }
        public List<UnapprovedServiceTicketViewModel> ApprovedTickets { get; set; }
    }

    public class UnapprovedServiceTicketViewModel
    {
        public int EventTaskListId { get; set; }
        public string CrewName { get; set; }
        public string PropertyAddress { get; set; }
        public string Title { get; set; }
        public DateTime? TaskStartTime { get; set; }
        public DateTime? TaskEndTime { get; set; }
        public DateTime EventDate { get; set; }
    }
}
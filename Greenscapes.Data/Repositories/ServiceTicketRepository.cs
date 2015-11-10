using System;
using System.Collections.Generic;
using System.Linq;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class ServiceTicketRepository : IServiceTicketRepository
    {
        public CmsContext db = new CmsContext();

        public ServiceTicket GetServiceTicket(int id)
        {
            ServiceTicket serviceTicket = db.ServiceTickets.FirstOrDefault(s => s.Id == id);
            if (serviceTicket == null)
            {
                return null;
            }
            return serviceTicket;
        }

        public ServiceTicket GetServiceTicket(int eventTaskListId, DateTime eventDate)
        {
            ServiceTicket serviceTicket = db.ServiceTickets.FirstOrDefault(s => s.EventTaskListId == eventTaskListId && s.EventDate == eventDate);
            if (serviceTicket == null)
            {
                return null;
            }
            return serviceTicket;
        }
        
        public bool UpdateServiceTicket(ServiceTicket serviceTicket)
        {
            var existingTicket = db.ServiceTickets.FirstOrDefault(p => p.Id == serviceTicket.Id);
            if (existingTicket != null)
            {
                existingTicket.ServiceTemplateId = serviceTicket.ServiceTemplateId;
                existingTicket.EventTaskListId = serviceTicket.EventTaskListId;
                existingTicket.EventDate = serviceTicket.EventDate;
                existingTicket.ReferenceNumber = serviceTicket.ReferenceNumber;
                existingTicket.VisitFromTime = serviceTicket.VisitFromTime.Value.ToLocalTime();
                existingTicket.VisitToTime = serviceTicket.VisitToTime.Value.ToLocalTime();
                existingTicket.JsonFields = serviceTicket.JsonFields;
                existingTicket.ApprovedById = serviceTicket.ApprovedById;
                existingTicket.ApprovedDate = serviceTicket.ApprovedDate;
                existingTicket.Condition = serviceTicket.Condition;
                existingTicket.Notes = serviceTicket.Notes;
            }
            else
            {
                db.ServiceTickets.Add(serviceTicket);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteServiceTicket(int id)
        {
            var serviceTicket = db.ServiceTickets.FirstOrDefault(p => p.Id == id);
            if (serviceTicket == null)
                return false;

            db.ServiceTickets.Remove(serviceTicket);
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

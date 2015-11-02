using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface IServiceTicketRepository : IDisposable
    {
        ServiceTicket GetServiceTicket(int id);
        ServiceTicket GetServiceTicket(int eventTaskListId, DateTime eventDate);
        bool UpdateServiceTicket(ServiceTicket serviceTicket);
        bool DeleteServiceTicket(int id);
    }
}

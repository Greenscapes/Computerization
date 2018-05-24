using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        Customer GetCustomer(int id);
        List<Customer> GetCustomers();
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CmsContext db = new CmsContext();

        public Customer GetCustomer(int id)
        {
            Customer customer = db.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public List<Customer> GetCustomers()
        {
            return db.Customers.ToList();
        }

        public bool UpdateCustomer(Customer customer)
        {
            var existingCustomer = db.Customers.FirstOrDefault(p => p.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Title = customer.Title;
                existingCustomer.Suffix = customer.Suffix;
                existingCustomer.AccessDetails = customer.AccessDetails;
                existingCustomer.RequiresAccess = customer.RequiresAccess;
            }
            else
            {
                db.Customers.Add(customer);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteCustomer(int id)
        {
            var customer = db.Customers.FirstOrDefault(p => p.Id == id);
            if (customer == null)
                return false;

            db.Customers.Remove(customer);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
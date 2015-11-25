using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    internal interface IEmployeeRepository : IDisposable
    {
        Employee GetEmployee(int id);
        List<Employee> GetEmployees();
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }
}
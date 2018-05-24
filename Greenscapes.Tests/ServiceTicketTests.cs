using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Greenscapes.Data.Repositories;

namespace Greenscapes.Tests
{
    [TestClass]
    public class ServiceTicketTests
    {
        [TestMethod]
        public void ServiceTicket_TestGet()
        {
            var repo = new ServiceTicketRepository();
            DateTime start = DateTime.Now;
            var serviceTicket = repo.GetServiceTicket(1);
            TimeSpan executionTime = DateTime.Now - start;
            Console.WriteLine("Took {0} miliseconds to execute!", executionTime);
            Assert.IsNotNull(serviceTicket);
            //Console.WriteLine(serviceTicket.ServiceTemplate.Name);
            //Console.WriteLine("PropertyTask: {0}", serviceTicket.PropertyTask);
            //Console.WriteLine(serviceTicket.ServiceTemplate.Url);
            Console.WriteLine(serviceTicket.ReferenceNumber);
            Console.WriteLine(serviceTicket.Notes);

            Assert.IsTrue(true);
        }
    }
}

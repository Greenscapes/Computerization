using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Greenscapes.Data.Repositories;

namespace Greenscapes.Tests
{
    [TestClass]
    public class TestServiceTemplates
    {
        [TestMethod]
        public void TestGet()
        {
            var repo = new ServiceTemplateRepository();
            var serviceTemplate = repo.GetServiceTemplate(1);

            DateTime start = DateTime.Now;
            serviceTemplate = repo.GetServiceTemplate(2);
            TimeSpan executionTime = DateTime.Now - start;
            Console.WriteLine("Took {0} miliseconds to execute!", executionTime);
            Assert.IsNotNull(serviceTemplate);
            Console.WriteLine(serviceTemplate.Name);
            Console.WriteLine(serviceTemplate.Url);
            Console.WriteLine(serviceTemplate.JsonFields);

            Assert.IsTrue(true);
        }
    }
}

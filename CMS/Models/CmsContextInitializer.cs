using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Entity;

namespace CMS.Models
{
    //public class CmsContextInitializer : DropCreateDatabaseAlways<CmsContext>
    public class CmsContextInitializer : DropCreateDatabaseIfModelChanges<CmsContext>
    {
        protected override void Seed(CmsContext context)
        {
            var properties = new List<Property>
            {
                new Property
                {
                    Name = "WMOA",
                    Address1 = "123 Fake St.",
                    Address2 = "STE 210",
                    City = "Townsville",
                    State = "Illinois",
                    Zip = "11111",
                    PropertyRefNumber ="RRSRS22323",
                    ContractDate = DateTime.Now
                }
            };

            properties.ForEach(p => context.Properties.Add(p));
            context.SaveChanges();

            var taskListTypes = new List<PropertyTaskListType>
            {
                new PropertyTaskListType
                {
                    Name = "Fertilization",
                    Description = "Fertilization task list"
                }
            };

            taskListTypes.ForEach(p => context.PropertyTaskListTypes.Add(p));
            context.SaveChanges();

            var propertyTaskHeaders = new List<PropertyTaskHeader>
            {
                new PropertyTaskHeader {Name = "Type", PropertyTaskListType = taskListTypes[0]},
                new PropertyTaskHeader {Name = "Treatment", PropertyTaskListType = taskListTypes[0]},
                new PropertyTaskHeader {Name = "Product", PropertyTaskListType = taskListTypes[0]}
            };

            propertyTaskHeaders.ForEach(p => context.PropertyTaskHeaders.Add(p));
            context.SaveChanges();

            var taskLists = new List<PropertyTaskList>
            {
                new PropertyTaskList
                {
                    PropertyTaskListType = taskListTypes[0],
                    Property = properties[0],
                    Name = "Fertilization"
                }
            };

            taskLists.ForEach(t => context.PropertyTaskLists.Add(t));
            context.SaveChanges();

            var propertyTasksCrs = new List<PropertyTask>
            {
                new PropertyTask
                {
                    Location = "444 GG",
                    EstimatedDuration = 30,
                    PropertyTaskList = taskLists[0]
                }
            };
            var crewTypep = new CrewType
            {
                Name = "Mowing",
                Description = "Mowing"
            };

            context.CrewTypes.Add(crewTypep);
            context.SaveChanges();

            var crewp = new Crew
            {
                CrewType = crewTypep,
                Name = "Morning Mowing"
            };
            foreach (var p in  propertyTasksCrs)
            {
                p.Crews.Add(crewp);
                context.PropertyTasks.Add(p);
            }
          
            context.SaveChanges();

            var propertyTasks = new List<PropertyTask>
            {
                new PropertyTask
                {
                    Location = "1447 Gleneagles",
                    EstimatedDuration = 30,
                    PropertyTaskList = taskLists[0]
                }
            };

            propertyTasks.ForEach(p => context.PropertyTasks.Add(p));
            context.SaveChanges();
            var propertyTaskDetails = new List<PropertyTaskDetail>
            {
                new PropertyTaskDetail
                {
                    PropertyTask = propertyTasks[0],
                    PropertyTaskHeader = propertyTaskHeaders[0],
                    Value = "St. Augustine Fertilization"
                },
                new PropertyTaskDetail
                {
                    PropertyTask = propertyTasks[0],
                    PropertyTaskHeader = propertyTaskHeaders[1],
                    Value = "Spot"
                },
                new PropertyTaskDetail
                {
                    PropertyTask = propertyTasks[0],
                    PropertyTaskHeader = propertyTaskHeaders[2],
                    Value = "18-0-6 Atrazine and Surfactant"
                }
            };

            propertyTaskDetails.ForEach(p => context.PropertyTaskDetails.Add(p));
            context.SaveChanges();

            var crewType = new CrewType
            {
                Name = "Mowing",
                Description = "Mowing"
            };

            context.CrewTypes.Add(crewType);
            context.SaveChanges();

            var crew = new Crew
            {
                CrewType = crewType,
                Name = "Morning Mowing"
            };

            context.Crews.Add(crew);
            context.SaveChanges();
           
            var ev = new EventSchedule
           {
                 Property = new Property
                {
                    Name = "QQQQ",
                    Address1 = "123 Fake St.",
                    Address2 = "STE 210",
                    City = "Townsville",
                    State = "Illinois",
                    Zip = "11111",
                    PropertyRefNumber ="RRSRS22323",
                    ContractDate = DateTime.Now
                },
                StartTime = DateTime.Today,
                EndTime = DateTime.Now,
                Title = "Online Meeting"
                

           };

            context.EventSchedules.Add(ev);
            context.SaveChanges();
        }
    }
}
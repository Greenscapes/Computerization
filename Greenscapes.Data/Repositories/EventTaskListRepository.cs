using System;
using System.Collections.Generic;
using System.Linq;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class EventTaskListRepository : IEventTaskListRepository
    {
        public CmsContext db = new CmsContext();

        public EventTaskList GetEventTaskList(int id)
        {
            EventTaskList eventTaskList = db.EventTaskLists.Include("PropertyTasks").Include("Crew").FirstOrDefault(p => p.Id == id);

            return eventTaskList;
        }

        public List<EventTaskList> GetEventTaskLists(int propertyId, int taskId)
        {
            var eventTasks = new List<EventTaskList>();
            var task = db.PropertyTasks.Include("Crews").FirstOrDefault(p => p.Id == taskId);
            if (task != null)
            {
                foreach (var crew in task.Crews)
                {
                    eventTasks.AddRange(db.EventTaskLists.Where(p => p.PropertyId == propertyId && p.CrewId == crew.Id).ToList());
                }
            }

            return eventTasks;
        }

        public List<EventTaskList> GetEventTaskLists()
        {
            var eventTasks = db.EventTaskLists.Where(e => e.Property.PropertyType != 3);

            return eventTasks.ToList();
        }

        public EventTaskList UpdateEventTaskList(EventTaskList taskList)
        {
            var existingTaskList = db.EventTaskLists.FirstOrDefault(p => p.Id == taskList.Id);
            if (existingTaskList != null)
            {
                existingTaskList.Name = taskList.Name;
                existingTaskList.CrewId = taskList.CrewId;
                existingTaskList.ServiceTemplateId = taskList.ServiceTemplateId;
                existingTaskList.PropertyServiceId = taskList.PropertyServiceId;
                existingTaskList.NumberServices = taskList.NumberServices;
            }
            else
            { 
                db.EventTaskLists.Add(taskList);
            }

            db.SaveChanges();

            return taskList;
        }

        public bool DeleteEventTaskList(int id)
        {
            var taskList = db.EventTaskLists.FirstOrDefault(p => p.Id == id);
            if (taskList == null)
                return false;

            db.EventTaskLists.Remove(taskList);
            db.SaveChanges();
            return true;
        }

        public List<EventTaskList> GetEventTaskListsForCrew(int crewId)
        {
            return db.EventTaskLists.Where(e => e.CrewId == crewId).ToList();
        }

        public List<EventTaskList> GetPropertyEventTaskLists(int propertyId)
        {
            return db.EventTaskLists.Where(e => e.PropertyId == propertyId).ToList();
        }

        public void SetStartTime(int id, DateTime start)
        {
            var eventTaskListTime =
                db.EventTaskTimes.FirstOrDefault(e => e.EventDate == DateTime.Today && e.EventTaskListId == id) ??
                new EventTaskTime();
            eventTaskListTime.EventDate = DateTime.Today;
            eventTaskListTime.StartTime = start;
            eventTaskListTime.EventTaskListId = id;
            if (eventTaskListTime.Id == 0)
                db.EventTaskTimes.Add(eventTaskListTime);

            db.SaveChanges();
        }

        public void SetFinishTime(int id, DateTime finish)
        {
            var eventTaskListTime =
                db.EventTaskTimes.FirstOrDefault(e => e.EventDate == DateTime.Today && e.EventTaskListId == id) ??
                new EventTaskTime();
            eventTaskListTime.EventDate = DateTime.Today;
            eventTaskListTime.EndTime = finish;
            eventTaskListTime.EventTaskListId = id;
            if (eventTaskListTime.Id == 0)
                db.EventTaskTimes.Add(eventTaskListTime);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}



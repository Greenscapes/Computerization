using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class PropertyTaskRepository : IPropertyTaskRepository
    {
        public CmsContext db = new CmsContext();

        public PropertyTask GetPropertyTask(int id)
        {
            PropertyTask propertyTask = db.PropertyTasks.Include("Crews").FirstOrDefault(p => p.Id == id);
            if (propertyTask == null)
            {
                return null;
            }

            return propertyTask;
        }

        public List<PropertyTask> GetPropertyTasksForTaskList(int propertyTaskListId)
        {
            return db.PropertyTasks.Where(p => p.PropertyTaskListId == propertyTaskListId).ToList();
        }

        public List<PropertyTask> GetPropertyTasksForProperty(int propertyId)
        {
            var propertyTasks = new List<PropertyTask>();
            var propertyTaskLists = db.PropertyTaskLists.Include("PropertyTasks").Include("PropertyTasks.Crews").Include("PropertyTasks.EventTaskList").Where(p => p.PropertyId == propertyId);

            foreach (var taskList in propertyTaskLists)
            {
                propertyTasks.AddRange(taskList.PropertyTasks);
            }

            return propertyTasks;
        }

        public bool UpdatePropertyTask(PropertyTask propertyTask)
        {
            var existingTask = db.PropertyTasks.Include("Crews").FirstOrDefault(p => p.Id == propertyTask.Id);
            if (existingTask != null)
            {
                existingTask.Description = propertyTask.Description;
                existingTask.EstimatedDuration = propertyTask.EstimatedDuration;
                existingTask.EventTaskListId = propertyTask.EventTaskListId;
                existingTask.IsFreeService = propertyTask.IsFreeService;
                existingTask.Location = propertyTask.Location;
                existingTask.Notes = propertyTask.Notes;
                existingTask.EventTaskListId = propertyTask.EventTaskListId;

                List<Crew> crews = new List<Crew>();

                foreach (Crew cr in propertyTask.Crews)
                {
                    crews.Add(db.Crews.Single(c => c.Id == cr.Id));
                }
               
                existingTask.Crews = new List<Crew>();
                db.SaveChanges();

                existingTask.Crews = crews;
            }
            else
            {
                db.PropertyTasks.Add(propertyTask);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeletePropertyTask(int id)
        {
            var property = db.Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
                return false;

            db.Properties.Remove(property);
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}


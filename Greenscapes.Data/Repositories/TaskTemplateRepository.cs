using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;
using Greenscapes.Data.Repositories.Interfaces;

namespace Greenscapes.Data.Repositories
{
    public class TaskTemplateRepository : ITaskTemplateRepository
    {
        public CmsContext db = new CmsContext();

        public TaskTemplate GetTaskTemplate(int id)
        {
            TaskTemplate tasktemplate = db.TaskTemplates.FirstOrDefault(p => p.Id == id);
            if (tasktemplate == null)
            {
                return null;
            }

            return tasktemplate;
        }

        public List<TaskTemplate> GetTaskTemplates()
        {
            return db.TaskTemplates.ToList();
        }

        public bool UpdateTaskTemplate(TaskTemplate taskTemplate)
        {
            var existingTask = db.TaskTemplates.FirstOrDefault(p => p.Id == taskTemplate.Id);
            if (existingTask != null)
            {
                existingTask.Description = taskTemplate.Description;
                existingTask.EstimatedDuration = taskTemplate.EstimatedDuration;
                existingTask.IsFreeService = taskTemplate.IsFreeService;
                existingTask.Notes = taskTemplate.Notes;

                db.SaveChanges();
            }
            else
            {
                db.TaskTemplates.Add(taskTemplate);
            }

            db.SaveChanges();

            return true;
        }

        public bool DeleteTaskTemplate(int id)
        {
            var taskTemplate = db.TaskTemplates.FirstOrDefault(p => p.Id == id);
            if (taskTemplate == null)
                return false;

            db.TaskTemplates.Remove(taskTemplate);
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}


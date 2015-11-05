using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greenscapes.Data.DataContext;

namespace Greenscapes.Data.Repositories.Interfaces
{
    public interface ITaskTemplateRepository : IDisposable
    {
        TaskTemplate GetTaskTemplate(int id);
        List<TaskTemplate> GetTaskTemplates();
        bool UpdateTaskTemplate(TaskTemplate propertyTask);
        bool DeleteTaskTemplate(int id);
    }
}

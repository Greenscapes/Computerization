namespace Greenscapes.Data.Models
{
    public class TaskSchedule : ModelBase
    {
        public int PropertyTaskId
        {
            get;
            set;
        }

        public virtual PropertyTask PropertyTask
        {
            get;
            set;
        }
    }
}
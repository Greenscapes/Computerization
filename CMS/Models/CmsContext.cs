using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMS.Models
{
    public class CmsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CmsContext()
            : base("name=GSCMS_DB")
        {
        }

        public DbSet<Property> Properties { get; set; }
       
        public DbSet<PropertyTask> PropertyTasks { get; set; }

        public DbSet<PropertyTaskHeader> PropertyTaskHeaders { get; set; }

        public DbSet<PropertyTaskDetail> PropertyTaskDetails { get; set; }

        public DbSet<PropertyTaskList> PropertyTaskLists { get; set; }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<CrewMember> CrewMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<CMS.Models.PropertyTaskListType> PropertyTaskListTypes { get; set; }

        public System.Data.Entity.DbSet<CMS.Models.CrewType> CrewTypes { get; set; }

        public System.Data.Entity.DbSet<CMS.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<CMS.Models.WeeklySchedule> WeeklySchedules { get; set; }

        public System.Data.Entity.DbSet<CMS.Models.DailySchedule> DailySchedules { get; set; }
        public DbSet<EventSchedule> EventSchedules { get; set; }
        public DbSet<PropertyTaskEventNote> PropertyTaskEventNotes { get; set; }
      

    }
}

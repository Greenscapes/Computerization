namespace Greenscapes.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CmsContext : DbContext
    {
        public CmsContext()
            : base("name=CmsContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<CrewMember> CrewMembers { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<CrewType> CrewTypes { get; set; }
        public virtual DbSet<DailySchedule> DailySchedules { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EventSchedule> EventSchedules { get; set; }
        public virtual DbSet<EventTaskList> EventTaskLists { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyTaskDetail> PropertyTaskDetails { get; set; }
        public virtual DbSet<PropertyTaskEventNote> PropertyTaskEventNotes { get; set; }
        public virtual DbSet<PropertyTaskHeader> PropertyTaskHeaders { get; set; }
        public virtual DbSet<PropertyTaskList> PropertyTaskLists { get; set; }
        public virtual DbSet<PropertyTaskListType> PropertyTaskListTypes { get; set; }
        public virtual DbSet<PropertyTask> PropertyTasks { get; set; }
        public virtual DbSet<ServiceTemplate> ServiceTemplates { get; set; }
        public virtual DbSet<ServiceTicket> ServiceTickets { get; set; }
        public virtual DbSet<WeeklySchedule> WeeklySchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .HasMany(e => e.CrewMembers)
                .WithRequired(e => e.Crew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Crew>()
                .HasMany(e => e.EventTaskLists)
                .WithRequired(e => e.Crew)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Crew>()
                .HasMany(e => e.PropertyTasks)
                .WithMany(e => e.Crews)
                .Map(m => m.ToTable("PropertyTaskCrews"));

            modelBuilder.Entity<CrewType>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.CrewTypes)
                .Map(m => m.ToTable("CrewTypeEmployees"));

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules)
                .WithRequired(e => e.DailySchedule)
                .HasForeignKey(e => e.FridayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules1)
                .WithRequired(e => e.DailySchedule1)
                .HasForeignKey(e => e.MondayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules2)
                .WithRequired(e => e.DailySchedule2)
                .HasForeignKey(e => e.SaturdayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules3)
                .WithRequired(e => e.DailySchedule3)
                .HasForeignKey(e => e.SundayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules4)
                .WithRequired(e => e.DailySchedule4)
                .HasForeignKey(e => e.ThursdayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules5)
                .WithRequired(e => e.DailySchedule5)
                .HasForeignKey(e => e.TuesdayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailySchedule>()
                .HasMany(e => e.WeeklySchedules6)
                .WithRequired(e => e.DailySchedule6)
                .HasForeignKey(e => e.WednesdayScheduleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CrewMembers)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventSchedule>()
                .HasMany(e => e.PropertyTaskEventNotes)
                .WithRequired(e => e.EventSchedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.EventTaskLists)
                .WithRequired(e => e.Property)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.PropertyTaskLists)
                .WithRequired(e => e.Property)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyTaskHeader>()
                .HasMany(e => e.PropertyTaskDetails)
                .WithRequired(e => e.PropertyTaskHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyTaskList>()
                .HasMany(e => e.PropertyTasks)
                .WithRequired(e => e.PropertyTaskList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyTaskListType>()
                .HasMany(e => e.PropertyTaskHeaders)
                .WithRequired(e => e.PropertyTaskListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyTaskListType>()
                .HasMany(e => e.PropertyTaskLists)
                .WithRequired(e => e.PropertyTaskListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventTaskList>()
                .HasMany(e => e.EventSchedules)
                .WithRequired(e => e.EventTaskList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyTask>()
                .HasMany(e => e.PropertyTaskDetails)
                .WithRequired(e => e.PropertyTask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceTemplate>()
                .HasMany(e => e.ServiceTickets)
                .WithRequired(e => e.ServiceTemplate)
                .WillCascadeOnDelete(false);


        }
    }
}

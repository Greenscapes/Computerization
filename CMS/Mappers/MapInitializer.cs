using AutoMapper;
using CMS.Models;
using Greenscapes.Data.DataContext;

namespace CMS.Mappers
{
    public class MapInitializer
    {
        /// <summary>
		/// Ensures the initialized.
		/// </summary>
		public static void EnsureInitialized()
        {
            new MapInitializer().InitializationStrategy();
        }

        /// <summary>
        /// Initializations the strategy.
        /// </summary>
        protected void InitializationStrategy()
        {
            Mapper.CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.CustomerName, src => src.Ignore())
                .ForMember(dest => dest.CityName, src => src.ResolveUsing(c => c.City.Name));

            Mapper.CreateMap<PropertyViewModel, Property>()
                .ForMember(dest => dest.EventTaskLists, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskLists, src => src.Ignore())
                .ForMember(dest => dest.Customer, src => src.Ignore())
                .ForMember(dest => dest.City, src => src.Ignore());

            Mapper.CreateMap<Customer, CustomerViewModel>();
            Mapper.CreateMap<CustomerViewModel, Customer>()
                .ForMember(dest => dest.Properties, src => src.Ignore());

            Mapper.CreateMap<PropertyTask, PropertyTaskViewModel>()
                .ForMember(dest => dest.ScheduleName,
                    src => src.MapFrom(s => s.EventTaskList != null ? s.EventTaskList.Name : ""))
                .ForMember(dest => dest.Crew, src => src.MapFrom(s => s.EventTaskList.Crew));

            Mapper.CreateMap<PropertyTaskViewModel, PropertyTask>()
                .ForMember(dest => dest.EventTaskList, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskDetails, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskList, src => src.Ignore())
                .ForMember(dest => dest.Crews, src => src.Ignore());

            Mapper.CreateMap<Crew, CrewViewModel>();
            Mapper.CreateMap<CrewViewModel, Crew>()
                .ForMember(dest => dest.EventTaskLists, src => src.Ignore())
                .ForMember(dest => dest.PropertyTasks, src => src.Ignore());

            Mapper.CreateMap<EventTaskList, EventTaskListViewModel>();
            Mapper.CreateMap<EventTaskListViewModel, EventTaskList>()
                .ForMember(dest => dest.Property, src => src.Ignore())
                .ForMember(dest => dest.Crew, src => src.Ignore())
                .ForMember(dest => dest.PropertyTasks, src => src.Ignore())
                .ForMember(dest => dest.EventSchedules, src => src.Ignore())
                .ForMember(dest => dest.ServiceTemplate, src => src.Ignore())
                .ForMember(dest => dest.EventTaskTimes, src => src.Ignore())
                .ForMember(dest => dest.ServiceTickets, src => src.Ignore());

            Mapper.CreateMap<ServiceTemplate, ServiceTemplateViewModel>();
            Mapper.CreateMap<ServiceTemplateViewModel, ServiceTemplate>()
                .ForMember(dest => dest.ServiceTickets, src => src.Ignore())
                .ForMember(dest => dest.EventTaskLists, src => src.Ignore());

            Mapper.CreateMap<ServiceTicket, ServiceTicketViewModel>()
                .ForMember(dest => dest.TemplateName, src => src.Ignore())
                .ForMember(dest => dest.TemplateUrl, src => src.Ignore())
                .ForMember(dest => dest.TemplateUseTasks, src => src.Ignore())
                .ForMember(dest => dest.PropertyName, src => src.Ignore())
                .ForMember(dest => dest.Address1, src => src.Ignore())
                .ForMember(dest => dest.Address2, src => src.Ignore())
                .ForMember(dest => dest.City, src => src.Ignore())
                .ForMember(dest => dest.State, src => src.Ignore())
                .ForMember(dest => dest.Zip, src => src.Ignore())
                .ForMember(dest => dest.ShowAllEmployees, src => src.Ignore())
                .ForMember(dest => dest.Members, src => src.Ignore())
                .ForMember(dest => dest.CustomerName, src => src.Ignore())
                .ForMember(dest => dest.AccessDetails, src => src.Ignore());

            Mapper.CreateMap<ServiceTicketViewModel, ServiceTicket>()
                .ForMember(dest => dest.EventTaskList, src => src.Ignore())
                .ForMember(dest => dest.ServiceTemplate, src => src.Ignore())
                .ForMember(dest => dest.ServiceMembers, src => src.Ignore())
                .ForMember(dest => dest.Employee, src => src.Ignore());

            Mapper.CreateMap<TaskTemplate, TaskTemplateViewModel>();
            Mapper.CreateMap<TaskTemplateViewModel, TaskTemplate>();

            Mapper.CreateMap<Employee, EmployeeViewModel>();
            Mapper.CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.CrewMembers, src => src.Ignore())
                .ForMember(dest => dest.ServiceTickets, src => src.Ignore())
                .ForMember(dest => dest.ServiceMembers, src => src.Ignore());

            Mapper.CreateMap<EmployeeSkill, EmployeeSkillsViewModel>();
            Mapper.CreateMap<EmployeeSkillsViewModel, EmployeeSkill>()
                .ForMember(dest => dest.Employees, src => src.Ignore());

            Mapper.CreateMap<CrewMember, CrewMemberViewModel>();
            Mapper.CreateMap<CrewMemberViewModel, CrewMember>()
                .ForMember(dest => dest.Employee, src => src.Ignore())
                .ForMember(dest => dest.Crew, src => src.Ignore());

            Mapper.CreateMap<City, CityViewModel>();
            Mapper.CreateMap<CityViewModel, City>()
                .ForMember(dest => dest.Properties, src => src.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}

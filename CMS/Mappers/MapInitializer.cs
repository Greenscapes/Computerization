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
            Mapper.CreateMap<Property, PropertyViewModel>();
            Mapper.CreateMap<PropertyViewModel, Property>()
                .ForMember(dest => dest.EventTaskLists, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskLists, src => src.Ignore());

            Mapper.CreateMap<PropertyTask, PropertyTaskViewModel>()
                .ForMember(dest => dest.ScheduleName,
                    src => src.MapFrom(s => s.EventTaskList != null ? s.EventTaskList.Name : "Schedule Task"));

            Mapper.CreateMap<PropertyTaskViewModel, PropertyTask>()
                .ForMember(dest => dest.EventTaskList, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskDetails, src => src.Ignore())
                .ForMember(dest => dest.PropertyTaskList, src => src.Ignore());

            Mapper.CreateMap<Crew, CrewViewModel>();
            Mapper.CreateMap<CrewViewModel, Crew>()
                .ForMember(dest => dest.CrewMembers, src => src.Ignore())
                .ForMember(dest => dest.EventTaskLists, src => src.Ignore())
                .ForMember(dest => dest.PropertyTasks, src => src.Ignore());

            Mapper.CreateMap<EventTaskList, EventTaskListViewModel>();
            Mapper.CreateMap<EventTaskListViewModel, EventTaskList>()
                .ForMember(dest => dest.Property, src => src.Ignore())
                .ForMember(dest => dest.Crew, src => src.Ignore())
                .ForMember(dest => dest.PropertyTasks, src => src.Ignore())
                .ForMember(dest => dest.EventSchedules, src => src.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}

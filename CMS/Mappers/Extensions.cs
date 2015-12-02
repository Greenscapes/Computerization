using System.Collections.Generic;
using AutoMapper;
using CMS.Models;
using Greenscapes.Data.DataContext;

namespace CMS.Mappers
{
    public static class Extensions
    {
        static Extensions()
        {
            MapInitializer.EnsureInitialized();
        }

        public static TTarget MapTo<TTarget>(this Property source) where TTarget : PropertyViewModel
        {
            return Mapper.Map<Property, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this PropertyViewModel source) where TTarget : Property
        {
            return Mapper.Map<PropertyViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<Property> source)
            where TTarget : IEnumerable<PropertyViewModel>
        {
            return Mapper.Map<IEnumerable<Property>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this PropertyTask source) where TTarget : PropertyTaskViewModel
        {
            return Mapper.Map<PropertyTask, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this PropertyTaskViewModel source) where TTarget : PropertyTask
        {
            return Mapper.Map<PropertyTaskViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<PropertyTask> source)
            where TTarget : IEnumerable<PropertyTaskViewModel>
        {
            return Mapper.Map<IEnumerable<PropertyTask>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this EventTaskList source) where TTarget : EventTaskListViewModel
        {
            return Mapper.Map<EventTaskList, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this EventTaskListViewModel source) where TTarget : EventTaskList
        {
            return Mapper.Map<EventTaskListViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<EventTaskList> source)
            where TTarget : IEnumerable<EventTaskListViewModel>
        {
            return Mapper.Map<IEnumerable<EventTaskList>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this ServiceTemplate source) where TTarget : ServiceTemplateViewModel
        {
            return Mapper.Map<ServiceTemplate, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this ServiceTemplateViewModel source) where TTarget : ServiceTemplate
        {
            return Mapper.Map<ServiceTemplateViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<ServiceTemplate> source)
            where TTarget : IEnumerable<ServiceTemplateViewModel>
        {
            return Mapper.Map<IEnumerable<ServiceTemplate>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this ServiceTicket source) where TTarget : ServiceTicketViewModel
        {
            return Mapper.Map<ServiceTicket, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this ServiceTicketViewModel source) where TTarget : ServiceTicket
        {
            return Mapper.Map<ServiceTicketViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<ServiceTicket> source)
            where TTarget : IEnumerable<ServiceTicketViewModel>
        {
            return Mapper.Map<IEnumerable<ServiceTicket>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this TaskTemplate source) where TTarget : TaskTemplateViewModel
        {
            return Mapper.Map<TaskTemplate, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this TaskTemplateViewModel source) where TTarget : TaskTemplate
        {
            return Mapper.Map<TaskTemplateViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<TaskTemplate> source)
            where TTarget : IEnumerable<TaskTemplateViewModel>
        {
            return Mapper.Map<IEnumerable<TaskTemplate>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this Employee source) where TTarget : EmployeeViewModel
        {
            return Mapper.Map<Employee, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this EmployeeViewModel source) where TTarget : Employee
        {
            return Mapper.Map<EmployeeViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<Employee> source)
            where TTarget : IEnumerable<EmployeeViewModel>
        {
            return Mapper.Map<IEnumerable<Employee>, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this Crew source) where TTarget : CrewViewModel
        {
            return Mapper.Map<Crew, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this CrewViewModel source) where TTarget : Crew
        {
            return Mapper.Map<CrewViewModel, TTarget>(source);
        }

        public static TTarget MapTo<TTarget>(this IEnumerable<Crew> source)
            where TTarget : IEnumerable<CrewViewModel>
        {
            return Mapper.Map<IEnumerable<Crew>, TTarget>(source);
        }
    }
}
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

        public static TTarget MapTo<TTarget>(this IEnumerable<Property> source) where TTarget : IEnumerable<PropertyViewModel>
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

        public static TTarget MapTo<TTarget>(this IEnumerable<PropertyTask> source) where TTarget : IEnumerable<PropertyTaskViewModel>
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

        public static TTarget MapTo<TTarget>(this IEnumerable<EventTaskList> source) where TTarget : IEnumerable<EventTaskListViewModel>
        {
            return Mapper.Map<IEnumerable<EventTaskList>, TTarget>(source);
        }
    }
}

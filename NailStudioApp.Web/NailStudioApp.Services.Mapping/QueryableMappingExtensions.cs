using AutoMapper.Configuration.Conventions;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Mapping
{
    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] numbersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, numbersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, object parametars)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parametars);
        }
        //public static IQueryable<TDestination> To<TDestination>(this IQueryable source, object parameters = null)
        //{
        //    if (source == null)
        //    {
        //        throw new ArgumentNullException(nameof(source));
        //    }
        //    return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        //}
    }
}

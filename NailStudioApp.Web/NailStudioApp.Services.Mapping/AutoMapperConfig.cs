using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Mapping
{
    using System.Reflection;
    using AutoMapper;
    using AutoMapper.Configuration.Conventions;
    //using NailStudioApp.Services.Mapping.Mapping;

    public class AutoMapperConfig
    {
        private static bool initialized;
        public static IMapper MapperInstance { set; get; } = null!;
        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }
            initialized = true;
            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToList();

            var config = new MapperConfigurationExpression();
            config.CreateProfile(
                "ReflectionProfile",
                configuration =>
                {
                    foreach (var map in GetFromMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    foreach (var map in GetToMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    foreach (var map in GetCustomMappings(types))
                    {
                        map.CreateMappings(configuration);
                    }
                });
                MapperInstance = new Mapper(new MapperConfiguration(config));
          }
        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            var fromMaps = from t in types
                           from i in t.GetTypeInfo().GetInterfaces()
                           where i.GetTypeInfo().IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                 !t.GetTypeInfo().IsAbstract &&
                                 !i.GetTypeInfo().IsInterface
                           select new TypesMap
                           {
                               Source = i.GetTypeInfo().GetGenericArguments()[0],
                               Destination = t,
                           };
            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            var toMaps = from t in types
                         from i in t.GetTypeInfo().GetInterfaces()
                         where i.GetTypeInfo().IsGenericType &&
                               i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                               !t.GetTypeInfo().IsAbstract &&
                               !i.GetTypeInfo().IsInterface
                         select new TypesMap
                         {
                             Source = t,
                             Destination = i.GetTypeInfo().GetGenericArguments()[0],
                         };
            return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types) 
        { 
            var custoMaps= from t in types
                           from i in t.GetTypeInfo().GetInterfaces()
                           where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t)&&
                                 !t.GetTypeInfo().IsAbstract &&
                                 !t.GetTypeInfo().IsInterface
                           select (IHaveCustomMappings)Activator.CreateInstance(t);
            return custoMaps;
        }

        private class TypesMap
        {
            public Type Source { get; set; }
            public Type Destination { get; set; }
        }


                
        
    }
}

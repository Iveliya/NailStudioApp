using Microsoft.Extensions.DependencyInjection;
using NailStudio.Data.Models;
using NailStudio.Data.Repository;
using NailStudio.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.Infrastruction.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services,Assembly modelAssembly)
        {
            Type[] typesToExclude = new Type[] { typeof(User) };
            Type[] modelTypes = modelAssembly
                .GetTypes()
                .Where(t=>!t.IsAbstract && !t.IsInterface &&
                          !t.Name.ToLower().EndsWith("attribute"))
                .ToArray();
            foreach (Type type in modelTypes) 
            {
                if (!typesToExclude.Contains(type))
                {
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType=typeof(BaseRepository<,>);
                    PropertyInfo idPropInfo = type
                        .GetProperties()
                        .Where(p => p.Name.ToLower() == "id")
                        .SingleOrDefault();
                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;
                    if (idPropInfo == null)
                    {
                        constructArgs[1] = repositoryInterface;
                    }
                    else
                    {
                        constructArgs[1] = idPropInfo.PropertyType;
                    }
                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);
                    services.AddScoped(repositoryInterface,repositoryInstanceType);
                }
            }
        }
    }
}

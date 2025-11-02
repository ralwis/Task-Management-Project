using System.Reflection;
using TaskManagement.Application.Contracts.Base;

namespace TaskManagement.API.Extentions
{
    public static class ServiceDIExtention
    {
        private static IServiceCollection servicesList;
        public static IServiceCollection AddServiceImplementation(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                List<TypeInfo> classTypes = assembly.ExportedTypes.Select(t => IntrospectionExtensions.GetTypeInfo(t)).Where(t => t.IsClass && !t.IsAbstract).ToList();

                foreach (var type in classTypes)
                {
                    if (type.GetInterface(typeof(IBaseService).Name) != null)
                    {
                        var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                        foreach (var interfaceType in interfaces.Where(_ => _ != typeof(IBaseService)))
                        {
                            if(services.Any(_ => _.ServiceType == interfaceType))
                            {
                                services.Remove(services.First(_ => _.ServiceType == interfaceType));
                            }
                            services.AddScoped(interfaceType.AsType(), type.AsType());
                        }
                    } 
                }

            }

            servicesList = services;

            return services;
        }
    }
}

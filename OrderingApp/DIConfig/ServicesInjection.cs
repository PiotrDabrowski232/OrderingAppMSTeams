using System.Reflection;
using AutoMapper; 

namespace OrderingApp.DIConfig
{
    public static class ServicesInjection
    {

        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            var assemblies = Assembly.Load("OrderingApp.Logic");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            services.AddAutoMapper(assemblies);

            return services;
        }
    }
}

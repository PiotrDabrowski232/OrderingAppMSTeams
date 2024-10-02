namespace OrderingApp.DIConfig
{
    public static class ServicesInjection
    {

        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return services;
        }
    }
}

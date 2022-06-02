using Jobsity.Application.Events;
using MassTransit;

namespace Jobsity.API.Configurations
{
    public static class MassTransitConfig
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
                {
                    c.Host(configuration.GetConnectionString("RabbitMq"));
                    c.ConfigureEndpoints(context);
                }));

                x.AddRequestClient<StockConsumer>();
            });

            services.AddMassTransitHostedService();
        }
    }
}

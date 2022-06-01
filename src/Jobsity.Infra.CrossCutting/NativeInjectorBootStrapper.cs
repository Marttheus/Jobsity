using Jobsity.Application.Interfaces;
using Jobsity.Application.Services;
using Jobsity.Domain.Interfaces;
using Jobsity.Infra.Data.Context;
using Jobsity.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Jobsity.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();

            // Infra - Data
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<DataContext>();
        }
    }
}

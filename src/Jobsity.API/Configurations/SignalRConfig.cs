using Jobsity.Application.Hubs;

namespace Jobsity.API.Configurations
{
    public static class SignalRConfig
    {
        public static IApplicationBuilder AddChatEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>($"/{nameof(ChatHub)}");
            });

            return app;
        }
    }
}

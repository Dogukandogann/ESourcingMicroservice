using ESourcing.Order.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace ESourcing.Order.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusOrderCreateConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();
            var lifeTime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            lifeTime.ApplicationStarted.Register(OnStarted);
            lifeTime.ApplicationStarted.Register(OnStopping);

            return app;
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }
    }
}

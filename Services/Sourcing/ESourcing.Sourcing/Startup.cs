using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Repository;
using ESourcing.Sourcing.Repository.Interfaces;
using ESourcing.Sourcing.Settings;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Concrete;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Sourcing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.Configure<SourcingDbSettings>(Configuration.GetSection(nameof(SourcingDbSettings)));
            services.AddSingleton<ISourcingDbSettings>(sp => sp.GetRequiredService<IOptions<SourcingDbSettings>>().Value);
            services.AddTransient<ISourcingContext, SourcingContext>();
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IBidRepository, BidRepository>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(s=>
            {
                s.SwaggerDoc("v1",new OpenApiInfo { Title="ESourcing.Sourcing",Version="v1"});
            });

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory() { HostName = Configuration["EventBus:HostName"] };
                var retryCount = 5;
                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:UserName"]) && !string.IsNullOrWhiteSpace(Configuration["EventBus:Password"]) && !string.IsNullOrWhiteSpace(Configuration["EventBus:RetryCount"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                    factory.Password = Configuration["EventBus:Password"];
                    retryCount = int.Parse(Configuration["EventBus:RetryCount"]);
                }
                
                return new DefaultRabbitMQPersistentConnection(factory,retryCount,logger);
            });
            services.AddSingleton<EventBusRabbitMQProducer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sourcing API V1");
            });
        }
    }
}

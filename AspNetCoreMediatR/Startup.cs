using AspNetCoreMediatR.MassTransit;
using GreenPipes;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCoreMediatR
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMediatR();
            services.AddSignalR();

            services.AddScoped<SendMessageConsumer>();

            services.AddMassTransit(c =>
            {
                c.AddConsumer<SendMessageConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
              cfg =>
              {
                  var host = cfg.Host("localhost", "/", h => { });

                  cfg.ReceiveEndpoint(host, "web-service-endpoint", e =>
                  {
                      e.PrefetchCount = 16;
                      e.UseMessageRetry(x => x.Interval(2, 100));

                      e.LoadFrom(provider);

                      EndpointConvention.Map<SendMessageConsumer>(e.InputAddress);
                  });
              }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();



        }
    }
}

using System;
using DealerBear.Adaptor;
using DealerBear.Adaptor.Interface;
using DealerBear.Messages;
using GameBear.Consumers;
using GameBear.Gateways;
using GameBear.Gateways.Interface;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using GameBear.UseCases.SaveGameData;
using GameBear.UseCases.SaveGameData.Interface;
using GameBear.UseCases.SaveNewGameData;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameBear
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddUseCases(services);


            AddConsumers(services);

            string rabbitMQHost = $"rabbitmq://{Environment.GetEnvironmentVariable("RABBITMQ_HOST")}";

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                IRabbitMqHost host = cfg.Host(new Uri(rabbitMQHost), h =>
                {
                    h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"));
                    h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"));
                });

                SetEndPoints(cfg, host, provider);
            }));

            AddGateways(services);
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            AddRequestClients(services);
            AddAdaptors(services);
            
            services.AddSingleton<IHostedService, BusService>();
        }

        private static void SetEndPoints(IRabbitMqBusFactoryConfigurator cfg, IRabbitMqHost host,
            IServiceProvider provider)
        {
            SetEndpointForRequestIsSessionIDInUse(cfg, host, provider);
            SetEndpointForCreateNewGameData(cfg, host, provider);
        }
        
        private static void SetEndpointForCreateNewGameData(IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "CreateNewGameData", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));

                e.Consumer<CreateNewGameDataConsumer>(provider);
                EndpointConvention.Map<ICreateNewGameData>(e.InputAddress);
            });
        }
        

        private static void SetEndpointForRequestIsSessionIDInUse(IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            IServiceProvider provider)
        {
            cfg.ReceiveEndpoint(host, "IsExistingSession", e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, 100));

                e.Consumer<IsExistingSessionConsumer>(provider);
                EndpointConvention.Map<IRequestGameIsSessionIDInUse>(e.InputAddress);
            });
        }

        private static void AddRequestClients(IServiceCollection services)
        {
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<IRequestGameIsSessionIDInUse>());
            services.AddScoped(provider =>
                provider.GetRequiredService<IBus>().CreateRequestClient<ICreateNewGameData>());
        }

        private static void AddConsumers(IServiceCollection services)
        {
            
            services.AddScoped<IsExistingSessionConsumer>();
            services.AddScoped<CreateNewGameDataConsumer>();
            
            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<IsExistingSessionConsumer>();
                x.AddConsumer<CreateNewGameDataConsumer>();
            });
        }

        private static void AddGateways(IServiceCollection services)
        {
            services.AddSingleton<IGameDataGateway, InMemoryGameDataGateway>();
            services.AddSingleton<ISessionIDMessageHistoryGateway, InMemorySessionIDMessageHistoryGateway>();
        }
        private static void AddAdaptors(IServiceCollection services)
        {
            services.AddScoped<IPublishMessageAdaptor, PublishMessageMassTransitAdaptor>();
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IIsGameSessionInProgress, IsGameSessionInProgress>();
            services.AddScoped<CheckMessageHistory, CheckMessageHistory>();
            services.AddScoped<ISaveNewGameData, SaveNewGameData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
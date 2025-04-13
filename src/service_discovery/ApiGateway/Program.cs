using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Enable Eureka Discovery Client
        //builder.Services.AddDiscoveryClient(builder.Configuration);

        //// This configures "discovery only" mode (no registration)
        //builder.Services.Configure<EurekaClientConfig>(cfg =>
        //{
        //    cfg.ShouldRegisterWithEureka = false;
        //    cfg.ShouldFetchRegistry = true;
        //});

        builder.Services
            .AddOcelot()
            .AddEureka()
            .AddSingletonDefinedAggregator<ProductWithReviewsAggregator>();


        var app = builder.Build();

        // Gracefully handle application shutdown to deregister from Eureka
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStopping.Register(() =>
        {
            // Force deregistration from Eureka on application shutdown
            var eurekaClient = app.Services.GetRequiredService<IEurekaClient>();
            eurekaClient.ShutdownAsync();
        });

        await app.UseOcelot();
        app.Run();
    }
}

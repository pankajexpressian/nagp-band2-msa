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

        builder.Services
            .AddOcelot()
            .AddEureka()
            .AddSingletonDefinedAggregator<ProductWithReviewsAggregator>();


        var app = builder.Build();

        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStopping.Register(() =>
        {
            var eurekaClient = app.Services.GetRequiredService<IEurekaClient>();
            eurekaClient.ShutdownAsync();
        });

        await app.UseOcelot();
        app.Run();
    }
}

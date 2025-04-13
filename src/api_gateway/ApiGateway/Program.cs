using Ocelot.DependencyInjection;
using Ocelot.Middleware;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services
            .AddOcelot()
            .AddSingletonDefinedAggregator<ProductWithReviewsAggregator>();

        builder.Logging.AddConsole();

        var app = builder.Build();

        

        await app.UseOcelot();

        app.Run();
    }
}

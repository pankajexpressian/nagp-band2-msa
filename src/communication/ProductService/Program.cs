using MassTransit;
using Steeltoe.Discovery.Client;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDiscoveryClient();

            builder.Services.AddHttpClient("ReviewServiceHttpClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:2002/");
            });




            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.

         


            app.MapControllers();

            app.Run();
        }
    }
}

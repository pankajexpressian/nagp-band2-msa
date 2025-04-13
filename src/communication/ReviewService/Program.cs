using MassTransit;
using Steeltoe.Discovery.Client;

namespace ReviewService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDiscoveryClient();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<ReviewAddedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("review-service-queue", e =>
                    {
                        e.ConfigureConsumer<ReviewAddedConsumer>(context);
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

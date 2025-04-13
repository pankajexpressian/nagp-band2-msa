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


            var app = builder.Build();

            // Configure the HTTP request pipeline.

         


            app.MapControllers();

            app.Run();
        }
    }
}

using KafkaConsumer.Api.Services;
using log4net.Config;
using log4net;
using System.Reflection;

namespace KafkaConsumer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<IHostedService, KafkaConsumerService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen()

            
            var app = builder.Build();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger()
            //    app.UseSwaggerUI()
            //}

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
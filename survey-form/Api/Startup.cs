using Api.Services.Abstract;
using Api.Services;
using Carter;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Newtonsoft.Json;

namespace Api
{
    public class Startup
    {
        public IConfiguration config
        {
            get;
        }
        public Startup()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddEnvironmentVariables()
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCarter();
            services.AddScoped<ISurveyValidatingService, SurveyValidatingService>();
            services.AddScoped<INetworkService, NetworkService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<ITelegramBotClient>(
            x =>
            {
                var bot = config.GetSection("Bot");
                return new TelegramBotClient(bot["token"]);
            });
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    var context = services.GetRequiredService<DataContext>();
            //    if (context.Database.GetPendingMigrations().Any())
            //    {
            //        context.Database.Migrate();
            //    }
            //}

            app.MapControllers();
            app.MapCarter();
            app.Run();
        }
    }
}

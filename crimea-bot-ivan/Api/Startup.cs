using Api.Helpers;
using Api.Repository.Abstract;
using Api.Repository;
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IBotThemeService, BotThemeService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IHandleUpdateService, HandleUpdateService>();
            services.AddScoped<ISurveyValidatingService, SurveyValidatingService>();
            services.AddScoped<INetworkService, NetworkService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("BotContext")));
            services.AddSingleton<ITelegramBotClient>(
            x =>
            {
                var bot = config.GetSection("Bot");
                return new TelegramBotClient(bot["token"]);
            });

            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new DateTimeConverterForCustomStandardFormatR());
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

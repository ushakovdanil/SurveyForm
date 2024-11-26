using Carter;
using Api.Helpers;
using System.Text.Json.Serialization;
using Api.Repository.Abstract;
using Api.Repository;
using Microsoft.EntityFrameworkCore;
using Api.Services.Abstract;
using Api.Services;
using System.Configuration;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup();
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app, builder.Environment); 

        }
    }
}
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BotThemeEntity> BotThemes { get; set; }
        public DbSet<BotMessageEntity> BotMessages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<InlineKeyboardButtonEntity> InlineKeyboardButtons { get; set; }
        public DbSet<LocalizebleResource> LocalizebleResources { get; set; }
        public DbSet<ReplyKeyboardButtonEntity> ReplyKeyboardButtons { get; set;}
        public DbSet<Request> Requests { get; set; }

    }

}

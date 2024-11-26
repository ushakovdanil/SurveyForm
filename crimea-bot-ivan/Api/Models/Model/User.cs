namespace Api.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BotTheme LastTheme { get; set; }
    }
}

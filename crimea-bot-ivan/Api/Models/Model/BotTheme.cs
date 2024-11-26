namespace Api.Models.Domain
{
    public class BotTheme
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public List<BotMessage> BotMessages { get; set; }
    }
}
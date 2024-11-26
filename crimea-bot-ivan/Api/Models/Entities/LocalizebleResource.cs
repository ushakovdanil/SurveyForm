using Api.Models.Entities.Abstract;

namespace Api.Models.Entities
{
    public class LocalizebleResource : BaseEntity
    {
        public Language Language { get; set; }
        public string Value  { get; set; }

        //Required by EF core
        public LocalizebleResource() { }
        public LocalizebleResource(Language language, string value) : base()
        {
            Language = language;
            Value = value;
        }
    }
}
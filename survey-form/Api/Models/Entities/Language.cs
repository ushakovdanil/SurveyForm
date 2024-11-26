using Api.Models.Entities.Abstract;
using System.Globalization;

namespace Api.Models.Entities
{
    public class Language : BaseEntity
    {
        public string Culture { get; set; }
        public string Name { get; set; }


        //Required by EF core
        public Language()
        {
            
        }
        public Language(string culture, string name) : base()
        {
            Culture = culture;
            Name = name;
        }
    }
}
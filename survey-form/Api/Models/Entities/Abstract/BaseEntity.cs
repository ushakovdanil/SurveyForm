
using Api.Helpers;

namespace Api.Models.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();

            CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, Constants.FLETimeZone);
        }
    }
}

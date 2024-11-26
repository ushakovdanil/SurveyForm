namespace Api.Models.Entities.Abstract
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
    }
}

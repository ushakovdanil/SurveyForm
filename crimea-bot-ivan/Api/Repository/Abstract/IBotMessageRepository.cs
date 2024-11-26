using Api.Models.Entities;

namespace Api.Repository.Abstract
{
    public interface IBotMessageRepository
    {
        Task<UserEntity> GetUserById(Guid id);
        Task<UserEntity> GetUserByTelegramId(long id);
        Task<UserEntity> AddUser(UserEntity userEntity);
        Task UpdateUser(UserEntity userEntity);
        Task DeleteUser(Guid id);
    }
}

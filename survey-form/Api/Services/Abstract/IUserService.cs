using Api.Models.Entities;

namespace Api.Services.Abstract
{
	public interface IUserService
	{
        Task<UserEntity> GetUserById(Guid id);
        Task<UserEntity> GetUserByTelegramId(long id);
        Task<bool> IsUserByTelegramIdExist(long id);
        void AddUser(UserEntity userEntity);
        void UpdateUser(UserEntity userEntity);
        void DeleteUser(UserEntity userEntity);
    }
}


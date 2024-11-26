using System;
using Api.Models.Entities;
using Api.Repository.Abstract;
using Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
	public class UserService : IUserService
	{
		private IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
		{
			_userRepository = userRepository;
		}

        public async Task<UserEntity> GetUserById(Guid id)
        {
            return await _userRepository.FilteredList(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserByTelegramId(long id)
        {
            return await _userRepository.FilteredList(x => x.TelegramId == id)
                .Include(x => x.LastTheme)
                .Include(x => x.Language)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserByTelegramIdExist(long id)
        {
            return await GetUserByTelegramId(id) != null;
        }

        public void AddUser(UserEntity userEntity)
        {
            _userRepository.Insert(userEntity);
        }

        public void DeleteUser(UserEntity userEntity)
        {
            _userRepository.Delete(userEntity);
        }

        public void UpdateUser(UserEntity userEntity)
        {
            _userRepository.Update(userEntity);
        }
    }
}


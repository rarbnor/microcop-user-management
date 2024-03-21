
using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            return _mapper.Map<UserModel>(await GetUserById(id));
        }


        public async Task<UserModel> CreateUser(UserCreateModel userModel)
        {
            string pepper = _configuration.GetSection("PasswordHashPepper").Value ?? "Microcop";

            var user = _mapper.Map<User>(userModel);
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.ComputeHash(userModel.Password, user.PasswordSalt, pepper);

            await _userRepository.CreateAsync(user);
            return _mapper.Map<UserModel>(user);
        }


        public async Task<UserModel?> UpdateUser(UserUpdateModel userUpdateModel)
        {
            var user = await GetUserById(userUpdateModel.Id);
            if (user is null) return null;

            var userUpdate = _mapper.Map(userUpdateModel, user);
            await _userRepository.UpdateAsync(userUpdate);
            return _mapper.Map<UserModel>(userUpdate);
        }


        public async Task<UserModel?> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);
            if (user is null) return null;

            await _userRepository.DeleteAsync(user);
            return _mapper.Map<UserModel>(user);
        }


        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetAsync(x => x.Id == id);
        }


        public async Task<string?> GetUserApiKey(string apiKey)
        {
            var user = await _userRepository.GetAsync(user => user.ApiKey.ToString() == apiKey);
            return user?.ApiKey.ToString();
        }
    }
}


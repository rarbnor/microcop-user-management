using System.Linq.Expressions;
using Application.AutoMapper;
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Application.Validations;
using AutoMapper;
using Domain.Entities;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Users
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IGenericRepository<User>> _userRepository = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(m => m.AddProfile<UserMapper>()));
        private readonly Mock<IConfiguration> _configuration = new();


        public UserServiceTests()
        {
            _userService = new UserService(_userRepository.Object, _mapper, _configuration.Object);
        }


        [Fact]
        public async Task Should_Have_Error_When_Email_Is_Not_Correct()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new UserCreateModel() { Email = "Test" }; // Invalid email

            // Act
            var result = await validator.TestValidateAsync(user);

            // Assert
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }


        [Fact]
        public async Task Should_Not_Have_Error_When_Email_Is_Valid()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new UserCreateModel { Email = "test@example.com" }; // Valid email

            // Act
            var result = await validator.TestValidateAsync(user);

            // Assert
            result.ShouldNotHaveValidationErrorFor(u => u.Email);
        }


        [Fact]
        public async Task Should_Have_Error_When_Password_Does_Not_Meet_All_Conditions()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new UserCreateModel { Password = "Test1234" }; // Invalid Password

            // Act
            var result = await validator.TestValidateAsync(user);

            // Assert
            result.ShouldHaveValidationErrorFor(u => u.Password);
        }


        [Fact]
        public async Task Should_Not_Have_Error_When_Password_Meet_All_Conditions()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new UserCreateModel { Password = "Test12!@." }; // Valid Password

            // Act
            var result = await validator.TestValidateAsync(user);

            // Assert
            result.ShouldNotHaveValidationErrorFor(u => u.Password);
        }


        [Fact]
        public async Task Should_Not_Have_Error_When_Compare_Email_Before_And_After_Create_User()
        {
            // Arrange
            var userCreateModel = new UserCreateModel() { Email = "test@test.com", Password = "Test1234" };
            _configuration.Setup(x => x.GetSection("PasswordHashPepper").Value);
            _userRepository.Setup(x => x.CreateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var userModel = await _userService.CreateUser(userCreateModel);

            // Assert
            Assert.Equal(userModel.Email, userCreateModel.Email);
        }


        [Fact]
        public async Task Should_Not_Be_Equal_Object_When_There_Is_Data_And_There_Is_Not()
        {
            var user = new User() { Email = "test@test.com", Username = "test", PasswordHash = "", PasswordSalt = "" }; //with data

            // Arrange
            _userRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);

            // Act
            var userModel = await _userService.GetUser(Guid.NewGuid()); // empty

            // Assert
            Assert.NotEqual(_mapper.Map<User>(userModel), user);
        }
    }
}


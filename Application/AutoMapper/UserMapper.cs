using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<User, UserModel>().ReverseMap();

            CreateMap<User, UserCreateModel>().ReverseMap();

            CreateMap<User, UserUpdateModel>().ReverseMap();
        }
	}
}
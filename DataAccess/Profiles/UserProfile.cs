using AutoMapper;
using DataAccess.DTOs.Categories;
using DataAccess.DTOs.Users;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserDTO, User>();
            CreateMap<CreateUserDTO, User>();
        }
    }
}

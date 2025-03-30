using AutoMapper;
using DataAccess.DTOs.Roles;
using DataAccess.Models;

namespace DataAccess.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<UpdateRoleDTO, Role>();
            CreateMap<CreateRoleDTO, Role>();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using presentationProject.ViewModels;

namespace presentationProject.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<IdentityRole,RoleVm>().ReverseMap();
        }

    }
}

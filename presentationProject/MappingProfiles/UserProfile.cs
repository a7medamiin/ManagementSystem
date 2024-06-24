using AutoMapper;
using DAL.Models;
using presentationProject.ViewModels;

namespace presentationProject.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserVm>().ReverseMap();
        }
    }
}

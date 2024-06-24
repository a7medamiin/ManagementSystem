using AutoMapper;
using DAL.Models;
using presentationProject.ViewModels;

namespace presentationProject.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentVm,Department>().ReverseMap();
        }
    }
}

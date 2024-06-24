using AutoMapper;
using DAL.Models;
using presentationProject.ViewModels;

namespace presentationProject.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeVm, Employee>().ReverseMap()/*.ForMember(d => d.Name, options => options.MapFrom(s=>s.Name))*/;
        }
    }
}

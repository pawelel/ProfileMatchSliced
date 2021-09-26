using AutoMapper;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, EditUserVM>();
            CreateMap<EditUserVM, ApplicationUser>();
            CreateMap<Department, DepartmentVM>();
            CreateMap<DepartmentVM, Department>();
        }
    }
}

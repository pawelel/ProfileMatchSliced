using AutoMapper;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserVM>();
            CreateMap<ApplicationUserVM, ApplicationUser>();
            CreateMap<Department, DepartmentVM>();
            CreateMap<DepartmentVM, Department>();
        }
    }
}

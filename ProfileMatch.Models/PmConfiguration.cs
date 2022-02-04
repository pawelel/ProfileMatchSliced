using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Identity;

using ProfileMatch.Models.Entities;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Models
{
    public class PmConfiguration :Profile
    {
        public PmConfiguration()
        {
            CreateMap<AnswerOption, AnswerOptionVM>().ReverseMap();
            CreateMap<ClosedQuestion, ClosedQuestionVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<IdentityUserRole<string>, UserRoleVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Job, JobVM>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserVM>().ReverseMap();
        }
    }
}

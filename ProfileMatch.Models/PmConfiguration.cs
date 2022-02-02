using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

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
        }
    }
}

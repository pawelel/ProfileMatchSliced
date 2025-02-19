﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Entities;

namespace ProfileMatch.Models.ViewModels
{
    public class ClosedQuestionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNamePl { get; set; }
        public List<AnswerOptionVM> AnswerOptionsVM { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ganss.Excel;
using ProfileMatch.Models.Entities;

namespace ProfileMatch.Models.ViewModels
{
    public class AnswerOptionVM
    {
        public int Id { get; set; }
        public int ClosedQuestionId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
    }
}

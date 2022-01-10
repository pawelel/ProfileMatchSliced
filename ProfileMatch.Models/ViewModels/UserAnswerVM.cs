using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class UserAnswerVM
    {
        public string UserId { get; set; }
        public string OpenQuestionName { get; set; }
        public string OpenQuestionNamePl { get; set; }
        public string OpenQuestionDescription { get; set; }
        public string OpenQuestionDescriptionPl { get; set; }
        public int AnswerId { get; set; }
        public string UserDescription { get; set; }
        public bool IsDisplayed { get; set; }
    }
}

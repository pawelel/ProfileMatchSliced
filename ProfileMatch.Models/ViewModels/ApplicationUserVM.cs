using System;
using System.Collections.Generic;

using ProfileMatch.Models.Enumerations;

namespace ProfileMatch.Models.ViewModels
{
    public class ApplicationUserVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public int DepartmentId { get; set; } = 1; //NOTE had to initialize
        public DepartmentVM Department { get; set; }
        public string PhotoPath { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
        public List<UserNeedCategoryVM> UserNeedCategories { get; set; }
        public List<UserAnswerVM> UserAnswers { get; set; }
        public string JobTitle { get; set; }
        public string ConfirmEmail { get; set; }
        public string Email { get; set; }
    }
}

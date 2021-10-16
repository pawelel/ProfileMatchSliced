using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using ProfileMatch.Models.Enumerations;

namespace ProfileMatch.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{LastName}, {FirstName}";

        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        public Gender? Gender { get; set; }

        public int DepartmentId { get; set; } = 1; //NOTE had to initialize
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
        public List<UserNeedCategory> UserNeedCategories { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
        public string JobTitle { get; set; }
    }
}
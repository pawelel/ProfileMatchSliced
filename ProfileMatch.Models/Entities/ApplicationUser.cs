﻿using Ganss.Excel;

using Microsoft.AspNetCore.Identity;

using ProfileMatch.Models.Enumerations;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileMatch.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        [Ignore]
        public string FullName => $"{LastName}, {FirstName}";
        
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        public Gender? Gender { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }
        public int DepartmentId { get; set; } 
        [Ignore]
        public Department Department { get; set; }
        [Ignore]
        public virtual ICollection<Certificate> Certificates { get; set; }
        public string PhotoPath { get; set; }
        public bool IsActive { get; set; }
        [Ignore]
        public List<UserCategory> UserNeedCategories { get; set; }
        [Ignore]
        public List<UserClosedAnswer> UserClosedAnswers { get; set; }

        [Ignore]
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; } = new List<IdentityUserRole<string>>();
    }
}
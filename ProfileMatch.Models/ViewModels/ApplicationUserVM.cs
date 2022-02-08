using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using ProfileMatch.Models.Entities;
using ProfileMatch.Models.Enumerations;

namespace ProfileMatch.Models.ViewModels
{
    public class ApplicationUserVM : IdentityUser
    {
        public int DepartmentId { get; set; }
        public DepartmentVM Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public JobVM Job { get; set; }
        public int JobId { get; set; }
        public bool IsActive { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<UserRoleVM> UserRolesVM { get; set; }
        public Gender? Gender { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
    }
}

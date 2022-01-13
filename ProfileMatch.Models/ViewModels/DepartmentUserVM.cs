using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class DepartmentUserVM
    {
        public int DepartmentId { get; set; }
        public string DepartmentNamePl { get; set; }
        public string DepartmentName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string JobTitlePl { get; set; }
        public bool IsActive { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<UserRoleVM> UserRolesVM { get; set; } = new();
        public string FullName => $"{LastName} {FirstName}";
    }
}

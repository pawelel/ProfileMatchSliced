using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class UserRolesVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public bool IsSelected { get; set; }
    }
}

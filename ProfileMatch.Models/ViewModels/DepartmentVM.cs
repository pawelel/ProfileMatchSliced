using System.Collections.Generic;

namespace ProfileMatch.Models.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUserVM> ApplicationUsers { get; set; }
    }
}

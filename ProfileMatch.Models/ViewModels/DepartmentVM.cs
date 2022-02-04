using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }
        public string Description { get; set; }
        public string DescriptionPl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Models.ViewModels
{
    public class UserCategoryVM
    {
        public string UserId { get; set; }
        public bool IsSelected { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

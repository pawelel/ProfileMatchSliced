using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Models.ViewModels
{
   public class UserNeedCategoryVM
    {
        public bool Want { get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ApplicationUserVM ApplicationUser { get; set; }
    }
}

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}

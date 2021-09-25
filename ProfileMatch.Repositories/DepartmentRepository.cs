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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext repositoryContext) :base(repositoryContext)
        {
        }
    }
}

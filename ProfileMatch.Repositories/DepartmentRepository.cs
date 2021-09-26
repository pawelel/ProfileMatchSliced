using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace ProfileMatch.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Department> GetDepartment(int id)
{
           return await this.RepositoryContext.Set<Department>().Where(d=>d.Id==id).Include(u=>u.ApplicationUsers).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithPeople()
        {
            return await this.RepositoryContext.Set<Department>().Include(u=>u.ApplicationUsers).AsNoTracking().ToListAsync();
        }
    }
}

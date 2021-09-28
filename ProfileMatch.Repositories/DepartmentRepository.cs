using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await this.RepositoryContext.Set<Department>().Where(d => d.Id == id).Include(u => u.ApplicationUsers).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithPeople()
        {
            return await this.RepositoryContext.Set<Department>().Include(u => u.ApplicationUsers).AsNoTracking().ToListAsync();
        }
    }
}

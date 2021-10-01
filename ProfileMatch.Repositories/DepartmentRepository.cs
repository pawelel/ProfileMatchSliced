using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public DepartmentRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Department> GetById(int id)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Departments.Where(d => d.Id == id).Include(u => u.ApplicationUsers).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithPeople()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Departments.Include(u => u.ApplicationUsers).AsNoTracking().ToListAsync();
        }
        public async Task<List<Department>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Departments.AsNoTracking().ToListAsync();
        }
        public async Task<Department> Create(Department dep)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.Departments.AddAsync(dep);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<Department> Update(Department dep)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Departments.FindAsync(dep.Id);
            if (existing!=null)
            {
            repositoryContext.Entry(existing).CurrentValues.SetValues(dep);
            await repositoryContext.SaveChangesAsync();
            return existing;
            }
            else
            {
                return dep;
            }
        }
        public async Task<Department> Delete(Department dep)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Departments.Remove(dep).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }
    }
}

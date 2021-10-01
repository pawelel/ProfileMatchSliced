using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext repositoryContext;

        public UserRepository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public async Task<ApplicationUser> Create(ApplicationUser user)
        {
            var data = await repositoryContext.AddAsync(user);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            var existing = await repositoryContext.Users.FindAsync(user.Id);
            repositoryContext.Entry(existing).CurrentValues.SetValues(user);
            await repositoryContext.SaveChangesAsync();
            return existing;
        }
        public async Task<ApplicationUser> Delete(ApplicationUser user)
        {
            var data = repositoryContext.Users.Remove(user).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            return await repositoryContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> FindById(string id)
        {
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
        public async Task<ApplicationUser> FindByEmail(string email)
        {
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(email.ToUpper()));
        }
        
    }
}

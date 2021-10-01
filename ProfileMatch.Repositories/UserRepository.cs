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
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
       
        public async Task<ApplicationUser> Create(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.AddAsync(user);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Users.FindAsync(user.Id);
            repositoryContext.Entry(existing).CurrentValues.SetValues(user);
            await repositoryContext.SaveChangesAsync();
            return existing;
        }
        public async Task<ApplicationUser> Delete(ApplicationUser user)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Users.Remove(user).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> FindById(string id)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
        public async Task<ApplicationUser> FindByEmail(string email)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(email.ToUpper()));
        }
        
    }
}

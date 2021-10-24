using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    public class UserNoteRepository : IUserNoteRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public UserNoteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Task<UserNote> Create(UserNote note)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNote> Delete(UserNote note)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNote> FindByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNote> FindById(string userId, int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserNote>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserNote> Update(UserNote note)
        {
            throw new System.NotImplementedException();
        }
    }
}
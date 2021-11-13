using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
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

        public async Task<UserNote> Create(UserNote unote)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.UserNotes.AddAsync(unote);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<UserNote> Delete(UserNote unote)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.UserNotes.Remove(unote).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<UserNote> FindByName(string unname)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNotes.FirstOrDefaultAsync(u=>u.Note.Name == unname);
        }

        public async Task<UserNote> FindById(string userId, int noteId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNotes.FindAsync(userId, noteId);
        }

        public async Task<List<UserNote>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.UserNotes.AsNoTracking().ToListAsync();
        }

        public async Task<UserNote> Update(UserNote unote)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.UserNotes.FindAsync(unote.ApplicationUserId, unote.NoteId);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(unote);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return unote;
            }
        }
    }
}
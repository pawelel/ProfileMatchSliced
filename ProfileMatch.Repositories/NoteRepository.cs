using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public NoteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Note> Create(Note note)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = await repositoryContext.Notes.AddAsync(note);
            await repositoryContext.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<Note> Delete(Note note)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var data = repositoryContext.Notes.Remove(note).Entity;
            await repositoryContext.SaveChangesAsync();
            return data;
        }

        public async Task<Note> FindByName(string noteName)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Notes.FirstOrDefaultAsync(n => n.Name == noteName);
        }

        public async Task<Note> FindById(int noteId)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public async Task<List<Note>> GetAll()
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            return await repositoryContext.Notes.AsNoTracking().ToListAsync();
        }

        public async Task<Note> Update(Note note)
        {
            using ApplicationDbContext repositoryContext = contextFactory.CreateDbContext();
            var existing = await repositoryContext.Notes.FindAsync(note.Id);
            if (existing != null)
            {
                repositoryContext.Entry(existing).CurrentValues.SetValues(note);
                await repositoryContext.SaveChangesAsync();
                return existing;
            }
            else
            {
                return note;
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{
    public class NoteRepository :  INoteRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public NoteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Task<Note> Create(Note note)
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> Delete(Note note)
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> FindByName(string noteName)
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> FindById(int noteId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Note>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> Update(Note note)
        {
            throw new System.NotImplementedException();
        }
    }
}

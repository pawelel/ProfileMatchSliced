using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface INoteRepository
    {
        Task<Note> Create(Note note);

        Task<Note> Delete(Note note);

        Task<Note> FindByName(string noteName);

        Task<Note> FindById(int noteId);

        Task<List<Note>> GetAll();

        Task<Note> Update(Note note);
    }
}
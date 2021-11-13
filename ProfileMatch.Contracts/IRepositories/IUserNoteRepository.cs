using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IUserNoteRepository
    {
        Task<UserNote> Create(UserNote unote);

        Task<UserNote> Delete(UserNote unote);

        Task<UserNote> FindByName(string unname);

        Task<UserNote> FindById(string userId, int noteId);

        Task<List<UserNote>> GetAll();

        Task<UserNote> Update(UserNote unote);
    }
}
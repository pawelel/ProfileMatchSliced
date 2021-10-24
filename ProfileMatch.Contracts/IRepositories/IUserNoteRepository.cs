using ProfileMatch.Models.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IUserNoteRepository
    {
        Task<UserNote> Create(UserNote note);

        Task<UserNote> Delete(UserNote note);

        Task<UserNote> FindByName(string name);

        Task<UserNote> FindById(string userId, int categoryId);

        Task<List<UserNote>> GetAll();

        Task<UserNote> Update(UserNote note);
    }
}
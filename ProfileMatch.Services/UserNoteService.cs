


using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserNoteService : IUserNoteService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public UserNoteService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }
    }
}




using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public NoteService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }
    }
}

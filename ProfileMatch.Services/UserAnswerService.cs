


using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public UserAnswerService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }
    }
}

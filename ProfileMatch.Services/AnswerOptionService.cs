


using ProfileMatch.Contracts;

namespace ProfileMatch.Services
{
    public class AnswerOptionService : IAnswerOptionService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public AnswerOptionService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }
    }
}

using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }
        IAnswerOptionRepository AnswerOption { get; }
        ICategoryRepository Category { get; }
        IQuestionRepository Question { get; }
        IUserAnswerRepository UserAnswer { get; }
        IUserNeedCategoryRepository UserNeedCategory { get; }
        IUserNoteRepository UserNote { get; }
        INoteRepository Note { get; }


        void Save();
        Task SaveAsync();
    }
}

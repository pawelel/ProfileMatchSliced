using System.Threading.Tasks;

using ProfileMatch.Contracts;
using ProfileMatch.Data;

namespace ProfileMatch.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _repoContext;
        private IUserRepository _user;
        private IDepartmentRepository _department;
        private IAnswerOptionRepository _answerOption;
        private ICategoryRepository _category;
        private IQuestionRepository _question;
        private IUserAnswerRepository _userAnswer;
        private IUserNeedCategoryRepository _userNeedCategory;
        private IUserNoteRepository _userNote;
        private INoteRepository _note;

        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_repoContext);
                }
                return _question;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext);
                }
                return _category;
            }
        }
        public IAnswerOptionRepository AnswerOption
        {
            get
            {
                if (_answerOption == null)
                {
                    _answerOption = new AnswerOptionRepository(_repoContext);
                }
                return _answerOption;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_repoContext);
                }
                return _department;
            }
        }

        public RepositoryWrapper(ApplicationDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }

        public IUserAnswerRepository UserAnswer
        {
            get
            {

                if (_userAnswer == null)
                {
                    _userAnswer = new UserAnswerRepository(_repoContext);
                }
                return _userAnswer;
            }
        }
        public IUserNeedCategoryRepository UserNeedCategory
        {
            get
            {

                if (_userNeedCategory == null)
                {
                    _userNeedCategory = new UserNeedCategoryRepository(_repoContext);
                }
                return _userNeedCategory;
            }
        }
        public IUserNoteRepository UserNote
        {
            get
            {

                if (_userNote == null)
                {
                    _userNote = new UserNoteRepository(_repoContext);
                }
                return _userNote;
            }
        }
        public INoteRepository Note
        {
            get
            {

                if (_note == null)
                {
                    _note = new NoteRepository(_repoContext);
                }
                return _note;
            }
        }
    }
}

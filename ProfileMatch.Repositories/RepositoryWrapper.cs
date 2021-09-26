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
        pri

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
    }
}

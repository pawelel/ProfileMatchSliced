using ProfileMatch.Contracts;
using ProfileMatch.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Repositories
{
  public  class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _repoContext;
        private IUserRepository _user;
        private IDepartmentRepository _department;

        public IUserRepository User
        {
            get
            {
                if (_user==null)
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
                if (_department==null)
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
    }
}

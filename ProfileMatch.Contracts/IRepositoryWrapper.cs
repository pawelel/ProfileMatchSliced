using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Contracts
{
  public  interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }
    }
}

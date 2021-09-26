using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentService
    {
        Task<Department> Create(Department entity);
        Task<bool> Delete(Department entity);
        Task<IEnumerable<Department>> FindAllAsync();
        Task<Department> Update(Department entity);
        Task<bool> Exist(Department entity);
        Task<Department> GetDepartment(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentRepository
    {
        Task<Department> Create(Department dep);

        Task<Department> Delete(Department dep);

        Task<List<Department>> GetAll();

        Task<Department> GetById(int id);

        Task<IEnumerable<Department>> GetDepartmentsWithPeople();

        Task<Department> Update(Department dep);
    }
}
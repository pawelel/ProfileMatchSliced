using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartment(int id);
        Task<IEnumerable<Department>> GetDepartmentsWithPeople();
    }
}

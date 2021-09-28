using System.Collections.Generic;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<Department>> Create(Department entity);
        Task<bool> Delete(Department entity);
        Task<ServiceResponse<List<Department>>> FindAllAsync();
        Task<IEnumerable<DepartmentVM>> GetDepartmentsWithPeople();
        Task<ServiceResponse<Department>> Update(Department entity);
        Task<bool> Exist(Department entity);
        Task<Department> GetDepartment(int id);

    }
}

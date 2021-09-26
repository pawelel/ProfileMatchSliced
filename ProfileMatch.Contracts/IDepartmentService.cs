using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Contracts
{
    public interface IDepartmentService
    {
        Task<Department> Create(Department entity);
        Task<bool> Delete(Department entity);
        Task<IEnumerable<Department>> FindAllAsync();
        Task<IEnumerable<DepartmentVM>> GetDepartmentsWithPeople();
        Task<Department> Update(Department entity);
        Task<bool> Exist(Department entity);
        Task<Department> GetDepartment(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Contracts
{
    public interface IDepartmentService
    {
        Task<Department> Create(Department entity);
        Task<bool> Delete(Department entity);
        Task<List<Department>> FindAllAsync();
        Task<IEnumerable<Department>> GetDepartmentsWithPeople();
        Task<Department> Update(Department entity);
        Task<bool> Exist(int id);
        Task<Department> GetDepartment(int id);
        Task<bool> Exist(Department department);
    }
}

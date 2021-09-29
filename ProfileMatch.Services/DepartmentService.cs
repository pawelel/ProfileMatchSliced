using System.Collections.Generic;
using System.Threading.Tasks;



using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IRepositoryWrapper wrapper;
        

        public DepartmentService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }

        public async Task<List<Department>>> FindAllAsync()
        {
            return await wrapper.Department.FindAllAsync();
        }

        public async Task<Department>> Create(Department entity)
        {
            var doesExist = await wrapper.Department.FindSingleByConditionAsync(d => d.Name.Contains(entity.Name));
            if (doesExist == null)
            {
              await  wrapper.Department.Create(entity);
            }
            return await wrapper.Department.FindSingleByConditionAsync(d => d.Name == entity.Name);
        }

        public async Task<Department>> Update(Department entity)
        {
            var doesExist = await wrapper.Department.FindSingleByConditionAsync(d => d.Id == entity.Id);
            if (doesExist != null)
            {
             await   wrapper.Department.Update(entity, entity.Id);
            }
            return await wrapper.Department.FindSingleByConditionAsync(d => d.Id == entity.Id);
        }

        public async Task<bool> Delete(Department entity)
        {
            var doesExist = await Exist(entity);
            if (doesExist)
            {
                wrapper.Department.Delete(entity);
                return true;
            }
            return false;
        }

        public async Task<bool> Exist(Department entity)
        {
            return await wrapper.Department.Exist(d => d == entity);
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await wrapper.Department.GetDepartment(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithPeople()
        {
           return await wrapper.Department.GetDepartmentsWithPeople();
           
    
        }
    }
}




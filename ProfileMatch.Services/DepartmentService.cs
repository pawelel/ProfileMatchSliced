using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public DepartmentService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<Department>>> FindAllAsync()
        {
            return await wrapper.Department.FindAllAsync();
        }

        public async Task<ServiceResponse<Department>> Create(Department entity)
        {
            var doesExist = await wrapper.Department.FindSingleByConditionAsync(d => d.Name.Contains(entity.Name));
            if (doesExist == null)
            {
                wrapper.Department.Create(entity);
            }
            return await wrapper.Department.FindSingleByConditionAsync(d => d.Name == entity.Name);
        }

        public async Task<ServiceResponse<Department>> Update(Department entity)
        {
            var doesExist = await wrapper.Department.FindSingleByConditionAsync(d => d.Id == entity.Id);
            if (doesExist != null)
            {
                wrapper.Department.Update(entity);
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

        public async Task<IEnumerable<DepartmentVM>> GetDepartmentsWithPeople()
        {
            var depts = await wrapper.Department.GetDepartmentsWithPeople();
           var result = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentVM>>(depts);
            return result;
        }
    }
}




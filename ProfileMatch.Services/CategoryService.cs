using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Responses;
using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;

        public CategoryService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<CategoryVM>>> GetCategories()
        {
            ServiceResponse<List<CategoryVM>> result = new();
               var response = await wrapper.Category.FindAllAsync();
           result.Data=  mapper.Map<List<CategoryVM>>(response.Data);
            result.Message = response.Message;
            result.Success = response.Success;
            return result;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;



using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWrapper wrapper;
        

        public CategoryService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }

        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
               return await wrapper.Category.FindAllAsync();
        }
    }
}

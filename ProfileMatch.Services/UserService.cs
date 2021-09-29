using System.Collections.Generic;
using System.Threading.Tasks;



using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper wrapper;
        
        public UserService(IRepositoryWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }

        public async Task<List<ApplicationUser>> FindAllAsync()
        {
            return await wrapper.User.FindAllAsync();
        }

        public async Task<ApplicationUser> Create(ApplicationUser user)
        {

            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper() ));
            if (doesExist==null)
            {

               return await wrapper.User.Create(user);
           
            }
            else
            {
                return null;
                
            }
        }

        public async Task<ApplicationUser> Delete(string id)
        {
            
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);

            return wrapper.User.Delete(doesExist);
        
         
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {

            if (await Exist(user.Email))
            {

                return await wrapper.User.Update(user, user.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<ApplicationUser> FindSingleByIdAsync(string id)
        {
            
            return await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            
        }
        public async Task<ApplicationUser> FindSingleByEmailAsync(string email)
        {
            
            return await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail == email.ToUpper());
           
        }

        public async Task<bool> Exist(ApplicationUser editUser)
        {
            return await wrapper.User.Exist(u => u.Id == editUser.Id);
        }

        public async Task<bool> Exist(string email)
        {
            return await wrapper.User.Exist(u => u.NormalizedEmail == email.ToUpper());
        }


    }
}

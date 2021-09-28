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

        public async Task<ServiceResponse<List<ApplicationUser>>> FindAllAsync()
        {
            return await wrapper.User.FindAllAsync();
            
        }

        public async Task<ServiceResponse<ApplicationUser>> Create(ApplicationUser user)
        {

            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.NormalizedEmail.Equals(user.Email.ToUpper() ));
            if (!doesExist.Success)
            {

                var response = await wrapper.User.Create(user);
                return response;
            }
            else
            {
                return new()
                {
                    Message = "User already exists.",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<ApplicationUser>> Delete(string id)
        {
            
            var doesExist = await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);

            return wrapper.User.Delete(doesExist.Data);
        
         
        }

        public async Task<ServiceResponse<ApplicationUser>> Update(ApplicationUser user)
        {

            if (await Exist(user.Email))
            {

                return await wrapper.User.Update(user, user.Id);
            }
            else
            {
                return new()
                {
                    Message = "User with provided data doesn't exist.",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<ApplicationUser>> FindSingleByIdAsync(string id)
        {
            
            return await wrapper.User.FindSingleByConditionAsync(u => u.Id == id);
            
        }
        public async Task<ServiceResponse<ApplicationUser>> FindSingleByEmailAsync(string email)
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

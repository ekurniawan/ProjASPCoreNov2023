using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyBackendApp.DTO;

namespace MyBackendApp.Repository
{
    public class UserRepository : IUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<UserDto> Authenticate(UserCreateDto userCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Registration(UserCreateDto userCreateDto)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = userCreateDto.Username,
                    Email = userCreateDto.Username
                };
                var result = await _userManager.CreateAsync(newUser, userCreateDto.Password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n");
                    }
                    throw new Exception(sb.ToString());
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
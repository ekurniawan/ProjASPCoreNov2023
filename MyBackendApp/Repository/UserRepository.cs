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

        public IEnumerable<UserDto> GetAll()
        {
            var users = new List<UserDto>();
            foreach (var user in _userManager.Users)
            {
                users.Add(new UserDto
                {
                    Username = user.UserName
                });
            }
            return users;
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
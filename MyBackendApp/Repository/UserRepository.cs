using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyBackendApp.DTO;
using MyBackendApp.Helpers;

namespace MyBackendApp.Repository
{
    public class UserRepository : IUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public UserRepository(UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<UserDto> Authenticate(UserCreateDto userCreateDto)
        {
            var currUser = await _userManager.FindByNameAsync(userCreateDto.Username);
            var userResult = await _userManager.CheckPasswordAsync(currUser, userCreateDto.Password);
            if (!userResult)
                throw new Exception($"Authentication Failed !");

            UserDto userWithToken = new UserDto
            {
                Username = userCreateDto.Username
            };
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userCreateDto.Username));
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);
            return userWithToken;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DTO;
using MyBackendApp.Repository;

namespace MyBackendApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;
        public UsersController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserCreateDto userCreateDto)
        {
            try
            {
                await _user.Registration(userCreateDto);
                return Ok($"Registration User {userCreateDto.Username} success");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
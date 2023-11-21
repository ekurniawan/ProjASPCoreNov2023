using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBackendApp.DTO;

namespace MyBackendApp.Repository
{
    public interface IUser
    {
        Task Registration(UserCreateDto userCreateDto);
        IEnumerable<UserDto> GetAll();
        Task<UserDto> Authenticate(UserCreateDto userCreateDto);
    }
}
using Practicando_WEBAPI.Models.Auth;
using Practicando_WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> CreateRoleAsync(CreateRoleViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginModel model);
        Task<UserManagerResponse> CreateUserRoleAsync(CreateUserRoleViewModel model);
    }
}

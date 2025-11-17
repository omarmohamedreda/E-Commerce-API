using ECommerce.Shared.DTOS.AuthenticationDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction
{
    public interface IAuthenticationServices
    {
        // login

        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Register
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
    }
}

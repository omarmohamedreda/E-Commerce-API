using ECommerce.Abstraction;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Identity;
using ECommerce.Shared.DTOS.AuthenticationDto_s;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager) : IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = "To Do"
                };
            }
            else 
            {
                throw new UnAuthorizedException();
            }
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email
            };

            var Result = await  _userManager.CreateAsync(user, registerDto.Password);

            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = "To Do"
                };
            }
            else
            {
                // Validation Errors
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

    }
}

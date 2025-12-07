using AutoMapper;
using ECommerce.Abstraction;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Identity;
using ECommerce.Shared.DTOS.AuthenticationDto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper _mapper) : IAuthenticationServices
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
                    Token = await CreateTokenAsync(user)
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

            var Result = await _userManager.CreateAsync(user, registerDto.Password);

            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                // Validation Errors
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }


        // Generate JWT Token
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Clamis = new List<Claim>()
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.NameIdentifier, user.Id)


            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Clamis.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecurityKey = _configuration.GetSection("JWTOptions")["SecurityKey"]; // Get from app settings
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                    issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                    audience: _configuration.GetSection("JWTOptions")["Audience"],
                    claims: Clamis,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: Creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);


        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await  _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<UserDto> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new UserNotFoundException(email);
            if (user.Address is not null)
            {
                return _mapper.Map<Address, AddressDto>(user.Address);
            }
            else 
            {
                throw new AddressNotFoundException(user.UserName);

            }
        }

        public async Task<AddressDto> UpdateCurrentUserAddress(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new UserNotFoundException(email);

            if (user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else 
            {
                // Create new Address
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);

            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }
    }
}

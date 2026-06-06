using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOs;
using Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<AppUser> userManager, IConfiguration configuration , IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmail(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User is not null;
        }
        public async Task<UserDTO> GetCurrentUser(string email)
        {
            var User = await userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDTO
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await GenerateTokenAsync(User)
            };
        }
        public async Task<AddressDTO> GetUserAddress(string email)
        {
            var User = await userManager.Users.Include(U => U.Address)
                .FirstOrDefaultAsync(U => U.Email == email) 
                ?? throw new UserNotFoundException(email);

            if (User.Address is not null)
            {
                return mapper.Map<Address, AddressDTO>(User.Address);
            }
            else
                throw new AddressNotFoundException(User.DisplayName);
        }
        public async Task<AddressDTO> UpdateUserAddress(string email, AddressDTO addressDTO)
        {
            var User = await userManager.Users.Include(U => U.Address)
                .FirstOrDefaultAsync(U => U.Email == email)
                ?? throw new UserNotFoundException(email);

            if (User.Address is not null)
            {
                User.Address.Street = addressDTO.Street;
                User.Address.City = addressDTO.City;
                User.Address.Country = addressDTO.Country;
                User.Address.Fname = addressDTO.FirstName;
                User.Address.Lname = addressDTO.LastName;
            }
            else
            {
                User.Address = mapper.Map<AddressDTO, Address>(addressDTO);
            }

            await userManager.UpdateAsync(User);
            return mapper.Map<AddressDTO>(User.Address);
        }
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var User = await userManager.FindByEmailAsync(loginDTO.Email);
            if (User is null) throw new UserNotFoundException(loginDTO.Email);

            var TruePassword = await userManager.CheckPasswordAsync(User, loginDTO.Password);
            if (TruePassword)
            {
                return new UserDTO
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await GenerateTokenAsync(User)
                };
            }
            else
                throw new UnAuthorizedException();
        }
        public async Task<UserDTO> RegisterAsync(RegisterDTO RegDTO)
        {
            var User = new AppUser()
            {
                DisplayName = RegDTO.DisplayName,
                UserName = RegDTO.UserName,
                Email = RegDTO.Email,
                PhoneNumber = RegDTO.PhoneNumber
            };

            var IsCreated = await userManager.CreateAsync(User, RegDTO.Password);
            if (IsCreated.Succeeded)
            {
                return new UserDTO()
                {
                    DisplayName = RegDTO.DisplayName,
                    Email = RegDTO.Email,
                    Token = await GenerateTokenAsync(User)
                };

            }
            else
            {
                var Errors = IsCreated.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }
        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.NameIdentifier , user.Id!),
            };
            var roles = await userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecurityKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: Creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

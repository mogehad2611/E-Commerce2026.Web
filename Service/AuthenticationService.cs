using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<AppUser> userManager) : IAuthenticationService
    {
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
                    Token = GenerateToken(User)
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
            if (IsCreated)
            {
                return new UserDTO()
                {
                    DisplayName = RegDTO.DisplayName,
                    Email = RegDTO.Email,
                    Token = GenerateToken(User)
                };

            }
            else
            {
                var Errors = IsCreated.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }
        private static string GenerateToken(AppUser user)
        {
            return "ToDo";
        }
    }
}

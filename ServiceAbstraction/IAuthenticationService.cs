using Shared.DTOs;
using Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterAsync(RegisterDTO RegDTO);
        Task<bool> CheckEmail(string email);
        Task<AddressDTO> GetUserAddress(string email);
        Task<AddressDTO> UpdateUserAddress(string email,AddressDTO addressDTO);
        Task<UserDTO> GetCurrentUser(string email);
    }
}

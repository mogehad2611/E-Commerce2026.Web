using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs;
using Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) : APIBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var User = await serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(User);

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var User = await serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(User);

        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = serviceManager.AuthenticationService.CheckEmail(email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser =await serviceManager.AuthenticationService.GetCurrentUser(Email);
            return Ok(AppUser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await serviceManager.AuthenticationService.GetUserAddress(email);
            return Ok(Address);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await serviceManager.AuthenticationService.UpdateUserAddress(email, addressDTO);
            return Ok(Result);
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

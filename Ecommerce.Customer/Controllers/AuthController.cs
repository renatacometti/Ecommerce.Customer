using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Ecommerce.Customer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        //[AllowAnonymous]
        [HttpPost("Sign")]
       // [Authorize]
        public ActionResult<APIResponse<string>> Sign([FromBody] User user)
        {

            var token = _tokenService.Sign(user.Email, user.Password);
           // return Ok(token);
           if (token == null) 
           {
                return Unauthorized(new APIResponse<string>
                {
                    Success = false,
                    Message = "Invalid credentials",
                    Item = null
                });
            }
            return Ok(new APIResponse<string>
            {
                Success = true,
                Message = "Token generated successfully",
                Item = token
            });


        }
        //[AllowAnonymous]
        [Authorize]
        [HttpGet("ValidateToken/{token}")]
        public ActionResult ValidateToken([FromRoute] string token)
        {

            var response = _tokenService.ValidateToken(token);
            return Ok(response);


        }

    }
}

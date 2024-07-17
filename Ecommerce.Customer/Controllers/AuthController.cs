using Domain.Entities;
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


        [AllowAnonymous]
        [HttpPost("Sign")]
        public ActionResult Sign([FromBody] User user)
        {

            var response = _tokenService.Sign(user.Email, user.Senha);
            return Ok(response);


        }
        //[AllowAnonymous]
        [Authorize]
        [HttpGet("ValidaToken/{token}")]
        public ActionResult ValidaToken([FromRoute] string token)
        {

            var response = _tokenService.ValidateToken(token);
            return Ok(response);


        }

    }
}

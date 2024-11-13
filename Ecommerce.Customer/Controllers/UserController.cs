using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Ecommerce.Customer.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page, [FromQuery]int rows, [FromQuery] string SortColumn, [FromQuery] string SortDirection)
        {
            try
            {
                var response = this._userService.GetAll(page, rows, SortColumn, SortDirection);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os clientes", ex);
            }
        }

     
        [HttpGet("GetbyId/{userId}")]
        public IActionResult GetbyId([FromRoute] int userId)
        {
            try
            {
                var response = this._userService.GetById(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os cliente", ex);
            }


        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
   
            try
            {
                if (!await this._userService.Delete(userId))
                    return Conflict(this._userService.RetornaErros());
                else
                    return Ok();

            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar excluir o Cliente", ex);
            }
        }

        [HttpPost]
        [Route("Created/{password}")]
        public async Task<IActionResult> Created([FromBody] UserEntity user, [FromRoute] string password)
        {
            try
            {
                if (!await this._userService.Created(user, password))
                    return Conflict(this._userService.RetornaErros());
                else
                     return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar cadastrar um cliente.", ex);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserEntity user)
        {
            try
            {
                if (!await this._userService.Update(user))
                    return Conflict(this._userService.RetornaErros());
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar altera um cliente.", ex);
            }

        }

        [Authorize]
        [HttpGet("SearchCustomerByCpf/{userCpf}")]
        public IActionResult SearchCustomerByCpf([FromRoute] string userCpf)
        {
            try
            {
                var response = this._userService.SearchCustomerByCpf(userCpf);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os cliente", ex);
            }


        }

        [HttpGet("ReturnAddress/{postalCode}/{cpf}")]
        public IActionResult ReturnAddress([FromRoute]string postalCode, [FromRoute] string cpf) 
        {
            try
            {
                var response = this._userService.SearchUserAddress(postalCode, cpf);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar o endereco", ex);
            }
        }

    }
}

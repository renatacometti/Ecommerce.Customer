using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Ecommerce.Customer.Controllers
{
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(JwtAuthenticationFilter))]
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewAddressForUser([FromBody] Address endereco)
        {
            try
            {
                if (!await this._addressService.RegisterNewAddressForUser(endereco))
                    return Conflict(this._addressService.RetornaErros());
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar cadastrar um usuário.", ex);
            }

        }

    }
}

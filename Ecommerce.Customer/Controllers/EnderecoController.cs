using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Ecommerce.Customer.Controllers
{
    [Route("api/[controller]")]
    public class EnderecoController : BaseController
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }
        [HttpPost]
        [Route("CadastrarNovoEnderecoParaUsuario/{idUsuario}")]
        public async Task<IActionResult> CadastrarNovoEnderecoParaUsuario([FromBody] Endereco endereco, [FromRoute] int idUsuario)
        {
            try
            {
                if (!await this._enderecoService.CadastrarNovoEnderecoParaUsuario(endereco, idUsuario))
                    return Conflict(this._enderecoService.RetornaErros());
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar cadastrar um cliente.", ex);
            }

        }

    }
}

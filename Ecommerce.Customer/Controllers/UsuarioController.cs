
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;


namespace Ecommerce.Customer.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page, [FromQuery]int rows, [FromQuery] string colunaOrdenacao, [FromQuery] string direcaoOrdenacao)
        {
            try
            {
                var response = this._usuarioService.GetAll(page, rows, colunaOrdenacao, direcaoOrdenacao);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os clientes", ex);
            }
            

        }

     
        [HttpGet("GetbyId/{idCliente}")]
        public IActionResult GetbyId([FromRoute] int idCliente)
        {
            try
            {
                var response = this._usuarioService.GetById(idCliente);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os cliente", ex);
            }


        }

        [HttpDelete("{idCliente}")]
        public async Task<IActionResult> Delete([FromRoute] int idCliente)
        {
   
            try
            {
                if (!await this._usuarioService.Delete(idCliente))
                    return Conflict(this._usuarioService.RetornaErros());
                else
                    return Ok();

            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar excluir o Cliente", ex);
            }


        }


        [HttpPost]
        [Route("Created/{senha}")]
        public async Task<IActionResult> Created([FromBody] Usuario cliente, [FromRoute] string senha)
        {
            try
            {
                if (!await this._usuarioService.Created(cliente, senha))
                    return Conflict(this._usuarioService.RetornaErros());
                else
                     return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar cadastrar um cliente.", ex);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Usuario cliente)
        {
            try
            {
                if (!await this._usuarioService.Update(cliente))
                    return Conflict(this._usuarioService.RetornaErros());
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar altera um cliente.", ex);
            }

        }

        [Authorize]
        [HttpGet("BuscarClienteporCpf/{cpfCliente}")]
        public IActionResult BuscarClienteporCpf([FromRoute] string cpfCliente)
        {
            try
            {
                var response = this._usuarioService.BuscarClienteporCpf(cpfCliente);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar os cliente", ex);
            }


        }

        [HttpGet("RetornaEndereco/{cep}/{cpf}")]
        public IActionResult RetornaEndereco([FromRoute]string cep, [FromRoute] string cpf) 
        {
            try
            {
                var response = this._usuarioService.BuscarEnderecoCLiente(cep, cpf);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return TratarExcecao(ControllerContext, "Ocorreu um erro ao tentar recuperar o endereco", ex);
            }
        }

   

    }
}

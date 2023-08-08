using Domain.DTO;
using Domain.Entities;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<ClienteVM> GetAll(int page, int rows, string colunaOrdenacao, string direcaoOrdenacao);
        bool ValidacaodeSenha(string senhaCliente, string senhaValidacao);
        bool validaEmail(string email);
        Task<bool> Created(Cliente cliente, string senha);
        Task<bool> Update(Cliente cliente);
        string RetornaErros();
        ClienteVM GetById(int id);
        ClienteVM BuscarClienteporCpf(string cpf);
        Task<bool> Delete(int id);
        EnderecoVM BuscarEnderecoCLiente(string cep, string cpf);
        

    }
        
}

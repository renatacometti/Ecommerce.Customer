﻿using Domain.DTO;
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
        IEnumerable<ClienteVM> GetAll();
        bool ValidacaodeSenha(string senhaCliente, string senhaValidacao);
        bool validaEmail(string email);
        Task<bool> Created(Cliente cliente, string senha);
        Task<bool> Update(Cliente cliente);
        string RetornaErros();
        ClienteVM GetById(int id);
        Task<bool> Delete(int id);

    }
        
}

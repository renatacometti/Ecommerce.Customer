using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class EnderecoRepository: BaseRepository<Endereco>, IEnderecoRepository
    {
        private IConfiguration _configuration;
        public EnderecoRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }

        public void Add(Endereco endereco)
        {
            endereco.Create_Date = DateTime.Now;
            endereco.Update_Date = DateTime.Now;
            _context.Endereco.Add(endereco);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }

    }
}

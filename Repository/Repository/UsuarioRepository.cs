using Domain.DTO;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Repository.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private IConfiguration _configuration;
        public UsuarioRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }

        public User ValidaCliente(string email, string senha)
        {

            var user = _context.Usuario.Where(c => c.Email == email && c.Senha == senha)
                .Select(user => new User() { Email = user.Email, Senha = user.Senha, Id = user.Id }).SingleOrDefault();

            return user;

        }


        public UsuarioDTO BuscarPermissoesUsuario(int idUsuario)
        {

            //var user = (from u in _context.Usuario
            //            join p in _context.Perfil on u.Id_Perfil equals p.Id
            //            join pp in _context.Perfil_Permissao on p.Id equals pp.Id_Perfil
            //            join per in _context.Permissao on pp.Id_Permissao equals per.Id
            //            where u.Id == idUsuario)
            //             .Select(user => new UsuarioDTO() { Cpf = user.cpf, Senha = user.Senha, Id = user.Id }).SingleOrDefault();


            //return user;

            var userTwo = (from user in _context.Usuario
                           join p in _context.Perfil on user.Id_Perfil equals p.Id
                           join pp1 in _context.Perfil_Permissao on p.Id equals pp1.Id_Perfil
                           join per in _context.Permissao on pp1.Id_Permissao equals per.Id
                           where user.Id == idUsuario
                           select new UsuarioDTO
                           {
                               Id = user.Id,
                               Nome = user.Nome,
                               Email = user.Email,
                               Perfil = new Perfil
                               {
                                   Id = p.Id,
                                   Nome = p.Nome
                               },
                               Permissoes = (from perm in _context.Permissao
                                             join pp in _context.Perfil_Permissao on perm.Id equals pp.Id_Permissao
                                             where pp.Id_Perfil == p.Id
                                             select new Permissao
                                             {
                                                 Id = perm.Id,
                                                 Nome = perm.Nome
                                             }).ToList()
                           }).FirstOrDefault();

           return userTwo;


        }
        public bool CustomerExist(string cpf, string email)
        {
            var customerExist = _context.Usuario.Where(c => c.Email == email || c.Cpf == cpf).FirstOrDefault();
            if (customerExist != null)
                return true;

            return false;
        }

        public Usuario BuscarClienteporCpf(string cpf)
        {
            var cliente = _context.Usuario.Where(c => c.Cpf == cpf).FirstOrDefault();
            return cliente;

        }

        public Usuario GetByIDTest(int id)
        {

            var user = _context.Usuario.Where(c => c.Id == id).FirstOrDefault();
            return user;

        }

        public Usuario GetByIDTestTwo(int id)
        {

            var user = (from c in _context.Usuario
                        where c.Id == id
                        select c).FirstOrDefault();
            return user;

        }

        public Endereco BuscarEnderecoPorCliente(string cep, string cpf)
        {
            var enderecoRetornado = (from c in _context.Usuario
                                     join e in _context.Endereco on c.Id equals e.Id_Cliente
                                     where c.Cpf == cpf && e.Cep == cep
                                     select e).FirstOrDefault();
            return enderecoRetornado;

        }

        public void EnviarEmail(Usuario cliente)
        {

            CultureInfo cult = new CultureInfo("pt-BR");

            SmtpClient client = new SmtpClient();
            client.Host = _configuration["Email:Host"];
            client.Port = int.Parse(_configuration["Email:Port"]);
            client.EnableSsl = bool.Parse(_configuration["Email:EnableSsl"]);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Email:SenderEmail"], _configuration["Email:SenderPassword"]);


            MailAddress de = new MailAddress(_configuration["Email:SenderEmail"]);
            string emailpara = cliente.Email;
            MailAddress para = new MailAddress(emailpara); //email que peguei no site email 10 mint
            MailMessage email = new MailMessage(de, para);

            email.IsBodyHtml = true;
            email.Subject = "Seja bem vindo " + cliente.Nome; // titulo do email

            var caminho = Environment.CurrentDirectory.ToString() + _configuration.GetValue<string>("TemplatePath") + "EmailBoasVindas.cshtml";
            using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
            {
                var strMail = objReader.ReadToEnd();


                strMail = strMail.Replace("[Nome]", cliente.Nome);
                strMail = strMail.Replace("[Email]", cliente.Email);
                strMail = strMail.Replace("[Senha]", cliente.Senha);

                email.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                email.Body = strMail;

            }
            try
            {
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + client.Port + client.Host + _configuration["Email:SenderEmail"] + _configuration["Email:SenderPassword"]);
            }

            //CultureInfo cult = new CultureInfo("pt-BR");
            //MailMessage mailMessage = new MailMessage();
            //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.EnableSsl = true;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new NetworkCredential("luanaanalist@gmail.com", "ewxjoizhprqzwuof");

            //mailMessage.From = new MailAddress("renatacometti2@gmail.com", "Olá, Seja bem vindo ao nosso Ecommerce");
            //mailMessage.IsBodyHtml = true;

            //var caminho = Environment.CurrentDirectory.ToString() + _configuration.GetValue<string>("TemplatePath") + "EmailBoasVindas.cshtml";
            //using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
            //{
            //    var strMail = objReader.ReadToEnd();


            //    strMail = strMail.Replace("[Nome]", cliente.Nome);
            //    strMail = strMail.Replace("[Email]", cliente.Email);
            //    strMail = strMail.Replace("[Senha]", cliente.Senha);

            //    mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            //    mailMessage.Body = strMail;
            //}

            //mailMessage.Subject = "Seja bem vindo " + cliente.Nome; // titulo do email
            //mailMessage.To.Add(cliente.Email); // email de envio

            //try
            //{
            //    smtpClient.Send(mailMessage);
            //}
            //catch (Exception ex)
            //{

            //    throw new Exception(ex.Message);
            //}
        }

        public Usuario BuscarClienteESeusRelacionamentos(int idCliente)
        {

            var cliente = _context.Usuario.AsNoTracking()
                   .Include(p => p.Enderecos)
                    .Where(c => c.Id == idCliente)
                                 .SingleOrDefault();

            return cliente;
        }

        public void Alterar(Usuario cliente)
        {
            var buscaClienteERelacionamento = _context.Usuario
             .Where(p => p.Id == cliente.Id)
             .Include(p => p.Enderecos)
            .SingleOrDefault();

            if (buscaClienteERelacionamento != null)
            {
                cliente.Update_Date = DateTime.Now;
                _context.Entry(buscaClienteERelacionamento).CurrentValues.SetValues(cliente);
            }


            foreach (var enderecoModel in cliente.Enderecos)
            {
                var existeEndereco = buscaClienteERelacionamento.Enderecos
                    .Where(c => c.Id == enderecoModel.Id && c.Id != default(int))
                    .SingleOrDefault();

                if (existeEndereco != null)
                {
                    enderecoModel.Update_Date = DateTime.Now;
                    _context.Entry(existeEndereco).CurrentValues.SetValues(enderecoModel);

                }
                else
                {
                    // Insere endereco
                    var novoEndereco = new Endereco
                    {
                        Rua = enderecoModel.Rua,
                        Numero = enderecoModel.Numero,
                        Bairro = enderecoModel.Bairro,
                        Cidade = enderecoModel.Cidade,
                        Uf = enderecoModel.Uf,
                        Cep = enderecoModel.Cep,
                        Nome_Endereco = enderecoModel.Nome_Endereco,
                        Create_Date = DateTime.Now,
                        Update_Date = DateTime.Now,

                    };
                    buscaClienteERelacionamento.Enderecos.Add(novoEndereco);
                }

            }

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }


    }
}

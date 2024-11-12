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
    public class UsuarioRepository : BaseRepository<User>, IUserRepository
    {
        private IConfiguration _configuration;
        public UsuarioRepository(AppDbContext appDbContext, IConfiguration configuration) : base(appDbContext)
        {
            _configuration = configuration;
        }

        public User ValidateUser(string email, string senha)
        {

            var user = _context.Usuario.Where(c => c.Email == email && c.Password == senha)
                .Select(user => new User() { Email = user.Email, Password = user.Password, Id = user.Id }).SingleOrDefault();

            return user;

        }


        public UserDTO SearchInfosToken(int idUsuario)
        {

            //var user = (from u in _context.Usuario
            //            join p in _context.Perfil on u.Id_Perfil equals p.Id
            //            join pp in _context.Perfil_Permissao on p.Id equals pp.Id_Perfil
            //            join per in _context.Permissao on pp.Id_Permissao equals per.Id
            //            where u.Id == idUsuario)
            //             .Select(user => new UsuarioDTO() { Cpf = user.cpf, Senha = user.Senha, Id = user.Id }).SingleOrDefault();


            //return user;

            var userTwo = (from user in _context.Usuario
                           join p in _context.Perfil on user.Id_Profile equals p.Id
                           join pp1 in _context.Perfil_Permissao on p.Id equals pp1.ProfileId
                           join per in _context.Permissao on pp1.PermissionId equals per.Id
                           where user.Id == idUsuario
                           select new UserDTO
                           {
                               Id = user.Id,
                               Nome = user.Name,
                               Email = user.Email,
                               Perfil = new Profile
                               {
                                   Id = p.Id,
                                   Name = p.Name
                               },
                               Permissoes = (from perm in _context.Permissao
                                             join pp in _context.Perfil_Permissao on perm.Id equals pp.PermissionId
                                             where pp.ProfileId == p.Id
                                             select new PermissionDTO
                                             {
                                                 Id = perm.Id,
                                                 Name = perm.Name
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

        public User SearchUserByCpf(string cpf)
        {
            var cliente = _context.Usuario.Where(c => c.Cpf == cpf).FirstOrDefault();
            return cliente;

        }

        public User GetByIDTest(int id)
        {

            var user = _context.Usuario.Where(c => c.Id == id).FirstOrDefault();
            return user;

        }

        public User GetByIDTestTwo(int id)
        {

            var user = (from c in _context.Usuario
                        where c.Id == id
                        select c).FirstOrDefault();
            return user;

        }

        public Address SearchAddressByUser(string cep, string cpf)
        {
            var enderecoRetornado = (from c in _context.Usuario
                                     join e in _context.Endereco on c.Id equals e.Id_Cliente
                                     where c.Cpf == cpf && e.PostalCode == cep
                                     select e).FirstOrDefault();
            return enderecoRetornado;

        }

        public void SendEmail(User cliente)
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
            email.Subject = "Seja bem vindo " + cliente.Name; // titulo do email

            var caminho = Environment.CurrentDirectory.ToString() + _configuration.GetValue<string>("TemplatePath") + "EmailBoasVindas.cshtml";
            using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
            {
                var strMail = objReader.ReadToEnd();


                strMail = strMail.Replace("[Nome]", cliente.Name);
                strMail = strMail.Replace("[Email]", cliente.Email);
                strMail = strMail.Replace("[Senha]", cliente.Password);

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

        public User SearchUserAndTheirRelationships(int idCliente)
        {

            var cliente = _context.Usuario.AsNoTracking()
                   .Include(p => p.Addresses)
                    .Where(c => c.Id == idCliente)
                                 .SingleOrDefault();

            return cliente;
        }

        public void Change(User cliente)
        {
            var buscaClienteERelacionamento = _context.Usuario
             .Where(p => p.Id == cliente.Id)
             .Include(p => p.Addresses)
            .SingleOrDefault();

            if (buscaClienteERelacionamento != null)
            {
                cliente.Update_Date = DateTime.Now;
                _context.Entry(buscaClienteERelacionamento).CurrentValues.SetValues(cliente);
            }


            foreach (var enderecoModel in cliente.Addresses)
            {
                var existeEndereco = buscaClienteERelacionamento.Addresses
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
                    var novoEndereco = new Address
                    {
                        Street = enderecoModel.Street,
                        Number = enderecoModel.Number,
                        District = enderecoModel.District,
                        City = enderecoModel.City,
                        State = enderecoModel.State,
                        PostalCode = enderecoModel.PostalCode,
                        AddressName = enderecoModel.AddressName,
                        Create_Date = DateTime.Now,
                        Update_Date = DateTime.Now,

                    };
                    buscaClienteERelacionamento.Addresses.Add(novoEndereco);
                }

            }

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }
    }
}

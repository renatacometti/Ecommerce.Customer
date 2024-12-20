using Domain.DTOs.Permission;
using Domain.DTOs.Profile;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Repository
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public UserEntity ValidateUser(string email, string password)
        {

            var user = _context.Users.Where(c => c.Email == email && c.Password == password)
                .Select(user => new UserEntity() { Email = user.Email, Password = user.Password, Id = user.Id }).SingleOrDefault();

            return user;

        }


        public UserDTO SearchInfosToken(int userId)
        {
            // innerJoin com Profile e Permission
            //var userDTO = (from user in _context.Users
            //               join p in _context.Profile on user.ProfileId equals p.Id
            //               join pp1 in _context.ProfilePermission on p.Id equals pp1.ProfileId
            //               join per in _context.Permission on pp1.PermissionId equals per.Id
            //               where user.Id == userId
            //               select new UserDTO
            //               {
            //                   Id = user.Id,
            //                   Name = user.Name,
            //                   Email = user.Email,
            //                   Profile = new ProfileDTO
            //                   {
            //                       Id = p.Id,
            //                       Name = p.Name
            //                   },
            //                   Permissions = (from perm in _context.Permission
            //                                  join pp in _context.ProfilePermission on perm.Id equals pp.PermissionId
            //                                  where pp.ProfileId == p.Id
            //                                  select new PermissionDTO
            //                                  {
            //                                      Id = perm.Id,
            //                                      Name = perm.Name
            //                                  }).ToList()
            //               }).FirstOrDefault();


            //LeftJoin com Profile e Permission
            var userDTO = (from user in _context.Users
                           join p in _context.Profile on user.ProfileId equals p.Id
                           join pp1 in _context.ProfilePermission on p.Id equals pp1.ProfileId into profilePermissionGroup
                           from pp1 in profilePermissionGroup.DefaultIfEmpty()
                           join per in _context.Permission on pp1.PermissionId equals per.Id into permissionGroup
                           from per in permissionGroup.DefaultIfEmpty()
                           where user.Id == userId
                           select new UserDTO
                           {
                               Id = user.Id,
                               Name = user.Name,
                               Email = user.Email,
                               Profile = new ProfileDTO
                               {
                                   Id = p.Id,
                                   Name = p.Name
                               },
                               Permissions = (from perm in _context.Permission
                                              join pp in _context.ProfilePermission on perm.Id equals pp.PermissionId into ppGroup
                                              from pp in ppGroup.DefaultIfEmpty()
                                              where pp.ProfileId == p.Id || pp.ProfileId == null
                                              select new PermissionDTO
                                              {
                                                  Id = perm != null ? perm.Id : 0,
                                                  Name = perm != null ? perm.Name : "Usuario não possui Permissoes"
                                              }).ToList()
                           }).FirstOrDefault();

            return userDTO;

        }
        public bool UserExist(string cpf, string email)
        {
            var userExist = _context.Users.Where(u => u.Email == email || u.Cpf == cpf).FirstOrDefault();
            
            if (userExist != null)
                return true;

            return false;
        }

        public UserEntity SearchUserByCpf(string cpf)
        {
            var user = _context.Users.Where(c => c.Cpf == cpf).FirstOrDefault();
            return user;

        }

        public UserEntity GetByIDTest(int id)
        {

            var user = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            return user;

        }

        public UserEntity GetByIDTestTwo(int id)
        {

            var user = (from c in _context.Users
                        where c.Id == id
                        select c).FirstOrDefault();
            return user;

        }

        public AddressEntity SearchAddressByUser(string cep, string cpf)
        {
            var returnedAddress = (from c in _context.Users
                                     join e in _context.Address on c.Id equals e.UserId
                                     where c.Cpf == cpf && e.PostalCode == cep
                                     select e).FirstOrDefault();
            return returnedAddress;

        }

        //public void SendEmail(UserEntity cliente)
        //{
        //    using (SmtpClient client = new SmtpClient())
        //    {
        //        client.Host = _configuration["Email:Host"];
        //        client.Port = int.Parse(_configuration["Email:Port"]);
        //        client.EnableSsl = bool.Parse(_configuration["Email:EnableSsl"]);
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(
        //            _configuration["Email:SenderEmail"],
        //            _configuration["Email:SenderPassword"]);

        //        MailAddress de = new MailAddress(_configuration["Email:SenderEmail"]);
        //        MailAddress para = new MailAddress(cliente.Email);
        //        using (MailMessage email = new MailMessage(de, para))
        //        {
        //            email.IsBodyHtml = true;
        //            email.Subject = "Seja bem vindo " + cliente.Name;
        //            email.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");


        //            var caminho = Path.Combine(Environment.CurrentDirectory, _configuration["TemplatePath"], "EmailBoasVindas.cshtml");
                   

        //            using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
        //            {
        //                string strMail = objReader.ReadToEnd();
        //                strMail = strMail.Replace("[Nome]", cliente.Name)
        //                                 .Replace("[Email]", cliente.Email)
        //                                 .Replace("[Senha]", cliente.Password);

        //                email.Body = strMail;
        //            }

        //            try
        //            {
        //                client.Send(email);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception($"Erro ao enviar e-mail: {ex.Message}. Host: {client.Host}, Porta: {client.Port}, E-mail: {_configuration["Email:SenderEmail"]}");
        //            }
        //        }
        //    }

        //    //CultureInfo cult = new CultureInfo("pt-BR");

        //    //SmtpClient client = new SmtpClient();
        //    //client.Host = _configuration["Email:Host"];
        //    //client.Port = int.Parse(_configuration["Email:Port"]);
        //    //client.EnableSsl = bool.Parse(_configuration["Email:EnableSsl"]);
        //    //client.UseDefaultCredentials = false;
        //    //client.Credentials = new NetworkCredential(_configuration["Email:SenderEmail"], _configuration["Email:SenderPassword"]);


        //    //MailAddress de = new MailAddress(_configuration["Email:SenderEmail"]);
        //    //string emailpara = cliente.Email;
        //    //MailAddress para = new MailAddress(emailpara); //email que peguei no site email 10 mint
        //    //MailMessage email = new MailMessage(de, para);

        //    //email.IsBodyHtml = true;
        //    //email.Subject = "Seja bem vindo " + cliente.Name; // titulo do email

        //    //var caminho = Environment.CurrentDirectory.ToString() + _configuration.GetValue<string>("TemplatePath") + "EmailBoasVindas.cshtml";
        //    //using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
        //    //{
        //    //    var strMail = objReader.ReadToEnd();


        //    //    strMail = strMail.Replace("[Nome]", cliente.Name);
        //    //    strMail = strMail.Replace("[Email]", cliente.Email);
        //    //    strMail = strMail.Replace("[Senha]", cliente.Password);

        //    //    email.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        //    //    email.Body = strMail;

        //    //}
        //    //try
        //    //{
        //    //    client.Send(email);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new Exception(ex.Message + client.Port + client.Host + _configuration["Email:SenderEmail"] + _configuration["Email:SenderPassword"]);
        //    //}

        //    //CultureInfo cult = new CultureInfo("pt-BR");
        //    //MailMessage mailMessage = new MailMessage();
        //    //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //    //smtpClient.EnableSsl = true;
        //    //smtpClient.UseDefaultCredentials = false;
        //    //smtpClient.Credentials = new NetworkCredential("luanaanalist@gmail.com", "ewxjoizhprqzwuof");

        //    //mailMessage.From = new MailAddress("renatacometti2@gmail.com", "Olá, Seja bem vindo ao nosso Ecommerce");
        //    //mailMessage.IsBodyHtml = true;

        //    //var caminho = Environment.CurrentDirectory.ToString() + _configuration.GetValue<string>("TemplatePath") + "EmailBoasVindas.cshtml";
        //    //using (StreamReader objReader = new StreamReader(caminho, Encoding.GetEncoding("iso-8859-1")))
        //    //{
        //    //    var strMail = objReader.ReadToEnd();


        //    //    strMail = strMail.Replace("[Nome]", cliente.Nome);
        //    //    strMail = strMail.Replace("[Email]", cliente.Email);
        //    //    strMail = strMail.Replace("[Senha]", cliente.Senha);

        //    //    mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        //    //    mailMessage.Body = strMail;
        //    //}

        //    //mailMessage.Subject = "Seja bem vindo " + cliente.Nome; // titulo do email
        //    //mailMessage.To.Add(cliente.Email); // email de envio

        //    //try
        //    //{
        //    //    smtpClient.Send(mailMessage);
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    throw new Exception(ex.Message);
        //    //}
        //}

        public UserEntity SearchUserAndTheirRelationships(int idCliente)
        {

            var cliente = _context.Users.AsNoTracking()
                   .Include(p => p.Addresses)
                    .Where(c => c.Id == idCliente)
                                 .SingleOrDefault();

            return cliente;
        }

        public void Change(UserEntity cliente)
        {
            var buscaClienteERelacionamento = _context.Users
             .Where(p => p.Id == cliente.Id)
             .Include(p => p.Addresses)
            .SingleOrDefault();

            if (buscaClienteERelacionamento != null)
            {
                cliente.UpdateDate = DateTime.Now;
                _context.Entry(buscaClienteERelacionamento).CurrentValues.SetValues(cliente);
            }


            foreach (var enderecoModel in cliente.Addresses)
            {
                var existeEndereco = buscaClienteERelacionamento.Addresses
                    .Where(c => c.Id == enderecoModel.Id && c.Id != default(int))
                    .SingleOrDefault();

                if (existeEndereco != null)
                {
                    enderecoModel.UpdateDate = DateTime.Now;
                    _context.Entry(existeEndereco).CurrentValues.SetValues(enderecoModel);

                }
                else
                {
                    // Insere endereco
                    var novoEndereco = new AddressEntity
                    {
                        Street = enderecoModel.Street,
                        Number = enderecoModel.Number,
                        District = enderecoModel.District,
                        City = enderecoModel.City,
                        State = enderecoModel.State,
                        PostalCode = enderecoModel.PostalCode,
                        AddressName = enderecoModel.AddressName,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,

                    };
                    buscaClienteERelacionamento.Addresses.Add(novoEndereco);
                }

            }

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;

        }


        public void SendEmail(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}

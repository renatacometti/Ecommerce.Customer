using Domain.DTOs.User;
using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Service.Services
{
    public class TokenService : ITokenService
    {
        //private readonly SymmetricSecurityKey _chave; // usa para o metodo CreateToken
        private readonly SymmetricSecurityKey _key;
        private readonly IUserRepository _clienteRepository;

        public TokenService(IConfiguration config, IUserRepository clienteRepository)
        {
            //_chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["chaveSecreta"])); // usa para o metodo CreateToken
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));

            _clienteRepository = clienteRepository;

        }

        public string CreateToken(UserEntity user)
        {
            var credenciais = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var permissoesUsuario = _clienteRepository.SearchInfosToken(user.Id);

            // Inicializando a lista de claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("nomeUsuario", permissoesUsuario.Name),
                new Claim("perfil", permissoesUsuario.Profile.Name),
                
            };

            // Iterando sobre cada permissão e adicionando como claim individual
            if (permissoesUsuario.Permissions != null)
            {
                foreach (var permissao in permissoesUsuario.Permissions)
                {
                    claims.Add(new Claim("permissao", permissao.Name)); // Ajuste a propriedade conforme necessário
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = credenciais
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string Sign(UserDTO user)
        {
            string token = null;
            var existingUser = _clienteRepository.ValidateUser(user.Email, user.Password);
            if (user.Email != null)
            {
                token = CreateToken(existingUser);

            }

            return token;
        }

        public bool ValidateToken(string token)
        {

            //string secretKey = "sua-chave-secreta";
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Convert.FromBase64String(secretKey); // Converta sua chave para bytes

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key, //new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Você pode validar o emissor se necessário
                    ValidateAudience = false, // Você pode validar a audiência se necessário
                    ClockSkew = TimeSpan.Zero // Não permitir diferença de tempo
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return true; // Token válido
            }
            catch (Exception)
            {
                return false; // Token inválido
            }
        }
    }
}


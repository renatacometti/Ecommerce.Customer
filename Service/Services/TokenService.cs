﻿using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        //private readonly SymmetricSecurityKey _chave; // usa para o metodo CreateToken
        private readonly SymmetricSecurityKey _key;
        private readonly IClienteRepository _clienteRepository;

        public TokenService(IConfiguration config, IClienteRepository clienteRepository)
        {
            //_chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["chaveSecreta"])); // // usa para o metodo CreateToken
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            
            _clienteRepository = clienteRepository; 

        }

  

        //public string CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email)
        //    };
        //    var credenciais = new SigningCredentials(_chave, SecurityAlgorithms.HmacSha512Signature);
        //    var tokenDecriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.Now.AddDays(7),
        //        SigningCredentials = credenciais
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDecriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public string CreateToken2(User user) 
        {
            var credenciais = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)

                }),

                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = credenciais
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
  
        }



        public string Sign(string email, string senha)
        {
            string token = null;
            var user = _clienteRepository.ValidaCliente(email, senha);
            if (user.Email != null)
            {
                token = CreateToken2(user);

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


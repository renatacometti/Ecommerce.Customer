using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


//classe criada para decorar as controlers para usar o Token - nao estou usando
public class JwtAuthenticationFilter : Attribute, IAuthorizationFilter
{
    private readonly SymmetricSecurityKey _key;
    public JwtAuthenticationFilter(IConfiguration config)
    {
        
        _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));

    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Convert.FromBase64String(secretKey);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            // Se chegou até aqui, o token é válido
        }
        catch (Exception)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}

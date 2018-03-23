using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Noah.Service.Utilities
{
  public static class SecurityUtilities
  {
    public static string VerifyToken(string token)
    {
      string secret = NoahServiceSettings.ServiceSecret;
      var now = DateTime.UtcNow;
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

      SecurityToken securityToken;
      JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
      TokenValidationParameters validationParameters = new TokenValidationParameters()
      {
        ValidAudience = "http://noah-chatbot-service",
        ValidIssuer = "http://noah-chatbot-web",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        LifetimeValidator = LifetimeValidator,
        IssuerSigningKey = securityKey
      };

      ClaimsPrincipal user = handler.ValidateToken(token, validationParameters, out securityToken);

      return user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }


    private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    {
      if (expires != null)
      {
        if (DateTime.UtcNow < expires) return true;
      }
      return false;
    }
  }
}

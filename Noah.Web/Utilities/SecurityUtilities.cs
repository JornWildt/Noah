using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin.Security;

namespace Noah.Web.Utilities
{
  public static class SecurityUtilities
  {
    public static string GenerateServerToken(IAuthenticationManager auth)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      ClaimsIdentity identity = (ClaimsIdentity)auth.User.Identity;

      DateTime issuedAt = DateTime.UtcNow;
      DateTime expires = DateTime.UtcNow.AddDays(1);

      string secret = WebAppSettings.ServiceSecret;
      var now = DateTime.UtcNow;
      var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
      var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

      var token = tokenHandler.CreateJwtSecurityToken(
        issuer: "http://noah-chatbot-web",
        audience: "http://noah-chatbot-service",
        subject: identity,
        notBefore: issuedAt,
        expires: expires,
        signingCredentials: signingCredentials);

      var tokenString = tokenHandler.WriteToken(token);

      return tokenString;
    }
  }
}

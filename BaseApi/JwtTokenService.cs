using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Zuhid.BaseApi;

public class JwtTokenService(IdentityModel identityModel) : ITokenService
{
  public void Configure(JwtBearerOptions options)
  {
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      IssuerSigningKey = PublicKey(),
      RequireExpirationTime = true,
      RequireSignedTokens = true,
      ValidateAudience = true,
      ValidateIssuer = true,
      ValidateIssuerSigningKey = true,
      ValidateLifetime = true,
      ValidAudience = identityModel.Audience,
      ValidIssuer = identityModel.Issuer,
    };
  }

  public string Build(Guid id, IList<Claim> claims, IList<string> roles)
  {
    claims.Add(new Claim("id", id.ToString()));
    // put the roles in the claims
    foreach (var role in roles)
    {
      claims.Add(new Claim(ClaimTypes.Role, role));
    }
    return new JwtSecurityTokenHandler().WriteToken(
        new JwtSecurityToken(identityModel.Issuer, identityModel.Audience, claims, null, DateTime.UtcNow.AddMinutes(identityModel.ExpireMinutes), PrivateKey())
    );
  }

  private SecurityKey PublicKey()
  {
    if (!string.IsNullOrWhiteSpace(identityModel.SymmetricKey))
    {
      return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityModel.SymmetricKey));
    }
    else
    {
      var rsa = RSA.Create();
      rsa.ImportRSAPublicKey(Convert.FromBase64String(identityModel.PublicKey), out _);
      return new RsaSecurityKey(rsa);
    }
  }

  private SigningCredentials PrivateKey()
  {
    if (!string.IsNullOrWhiteSpace(identityModel.SymmetricKey))
    {
      return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityModel.SymmetricKey)), SecurityAlgorithms.HmacSha256);
    }
    else
    {
      using var rsa = RSA.Create();
      rsa.ImportRSAPrivateKey(Convert.FromBase64String(identityModel.PrivateKey), out _);
      return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
    }
  }
}

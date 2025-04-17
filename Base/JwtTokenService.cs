using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Zuhid.Base;
public class JwtTokenService
{

    private readonly IdentityModel identityModel;
    public JwtTokenService(IdentityModel identityModel)
    {
        this.identityModel = identityModel;
    }

    public string Build(Guid id, IList<Claim> claims, IList<string> roles)
    {
        // put the roles in the claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return new JwtSecurityTokenHandler().WriteToken(
          new JwtSecurityToken(identityModel.Issuer, identityModel.Audience, claims, null, DateTime.Now.AddMinutes(identityModel.ExpireMinutes), PrivateKey())
        );
    }

    public SigningCredentials PrivateKey()
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

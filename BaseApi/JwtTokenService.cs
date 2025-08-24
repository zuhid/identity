using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Zuhid.BaseApi;

public class JwtTokenService(IdentityModel identityModel)
{

    private readonly IdentityModel identityModel = identityModel;

    public string Build(Guid id, IList<Claim> claims, IList<string> roles)
    {
        // put the roles in the claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(identityModel.Issuer, identityModel.Audience, claims, null, DateTime.UtcNow.AddMinutes(identityModel.ExpireMinutes), PrivateKey())
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

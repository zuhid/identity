using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Zuhid.BaseApi;

public interface ITokenService {
  void Configure(JwtBearerOptions options);
  string Build(Guid id, IList<Claim> claims, IList<string> roles);
}

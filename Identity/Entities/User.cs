using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;

namespace Zuhid.Identity.Entities;

public class User : IdentityUser<Guid>, IEntity {
  public User() {
    Id = Guid.NewGuid();
    SecurityStamp = Guid.NewGuid().ToString();
  }

  public Guid UpdatedById { get; set; } = Guid.Empty;

  public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}

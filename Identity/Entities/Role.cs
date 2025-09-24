using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;
namespace Zuhid.Identity.Entities;

public class Role : IdentityRole<Guid>, IEntity {
  public Guid UpdatedById { get; set; }
  public DateTime UpdatedDate { get; set; }
}

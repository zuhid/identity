using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zuhid.Base;

namespace Zuhid.Identity.Mappers
{
  public interface IUserMapper
  {
    Entities.User GetEntity(Models.User model);
    Models.User GetModel(Entities.User entity);
  }

  public class UserMapper : BaseMapper<Models.User, Entities.User>, IUserMapper
  {
    public override Entities.User GetEntity(Models.User model)
    {
      return new Entities.User
      {
        Id = model.Id,
        Updated = model.Updated,
        IsActive = model.IsActive,
        Email = model.Email,
        Password = model.Password,
        Phone = model.Phone,
      };

    }

    public override Models.User GetModel(Entities.User entity)
    {
      throw new NotImplementedException();
    }
  }
}

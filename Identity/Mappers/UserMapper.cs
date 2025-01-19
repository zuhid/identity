using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zuhid.Base;

namespace Zuhid.Identity.Mappers
{
    public class UserMapper : BaseMapper<Models.User, Entities.User>
    {
        public override Entities.User GetEntity(Models.User model)
        {
            return new Entities.User
            {
                Id = model.Id,
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
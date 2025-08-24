using Zuhid.BaseApi;

namespace Zuhid.Identity.Mappers;

public interface IUserMapper
{
    Entities.User GetEntity(Models.User model);
    Models.User GetModel(Entities.User entity);
}

internal class UserMapper : BaseMapper<Models.User, Entities.User>, IUserMapper
{
    public override Entities.User GetEntity(Models.User model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return new Entities.User
        {
            Id = model.Id,
            UpdatedDate = model.UpdatedDate,
            // IsActive = model.IsActive,
            Email = model.Email,
            PasswordHash = model.Password,
            PhoneNumber = model.Phone,
        };
    }
}

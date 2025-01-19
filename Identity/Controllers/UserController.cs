using Microsoft.AspNetCore.Mvc;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IdentityRepository identityRepository, UserMapper userMapper) : ControllerBase
{
    [HttpGet()]
    public async Task<List<User>> Get(Guid? id)
    {
        return await (id == null ? identityRepository.Get() : identityRepository.Get(id.Value));
    }

    [HttpPost]
    public async Task Add(User user)
    {
        await identityRepository.Add(userMapper.GetEntity(user));
    }

    [HttpPut]
    public async Task Update(User user)
    {
        await identityRepository.Update(userMapper.GetEntity(user));

    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await identityRepository.Delete(id);
    }
}

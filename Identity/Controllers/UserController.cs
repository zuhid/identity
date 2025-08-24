using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IIdentityRepository identityRepository, IUserMapper userMapper) : ControllerBase
{
    [HttpGet()]
    public async Task<List<User>> Get(Guid? id)
    {
        return await (id == null ? identityRepository.Get() : identityRepository.Get(id.Value)).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<SaveRespose> Add(User user)
    {
        return await identityRepository.Add(userMapper.GetEntity(user)).ConfigureAwait(false);
    }

    [HttpPut]
    public async Task<SaveRespose> Update(User user)
    {
        return await identityRepository.Update(userMapper.GetEntity(user)).ConfigureAwait(false);
    }

    [HttpDelete("Id/{id}")]
    public async Task Delete(Guid id)
    {
        await identityRepository.Delete(id).ConfigureAwait(false);
    }
}

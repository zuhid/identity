using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository userRepository, IIdentityRepository identityRepository, IUserMapper userMapper,
UserManager<Entities.User> userManager) : ControllerBase
{
    [HttpGet()]
    public async Task<List<User>> Get(Guid? id)
    {
        return await userRepository.Get().ConfigureAwait(false);
    }

    [HttpGet("IsPasswordValid")]
    public async Task<bool> IsPasswordValid(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByNameAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null) return false;
        return await userManager.CheckPasswordAsync(userEntity, user.Password).ConfigureAwait(false);
    }

    [HttpGet("EmailToken")]
    public async Task<string> EmailToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null) return string.Empty;
        return await userManager.GenerateEmailConfirmationTokenAsync(userEntity).ConfigureAwait(false);
    }

    [HttpGet("IsEmailTokenValid")]
    public async Task<bool> IsEmailTokenValid(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null) return false;
        var result = await userManager.ConfirmEmailAsync(userEntity, user.EmailToken).ConfigureAwait(false);
        return result.Succeeded;
    }

    [HttpGet("PhoneToken")]
    public async Task<string> PhoneToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return string.Empty;
        return await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, userEntity.PhoneNumber).ConfigureAwait(false);
    }

    [HttpGet("IsPhoneTokenValid")]
    public async Task<bool> IsPhoneTokenValid(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return false;
        var result = await userManager.VerifyChangePhoneNumberTokenAsync(userEntity, user.PhoneToken, userEntity.PhoneNumber).ConfigureAwait(false);
        if (result && !userEntity.PhoneNumberConfirmed)
        {
            userEntity.PhoneNumberConfirmed = true;
            await userManager.UpdateAsync(userEntity).ConfigureAwait(false);
        }
        return result;
    }

    [HttpGet("TfaToken")]
    public async Task<string> TfaToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null) return string.Empty;
        // await userManager.ResetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);
        var result = await userManager.GetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);
        return result ?? string.Empty;
    }

    [HttpGet("VerifyTfaToken")]
    public async Task<bool> VerifyTfaToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
        if (userEntity == null || string.IsNullOrWhiteSpace(user.TfaToken)) return false;
        var result = await userManager.VerifyTwoFactorTokenAsync(userEntity, userManager.Options.Tokens.AuthenticatorTokenProvider, user.TfaToken).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    public async Task<SaveRespose> Add(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var userEntity = userMapper.GetEntity(user);
        userEntity.PasswordHash = userManager.PasswordHasher.HashPassword(userEntity, user.Password);
        userEntity.UpdatedDate = DateTime.UtcNow;
        await userManager.CreateAsync(userEntity).ConfigureAwait(false);
        return new SaveRespose { Updated = userEntity.UpdatedDate };
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

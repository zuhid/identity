using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web;
using Zuhid.BaseApi;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Models;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository userRepository, IIdentityRepository identityRepository, IUserMapper userMapper,
    UserManager<Entities.User> userManager, SignInManager<Entities.User> signInManager,
    ITokenService tokenService, IMessageService messageService) : ControllerBase {
  [HttpPost("Register")]
  public async Task<bool> Register([NotNull] User user) {
    var userEntity = userMapper.GetEntity(user);
    // create the entity
    var result = await userManager.CreateAsync(userEntity, user.Password).ConfigureAwait(false); // create the user
    foreach (var error in result.Errors) {
      ModelState.AddModelError(error.Code, error.Description); // Add any errors to ModelState
    }
    if (result.Errors.ToArray().Length == 0) {
      // Email token
      var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(userEntity).ConfigureAwait(false);
      var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailToken));
      return await messageService.SendEmail("Your verification code",
          $"<a href='http://localhost:4200/identity/register?email={userEntity.Email}&emailToken={encodedToken}'>Click to veirfy your Email</a>",
          userEntity.Email ?? ""
          ).ConfigureAwait(false);
    }
    // return response
    return false;
  }


  [AllowAnonymous]
  [HttpPut("Login")]
  public async Task<LoginResponse> Login(User user) {
    var loginResponse = new LoginResponse();
    ArgumentNullException.ThrowIfNull(user);
    var signInResult = await signInManager.PasswordSignInAsync(user.Email, user.Password, true, false).ConfigureAwait(false);
    if (signInResult.Succeeded) {
      var userEntity = await userManager.FindByNameAsync(user.Email).ConfigureAwait(false);
      if (userEntity != null) {
        loginResponse.AuthToken = tokenService.Build(userEntity.Id, [
            new("userName", userEntity.UserName ?? string.Empty),
                    new("email", userEntity.Email ?? string.Empty),
                    new("phoneNumber", userEntity.PhoneNumber ?? string.Empty),
                ], []);
      }
    }
    return loginResponse;
  }

  [HttpPut("EmailVerify")]
  public async Task<bool> EmailVerify(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    if (userEntity == null) {
      return false;
    }

    var emailToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(user.EmailToken));

    var result = await userManager.ConfirmEmailAsync(userEntity, emailToken).ConfigureAwait(false);
    foreach (var error in result.Errors) {
      ModelState.AddModelError(error.Code, error.Description); // Add any errors to ModelState
    }

    return result.Succeeded;
  }

  [HttpPut("EmailSendToken")]
  public async Task<bool> EmailSendToken(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    if (userEntity == null) {
      return false;
    }

    var tfa = await userManager.GenerateTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);
    return await messageService.SendEmail("Your verification code", $"Here is your tfa verification code: {tfa}", userEntity.Email ?? "").ConfigureAwait(false);
  }

  [HttpPut("EmailVerifyToken")]
  public async Task<bool> EmailVerifyToken(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    return userEntity != null && await userManager.VerifyTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider, user.EmailToken).ConfigureAwait(false);
  }

  [HttpPut("PhoneSendToken")]
  public async Task<bool> PhoneSendToken(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    if (userEntity == null) {
      return false;
    }

    var tfa = await userManager.GenerateTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);
    return await messageService.SendSms(userEntity.PhoneNumber ?? "None", $"Here is your tfa verification code: {tfa}").ConfigureAwait(false);
  }

  [HttpPut("PhoneVerifyToken")]
  public async Task<bool> PhoneVerifyToken(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.PhoneNumber).ConfigureAwait(false);
    return userEntity != null
      && await userManager.VerifyTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider, user.PhoneToken).ConfigureAwait(false);
  }


  [HttpPut("GenerateQrCodeUri")]
  public async Task<User> GenerateQrCodeUri(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    if (userEntity == null) {
      return new User();
    }

    await userManager.ResetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);
    var unformattedKey = await userManager.GetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);

    return new User {
      TfaToken = $"otpauth://totp/{HttpUtility.UrlEncode(userEntity.Email)}?secret={unformattedKey}&issuer=Zuhid&digits=6"
    };
  }

  [HttpPut("VerifyQrCode")]
  public async Task<bool> VerifyQrCode(User user) {
    ArgumentNullException.ThrowIfNull(user);
    var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
    return userEntity != null
&& await userManager.VerifyTwoFactorTokenAsync(userEntity, userManager.Options.Tokens.AuthenticatorTokenProvider, user.PhoneToken).ConfigureAwait(false);
  }





  // [AllowAnonymous]
  // [HttpPost("Login")]
  // public async Task<LoginResponse> Login(User user)
  // {
  //     var loginResponse = new LoginResponse();
  //     ArgumentNullException.ThrowIfNull(user);
  //     var signInResult = await signInManager.PasswordSignInAsync(user.Email, user.Password, true, false).ConfigureAwait(false);
  //     if (signInResult.Succeeded)
  //     {
  //         var userEntity = await userManager.FindByNameAsync(user.Email).ConfigureAwait(false);
  //         if (userEntity != null)
  //         {
  //             loginResponse.AuthToken = tokenService.Build(userEntity.Id,
  //             [
  //                 new("userName", userEntity.UserName ?? ""),
  //                 new("email", userEntity.Email ?? ""),
  //                 new("phoneNumber", userEntity.PhoneNumber ?? ""),
  //             ], []);
  //         }
  //     }
  //     return loginResponse;
  // }

  // [HttpPut("SmsToken")]
  // public async Task<bool> SmsToken(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return false;
  //     var result = await userManager.GenerateTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);
  //     await messageService.SendSms(userEntity.PhoneNumber ?? "", $"Your verification code is {result}").ConfigureAwait(false);
  //     return true;
  // }

  // [HttpPut("EmailToken")]
  // public async Task<bool> EmailToken(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return false;
  //     var result = await userManager.GenerateTwoFactorTokenAsync(userEntity, TokenOptions.DefaultEmailProvider).ConfigureAwait(false);
  //     await messageService.SendEmail("Your verification code", $"Your verification code is {result}", userEntity.Email ?? "").ConfigureAwait(false);
  //     return true;
  // }



  // [HttpGet()]
  // public async Task<List<User>> Get(Guid? id)
  // {
  //     return await userRepository.Get().ConfigureAwait(false);
  // }

  // [HttpGet("IsPasswordValid")]
  // public async Task<bool> IsPasswordValid(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByNameAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null) return false;
  //     return await userManager.CheckPasswordAsync(userEntity, user.Password).ConfigureAwait(false);
  // }

  // // [HttpGet("EmailToken")]
  // // public async Task<string> EmailTokenGet(User user)
  // // {
  // //     ArgumentNullException.ThrowIfNull(user);
  // //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  // //     if (userEntity == null) return string.Empty;
  // //     return await userManager.GenerateEmailConfirmationTokenAsync(userEntity).ConfigureAwait(false);
  // // }

  // // [HttpGet("IsEmailTokenValid")]
  // // public async Task<bool> IsEmailTokenValid(User user)
  // // {
  // //     ArgumentNullException.ThrowIfNull(user);
  // //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  // //     if (userEntity == null) return false;
  // //     var result = await userManager.ConfirmEmailAsync(userEntity, user.EmailToken).ConfigureAwait(false);
  // //     return result.Succeeded;
  // // }

  // // [HttpGet("PhoneToken")]
  // // public async Task<string> PhoneToken(User user)
  // // {
  // //     ArgumentNullException.ThrowIfNull(user);
  // //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  // //     if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return string.Empty;
  // //     return await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, userEntity.PhoneNumber).ConfigureAwait(false);
  // // }

  // [HttpGet("IsPhoneTokenValid")]
  // public async Task<bool> IsPhoneTokenValid(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) return false;
  //     var result = await userManager.VerifyChangePhoneNumberTokenAsync(userEntity, user.PhoneToken, userEntity.PhoneNumber).ConfigureAwait(false);
  //     if (result && !userEntity.PhoneNumberConfirmed)
  //     {
  //         userEntity.PhoneNumberConfirmed = true;
  //         await userManager.UpdateAsync(userEntity).ConfigureAwait(false);
  //     }
  //     return result;
  // }

  // [HttpGet("TfaToken")]
  // public async Task<string> TfaToken(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null) return string.Empty;
  //     // await userManager.ResetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);
  //     var result = await userManager.GetAuthenticatorKeyAsync(userEntity).ConfigureAwait(false);
  //     return result ?? string.Empty;
  // }

  // [HttpGet("VerifyTfaToken")]
  // public async Task<bool> VerifyTfaToken(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null || string.IsNullOrWhiteSpace(user.TfaToken)) return false;
  //     var result = await userManager.VerifyTwoFactorTokenAsync(userEntity, userManager.Options.Tokens.AuthenticatorTokenProvider, user.TfaToken).ConfigureAwait(false);
  //     return result;
  // }

  // [HttpPost]
  // public async Task<SaveRespose> Add([NotNull] User user)
  // {
  //     var userEntity = userMapper.GetEntity(user);
  //     // create the entity
  //     var result = await userManager.CreateAsync(userEntity, user.Password).ConfigureAwait(false); // create the user
  //     foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description); // Add any errors to ModelState
  //     if (result.Errors.ToArray().Length == 0)
  //     {
  //         // Email token
  //         var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(userEntity).ConfigureAwait(false);
  //         string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailToken));
  //         await messageService.SendEmail("Your verification code", $"<a href='http://localhost:4200/identity/verify-email?email={userEntity.Email}&emailToken={encodedToken}'>Click to veirfy your Email</a>", userEntity.Email ?? "").ConfigureAwait(false);
  //     }

  //     // return response
  //     return new SaveRespose { Updated = userEntity.UpdatedDate }; // return the reponse
  // }


  // [HttpPut("VerifyEmail")]
  // public async Task<bool> VerifyEmail(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null) return false;
  //     var emailToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(user.EmailToken));

  //     var result = await userManager.ConfirmEmailAsync(userEntity, emailToken).ConfigureAwait(false);
  //     return result.Succeeded;
  // }

  // [HttpPut("CreatePhone")]
  // public async Task<bool> CreatePhone(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null) return false;
  //     var identityResult = await userManager.SetPhoneNumberAsync(userEntity, user.Phone).ConfigureAwait(false);
  //     if (identityResult.Succeeded)
  //     {
  //         var result = await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, user.Phone).ConfigureAwait(false);
  //         return await messageService.SendSms(userEntity.PhoneNumber ?? "", $"Your phone verification token is {result}").ConfigureAwait(false);
  //     }
  //     return false;
  // }


  // [HttpPut("VerifyPhone")]
  // public async Task<bool> VerifyPhone(User user)
  // {
  //     ArgumentNullException.ThrowIfNull(user);
  //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  //     if (userEntity == null) return false;
  //     var identityResult = await userManager.ChangePhoneNumberAsync(userEntity, user.Phone, user.PhoneToken).ConfigureAwait(false);
  //     return identityResult.Succeeded;
  // }

  // // [HttpPut("SendPhoneToken")]
  // // public async Task<bool> SendPhoneToken(User user)
  // // {
  // //     ArgumentNullException.ThrowIfNull(user);
  // //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  // //     if (userEntity == null || string.IsNullOrWhiteSpace(userEntity.PhoneNumber) && userEntity.PhoneNumberConfirmed) return false;
  // //     var result = await userManager.GenerateTwoFactorTokenAsync(userEntity, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);
  // //     await messageService.SendSms(userEntity.PhoneNumber ?? "", $"Your verification code is {result}").ConfigureAwait(false);
  // //     return true;
  // // }




  // // [HttpPut("VerifyEmailOld")]
  // // public async Task<bool> VerifyEmailOld(User user)
  // // {
  // //     ArgumentNullException.ThrowIfNull(user);
  // //     var userEntity = await userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
  // //     if (userEntity == null) return false;
  // //     var result = await userManager.VerifyTwoFactorTokenAsync(userEntity, TokenOptions.DefaultEmailProvider, user.EmailToken).ConfigureAwait(false);
  // //     return result;
  // // }


  // [HttpPut]
  // public async Task<SaveRespose> Update(User user)
  // {
  //     return await identityRepository.Update(userMapper.GetEntity(user)).ConfigureAwait(false);
  // }

  // [HttpDelete("Id/{id}")]
  // public async Task Delete(Guid id)
  // {
  //     await identityRepository.Delete(id).ConfigureAwait(false);
  // }
}

using System.ComponentModel.DataAnnotations;
using Zuhid.BaseApi;

namespace Zuhid.Identity.Models;

public class User : BaseModel
{
    public bool IsActive { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string EmailToken { get; set; } = string.Empty;
    public string PhoneToken { get; set; } = string.Empty;
    public string TfaToken { get; set; } = string.Empty;
}

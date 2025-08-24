using System.ComponentModel.DataAnnotations;
using Zuhid.BaseApi;

namespace Zuhid.Identity.Models;

public class User : BaseModel
{
    public bool IsActive { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Phone { get; set; } = "";
    public string EmailToken { get; set; } = "";
    public string PhoneToken { get; set; } = "";
    public string TfaToken { get; set; } = "";
}

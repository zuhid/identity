namespace Zuhid.BaseApi;

public class IdentityModel
{
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string SymmetricKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public double ExpireMinutes { get; set; } = 60;
}
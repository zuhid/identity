using OtpNet;

internal class TotpServer {
  public static void Run() {
    // Your base32 secret key
    var secretKey = "C2OMCUHLEPOBTRTGFSOJASVGTWRDJ4LO";

    // Convert the base32 key to bytes
    var bytes = Base32Encoding.ToBytes(secretKey);

    // Create a TOTP generator
    var totp = new Totp(bytes);

    // Get the current TOTP code
    var code = totp.ComputeTotp();

    Console.WriteLine("Current TOTP code: " + code); // 677426
  }
}

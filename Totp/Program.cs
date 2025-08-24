using OtpNet;
using System;

class Program
{
  static void Main()
  {
    // Your base32 secret key
    string secretKey = "C2OMCUHLEPOBTRTGFSOJASVGTWRDJ4LO";

    // Convert the base32 key to bytes
    byte[] bytes = Base32Encoding.ToBytes(secretKey);

    // Create a TOTP generator
    var totp = new Totp(bytes);

    // Get the current TOTP code
    string code = totp.ComputeTotp();

    Console.WriteLine("Current TOTP code: " + code); // 677426
  }
}

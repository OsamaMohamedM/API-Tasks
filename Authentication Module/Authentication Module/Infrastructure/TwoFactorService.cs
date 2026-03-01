using OtpNet;
using QRCoder;
using System;

namespace Authentication_Module.Infrastructure
{
    public class TwoFactorService : Domain.Interfaces.Services.ITwoFactorService
    {
        public string GenerateSecret()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        public string GetQrCodeBase64(string email, string secret)
        {
            var authUrl = $"otpauth://totp/HRApp:{email}?secret={secret}&issuer=HRManagement";

            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(authUrl, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            return Convert.ToBase64String(qrCodeImage);
        }

        public bool ValidateCode(string secret, string code)
        {
            var secretBytes = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretBytes);
            return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2));
        }
    }
}
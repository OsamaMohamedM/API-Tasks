namespace Authentication_Module.Domain.Interfaces.Services
{
    public interface ITwoFactorService
    {
        string GenerateSecret();
        string GetQrCodeBase64(string email, string secret);
        bool ValidateCode(string secret, string code);
    }
}

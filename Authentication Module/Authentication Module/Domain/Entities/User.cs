namespace Authentication_Module.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }
        public string Role { get; set; } = "User";
        public bool TwoFactorEnabled { get; set; }
        public string? TwoFactorSecret { get; set; }
    }
}
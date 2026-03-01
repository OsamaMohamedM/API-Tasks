using System.ComponentModel.DataAnnotations;

namespace Authentication_Module.Domain.DTO
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
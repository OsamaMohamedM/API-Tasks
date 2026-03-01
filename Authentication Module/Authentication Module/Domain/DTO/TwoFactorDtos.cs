namespace Authentication_Module.Domain.DTO
{
    public class Verify2FADto
    {
        public string Code { get; set; }
    }

    public class Login2FADto
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}

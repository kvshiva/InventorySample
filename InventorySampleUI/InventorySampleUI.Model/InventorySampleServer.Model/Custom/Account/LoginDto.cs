namespace Model.Custom.Account
{
    public class LoginDto
    {
        public int Code { get; set; }
        public string Password { get; set; } = null!;
        public bool IsRememberMe { get; set; }
    }
}

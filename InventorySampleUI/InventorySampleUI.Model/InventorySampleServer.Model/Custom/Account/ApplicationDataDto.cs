namespace ZafarTC.Model.Custom.Account
{
    public class ApplicationDataDto
    {
        public bool IsRememberMe { get; set; } = false;
        public int UserId { get; set; } = 0;
        public int UserCode { get; set; } = 0;
        public string UserFullName { get; set; } = null!;
        public string? UserMobile { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? UserPicture { get; set; }
        public string AppVersion { get; set; } = null!;
        public string CallCenter { get; set; } = null!;
        public int PageSize { get; set; }

    }
}

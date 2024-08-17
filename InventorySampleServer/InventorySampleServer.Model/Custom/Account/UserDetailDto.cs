namespace Model.Custom.Account
{
    public class UserDetailDto : BaseModel
    {
        public int Code { get; set; }
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AppVersion { get; set; } = "1.0.0.0";
        public bool IsActive { get; set; }
        public bool IsRememberMe { get; set; }
        public List<OperationRoleDto> UserOperationRoleList { get; set; } = null!;
    }
}

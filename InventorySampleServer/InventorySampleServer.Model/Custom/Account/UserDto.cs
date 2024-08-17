namespace Model.Custom.Account
{
    public class UserDto : BaseModel
    {
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; } = null!;
        public string? Picture { get; set; } 
        public int Code { get; set; } 
        public bool IsActive { get; set; }
    }
}

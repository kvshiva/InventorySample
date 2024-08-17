using Model;

namespace ZafarTC.Model.Custom.Account
{
    public class UserPhoneDto : BaseModel
    {
        public int Code { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; } = null!;
        public string FullName { get; set; } = null!;

    }
}

namespace ZafarTC.Model.Custom.Account
{
    public class AppProfileDto
    {
        public int Code { get; set; }
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; } = null!;
        public string? Picture { get; set; }
        public bool IsActive { get; set; }

    }
}

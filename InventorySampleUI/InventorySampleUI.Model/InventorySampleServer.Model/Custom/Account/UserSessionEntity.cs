namespace Model.Custom.Account
{
    public class UserSessionEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; } = null!;
        public string Agent { get; set; } = null!;
        public string StartDateTime { get; set; } = null!;
        public string EndDateTime { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}

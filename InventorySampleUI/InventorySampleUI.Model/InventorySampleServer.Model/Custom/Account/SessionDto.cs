namespace Model.Custom.Account
{
    public class SessionDto
    {
        public string IP { get; set; } = null!;
        public string UserDeviceName { get; set; } = null!;
        public string? UserDeviceType { get; set; } = null!;
    }
}

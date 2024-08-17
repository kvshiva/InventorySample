namespace Model.Custom.Rahkaran
{
    public class UserListDto
    {
        public long EmployeeID { get; set; }
        public long UserID { get; set; }
        public string? Code { get; set; } = null!;
        public string? NationalID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; } = null!;
    }
}

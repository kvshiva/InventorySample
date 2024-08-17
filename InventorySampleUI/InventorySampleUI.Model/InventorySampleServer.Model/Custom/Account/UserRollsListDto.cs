namespace Model.Custom.Account
{
    public class UserRollsListDto 
    {
        public int OperationRoleTypeEnumId { get; set; }
        public string OperationRoleTypeEnumTitle { get; set; } = null!;
        public int UserId { get; set; }
    }
}

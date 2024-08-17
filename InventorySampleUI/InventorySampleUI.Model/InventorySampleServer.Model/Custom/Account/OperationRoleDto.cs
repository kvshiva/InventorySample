namespace Model.Custom.Account
{
    public class OperationRoleDto
    {
        public int OperationRoleTypeEnumId { get; set; }
        public int WorkUnitId { get; set; }
        public string OperationRoleTypeEnumTitle { get; set; } = null!;
        public string WorkUnitTitle { get; set; } = null!;
    }
}

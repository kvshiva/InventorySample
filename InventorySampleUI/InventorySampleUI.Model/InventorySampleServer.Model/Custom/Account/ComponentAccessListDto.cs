namespace Model.Custom.Account
{
    public class ComponentAccessListDto : BaseModel
    {
        public string FieldName { get; set; } = null!;
        public string PermissionTypeTitle { get; set; } = null!;
        public string ComponentTitle { get; set; } = null!;
        public int ComponentAccessId { get; set; }
        public int PermissionTypeEnumId { get; set; }
    }
}

namespace Model.Custom.Account
{
    public class MenuAccessListDto : BaseModel
    {
        public string Title { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public int? ParentId { get; set; }
        public int MenuAccessId { get; set; }
        public int OperationRoleTypeEnumId { get; set; }
    }
}

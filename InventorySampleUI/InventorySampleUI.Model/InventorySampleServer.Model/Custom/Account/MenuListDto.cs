namespace Model.Custom.Account
{
    public class MenuListDto : BaseModel
    {
        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Icon { get; set; }
        public string? RouterLink { get; set; }
        public bool HasSubMenu { get; set; }
    }
}

namespace Model.Custom.State
{
    public class UserChangeStateDto : BaseModel
    {
        public int StateTransitionId { get; set; }
        public int EntityEnumId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
    }
}

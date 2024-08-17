namespace Model.Custom.State
{
    public class EntityStateTransitionEntity : BaseModel
    {
        public int EntityEnumId { get; set; }
        public int StateTransitionId { get; set; }
        public int? UserId { get; set; }
        public long RecordId { get; set; }
        public string Comment { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string Time { get; set; } = null!;
        public int? StateEnumId { get; set; }
    }
}

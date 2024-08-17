namespace Model.Custom.State
{
    public class StateTransitionListDto : BaseModel
    {
        public string Title { get; set; } = null!;
        public string SourceStateTitle { get; set; } = null!;
        public string TargetStateTitle { get; set; } = null!;
        public int TargetStateId { get; set; }
        public int SourceStateId { get; set; }
        public int SourceStateEnumId { get; set; }
        public int TargetStateEnumId { get; set; }
        public bool CommentNeeded { get; set; }
    }
}

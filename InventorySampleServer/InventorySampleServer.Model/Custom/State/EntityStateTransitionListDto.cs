namespace Model.Custom.State
{
    public class EntityStateTransitionListDto : BaseModel
    {
        public long RecordId { get; set; }
        public string? Comment { get; set; }
        public string Date { get; set; } = null!;
        public string Time { get; set; } = null!;
        public string EntityTitle { get; set; } = null!;
        public string StateTransitionTitle { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string StateTitle { get; set; } = null!;
        public string SourceStateTitle { get; set; } = null!;
        public string TargetStateTitle { get; set; } = null!;
        public int? StateEnumId { get; set; }
    }
}

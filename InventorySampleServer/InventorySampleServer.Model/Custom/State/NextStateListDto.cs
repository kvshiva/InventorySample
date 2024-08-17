namespace Model.Custom.State
{
    public class NextStateListDto : BaseModel
    {
        public int StateTransitionId { get; set; }
        public string Title { get; set; } = null!;
    }
}

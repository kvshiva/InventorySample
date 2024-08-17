namespace Model.Custom.State
{
    public class CurrentStateDto : BaseModel
    {
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool IsStartState { get; set; }
        public bool IsFinishState { get; set; }
        public int StateOrder { get; set; }
        public int StateMachineId { get; set; }
        public string StateEnumTitle { get; set; } = null!;
        public string StateMachineTitle { get; set; } = null!;
    }
}

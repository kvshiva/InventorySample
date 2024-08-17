namespace Model.Custom.State
{
    public class StateMachineDto: BaseModel
    {
        public string Title { get; set; } = null!;
        public int EntityId { get; set; }
    }
}

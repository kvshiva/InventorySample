namespace Model.Custom.Other
{
    public class EntityDto : BaseModel
    {
        public string Title { get; set; } = null!;
        public string EntitySchema { get; set; } = null!;
        public string Prefix { get; set; } = null!;
        public int? CounterLength { get; set; }
    }
}

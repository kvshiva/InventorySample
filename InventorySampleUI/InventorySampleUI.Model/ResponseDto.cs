namespace InventorySampleUI.Model
{
    public class ResponseDto
    {
        public bool IsSucceed { get; set; }
        public int Count { get; set; }
        public string Message { get; set; } = null!;
        public object? Data { get; set; } = null!;
        public List<string> ErrorList { get; set; } = null!;
    }
}

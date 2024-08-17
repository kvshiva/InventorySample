using Common.DTO;

namespace Common
{
    public class ResultDto
    {
        public bool IsSucceed { get; set; }
        public int Count { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public object? Data { get; set; } = null!;
        public List<ErrorDto> ErrorList { get; set; } = null!;
    }
}

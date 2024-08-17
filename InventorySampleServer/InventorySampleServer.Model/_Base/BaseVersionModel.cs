namespace Model
{
	public class BaseVersionModel : BaseModel
	{
		public byte[] Version { get; set; } = null!;
		public string CreatedBy { get; set; } = null!;
		public string CreatedDateTime { get; set; } = null!;
		public string? UpdatedBy { get; set; }
		public string? UpdatedDateTime { get; set; }
	}
}

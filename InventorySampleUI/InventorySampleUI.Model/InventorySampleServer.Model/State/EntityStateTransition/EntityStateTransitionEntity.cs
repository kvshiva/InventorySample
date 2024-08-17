using Model;

namespace InventorySampleServer.Model.State.EntityStateTransition
{
	public class EntityStateTransitionEntity : BaseVersionModel
	{
		public int EntityEnumId { get; set; } 
		public int StateTransitionId { get; set; } 
		public int? UserId { get; set; }
		public long RecordId { get; set; } 
		public string? Comment { get; set; }
		public string? Date { get; set; }
		public string? Time { get; set; }
		public int? StateEnumId { get; set; }
		public DateTime? RecordDateTime { get; set; }
	}
}

using Model;

namespace InventorySampleServer.Model.State.EntityStateTransition
{
	public class EntityStateTransitionListDto : BaseVersionModel
	{
		public int EntityEnumId { get; set; } 
		public string EntityEnumTitle { get; set; } = null!;
		public int StateTransitionId { get; set; } 
		public string StateTransitionTitle { get; set; } = null!;
		public int? UserId { get; set; }
		public string UserFullName { get; set; } = null!;
		public long RecordId { get; set; } 
		public string? Comment { get; set; }
		public string? Date { get; set; }
		public string? Time { get; set; }
		public int? StateEnumId { get; set; }
		public string StateEnumTitle { get; set; } = null!;
		public DateTime? RecordDateTime { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
	public class BaseEntity
	{
		public virtual DateTime? CreatedAt { get; set; }
		public virtual string? CreatedBy { get; set; } = string.Empty;
		public virtual DateTime? ModifiedAt { get; set; }
		public virtual string? ModifiedBy { get; set; } =string.Empty;
	
		[NotMapped]
		public virtual long Id { get; set; }

		
	}
}

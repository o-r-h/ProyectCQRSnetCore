using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
	public partial class User//: BaseEntity
	{
		public long UserId { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public long? RolId { get; set; }
		public long UserStatusId { get; set; }
		public int? AccessFailedCount { get; set; }
		public int? UserLocked { get; set; }
		public bool EmailConfirmed { get; set; }
		public Guid? RecoveryToken { get; set; }
		public DateTime? TokenExpiration { get; set; }


		public virtual Rol Rol { get; set; }
		public virtual UserStatus UserStatus { get; set; } 
	}
}

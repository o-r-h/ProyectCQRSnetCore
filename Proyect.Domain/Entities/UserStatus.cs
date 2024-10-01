
namespace Project.Domain.Entities
{
	public partial class UserStatus
	{
		public UserStatus()
		{
			Users = new HashSet<User>();
		}

		public long UserStatusId { get; set; }
		public string Name { get; set; } = string.Empty;

		public virtual ICollection<User> Users { get; set; }
	}
}

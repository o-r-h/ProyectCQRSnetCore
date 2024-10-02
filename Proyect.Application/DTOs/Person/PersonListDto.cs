

namespace Project.Application.DTOs.Person
{
	public class PersonListDto
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Email { get; set; }

		public int DaysUntilBirthDate {
			get {
				DateTime today = DateTime.Today;
				DateTime nextBirthday = new DateTime(today.Year, BirthDate.Month, BirthDate.Day);
				if (nextBirthday < today)
					nextBirthday = nextBirthday.AddYears(1);
				int daysLeft = (nextBirthday - today).Days;
				return daysLeft;
		
			}
		}
	}
}

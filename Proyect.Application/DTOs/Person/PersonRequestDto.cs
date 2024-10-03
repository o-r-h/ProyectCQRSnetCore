namespace Project.Application.DTOs.Person
{
    public class PersonRequestDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

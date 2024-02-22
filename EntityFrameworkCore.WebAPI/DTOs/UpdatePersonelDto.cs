namespace EntityFrameworkCore.WebAPI.DTOs
{
    public class UpdatePersonelDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FulName => string.Join("", FirstName, LastName);

        public string Email { get; set; } = string.Empty;

    }
}

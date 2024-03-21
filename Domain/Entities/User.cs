namespace Domain.Entities
{
	public class User : BaseEntity
	{
        public required string Username { get; set; }
        public Guid ApiKey { get; set; } = Guid.NewGuid();
        public string Fullname { get; set; } = string.Empty;
        public required string Email { get; set; }
        public required string PasswordSalt { get; set; }
        public required string PasswordHash { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
    }
}


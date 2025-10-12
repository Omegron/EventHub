namespace EventHub.DTOs
{
    public class UserViewDto
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required ICollection<string> Roles { get; set; }
    }
}

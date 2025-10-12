namespace EventHub.DTOs
{
    public class UserUsernameUpdateDto
    {
        public required string Id { get; set; }
        public required string NewUsername { get; set; }
    }
}

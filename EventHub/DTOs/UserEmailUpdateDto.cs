namespace EventHub.DTOs
{
    public class UserEmailUpdateDto
    {
        public required string Id { get; set; }
        public required string NewEmail { get; set; }
    }
}

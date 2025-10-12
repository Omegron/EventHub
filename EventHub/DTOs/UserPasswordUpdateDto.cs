namespace EventHub.DTOs
{
    public class UserPasswordUpdateDto
    {
        public required string Id { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmNewPassword { get; set; }
    }
}

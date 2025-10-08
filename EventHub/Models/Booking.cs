namespace EventHub.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

        public required User User { get; set; }
        public required Event Event { get; set; }
    }
}

namespace EventHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string HashPassword { get; set; }

        public ICollection<Booking> Bookings { get; set; } = []; //ordinary user
        public ICollection<Event> OrganizedEvents { get; set; } = []; //organizer
        public ICollection<Venue> CreatedVenues { get; set; } = []; //admin
    }
}

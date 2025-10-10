namespace EventHub.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public DateTime BookingEndDate { get; set; }
        public int OrganizerId { get; set; }
        public ICollection<Booking> Bookings { get; set; } = [];

        public required User Organizer { get; set; }
    }
}

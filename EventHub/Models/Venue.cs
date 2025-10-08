namespace EventHub.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public int Capacity { get; set; }

        public ICollection<Event> Events { get; set; } = [];
    }
}

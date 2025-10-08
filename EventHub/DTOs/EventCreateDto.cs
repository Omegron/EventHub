namespace EventHub.DTOs
{
    public class EventCreateDto
    {
        public required string Title { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public DateTime BookingEndDate { get; set; }
        public int OrganizerId { get; set; }
        public int? VenueId { get; set; }
    }
}

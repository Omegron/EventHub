namespace EventHub.DTOs
{
    public class EventViewDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public DateTime BookingEndDate { get; set; }
    }
}

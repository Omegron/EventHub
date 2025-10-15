using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventViewDto> GetEventByIdAsync(int id); //HomeC, EventsC, AdminC
        Task<IEnumerable<EventViewDto>> GetEventsByOrganizerIdAsync(string id); //HomeC, EventsC, AdminC
        Task<IEnumerable<EventViewDto>> GetAllEventsAsync(); //HomeC, AdminC
        Task<int> GetEventFreeSeatsAsync(int id); //HomeC, EventsC, BookingsC
        Task CreateEventAsync(EventCreateDto dto); //EventsC
        Task UpdateEventTitleAsync(EventTitleUpdateDto dto); //EventsC
        Task UpdateEventCapacityAsync(EventCapacityUpdateDto dto); //EventsC
        Task UpdateEventDateAsync(EventDateUpdateDto dto); //EventsC
        Task UpdateEventBookingEndDateAsync(EventBookingEndDateUpdateDto dto); //EventsC
        Task DeleteEventAsync(int id); //EventsC, AdminC
    }
}

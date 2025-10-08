using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventViewDto> GetEventByIdAsync(int id);
        Task<IEnumerable<EventViewDto>> GetEventsByOrganizerIdAsync(int id);
        Task<IEnumerable<EventViewDto>> GetAllEventsAsync();
        Task<int> GetEventFreeSeatsAsync(int id);
        Task CreateEventAsync(EventCreateDto dto);
        Task UpdateEventTitleAsync(EventTitleUpdateDto dto);
        Task UpdateEventCapacityAsync(EventCapacityUpdateDto dto);
        Task UpdateEventDateAsync(EventDateUpdateDto dto);
        Task UpdateEventBookingEndDateAsync(EventBookingEndDateUpdateDto dto);
        Task DeleteEventAsync(int id);

    }
}

using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingViewDto> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingViewDto>> GetBookingsByUserIdAsync(string id);
        Task<IEnumerable<BookingViewDto>> GetBookingsByEventIdAsync(int id);
        Task CreateBookingAsync(BookingCreateDto dto);
        Task DeleteBookingAsync(int id);
    }
}

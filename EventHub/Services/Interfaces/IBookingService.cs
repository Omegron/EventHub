using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingViewDto> GetBookingByIdAsync(int id); //BookingsC
        Task<IEnumerable<BookingViewDto>> GetBookingsByUserIdAsync(string id); //BookingsC, AdminC
        Task<IEnumerable<BookingViewDto>> GetBookingsByUserIdAsync(string userId, string organizerId); //EventsC
        Task<IEnumerable<BookingViewDto>> GetBookingsByEventIdAsync(int id); //EventsC, //AdminC
        Task<IEnumerable<BookingViewDto>> GetAllBookingsAsync(); //Admin
        Task CreateBookingAsync(BookingCreateDto dto); //BookingsC
        Task DeleteBookingAsync(int id); //BookingsC, AdminC
    }
}

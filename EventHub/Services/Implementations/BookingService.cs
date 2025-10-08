using AutoMapper;
using EventHub.Data;
using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public BookingService(AppDbContext context, IMapper mapper, IEventService eventService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        public async Task<BookingViewDto> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.FirstAsync(b => b.Id == id);
            return _mapper.Map<BookingViewDto>(booking);
        }
        public async Task<IEnumerable<BookingViewDto>> GetBookingsByUserIdAsync(int id)
        {
            var bookings = await _context.Bookings.Where(b => b.UserId == id).ToListAsync();
            return _mapper.Map<IEnumerable<BookingViewDto>>(bookings);
        }
        public async Task<IEnumerable<BookingViewDto>> GetBookingsByEventIdAsync(int id)
        {
            var bookings = await _context.Bookings.Where(b => b.EventId == id).ToListAsync();
            return _mapper.Map<IEnumerable<BookingViewDto>>(bookings);
        }
        public async Task CreateBookingAsync(BookingCreateDto dto)
        {
            int freeSeats = await _eventService.GetEventFreeSeatsAsync(dto.EventId);
            if (freeSeats <= 0) { throw new Exception("Event don't has free seats"); }
            var ev = await _context.Events.FirstAsync(e => e.Id == dto.EventId);
            if (ev.BookingEndDate < DateTime.Now) { throw new Exception("The booking time has already ended"); }
            var booking = _mapper.Map<Booking>(dto);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FirstAsync(b => b.Id == id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}

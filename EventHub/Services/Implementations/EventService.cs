using AutoMapper;
using EventHub.Data;
using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventHub.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventViewDto> GetEventByIdAsync(int id)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == id);
            return _mapper.Map<EventViewDto>(ev);
        }
        public async Task<IEnumerable<EventViewDto>> GetEventsByOrganizerIdAsync(int id)
        {
            var events = await _context.Events.Where(e => e.OrganizerId == id).ToListAsync();
            return _mapper.Map<IEnumerable<EventViewDto>>(events);
        }
        public async Task<IEnumerable<EventViewDto>> GetAllEventsAsync()
        {
            var events = await _context.Events.ToListAsync();
            return _mapper.Map<IEnumerable<EventViewDto>>(events);
        }
        public async Task<int> GetEventFreeSeatsAsync(int id)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == id);
            var bookings = await _context.Bookings.Where(b => b.EventId == ev.Id).ToListAsync();
            return ev.Capacity - bookings.Count;
        }
        public async Task CreateEventAsync(EventCreateDto dto)
        {
            if (dto.Date < DateTime.Now) { throw new Exception("Cannot make event's date in the past"); }
            if (dto.Date < dto.BookingEndDate) { throw new Exception("Booking end date cannot be after the event"); }
            var ev = _mapper.Map<Event>(dto);
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventTitleAsync(EventTitleUpdateDto dto)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == dto.Id);
            ev.Title = dto.Title;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventCapacityAsync(EventCapacityUpdateDto dto)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == dto.Id);
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.Id == ev.VenueId);
            if (venue == null || dto.Capacity <= venue.Capacity)
            {
                ev.Capacity = dto.Capacity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Venue's capacity is too small");
            }
        }
        public async Task UpdateEventDateAsync(EventDateUpdateDto dto)
        {
            if (dto.Date < DateTime.Now) { throw new Exception("Cannot make event's date in the past"); }
            var ev = await _context.Events.FirstAsync(e => e.Id == dto.Id);
            if (dto.Date < ev.BookingEndDate) { throw new Exception("Booking end date cannot be after the event"); }
            ev.Date = dto.Date;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventBookingEndDateAsync(EventBookingEndDateUpdateDto dto)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == dto.Id);
            if (ev.Date < dto.BookingEndDate) { throw new Exception("Booking end date cannot be after the event"); }
            ev.BookingEndDate = dto.BookingEndDate;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FirstAsync(e => e.Id == id);
            await _context.Bookings.Where(b => b.EventId == id).ExecuteDeleteAsync();
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}

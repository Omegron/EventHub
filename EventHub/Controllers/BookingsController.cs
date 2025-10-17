using EventHub.DTOs;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventHub.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IEventService _eventService;

        public BookingsController(IBookingService bookingService, IEventService eventService)
        {
            _bookingService = bookingService;
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> IndexByUser(string id)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(id);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> EventFreeSeats(int id)
        {
            int freeSeats = await _eventService.GetEventFreeSeatsAsync(id);
            return View(freeSeats);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateDto dto)
        {
            await _bookingService.CreateBookingAsync(dto);
            return RedirectToAction(nameof(IndexByUser), new { id = dto.UserId });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return View();
        }
    }
}

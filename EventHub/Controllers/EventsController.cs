using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Implementations;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EventHub.Controllers
{
    public class EventsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IBookingService _bookingService;
        private readonly IVenueService _venueService;

        public EventsController(IUserService userService, IEventService eventService, IBookingService bookingService, IVenueService venueService)
        {
            _userService = userService;
            _eventService = eventService;
            _bookingService = bookingService;
            _venueService = venueService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> IndexByOrganizer(string id)
        {
            var ev = await _eventService.GetEventsByOrganizerIdAsync(id);
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> IndexUsersByEvent(int id)
        {
            var users = await _userService.GetUsersByEventIdAsync(id);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> IndexBookingsByUserAndOrganiser(string userId, string organizerId)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId, organizerId);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> IndexBookingsByEvent(int id)
        {
            var bookings = await _bookingService.GetBookingsByEventIdAsync(id);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> VenueDetails(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            return View(venue);
        }

        [HttpGet]
        public async Task<IActionResult> IndexVenues()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            return View(venues);
        }

        [HttpGet]
        public async Task<IActionResult> FreeSeats(int id)
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
        public async Task<IActionResult> Create(EventCreateDto dto)
        {
            await _eventService.CreateEventAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditTitle(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            var dto = new EventTitleUpdateDto
            {
                Id = ev.Id,
                Title = ev.Title
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditTitle(EventTitleUpdateDto dto)
        {
            await _eventService.UpdateEventTitleAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditCapacity(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            var dto = new EventCapacityUpdateDto
            {
                Id = ev.Id,
                Capacity = ev.Capacity
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditCapacity(EventCapacityUpdateDto dto)
        {
            await _eventService.UpdateEventCapacityAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditDate(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            var dto = new EventDateUpdateDto
            {
                Id = ev.Id,
                Date = ev.Date
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditDate(EventDateUpdateDto dto)
        {
            await _eventService.UpdateEventDateAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditBookingEndDate(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            var dto = new EventBookingEndDateUpdateDto
            {
                Id = ev.Id,
                BookingEndDate = ev.BookingEndDate
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditBookingEndDate(EventBookingEndDateUpdateDto dto)
        {
            await _eventService.UpdateEventBookingEndDateAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

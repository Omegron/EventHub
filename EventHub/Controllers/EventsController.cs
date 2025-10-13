using EventHub.DTOs;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EventHub.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> IndexByOrganizer(string id)
        {
            var ev = await _service.GetEventsByOrganizerIdAsync(id);
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAll()
        {
            var events = await _service.GetAllEventsAsync();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> FreeSeats(int id)
        {
            int freeSeats = await _service.GetEventFreeSeatsAsync(id);
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
            await _service.CreateEventAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditTitle(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
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
            await _service.UpdateEventTitleAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditCapacity(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
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
            await _service.UpdateEventCapacityAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditDate(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
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
            await _service.UpdateEventDateAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditBookingEndDate(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
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
            await _service.UpdateEventBookingEndDateAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

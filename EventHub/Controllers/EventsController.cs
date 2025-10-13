using EventHub.DTOs;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _service.GetEventByIdAsync(id);
            return View(ev);
        }

        public async Task<IActionResult> Index(string id)
        {
            var ev = await _service.GetEventsByOrganizerIdAsync(id);
            return View(ev);
        }

        public async Task<IActionResult> Index()
        {
            var events = await _service.GetAllEventsAsync();
            return View(events);
        }

        public async Task<IActionResult> FreeSeats(int id)
        {
            int freeSeats = await _service.GetEventFreeSeatsAsync(id);
            return View(freeSeats);
        }

        public async Task<IActionResult> Create(EventCreateDto dto)
        {
            await _service.CreateEventAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditTitle(EventTitleUpdateDto dto)
        {
            await _service.UpdateEventTitleAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        public async Task<IActionResult> EditCapacity(EventCapacityUpdateDto dto)
        {
            await _service.UpdateEventCapacityAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        public async Task<IActionResult> EditDate(EventDateUpdateDto dto)
        {
            await _service.UpdateEventDateAsync(dto);
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

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

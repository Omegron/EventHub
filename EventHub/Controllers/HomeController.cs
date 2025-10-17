using System.Diagnostics;
using EventHub.Models;
using Microsoft.AspNetCore.Mvc;
using EventHub.Services.Interfaces;

namespace EventHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> EventDetails(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            return View(ev);
        }

        [HttpGet]
        public async Task<IActionResult> IndexEventsByOrganizer(string id)
        {
            var events = await _eventService.GetEventsByOrganizerIdAsync(id);
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> EventFreeSeats(int id)
        {
            int freeSeats = await _eventService.GetEventFreeSeatsAsync(id);
            return View(freeSeats);
        }
    }
}

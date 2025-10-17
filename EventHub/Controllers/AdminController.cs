using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Implementations;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace EventHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IBookingService _bookingService;
        private readonly IVenueService _venueService;

        public AdminController(IUserService userService, IEventService eventService, IBookingService bookingService, IVenueService venueService)
        {
            _userService = userService;
            _eventService = eventService;
            _bookingService = bookingService;
            _venueService = venueService;
        }

        //User
        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> IndexUsersByEvent(int id)
        {
            var users = await _userService.GetUsersByEventIdAsync(id);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> IndexUsersByRole(string role)
        {
            var users = await _userService.GetUserRoleAsync(role);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserRole(string id)
        {
            var role = await _userService.GetUserRoleAsync(id);
            return View(role);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return View(); //add different redirects
        }

        //Event
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

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return View(); //add different redirects
        }

        //Booking
        [HttpGet]
        public async Task<IActionResult> IndexBookingsByUser(string id)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(id);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> IndexBookingsByEvent(int id)
        {
            var bookings = await _bookingService.GetBookingsByEventIdAsync(id);
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return View(); //add different redirects
        }

        //Venue
        [HttpGet]
        public async Task<IActionResult> VenueDetails(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            return View(venue);
        }

        [HttpGet]
        public async Task<IActionResult> IndexAllVenues()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            return View(venues);
        }

        [HttpGet]
        public IActionResult CreateVenue()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(VenueCreateDto dto)
        {
            await _venueService.CreateVenueAsync(dto);
            return RedirectToAction(nameof(IndexAllVenues));
        }

        [HttpGet]
        public async Task<IActionResult> EditVenue(int id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            var dto = new VenueDto
            {
                Id = id,
                Name = venue.Name,
                Address = venue.Address,
                Capacity = venue.Capacity
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditVenue(VenueDto dto)
        {
            await _venueService.UpdateVenueAsync(dto);
            return RedirectToAction(nameof(VenueDetails), new { id = dto.Id });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            await _venueService.DeleteVenueAsync(id);
            return RedirectToAction(nameof(IndexAllVenues));
        }
    }
}

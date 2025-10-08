using AutoMapper;
using EventHub.Data;
using EventHub.DTOs;
using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventHub.Services.Implementations
{
    public class VenueService : IVenueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VenueService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task<VenueDto> GetVenueByIdAsync(int id)
        {
            var venue = await _context.Venues.FirstAsync(v => v.Id == id);
            return _mapper.Map<VenueDto>(venue);
        }
        public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync()
        {
            var venues = await _context.Venues.ToListAsync();
            return _mapper.Map<IEnumerable<VenueDto>>(venues);
        }
        public async Task CreateVenueAsync(VenueCreateDto dto)
        {
            var venue = _mapper.Map<Venue>(dto);
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateVenueAsync(VenueDto dto)
        {
            var venue = await _context.Venues.FirstAsync(v => v.Id == dto.id);
            if (dto.Name != null)
            {
                venue.Name = dto.Name;
            }
            if (dto.Address != null)
            {
                venue.Address = dto.Address;
            }
            if (dto.Capacity != null)
            {
                venue.Capacity = (int)dto.Capacity;
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteVenueAsync(int id)
        {
            var venue = await _context.Venues.FirstAsync(v => v.Id == id);
            var events = await _context.Events.Where(e => e.VenueId == id).ToListAsync();
            foreach (var ev in events)
            {
                ev.VenueId = null;
            }
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }
    }
}

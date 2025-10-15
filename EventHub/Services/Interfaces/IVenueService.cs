using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IVenueService
    {
        Task<VenueDto> GetVenueByIdAsync(int id); //EventsC, AdminC
        Task<IEnumerable<VenueDto>> GetAllVenuesAsync(); //EventsC, AdminC
        Task CreateVenueAsync(VenueCreateDto dto); //AdminC
        Task UpdateVenueAsync(VenueDto dto); //AdminC
        Task DeleteVenueAsync(int id); //AdminC
    }
}

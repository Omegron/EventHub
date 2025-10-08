using EventHub.DTOs;

namespace EventHub.Services.Interfaces
{
    public interface IVenueService
    {
        Task<VenueDto> GetVenueByIdAsync(int id);
        Task<IEnumerable<VenueDto>> GetAllVenuesAsync();
        Task CreateVenueAsync(VenueCreateDto dto);
        Task UpdateVenueAsync(VenueDto dto);
        Task DeleteVenueAsync(int id);
    }
}

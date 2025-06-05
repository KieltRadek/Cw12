using System.Threading.Tasks;
using Cw12.Data.Models;
using Cw12.Services.Dtos;

namespace Cw12.Services
{
    public interface ITripService
    {
        Task<PagedResponse<TripDto>> GetTripsAsync(int page, int pageSize);
        Task<bool> TripExistsAsync(int idTrip);
        Task<Trip> GetTripByIdAsync(int idTrip);
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cw12.Data;
using Cw12.Data.Models;
using Cw12.Services.Dtos;

namespace Cw12.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TripService(ApplicationDbContext db, IMapper mapper)
        {
            _db     = db;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TripDto>> GetTripsAsync(int page, int pageSize)
        {
            var q = _db.Trips
                .Include(t => t.CountryTrips).ThenInclude(ct => ct.Country)
                .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
                .OrderByDescending(t => t.DateFrom);

            var total = await q.CountAsync();
            var allPages = (int)Math.Ceiling(total / (double)pageSize);

            var list = await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<TripDto> {
                PageNum  = page,
                PageSize = pageSize,
                AllPages = allPages,
                Items    = _mapper.Map<List<TripDto>>(list)
            };
        }

        public Task<bool> TripExistsAsync(int idTrip)
            => _db.Trips.AnyAsync(t => t.IdTrip == idTrip);

        public Task<Trip> GetTripByIdAsync(int idTrip)
            => _db.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);
    }
}
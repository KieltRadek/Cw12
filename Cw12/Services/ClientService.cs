using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cw12.Data;
using Cw12.Data.Models;
using Cw12.Services.Dtos;

namespace Cw12.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _db;

        public ClientService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> ClientHasAnyBookingAsync(int idClient)
            => _db.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);

        public async Task<bool> DeleteClientAsync(int idClient)
        {
            var c = await _db.Clients.FindAsync(idClient);
            if (c == null) return false;
            _db.Clients.Remove(c);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> PeselExistsAsync(string pesel)
            => _db.Clients.AnyAsync(c => c.Pesel == pesel);

        public Task<bool> AlreadyBookedAsync(int tripId, string pesel)
            => _db.ClientTrips
                  .Include(ct => ct.Client)
                  .AnyAsync(ct => ct.IdTrip == tripId && ct.Client.Pesel == pesel);

        public async Task<Client> CreateClientAsync(RegisterClientDto dto)
        {
            var c = new Client {
                FirstName = dto.FirstName,
                LastName  = dto.LastName,
                Email     = dto.Email,
                Telephone = dto.Telephone,
                Pesel     = dto.Pesel
            };
            _db.Clients.Add(c);
            await _db.SaveChangesAsync();
            return c;
        }

        public async Task AddClientToTripAsync(Client client, Trip trip, DateTime registeredAt, DateTime? paymentDate)
        {
            var ct = new ClientTrip {
                IdClient     = client.IdClient,
                IdTrip       = trip.IdTrip,
                RegisteredAt = registeredAt,
                PaymentDate  = paymentDate
            };
            _db.ClientTrips.Add(ct);
            await _db.SaveChangesAsync();
        }
    }
}

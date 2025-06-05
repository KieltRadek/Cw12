using System;
using System.Threading.Tasks;
using Cw12.Data.Models;
using Cw12.Services.Dtos;

namespace Cw12.Services
{
    public interface IClientService
    {
        Task<bool> ClientHasAnyBookingAsync(int idClient);
        Task<bool> DeleteClientAsync(int idClient);
        Task<bool> PeselExistsAsync(string pesel);
        Task<bool> AlreadyBookedAsync(int tripId, string pesel);
        Task<Client> CreateClientAsync(RegisterClientDto dto);
        Task AddClientToTripAsync(Client client, Trip trip, DateTime registeredAt, DateTime? paymentDate);
    }
}
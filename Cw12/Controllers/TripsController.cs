using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Cw12.Services;
using Cw12.Services.Dtos;

namespace Cw12.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService   _tripService;
        private readonly IClientService _clientService;

        public TripsController(ITripService tripService, IClientService clientService)
        {
            _tripService   = tripService;
            _clientService = clientService;
        }

        // GET /api/trips?page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResponse<TripDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Parametry page i pageSize muszą być > 0.");

            var resp = await _tripService.GetTripsAsync(page, pageSize);
            return Ok(resp);
        }

        // POST /api/trips/{idTrip}/clients
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> RegisterClient(
            int idTrip,
            [FromBody] RegisterClientDto dto)
        {
            var trip = await _tripService.GetTripByIdAsync(idTrip);
            if (trip == null)
                return NotFound($"Wycieczka o id={idTrip} nie istnieje.");
            if (trip.DateFrom <= DateTime.UtcNow)
                return BadRequest("Nie można zapisać się na wycieczkę, która już się zaczęła.");

            if (await _clientService.PeselExistsAsync(dto.Pesel))
                return Conflict($"Klient z PESEL={dto.Pesel} już istnieje.");
            if (await _clientService.AlreadyBookedAsync(idTrip, dto.Pesel))
                return Conflict("Ten klient jest już zapisany na tę wycieczkę.");

            var client = await _clientService.CreateClientAsync(dto);
            await _clientService.AddClientToTripAsync(client, trip, DateTime.UtcNow, dto.PaymentDate);

            return CreatedAtAction(
                nameof(ClientsController.GetById),
                "Clients",
                new { idClient = client.IdClient },
                new { client.IdClient, client.FirstName, client.LastName }
            );
        }
    }
}

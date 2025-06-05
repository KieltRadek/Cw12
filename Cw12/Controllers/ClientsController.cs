using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cw12.Services;

namespace Cw12.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET używane przez CreatedAtAction (możesz rozbudować)
        [HttpGet("{idClient}")]
        public async Task<IActionResult> GetById(int idClient)
        {
            // tu możesz zwracać pełnego DTO klienta
            return NotFound();
        }

        // DELETE /api/clients/{idClient}
        [HttpDelete("{idClient}")]
        public async Task<IActionResult> Delete(int idClient)
        {
            if (await _clientService.ClientHasAnyBookingAsync(idClient))
                return BadRequest("Nie można usunąć klienta który ma przypisane wycieczki.");

            var ok = await _clientService.DeleteClientAsync(idClient);
            if (!ok)
                return NotFound($"Klient o id={idClient} nie istnieje.");

            return NoContent();
        }
    }
}
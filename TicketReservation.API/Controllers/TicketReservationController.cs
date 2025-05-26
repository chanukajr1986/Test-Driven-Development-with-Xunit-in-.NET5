using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReservation.Core.Models;
using TicketReservation.Core.Requests;

namespace TicketReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketReservationController : ControllerBase
    {
        private ITicketReservationRequeestProcessor _ticketReservationProcessor;

        public TicketReservationController(ITicketReservationRequeestProcessor ticketReservationProcessor)
        {
            this._ticketReservationProcessor = ticketReservationProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> ReserveTicket(TicketReservationRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _ticketReservationProcessor.ReserveTicket(request);
                if (result.Flag == Core.Enums.ReservationResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(TicketReservationRequest.Date), "No tickets Available For Given Date");
            }

            return BadRequest(ModelState);
        }
    }
}

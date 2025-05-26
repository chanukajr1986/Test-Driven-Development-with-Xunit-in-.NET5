using TicketReservation.Core.Models;

namespace TicketReservation.Core.Requests
{
    public interface ITicketReservationRequeestProcessor
    {
        TicketReservationResult ReserveTicket(TicketReservationRequest request);
    }
}
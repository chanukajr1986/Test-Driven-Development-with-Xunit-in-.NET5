using TicketReservation.Core.Enums;
using TicketReservation.Domain.BaseModels;

namespace TicketReservation.Core.Models
{
    public class TicketReservationResult : TicketReservationBase
    {
        public ReservationResultFlag Flag { get; set; }
        public int? TicketReservationId { get; set; }
    }
}

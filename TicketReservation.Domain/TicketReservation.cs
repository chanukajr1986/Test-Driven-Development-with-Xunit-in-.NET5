using TicketReservation.Domain.BaseModels;

namespace TicketReservation.Domain
{
    public class TicketReservation : TicketReservationBase
    {

        public int Id { get; set; }

        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
    }
}

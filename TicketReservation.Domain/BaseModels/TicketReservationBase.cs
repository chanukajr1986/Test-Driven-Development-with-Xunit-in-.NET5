using System;

namespace TicketReservation.Domain.BaseModels
{
    public class TicketReservationBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}

using System.Collections.Generic;

namespace TicketReservation.Domain
{
    public class Reservation
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public List<TicketReservation> TicketReservations { get; set; }
    }
}

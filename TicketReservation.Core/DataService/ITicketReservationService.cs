using System;
using System.Collections.Generic;
using TicketReservation.Domain;

namespace TicketReservation.Core.DataService
{
    public interface ITicketReservationService
    {
        void Save(TicketReservation.Domain.TicketReservation ticketReservation);

        IEnumerable<Reservation> GetAvailableTickets(DateTime date);

        IEnumerable<Reservation> GetTickets();
    }
}

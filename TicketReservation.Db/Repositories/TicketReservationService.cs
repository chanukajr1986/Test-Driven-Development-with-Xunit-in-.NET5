using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Core.DataService;
using TicketReservation.Domain;

namespace TicketReservation.Db.Repositories
{
    public class TicketReservationService : ITicketReservationService
    {
        private readonly TicketReservationDbContext _context;

        public TicketReservationService(TicketReservationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Reservation> GetAvailableTickets(DateTime date)
        {
            return _context.Reservation.Where(q => !q.TicketReservations.Any(x => x.Date == date)).ToList();
        }

        public void Save(Domain.TicketReservation roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }

        public IEnumerable<Reservation> GetTickets()
        {
            throw new NotImplementedException();
        }
    }
}

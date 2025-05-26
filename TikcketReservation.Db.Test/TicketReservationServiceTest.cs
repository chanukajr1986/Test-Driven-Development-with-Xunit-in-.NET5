using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReservation.Db;
using TicketReservation.Db.Repositories;
using TicketReservation.Domain;
using Xunit;

namespace TikcketReservation.Db.Test
{
    public class TicketReservationServiceTest
    {
        [Fact]
        public void Should_Return_Available_tickets()
        {
            //Arrange
            var date = new DateTime(2021, 06, 09);

            var dbOptions = new DbContextOptionsBuilder<TicketReservationDbContext>()
                .UseInMemoryDatabase("AvailableticketTest")
                .Options;

            using var context = new TicketReservationDbContext(dbOptions);
            context.Add(new Reservation { Id = 1, Name = "ticket 1" });
            context.Add(new Reservation { Id = 2, Name = "ticket 2" });
            context.Add(new Reservation { Id = 3, Name = "ticket 3" });

            context.Add(new TicketReservation.Domain.TicketReservation { ReservationId = 1, Date = date });
            context.Add(new TicketReservation.Domain.TicketReservation { ReservationId = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var ticketBookingService = new TicketReservationService(context);

            //Act
            var availableTickets = ticketBookingService.GetAvailableTickets(date);

            //Assert
            Assert.Equal(2, availableTickets.Count());
            Assert.Contains(availableTickets, q => q.Id == 2);
            Assert.Contains(availableTickets, q => q.Id == 3);
            Assert.DoesNotContain(availableTickets, q => q.Id == 1);
        }


        [Fact]
        public void Should_Save_ticket_Booking()
        {
            var dbOptions = new DbContextOptionsBuilder<TicketReservationDbContext>()
               .UseInMemoryDatabase("ShouldSaveTest")
               .Options;

            var ticketBooking = new TicketReservation.Domain.TicketReservation { ReservationId = 1, Date = new DateTime(2021, 06, 09) };


            using var context = new TicketReservationDbContext(dbOptions);
            var ticketBookingService = new TicketReservationService(context);
            ticketBookingService.Save(ticketBooking);

            var bookings = context.TicketReservation.ToList();
            var booking = Assert.Single(bookings);

            Assert.Equal(ticketBooking.Date, booking.Date);
            Assert.Equal(ticketBooking.ReservationId, booking.ReservationId);
        }
    }
}

using System;
using System.Linq;
using TicketReservation.Core.DataService;
using TicketReservation.Core.Enums;
using TicketReservation.Core.Models;
using TicketReservation.Domain.BaseModels;

namespace TicketReservation.Core.Requests
{
    public class TicketReservationRequeestProcessor
    {
        private readonly ITicketReservationService _ticketReservationService;

        public TicketReservationRequeestProcessor(ITicketReservationService ticketReservationService)
        {
            this._ticketReservationService = ticketReservationService;
        }


        public TicketReservationResult ReserveTicket(TicketReservationRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var availabeTickets = _ticketReservationService.GetAvailableTickets(request.Date);
            var result = CreateTicketReservationObject<TicketReservationResult>(request);

            if (availabeTickets.Any())
            {
                var ticket = availabeTickets.First();
                var ticketReservation = CreateTicketReservationObject<TicketReservation.Domain.TicketReservation>(request);
                ticketReservation.ReservationId = ticket.Id;
                _ticketReservationService.Save(ticketReservation);

                result.TicketReservationId = ticketReservation.Id;
                result.Flag = ReservationResultFlag.Success;
            }
            else
            {
                result.Flag = ReservationResultFlag.Failure;
            }

            return result;
        }

        private static TBooking CreateTicketReservationObject<TBooking>(TicketReservationRequest bookingRequest) where TBooking
            : TicketReservationBase, new()
        {
            return new TBooking
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email,
            };
        }
    }
}

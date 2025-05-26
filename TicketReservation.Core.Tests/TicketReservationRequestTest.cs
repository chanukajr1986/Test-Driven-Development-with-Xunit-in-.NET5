using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Shouldly;
using TicketReservation.Core.DataService;
using TicketReservation.Core.Enums;
using TicketReservation.Core.Models;
using TicketReservation.Core.Requests;
using TicketReservation.Domain;
using Xunit;

namespace TicketReservation.Core.Tests
{

    public class TicketReservationRequestTest
    {

        private TicketReservationRequeestProcessor _processor;
        private TicketReservationRequest _request;
        private Mock<ITicketReservationService> _ticketReservationServiceMock;
        private List<Reservation> _availableTickets;

        public TicketReservationRequestTest()
        {
            //Arrange
            _request = new TicketReservationRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20)
            };
            _availableTickets = new List<Reservation>() { new Reservation() { Id = 1 } };

            _ticketReservationServiceMock = new Mock<ITicketReservationService>();
            _ticketReservationServiceMock.Setup(q => q.GetAvailableTickets(_request.Date))
                .Returns(_availableTickets);

            _processor = new TicketReservationRequeestProcessor(_ticketReservationServiceMock.Object);
        }

        [Fact]
        public void Should_Return_ticket_Booking_Response_With_Request_Values()
        {
            //Act
            TicketReservationResult result = _processor.ReserveTicket(_request);

            //Assert
            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);

        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.ReserveTicket(null));
            exception.ParamName.ShouldBe("request");
        }

        [Fact]
        public void Should_Save_ticket_Booking_Request()
        {
            // test commit 
            TicketReservation.Domain.TicketReservation savedReservation = null;
            _ticketReservationServiceMock.Setup(q => q.Save(It.IsAny<TicketReservation.Domain.TicketReservation>()))
                .Callback<TicketReservation.Domain.TicketReservation>(reservatioon =>
                {
                    savedReservation = reservatioon;
                });

            _processor.ReserveTicket(_request);

            _ticketReservationServiceMock.Verify(q => q.Save(It.IsAny<TicketReservation.Domain.TicketReservation>()), Times.Once);

            savedReservation.ShouldNotBeNull();
            savedReservation.FullName.ShouldBe(_request.FullName);
            savedReservation.Email.ShouldBe(_request.Email);
            savedReservation.Date.ShouldBe(_request.Date);
            savedReservation.ReservationId.ShouldBe(_availableTickets.First().Id);
        }

        [Fact]
        public void Should_Not_Save_ticket_Booking_Request_If_None_Available()
        {
            _availableTickets.Clear();
            _processor.ReserveTicket(_request);
            _ticketReservationServiceMock.Verify(q => q.Save(It.IsAny<TicketReservation.Domain.TicketReservation>()), Times.Never);
        }

        [Theory]
        [InlineData(ReservationResultFlag.Failure, false)]
        [InlineData(ReservationResultFlag.Success, true)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(ReservationResultFlag reserveationSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }

            var result = _processor.ReserveTicket(_request);

            reserveationSuccessFlag.ShouldBe(result.Flag);

        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_ticketBookingId_In_Result(int? ticketBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }
            else
            {
                _ticketReservationServiceMock.Setup(q => q.Save(It.IsAny<TicketReservation.Domain.TicketReservation>()))
               .Callback<TicketReservation.Domain.TicketReservation>(booking =>
               {
                   booking.Id = ticketBookingId.Value;
               });
            }

            var result = _processor.ReserveTicket(_request);
            result.TicketReservationId.ShouldBe(ticketBookingId);
        }
    }
}

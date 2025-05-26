using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using TicketReservation.API.Controllers;
using TicketReservation.Core.Enums;
using TicketReservation.Core.Models;
using TicketReservation.Core.Requests;
using Xunit;

namespace TicketReservation.API.Test
{
    public class TicketReservationControllerTest
    {
        private Mock<ITicketReservationRequeestProcessor> _ticketReservationProcessor;
        private TicketReservationController _controller;
        private TicketReservationRequest _request;
        private TicketReservationResult _result;

        public TicketReservationControllerTest()
        {
            _ticketReservationProcessor = new Mock<ITicketReservationRequeestProcessor>();
            _controller = new TicketReservationController(_ticketReservationProcessor.Object);
            _request = new TicketReservationRequest();
            _result = new TicketReservationResult();

            _ticketReservationProcessor.Setup(x => x.ReserveTicket(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), ReservationResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), ReservationResultFlag.Failure)]
        public async Task Should_Call_Booking_Method_When_Valid(int expectedMethodCalls, bool isModelValid,
            Type expectedActionResultType, ReservationResultFlag bookingResultFlag)
        {
            // Arrange code
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            _result.Flag = bookingResultFlag;


            // Act
            var result = await _controller.ReserveTicket(_request);

            // Assert
            result.ShouldBeOfType(expectedActionResultType);
            _ticketReservationProcessor.Verify(x => x.ReserveTicket(_request), Times.Exactly(expectedMethodCalls));

        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TicketReservation.API.Controllers;
using Xunit;

namespace TicketReservation.API.Test
{
    public class WeatherControllerTest
    {
        [Fact]
        public void Should_Return_Weather_Resut()
        {
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(loggerMock.Object);

            var result = controller.Get();

            result.ShouldNotBeNull();
            result.Count().ShouldBeGreaterThan(1);

        }
    }
}


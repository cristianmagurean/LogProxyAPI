using LogProxyAPI.Extensions;
using System;
using Xunit;

namespace LogProxyAPI.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(2021,02,13,0,0,0, "2021-02-13T00:00:00.000Z")]
        [InlineData(2021,02,13,10,11,12, "2021-02-13T10:11:12.000Z")]
        public void AirTableFormat_WhenCalled_FormatDate(int year, int month, int day, int hour, int minute, int second, string expected)
        {
            //Arrange
            DateTime date = new DateTime(year,month, day, hour, minute, second);

            // Act
            var formatedDate = date.AirTableFormat();

            // Assert
            Assert.Equal(formatedDate, expected);          
        }
    }
}

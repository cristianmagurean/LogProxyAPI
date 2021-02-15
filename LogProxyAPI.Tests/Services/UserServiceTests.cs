using System.Threading.Tasks;
using Xunit;
using LogProxyAPI.Services;

namespace LogProxyAPI.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Authenticate_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var service = new UserService();            

            // Act
            var result = await service.Authenticate("HC", "test");

            // Assert
            Assert.NotNull(result);
        }       

        [Theory]
        [InlineData("HC", "invalid")]
        [InlineData("invalid", "test")]
        [InlineData("invalid", "invalid")]
        public async Task Authenticate_WhenInvalidData_ReturnsBadRequestResult(string user, string pwd)
        {
            //Arrange
            var service = new UserService();     

            // Act
            var result = await service.Authenticate(user, pwd);

            // Assert
            Assert.Null(result);
        }
    }
}

using LogProxyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using LogProxyAPI.Models;
using LogProxyAPI.Services;

namespace LogProxyAPI.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Authenticate_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var service = new UserService();
            var controller = new UserController(service);
            var authModel = new AuthenticateDTO()
            {
                UserName = "HC",
                Password = "test"
            };

            // Act
            var result = await controller.Authenticate(authModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_WhenInvalidUser_ReturnsBadRequestResult()
        {
            //Arrange
            var service = new UserService();
            var controller = new UserController(service);
            var authModel = new AuthenticateDTO()
            {
                UserName = "invalid",
                Password = "test"
            };

            // Act
            var result = await controller.Authenticate(authModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_WhenInvalidPassword_ReturnsBadRequestResult()
        {
            //Arrange
            var service = new UserService();
            var controller = new UserController(service);
            var authModel = new AuthenticateDTO()
            {
                UserName = "HC",
                Password = "invalid"
            };

            // Act
            var result = await controller.Authenticate(authModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

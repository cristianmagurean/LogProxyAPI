using AutoMapper;
using LogProxyAPI.Controllers;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Mappers;
using LogProxyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LogProxyAPI.Tests.Controllers
{
    public class LogProxyControllerTests
    {        
        [Fact]
        public async Task GetMessages_WhenCalled_ReturnsOkResult()
        {          
            //Arrange
            var service = Substitute.For<IAirTableService>();         
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            var controller = new LogProxyController(service, mapper);
            service.GetMessagesAsync().Returns(
              new AirTableGetResponseDTO()
              {
                  records = new List<RecordsDTO>()
                  {
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="1", Message = "Message", receivedAt = DateTime.Now } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="2", Message = "Message", receivedAt = DateTime.Now } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="3", Message = "Message", receivedAt = DateTime.Now } }
                  }
              });

            // Act
            var result = await controller.GetMessages();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }       

        [Fact]
        public async Task SaveMessage_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var service = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            var controller = new LogProxyController(service, mapper);
            SaveRequestDTO request = new SaveRequestDTO() { Text = "text", Title = "title" };

            // Act
            var result = await controller.SaveMessage(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(null, "title")]
        [InlineData("text", null)]
        [InlineData(null, null)]
        public async Task SaveMessage_WhenInvalidModel_ReturnsBadResult(string text, string title)
        {
            //Arrange
            var service = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            var controller = new LogProxyController(service, mapper);
            controller.ModelState.AddModelError("Text", "Required");
            controller.ModelState.AddModelError("Title", "Required");
            SaveRequestDTO request = new SaveRequestDTO() { Text = text, Title = title };

            // Act
            var result = await controller.SaveMessage(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}

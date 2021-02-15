using LogProxyAPI.Interfaces;
using NSubstitute;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using LogProxyAPI.Models;
using System.Collections.Generic;
using LogProxyAPI.CQRS;
using NSubstitute.ReceivedExtensions;
using AutoMapper;
using LogProxyAPI.Mappers;
using Xunit;

namespace LogProxyAPI.Tests.CQRS
{
    public class GetMessagesQueryTests
    {
        [Fact]
        public async Task GetMessages_WhenCalled_ReturnsAllValidItems()
        {
            // Arrange            
            var airTableService = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            var command = new GetMessagesQuery();
            var handler = new GetMessagesQuery.GetMessagesQueryHandler(airTableService, mapper);                   

            var response = new AirTableGetResponseDTO()
            {
                records = new List<RecordsDTO>()
                  {
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="1", Message = "Message", receivedAt = DateTime.Now.ToString() } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="2", Message = "Message", receivedAt = DateTime.Now.ToString() } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="3", Message = "Message", receivedAt = DateTime.Now.ToString() } }
                  }
            };

            airTableService.GetMessagesAsync().Returns(response);

            // Act
            var result = handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await airTableService.Received(Quantity.AtLeastOne()).GetMessagesAsync();
            result.Should().NotBeNull();
            result.Result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetMessages_WhenCalled_FilterOutInvalidItems()
        {
            // Arrange            
            var airTableService = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            var command = new GetMessagesQuery();
            var handler = new GetMessagesQuery.GetMessagesQueryHandler(airTableService, mapper);

            var response = new AirTableGetResponseDTO()
            {
                records = new List<RecordsDTO>()
                  {
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id=null, Message = "Message", receivedAt = DateTime.Now.ToString() } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="2", Message = "Message", receivedAt = DateTime.Now.ToString() } },
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="3", Message = "Message", receivedAt = DateTime.Now.ToString() } }
                  }
            };

            airTableService.GetMessagesAsync().Returns(response);

            // Act
            var result = handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            await airTableService.Received(Quantity.AtLeastOne()).GetMessagesAsync();
            result.Should().NotBeNull();
            result.Result.Should().HaveCount(2);
        }
    }
}

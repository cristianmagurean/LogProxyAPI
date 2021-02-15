using AutoMapper;
using FluentAssertions;
using LogProxyAPI.CQRS;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Mappers;
using LogProxyAPI.Models;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LogProxyAPI.Tests.CQRS
{
    public class SaveMessageCommandTests
    {
        [Fact]
        public async Task SaveMessage_WhenCalled_ReturnsMessage()
        {
            //Arrange
            var airTableService = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            var command = new SaveMessageCommand("text", "title");
            var handler = new SaveMessageCommand.SaveMessageCommandHandler(airTableService, mapper);

            var records = new RecordsDTO() { id = "recCR2LP7wZVioc5H", fields = new FieldsDTO() { id = "1", Message = "Message", receivedAt = DateTime.Now.ToString() } };
                  
            airTableService.SaveMessageAsync(Arg.Any<AirTableSaveRequestDTO>()).Returns(new AirTableSaveResponseDTO() { records = new List<RecordsDTO>() { records } });

            // Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert            
            await airTableService.Received(Quantity.AtLeastOne()).SaveMessageAsync(Arg.Any<AirTableSaveRequestDTO>());
            result.Should().NotBeNull();
            result.records.Should().HaveCount(1);
            result.records.Should().Equals(records);
            result.records[0].fields.Message.Should().Equals("text");
            result.records[0].fields.Summary.Should().Equals("title");
        }
    }
}

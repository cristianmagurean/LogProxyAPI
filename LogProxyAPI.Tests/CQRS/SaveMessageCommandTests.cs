using AutoMapper;
using LogProxyAPI.CQRS;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Mappers;
using LogProxyAPI.Models;
using NSubstitute;
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
            var service = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            var request = new SaveRequestDTO() { Text = "text", Title = "title" };
            service.SaveMessageAsync(Arg.Any<AirTableSaveRequestDTO>()).Returns(
              new AirTableSaveResponseDTO()
              {
                  records = new List<RecordsDTO>()
                  {
                      new RecordsDTO() {id= "recCR2LP7wZVioc5H", fields = new FieldsDTO() {  id="1", Message = "Message", receivedAt = DateTime.Now } }                    
                  }
              });

            // Act
            var result = await new SaveMessageCommand(service, mapper).Execute(request);

            // Assert            
            Assert.Single(result.records);
        }
    }
}

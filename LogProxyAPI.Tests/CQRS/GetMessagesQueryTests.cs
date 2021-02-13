using AutoMapper;
using LogProxyAPI.CQRS;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Mappers;
using LogProxyAPI.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogProxyAPI.Tests.CQRS
{
    public class GetMessagesQueryTests
    {
        [Fact]
        public async Task GetMessages_WhenCalled_ReturnsAllItems()
        {
            //Arrange
            var service = Substitute.For<IAirTableService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MessageMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
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
            var result = await new GetMessagesQuery(service, mapper).Execute();

            // Assert            
            Assert.Equal(3, result.Count());
        }
    }
}

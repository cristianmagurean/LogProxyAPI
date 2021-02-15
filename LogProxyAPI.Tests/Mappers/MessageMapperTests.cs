using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Mappers;
using LogProxyAPI.Models;
using System;
using Xunit;

namespace LogProxyAPI.Tests.Mappers
{
    public class MessageMapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MessageMapper>());

            // Act & Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapMesageToSaveRecordsDTO_WhenCalled_MapValues()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MessageMapper>());
            var mapper = config.CreateMapper();
            var message = new Message()
            {
                Id = "1",
                Text = "text",
                Title = "Title",
                ReceivedAt = DateTime.Now.ToString()
            };

            // Act
            var record = mapper.Map<Message, SaveRecordsDTO>(message);

            // Assert
            Assert.Equal(message.Id, record.fields.id);
            Assert.Equal(message.Title, record.fields.Summary);
            Assert.Equal(message.Text, record.fields.Message);
            Assert.Equal(message.ReceivedAt, record.fields.receivedAt.ToString());
        }

        [Fact]
        public void MapRecordsDTOToMessage_WhenCalled_MapValues()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MessageMapper>());
            var mapper = config.CreateMapper();
            var record = new RecordsDTO() 
            { 
                id = "1", 
                fields = new FieldsDTO() { id = "1", Message = "message", receivedAt = DateTime.Now.ToString() }, 
                createdTime = DateTime.Now.ToString()
            };

            // Act
            var message = mapper.Map<RecordsDTO, Message>(record);

            // Assert
            Assert.Equal(message.Id, record.fields.id);
            Assert.Equal(message.Title, record.fields.Summary);
            Assert.Equal(message.Text, record.fields.Message);
            Assert.Equal(message.ReceivedAt, record.fields.receivedAt.ToString());
        }
    }
}

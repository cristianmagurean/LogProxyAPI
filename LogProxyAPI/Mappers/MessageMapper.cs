using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogProxyAPI.Mappers
{
    public class MessageMapper: Profile
    {
        public MessageMapper()
        {
            CreateMap<RecordsDTO, Message>()              
                .ForPath(dest => dest.Id, act => act.MapFrom(src => src.fields.id))
                .ForPath(dest => dest.Title, act => act.MapFrom(src => src.fields.Summary))
                .ForPath(dest => dest.Text, act => act.MapFrom(src => src.fields.Message))
                .ForPath(dest => dest.ReceivedAt, act => act.MapFrom(src => src.fields.receivedAt));       

            CreateMap<Message, SaveRecordsDTO>()
                .ForPath(dest => dest.fields.id, act => act.MapFrom(src => src.Id))
                .ForPath(dest => dest.fields.Summary, act => act.MapFrom(src => src.Title))
                .ForPath(dest => dest.fields.Message, act => act.MapFrom(src => src.Text))
                .ForPath(dest => dest.fields.receivedAt, act => act.MapFrom(src => src.ReceivedAt));               
        }
    }
}

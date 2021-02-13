using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogProxyAPI.CQRS
{
    public class GetMessagesQuery : IGetMessagesQuery
    {
        private readonly IAirTableService _airTableService;
        private readonly IMapper _mapper;

        public GetMessagesQuery(IAirTableService airTableService, IMapper mapper)
        {
            _airTableService = airTableService;       
            _mapper = mapper;
        }

        public async Task<IEnumerable<Message>> Execute()
        {
            List<Message> messages = new List<Message>();
            var response =  await _airTableService.GetMessagesAsync();          
            messages.AddRange(_mapper.Map<List<RecordsDTO>, List<Message>>(response.records));            
            return messages;
        }
    }
}

using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxyAPI.CQRS
{
    public class GetMessagesQuery : IRequest<IEnumerable<Message>>
    {
        public GetMessagesQuery()
        {           
        }     

        internal class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<Message>>
        {
            private readonly IAirTableService _airTableService;
            private readonly IMapper _mapper;

            public GetMessagesQueryHandler(IAirTableService airTableService, IMapper mapper)
            {
                _airTableService = airTableService;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Message>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
            {
                List<Message> messages = new List<Message>();
                var response = await _airTableService.GetMessagesAsync();
                messages.AddRange(_mapper.Map<IEnumerable<RecordsDTO>, List<Message>>(response.records.Where(r => !string.IsNullOrEmpty(r.fields.id))));
                return messages;
            }
        }
    }
}

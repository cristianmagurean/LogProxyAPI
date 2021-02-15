using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Extensions;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxyAPI.CQRS
{
    public class SaveMessageCommand : IRequest<AirTableSaveResponseDTO>
    {
        public string Text { get; }
        public string Title { get; }

        public SaveMessageCommand(string text, string title)
        {
            Text = text;
            Title = title;
        }            

        internal class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand, AirTableSaveResponseDTO>
        {
            private readonly IAirTableService _airTableService;
            private readonly IMapper _mapper;

            public SaveMessageCommandHandler(IAirTableService airTableService, IMapper mapper)
            {
                _airTableService = airTableService;
                _mapper = mapper;
            }

            public async Task<AirTableSaveResponseDTO> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
            {
                var record = _mapper.Map<Message, SaveRecordsDTO>(ProcessRequest(request.Title, request.Text));
                return await _airTableService.SaveMessageAsync(new AirTableSaveRequestDTO() { records = new List<SaveRecordsDTO>() { record } });
            }
            private Message ProcessRequest(string title, string text)
            {
                return new Message()
                {
                    Id = "1",
                    Title = title,
                    Text = text,
                    ReceivedAt = DateTime.Now.AirTableFormat()
                };
            }
        }
    }
}

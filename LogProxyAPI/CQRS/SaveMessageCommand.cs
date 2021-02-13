using AutoMapper;
using LogProxyAPI.Entities;
using LogProxyAPI.Extensions;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogProxyAPI.CQRS
{
    public class SaveMessageCommand : ISaveMessageCommand
    {
        private readonly IAirTableService _airTableService;
        private readonly IMapper _mapper;

        public SaveMessageCommand(IAirTableService airTableService, IMapper mapper)
        {
            _airTableService = airTableService;
            _mapper = mapper;
        }

        public async Task<AirTableSaveResponseDTO> Execute(SaveRequestDTO request)
        {
            var record = _mapper.Map<Message, SaveRecordsDTO>(ProcessRequest(request));           
            return await _airTableService.SaveMessageAsync(new AirTableSaveRequestDTO() { records = new List<SaveRecordsDTO>() { record } });
        }

        private Message ProcessRequest(SaveRequestDTO request)
        {
            return new Message()
            {
                Id = "1",
                Title = request.Title,
                Text = request.Text,
                ReceivedAt = DateTime.Now.AirTableFormat()
            };
        }
    }
}

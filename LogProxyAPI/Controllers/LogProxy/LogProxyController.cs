using AutoMapper;
using LogProxyAPI.CQRS;
using LogProxyAPI.Entities;
using LogProxyAPI.Interfaces;
using LogProxyAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LogProxyAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LogProxyController : ControllerBase
    {     
        private readonly IAirTableService _airTableService;
        private readonly IMapper _mapper;

        public LogProxyController(IAirTableService airTableService, IMapper mapper)
        {          
            _airTableService = airTableService;
            _mapper = mapper;
        }

        [HttpGet("messages")]
        [ProducesResponseType(typeof(IEnumerable<Message>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return Ok(await new GetMessagesQuery(_airTableService, _mapper).Execute());
        }       

        [HttpPost("messages")]
        [ProducesResponseType(typeof(SaveResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SaveResponseDTO>> SaveMessage([FromBody] SaveRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await new SaveMessageCommand(_airTableService, _mapper).Execute(request));            
        }        
    }
}

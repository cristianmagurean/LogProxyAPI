using LogProxyAPI.CQRS;
using LogProxyAPI.Entities;
using LogProxyAPI.Models;
using MediatR;
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
        private readonly IMediator _mediator;      

        public LogProxyController(IMediator mediator)
        {
            _mediator = mediator;         
        }

        [HttpGet("messages")]
        [ProducesResponseType(typeof(IEnumerable<Message>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {           
            return Ok(await _mediator.Send(new GetMessagesQuery()));
        }       

        [HttpPost("messages")]
        [ProducesResponseType(typeof(SaveResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SaveResponseDTO>> SaveMessage([FromBody] SaveMessageCommand request)
        {            
            return Ok(await _mediator.Send(new SaveMessageCommand(request.Title, request.Text)));            
        }        
    }
}

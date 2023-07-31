using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShrtLy.Api.ResponceMapper;
using ShrtLy.Application.DTOs;
using ShrtLy.Application.Features;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShrtLy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseMapper _responseMapper;

        public LinksController(IMediator mediator, IResponseMapper responseMapper)
        {
            _mediator = mediator;
            _responseMapper = responseMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetToShortLink(string url)
        {
            var command = new GetToShortLinkCommand(url);
            var response = await _mediator.Send(command);

            return _responseMapper.ExecuteAndMapStatus(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetShortLinks([FromQuery]PaginationDto pagination)
        {
            var query = new GetAllShortLinksQuery(pagination);
            var response = await _mediator.Send(query);

            return _responseMapper.ExecuteAndMapStatus(response);
        }
    }
}

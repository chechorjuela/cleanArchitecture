using CleanArchitecture.Application.Features.Videos.Queries.GetVideoList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArqchitecture.API.Controllers
{
    [ApiController]
    [Route("aoi/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{username}", Name = "GetVideo")]
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideoByUsername(string username) {
            var query = new GetVideoListQuery(username);
            var videos = await _mediator.Send(query);
            return videos;
        }
    }
}

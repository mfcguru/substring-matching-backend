using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SubtextMatching.Source.Domain.Controllers
{
    using SubtextMatching.Source.Domain.UseCases.FindMatch;

    [Route("[controller]")]
    [ApiController]
    public class Api : ControllerBase
    {
        private IMediator mediator;

        public Api(IMediator mediator) => this.mediator = mediator;

        [HttpPost("FindMatch")]
        public async Task<ActionResult> FindMatch([FromBody]FindMatchCommandDto dto)
        {
            var result = await mediator.Send(new FindMatchCommand(dto));

            return Ok(result);
        }
    }
}

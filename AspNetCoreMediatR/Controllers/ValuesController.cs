using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreMediatR.MassTransit;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IBus _bus;

        public ValuesController(IMediator mediator, IBus bus)
        {
            _mediator = mediator;
            _bus = bus;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            await _bus.Publish<Message>(new
            {
                Value = "123"
            });

            await _mediator.Publish(new SomeEvent("Hello World"));

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Travel.Booking.Dto;
using Travel.Booking.Services;

namespace Travel.Booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly ILogger<BookingController> _logger;
        private readonly BookingService _service;

        public BookingController(ILogger<BookingController> logger, BookingService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet()]
        public async Task<IActionResult> Get(string id)
        {

            var user = _service.Get(id);
            return new OkObjectResult(user);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string code)
        {
            if (!_service.IsExist(code))
                return new NotFoundObjectResult("user not found");
            var user = _service.GetByCode(code);
            return new OkObjectResult(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateDto dto)
        {
            if (_service.IsExist(dto.Code))
                return new ConflictObjectResult("code is exists");
            _service.Add(dto.Code, dto.Name);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateDto dto)
        {
            _service.Update(dto);

            return Ok();
        }
    }
}

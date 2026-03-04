using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto.Orden;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        // GET: api/orden
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ordenes = await _ordenService.GetAllAsync();
            return Ok(ordenes);
        }

        // GET: api/orden/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orden = await _ordenService.GetByIdAsync(id);
            if (orden == null)
                return NotFound();

            return Ok(orden);
        }

        // POST: api/orden
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestOrdenDto request)
        {

            await _ordenService.AddAsync(request);
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/orden/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RequestOrdenDto request)
        {
            await _ordenService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE: api/orden/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ordenService.DeleteAsync(id);
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto.Producto;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestProductoDto request)
        {
            await _productoService.SaveAsync(request);
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RequestProductoDto request)
        {
            await _productoService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
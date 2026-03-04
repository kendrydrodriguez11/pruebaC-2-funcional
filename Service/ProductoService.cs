using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Dto.Categoria;
using WebApplication1.Dto.Producto;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductoService(IProductoRepository  productoRepository, ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _productoRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw; 
            }
        }
        

        public async Task<List<ResponseProductoDto>> GetAllAsync()
        {
            try
            {
                List<Producto> productos = await _productoRepository.GetAllAsync();
                List<ResponseProductoDto> response = productos
                    .Select(x => MapperToResponse(x))
                    .ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ResponseProductoDto?> GetByIdAsync(Guid id)
        {
            try
            {
                Producto producto = await _productoRepository.GetByIdAsync(id);
                return MapperToResponse(producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveAsync(RequestProductoDto producto)
        {
            try
            {

                Producto producto1 = await MapperToModel(producto);
                await _productoRepository.SaveAsync(producto1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Guid id, RequestProductoDto producto)
        {
            try
            {
                Producto producto1 = await MapperToModel(producto);
                await _productoRepository.SaveAsync(producto1);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ResponseProductoDto MapperToResponse(Producto product)
        {
            return new ResponseProductoDto
            {
                Nombre = product.Nombre,
                Precio = product.Precio,
                NombreCategoria = product.Categoria.Nombre,
                Stock = product.Stock,
                Descripcion = product.Descripcion,
                Categoria = new ResponseCategoriaDto
                {
                    Id = product.Categoria.Id,
                    Descripcion = product.Descripcion,
                    Nombre = product.Categoria.Nombre
                }
            };
        }

        public async Task<Producto> MapperToModel(RequestProductoDto requestProductoDto)
        {
            Categoria categoria = await _categoriaRepository.GetByIdAsync(requestProductoDto.CategoriaId);
            return new Producto
            {
                Nombre = requestProductoDto.Nombre,
                Precio = requestProductoDto.Precio,
                Descripcion = requestProductoDto.Descripcion,
                Stock = requestProductoDto.Stock,
                CategoriaId = requestProductoDto.CategoriaId,
                Categoria = categoria

            };
        }

    }
}

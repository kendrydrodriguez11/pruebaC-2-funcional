using WebApplication1.Dto.Categoria;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<ResponseCategoriaDto>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(c => MapperToResponse(c)).ToList();
        }

        public async Task<ResponseCategoriaDto?> GetByIdAsync(Guid id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null) return null;
            return MapperToResponse(categoria);
        }

        public async Task SaveAsync(RequestCategoriaDto request)
        {
            var categoria = MapperToModel(request);
            await _categoriaRepository.SaveAsync(categoria);
        }

        public async Task UpdateAsync(Guid id, RequestCategoriaDto request)
        {
            var categoria = MapperToModel(request);
            await _categoriaRepository.UpdateAsync(id, categoria);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoriaRepository.DeleteAsync(id);
        }

        private ResponseCategoriaDto MapperToResponse(Categoria categoria)
        {
            return new ResponseCategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }

        private Categoria MapperToModel(RequestCategoriaDto request)
        {
            return new Categoria
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
        }
    }
}
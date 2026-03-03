using WebApplication1.Dto.Categoria;

namespace WebApplication1.Dto.Producto
{
    public class ResponseProductoDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string NombreCategoria { get; set; }

        public ResponseCategoriaDto Categoria { get; set; }

    }
}

namespace WebApplication1.Dto.Producto
{
    public class RequestProductoDto
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public Guid CategoriaId { get; set; }
    }
}

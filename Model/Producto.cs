namespace WebApplication1.Model
{
    public class Producto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public Guid CategoriaId {  get; set; }
        public Categoria Categoria { get; set; }

        public List<OrdenProducto> OrdenProducto { get; set; }
    }
}

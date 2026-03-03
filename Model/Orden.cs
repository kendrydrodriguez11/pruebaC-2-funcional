namespace WebApplication1.Model
{
    public class Orden
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public List<OrdenProducto> OrdenProductos { get; set; }
    }
}

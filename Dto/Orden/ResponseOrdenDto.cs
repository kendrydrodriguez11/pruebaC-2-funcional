namespace WebApplication1.Dto.Orden
{
    public class ResponseOrdenDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public List<ResponseOrdenProducto> ResponseOrdenProducto { get; set; }
    }
}

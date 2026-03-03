namespace WebApplication1.Dto.Orden
{
    public class RequestOrdenDto
    {
        public decimal Total { get; set; }

        public int CantidadProduct { get; set; }

        public Guid ProductoId { get; set; }
    }
}

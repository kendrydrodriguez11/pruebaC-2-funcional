namespace WebApplication1.Model
{
    public class OrdenProducto
    {
       public Guid Id { get; set; } = Guid.NewGuid();

       public int Cantidad { get; set; }

       public Guid ProductId { get; set; }
       public Producto Producto { get; set; }
    
       public Guid OrdenId {  get; set; }

        public Orden Orden { get; set; }
    }
}

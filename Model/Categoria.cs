namespace WebApplication1.Model
{
    public class Categoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Descripcion {  get; set; }

        public string Nombre { get; set; }

        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}

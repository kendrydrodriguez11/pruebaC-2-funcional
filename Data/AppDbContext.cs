using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets (bien nombrados, sin inglés raro)
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenProducto> OrdenProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // TABLAS
            // =========================
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Orden>().ToTable("Ordenes");
            modelBuilder.Entity<OrdenProducto>().ToTable("OrdenProductos");

            // =========================
            // ORDEN
            // =========================
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Total)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(o => o.Fecha)
                      .IsRequired();
            });

            // =========================
            // ORDEN ↔ ORDENPRODUCTO
            // =========================
            modelBuilder.Entity<OrdenProducto>()
                .HasOne(op => op.Orden)
                .WithMany(o => o.OrdenProductos)
                .HasForeignKey(op => op.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // PRODUCTO ↔ ORDENPRODUCTO
            // =========================
            modelBuilder.Entity<OrdenProducto>()
                .HasOne(op => op.Producto)
                .WithMany()
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
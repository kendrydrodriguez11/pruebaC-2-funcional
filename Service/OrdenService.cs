using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Dto.Orden;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IOrdenProductosRepository _ordenProductosRepository;
        private readonly IProductoRepository _productoRepository;

        public OrdenService(
            IOrdenRepository ordenRepository,
            IOrdenProductosRepository ordenProductosRepository,
            IProductoRepository productoRepository)
        {
            _ordenRepository = ordenRepository;
            _ordenProductosRepository = ordenProductosRepository;
            _productoRepository = productoRepository;
        }

        public async Task AddAsync(RequestOrdenDto orden)
        {
            try
            {
                Producto producto = await _productoRepository.GetByIdAsync(orden.ProductoId);
                
                if (producto == null)
                    throw new Exception("Producto no encontrado");
                if (producto.Stock < orden.CantidadProduct)
                    throw new Exception("noo hay stock suficiente");

                producto.Stock -= orden.CantidadProduct;
                Orden orden1 = await MapperToModel(orden, producto.Precio);
                OrdenProducto ordenProducto = new OrdenProducto
                {
                    Cantidad = orden.CantidadProduct,
                    ProductId = orden.ProductoId,
                    Orden = orden1,
                    Producto = producto
                };
                _productoRepository.SaveAsync(producto);
                _ordenProductosRepository.AddAsync(ordenProducto);
                _ordenRepository.AddAsync(orden1);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error, no se pudo almacenar"+ ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            Orden orden = await _ordenRepository.GetByIdAsync(id);
            if (orden == null) throw new Exception("la orden no exite");
            foreach (var ordenProducto in orden.OrdenProductos)
            {
                Producto producto = await _productoRepository.GetByIdAsync(ordenProducto.ProductId);
                producto.Stock += (int)ordenProducto.Cantidad;

                await _productoRepository.SaveAsync(producto);
            }
            await _ordenRepository.DeleteAsync(orden.Id);
        }

        public async Task<List<ResponseOrdenDto>> GetAllAsync()
        {
            var ordens = await _ordenRepository.GetAllAsync();

            var tasks = ordens.Select(or => MapperToResponse(or));
            var responseOrdenDtos = await Task.WhenAll(tasks);
            return responseOrdenDtos.ToList();
        }

        public async Task<ResponseOrdenDto> GetByIdAsync(Guid id)
        {
            Orden orden = await _ordenRepository.GetByIdAsync(id);
            var task = MapperToResponse(orden);
            return await task;
        }


        public async Task UpdateAsync(Guid ordenId, RequestOrdenDto request)
        {
            var orden = await _ordenRepository.GetByIdAsync(ordenId);
            if (orden == null)
                throw new Exception("La orden no existe");

            var ordenProducto = orden.OrdenProductos.FirstOrDefault();
            if (ordenProducto == null)
                throw new Exception("La orden no tiene productos");

            // 1️⃣ Revertir stock anterior
            var productoAnterior = await _productoRepository.GetByIdAsync(ordenProducto.ProductId);
            productoAnterior.Stock += ordenProducto.Cantidad;
            await _productoRepository.UpdateAsync(productoAnterior.Id,  productoAnterior);

            // 2️⃣ Obtener nuevo producto
            var nuevoProducto = await _productoRepository.GetByIdAsync(request.ProductoId);
            if (nuevoProducto == null)
                throw new Exception("Producto no encontrado");

            if (nuevoProducto.Stock < request.CantidadProduct)
                throw new Exception("No hay stock suficiente");

            // 3️⃣ Aplicar nuevo stock
            nuevoProducto.Stock -= request.CantidadProduct;
            await _productoRepository.UpdateAsync(nuevoProducto.Id, nuevoProducto);

            // 4️⃣ Actualizar orden-producto
            ordenProducto.ProductId = request.ProductoId;
            ordenProducto.Cantidad = request.CantidadProduct;
            ordenProducto.Producto = nuevoProducto;

            // 5️⃣ Recalcular total
            orden.Total = nuevoProducto.Precio * request.CantidadProduct;

            await _ordenProductosRepository.UpdateAsync(ordenProducto);
            await _ordenRepository.UpdateAsync(orden);
        }

        public async Task<Orden> MapperToModel(RequestOrdenDto requestOrdenDto, decimal Precio)
        {
            return new Orden
            {
                Total = Precio * requestOrdenDto.CantidadProduct,
            };
        }

        public async Task<ResponseOrdenDto> MapperToResponse(Orden orden)
        {
            var responseOrdenProductos = new List<ResponseOrdenProducto>();

            foreach (var ordenProducto in orden.OrdenProductos)
            {
                var producto = await _productoRepository.GetByIdAsync(ordenProducto.ProductId);

                responseOrdenProductos.Add(new ResponseOrdenProducto
                {
                    Id = ordenProducto.Id,
                    NombreProducto = producto.Nombre,
                    Cantidad = ordenProducto.Cantidad
                });
            }

            return new ResponseOrdenDto
            {
                Id = orden.Id,
                Fecha = orden.Fecha,
                Total = orden.Total,
                ResponseOrdenProducto = responseOrdenProductos
            };
        }
    }
    }

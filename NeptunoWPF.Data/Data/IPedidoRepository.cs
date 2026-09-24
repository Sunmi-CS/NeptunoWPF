using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public interface IPedidoRepository
{
    Task<List<Pedido>> ListarAsync();

    Task<int> InsertarAsync(Pedido pedido);

    Task<int> ActualizarAsync(Pedido pedido);

    Task<int> EliminarAsync(int pedidoID);

    Task<List<Cliente>> ListarClientesAsync();

    Task<List<Empleado>> ListarEmpleadosAsync();

    Task<List<Transportista>> ListarTransportistasAsync();

    Task<List<DetallePedidoReporte>> DetallePorFechasAsync(
        DateTime fechaInicio,
        DateTime fechaFin);
}
using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Models;

namespace NeptunoWPF.Data;

public interface IPedidoRepository
{
    Task<List<Pedido>> ListarAsync();

    Task<int> InsertarAsync(Pedido pedido);

    Task ActualizarAsync(Pedido pedido);

    Task EliminarAsync(int pedidoId);

    Task<List<DetallePedidoReporte>> ReportePorFechasAsync(
        DateTime fechaInicio,
        DateTime fechaFin);

    Task<List<Cliente>> ListarClientesAsync();

    Task<List<Empleado>> ListarEmpleadosAsync();

    Task<List<Transportista>> ListarTransportistasAsync();
}

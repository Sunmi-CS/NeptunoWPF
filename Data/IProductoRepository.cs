using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Models;

namespace NeptunoWPF.Data;

public interface IProductoRepository
{
    Task<List<Producto>> ListarAsync();

    Task<int> InsertarAsync(Producto producto);

    Task ActualizarAsync(Producto producto);

    Task EliminarAsync(int productoId);
}
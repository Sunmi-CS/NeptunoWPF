using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public interface IProductoRepository
{
    Task<List<Producto>> ListarAsync();

    Task<int> InsertarAsync(Producto producto);

    Task<int> ActualizarAsync(Producto producto);

    Task<int> EliminarAsync(int productoID);
}
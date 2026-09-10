using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Models;

namespace NeptunoWPF.Data;

public interface IProveedorRepository
{
    Task<List<Proveedor>> ListarAsync();

    Task<List<Proveedor>> BuscarAsync(
        string? nombreContacto,
        string? ciudad);

    Task<int> InsertarAsync(Proveedor proveedor);

    Task ActualizarAsync(Proveedor proveedor);

    Task EliminarAsync(int proveedorId);
}

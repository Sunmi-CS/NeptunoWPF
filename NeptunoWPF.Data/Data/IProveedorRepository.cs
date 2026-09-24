using System.Collections.Generic;
using System.Threading.Tasks;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public interface IProveedorRepository
{
    Task<List<Proveedor>> ListarAsync();

    Task<List<Proveedor>> BuscarAsync(
        string? nombreContacto,
        string? ciudad);

    Task<int> InsertarAsync(Proveedor proveedor);

    Task<int> ActualizarAsync(Proveedor proveedor);

    Task<int> EliminarAsync(int proveedorID);
}
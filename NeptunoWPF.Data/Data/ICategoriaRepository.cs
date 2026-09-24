using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public interface ICategoriaRepository
{
    Task<List<Categoria>> ListarAsync();

    Task<int> InsertarAsync(Categoria categoria);

    Task<int> ActualizarAsync(Categoria categoria);

    Task<int> EliminarAsync(int categoriaID);
}
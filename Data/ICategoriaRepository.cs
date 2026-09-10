using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Models;

namespace NeptunoWPF.Data;

public interface ICategoriaRepository
{
    Task<List<Categoria>> ListarAsync();

    Task<int> InsertarAsync(Categoria categoria);

    Task ActualizarAsync(Categoria categoria);

    Task EliminarAsync(int categoriaId);
}

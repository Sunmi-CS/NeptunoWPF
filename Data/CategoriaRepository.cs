using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using NeptunoWPF.Models;
using System.Data;

namespace NeptunoWPF.Data;

public class CategoriaRepository : ICategoriaRepository
{
    public async Task<List<Categoria>> ListarAsync()
    {
        var lista = new List<Categoria>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand("sp_Categorias_Listar", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Categoria
            {
                CategoriaID =
                    reader.GetInt32(reader.GetOrdinal("CategoriaID")),

                NombreCategoria =
                    reader.GetString(reader.GetOrdinal("NombreCategoria")),

                Descripcion =
                    reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Descripcion"))
            });
        }

        return lista;
    }

    public async Task<int> InsertarAsync(Categoria categoria)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand("sp_Categorias_Insertar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@NombreCategoria",
            SqlDbType.NVarChar, 30).Value =
            categoria.NombreCategoria;

        command.Parameters.Add("@Descripcion",
            SqlDbType.NVarChar, 200).Value =
            (object?)categoria.Descripcion ?? DBNull.Value;

        await connection.OpenAsync();

        return Convert.ToInt32(
            await command.ExecuteScalarAsync());
    }

    public async Task ActualizarAsync(Categoria categoria)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand("sp_Categorias_Actualizar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CategoriaID", SqlDbType.Int)
            .Value = categoria.CategoriaID;

        command.Parameters.Add("@NombreCategoria",
            SqlDbType.NVarChar, 30).Value =
            categoria.NombreCategoria;

        command.Parameters.Add("@Descripcion",
            SqlDbType.NVarChar, 200).Value =
            (object?)categoria.Descripcion ?? DBNull.Value;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    public async Task EliminarAsync(int categoriaId)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand("sp_Categorias_Eliminar", connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CategoriaID", SqlDbType.Int)
            .Value = categoriaId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
}
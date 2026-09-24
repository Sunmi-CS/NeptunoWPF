using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public class CategoriaRepository : ICategoriaRepository
{
    public async Task<List<Categoria>> ListarAsync()
    {
        var categorias = new List<Categoria>();

        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Categorias_Listar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            categorias.Add(new Categoria
            {
                CategoriaID = Convert.ToInt32(
                    reader["CategoriaID"]),

                NombreCategoria = reader["NombreCategoria"]?
                    .ToString() ?? string.Empty,

                Descripcion = reader["Descripcion"]?
                    .ToString(),

                Activo = reader["Activo"] != DBNull.Value
                    && Convert.ToBoolean(reader["Activo"])
            });
        }

        return categorias;
    }

    public async Task<int> InsertarAsync(Categoria categoria)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Categorias_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@NombreCategoria",
            categoria.NombreCategoria);

        command.Parameters.AddWithValue(
            "@Descripcion",
            (object?)categoria.Descripcion ?? DBNull.Value);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return 1;
    }

    public async Task<int> ActualizarAsync(Categoria categoria)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Categorias_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CategoriaID",
            categoria.CategoriaID);

        command.Parameters.AddWithValue(
            "@NombreCategoria",
            categoria.NombreCategoria);

        command.Parameters.AddWithValue(
            "@Descripcion",
            (object?)categoria.Descripcion ?? DBNull.Value);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<int> EliminarAsync(int categoriaID)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Categorias_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CategoriaID",
            categoriaID);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }
}
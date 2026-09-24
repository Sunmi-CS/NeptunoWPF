using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public class ProductoRepository : IProductoRepository
{
    public async Task<List<Producto>> ListarAsync()
    {
        var productos = new List<Producto>();

        using var connection = new SqlConnection(DbConfig.ConnectionString);
        using var command = new SqlCommand(
            "sp_Productos_Listar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            productos.Add(new Producto
            {
                ProductoID = Convert.ToInt32(reader["ProductoID"]),
                NombreProducto = reader["NombreProducto"]?.ToString() ?? string.Empty,

                ProveedorID = reader["ProveedorID"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["ProveedorID"]),

                Proveedor = reader["Proveedor"]?.ToString(),

                CategoriaID = reader["CategoriaID"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["CategoriaID"]),

                Categoria = reader["Categoria"]?.ToString(),

                CantidadPorUnidad = reader["CantidadPorUnidad"]?.ToString(),

                PrecioUnidad = reader["PrecioUnidad"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(reader["PrecioUnidad"]),

                UnidadesEnExistencia = reader["UnidadesEnExistencia"] == DBNull.Value
                    ? (short)0
                    : Convert.ToInt16(reader["UnidadesEnExistencia"]),

                UnidadesEnPedido = reader["UnidadesEnPedido"] == DBNull.Value
                    ? (short)0
                    : Convert.ToInt16(reader["UnidadesEnPedido"]),

                NivelDeReorden = reader["NivelDeReorden"] == DBNull.Value
                    ? (short)0
                    : Convert.ToInt16(reader["NivelDeReorden"]),
                Descontinuado = reader["Descontinuado"] != DBNull.Value
                    && Convert.ToBoolean(reader["Descontinuado"]),

                Activo = reader["Activo"] != DBNull.Value
                    && Convert.ToBoolean(reader["Activo"])
            });
        }

        return productos;
    }

    public async Task<int> InsertarAsync(Producto producto)
    {
        using var connection = new SqlConnection(DbConfig.ConnectionString);
        using var command = new SqlCommand(
            "sp_Productos_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@NombreProducto",
            producto.NombreProducto);

        command.Parameters.AddWithValue(
            "@ProveedorID",
            (object?)producto.ProveedorID ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CategoriaID",
            (object?)producto.CategoriaID ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CantidadPorUnidad",
            (object?)producto.CantidadPorUnidad ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@PrecioUnidad",
            producto.PrecioUnidad);

        command.Parameters.AddWithValue(
            "@UnidadesEnExistencia",
            producto.UnidadesEnExistencia);

        command.Parameters.AddWithValue(
            "@UnidadesEnPedido",
            producto.UnidadesEnPedido);

        command.Parameters.AddWithValue(
            "@NivelDeReorden",
            producto.NivelDeReorden);

        command.Parameters.AddWithValue(
            "@Descontinuado",
            producto.Descontinuado);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return 1;
    }

    public async Task<int> ActualizarAsync(Producto producto)
    {
        using var connection = new SqlConnection(DbConfig.ConnectionString);
        using var command = new SqlCommand(
            "sp_Productos_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ProductoID",
            producto.ProductoID);

        command.Parameters.AddWithValue(
            "@NombreProducto",
            producto.NombreProducto);

        command.Parameters.AddWithValue(
            "@ProveedorID",
            (object?)producto.ProveedorID ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CategoriaID",
            (object?)producto.CategoriaID ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CantidadPorUnidad",
            (object?)producto.CantidadPorUnidad ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@PrecioUnidad",
            producto.PrecioUnidad);

        command.Parameters.AddWithValue(
            "@UnidadesEnExistencia",
            producto.UnidadesEnExistencia);

        command.Parameters.AddWithValue(
            "@UnidadesEnPedido",
            producto.UnidadesEnPedido);

        command.Parameters.AddWithValue(
            "@NivelDeReorden",
            producto.NivelDeReorden);

        command.Parameters.AddWithValue(
            "@Descontinuado",
            producto.Descontinuado);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<int> EliminarAsync(int productoID)
    {
        using var connection = new SqlConnection(DbConfig.ConnectionString);
        using var command = new SqlCommand(
            "sp_Productos_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ProductoID",
            productoID);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }
}
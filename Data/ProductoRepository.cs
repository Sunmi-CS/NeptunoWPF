using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using NeptunoWPF.Models;
using System.Data;

namespace NeptunoWPF.Data;

public class ProductoRepository : IProductoRepository
{
    public async Task<List<Producto>> ListarAsync()
    {
        var lista = new List<Producto>();

        using var connection = new SqlConnection(DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Productos_Listar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Producto
            {
                ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
                NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),

                ProveedorID = reader.IsDBNull(reader.GetOrdinal("ProveedorID"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ProveedorID")),

                Proveedor = reader.IsDBNull(reader.GetOrdinal("Proveedor"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Proveedor")),

                CategoriaID = reader.IsDBNull(reader.GetOrdinal("CategoriaID"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("CategoriaID")),

                Categoria = reader.IsDBNull(reader.GetOrdinal("Categoria"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Categoria")),

                CantidadPorUnidad = reader.IsDBNull(reader.GetOrdinal("CantidadPorUnidad"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CantidadPorUnidad")),

                PrecioUnidad = reader.GetDecimal(reader.GetOrdinal("PrecioUnidad")),

                UnidadesEnExistencia =
                    reader.GetInt16(reader.GetOrdinal("UnidadesEnExistencia")),

                UnidadesEnPedido =
                    reader.GetInt16(reader.GetOrdinal("UnidadesEnPedido")),

                NivelDeReorden =
                    reader.GetInt16(reader.GetOrdinal("NivelDeReorden")),

                Descontinuado =
                    reader.GetBoolean(reader.GetOrdinal("Descontinuado"))
            });
        }

        return lista;
    }

    public async Task<int> InsertarAsync(Producto producto)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Productos_Insertar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        AgregarParametros(command, producto);

        await connection.OpenAsync();

        return Convert.ToInt32(
            await command.ExecuteScalarAsync());
    }
    public async Task ActualizarAsync(Producto producto)
    {
        using var connection = new SqlConnection(DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Productos_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ProductoID", SqlDbType.Int)
            .Value = producto.ProductoID;

        AgregarParametros(command, producto);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
    public async Task EliminarAsync(int productoId)
    {
        using var connection = new SqlConnection(DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Productos_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@ProductoID", SqlDbType.Int)
            .Value = productoId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    private static void AgregarParametros(
        SqlCommand command,
        Producto producto)
    {
        command.Parameters.Add("@NombreProducto", SqlDbType.NVarChar, 60)
            .Value = producto.NombreProducto;

        command.Parameters.Add("@ProveedorID", SqlDbType.Int)
            .Value = (object?)producto.ProveedorID ?? DBNull.Value;

        command.Parameters.Add("@CategoriaID", SqlDbType.Int)
            .Value = (object?)producto.CategoriaID ?? DBNull.Value;

        command.Parameters.Add("@CantidadPorUnidad", SqlDbType.NVarChar, 30)
            .Value = (object?)producto.CantidadPorUnidad ?? DBNull.Value;

        var precio = command.Parameters.Add(
            "@PrecioUnidad",
            SqlDbType.Decimal);

        precio.Precision = 10;
        precio.Scale = 2;
        precio.Value = producto.PrecioUnidad;

        command.Parameters.Add("@UnidadesEnExistencia", SqlDbType.SmallInt)
            .Value = producto.UnidadesEnExistencia;

        command.Parameters.Add("@UnidadesEnPedido", SqlDbType.SmallInt)
            .Value = producto.UnidadesEnPedido;

        command.Parameters.Add("@NivelDeReorden", SqlDbType.SmallInt)
            .Value = producto.NivelDeReorden;

        command.Parameters.Add("@Descontinuado", SqlDbType.Bit)
            .Value = producto.Descontinuado;
    }
}

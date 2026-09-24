using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;


namespace NeptunoWPF.Data;

public class ProveedorRepository : IProveedorRepository
{
    public async Task<List<Proveedor>> ListarAsync()
    {
        var proveedores = new List<Proveedor>();

        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Proveedores_Listar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            proveedores.Add(MapearProveedor(reader));
        }

        return proveedores;
    }

    public async Task<List<Proveedor>> BuscarAsync(
        string? nombreContacto,
        string? ciudad)
    {
        var proveedores = new List<Proveedor>();

        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Proveedores_Buscar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@NombreContacto",
            string.IsNullOrWhiteSpace(nombreContacto)
                ? DBNull.Value
                : nombreContacto);

        command.Parameters.AddWithValue(
            "@Ciudad",
            string.IsNullOrWhiteSpace(ciudad)
                ? DBNull.Value
                : ciudad);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            proveedores.Add(MapearProveedor(reader));
        }

        return proveedores;
    }

    public async Task<int> InsertarAsync(
        Proveedor proveedor)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Proveedores_Insertar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@CompaniaNombre",
            proveedor.CompaniaNombre);

        command.Parameters.AddWithValue(
            "@NombreContacto",
            (object?)proveedor.NombreContacto
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CargoContacto",
            (object?)proveedor.CargoContacto
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Direccion",
            (object?)proveedor.Direccion
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Ciudad",
            (object?)proveedor.Ciudad
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CodigoPostal",
            (object?)proveedor.CodigoPostal
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Pais",
            (object?)proveedor.Pais
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Telefono",
            (object?)proveedor.Telefono
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Fax",
            (object?)proveedor.Fax
                ?? DBNull.Value);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return 1;
    }

    public async Task<int> ActualizarAsync(
        Proveedor proveedor)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Proveedores_Actualizar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ProveedorID",
            proveedor.ProveedorID);

        command.Parameters.AddWithValue(
            "@CompaniaNombre",
            proveedor.CompaniaNombre);

        command.Parameters.AddWithValue(
            "@NombreContacto",
            (object?)proveedor.NombreContacto
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CargoContacto",
            (object?)proveedor.CargoContacto
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Direccion",
            (object?)proveedor.Direccion
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Ciudad",
            (object?)proveedor.Ciudad
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@CodigoPostal",
            (object?)proveedor.CodigoPostal
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Pais",
            (object?)proveedor.Pais
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Telefono",
            (object?)proveedor.Telefono
                ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Fax",
            (object?)proveedor.Fax
                ?? DBNull.Value);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<int> EliminarAsync(
        int proveedorID)
    {
        using var connection = new SqlConnection(
            DbConfig.ConnectionString);

        using var command = new SqlCommand(
            "sp_Proveedores_Eliminar",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@ProveedorID",
            proveedorID);

        await connection.OpenAsync();

        return await command.ExecuteNonQueryAsync();
    }

    private static Proveedor MapearProveedor(
        SqlDataReader reader)
    {
        return new Proveedor
        {
            ProveedorID = Convert.ToInt32(
                reader["ProveedorID"]),

            CompaniaNombre = reader["CompaniaNombre"]?
                .ToString() ?? string.Empty,

            NombreContacto = reader["NombreContacto"]?
                .ToString(),

            CargoContacto = reader["CargoContacto"]?
                .ToString(),

            Direccion = reader["Direccion"]?
                .ToString(),

            Ciudad = reader["Ciudad"]?
                .ToString(),

            CodigoPostal = reader["CodigoPostal"]?
                .ToString(),

            Pais = reader["Pais"]?
                .ToString(),

            Telefono = reader["Telefono"]?
                .ToString(),

            Fax = reader["Fax"]?
                .ToString(),

            Activo = reader["Activo"] != DBNull.Value
                && Convert.ToBoolean(reader["Activo"])
        };
    }
}
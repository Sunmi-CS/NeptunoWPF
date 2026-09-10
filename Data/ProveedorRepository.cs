using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using NeptunoWPF.Models;
using System.Data;

namespace NeptunoWPF.Data;

public class ProveedorRepository : IProveedorRepository
{
    public Task<List<Proveedor>> ListarAsync()
    {
        return EjecutarListaAsync(
            "sp_Proveedores_Listar",
            null,
            null);
    }

    public Task<List<Proveedor>> BuscarAsync(
        string? nombreContacto,
        string? ciudad)
    {
        return EjecutarListaAsync(
            "sp_Proveedores_Buscar",
            nombreContacto,
            ciudad);
    }

    private async Task<List<Proveedor>> EjecutarListaAsync(
        string procedimiento,
        string? nombreContacto,
        string? ciudad)
    {
        var lista = new List<Proveedor>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(procedimiento, connection);

        command.CommandType =
            CommandType.StoredProcedure;

        if (procedimiento == "sp_Proveedores_Buscar")
        {
            command.Parameters.Add(
                "@NombreContacto",
                SqlDbType.NVarChar,
                40).Value =
                string.IsNullOrWhiteSpace(nombreContacto)
                ? DBNull.Value
                : nombreContacto;

            command.Parameters.Add(
                "@Ciudad",
                SqlDbType.NVarChar,
                30).Value =
                string.IsNullOrWhiteSpace(ciudad)
                ? DBNull.Value
                : ciudad;
        }

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Proveedor
            {
                ProveedorID =
                    reader.GetInt32(
                        reader.GetOrdinal("ProveedorID")),

                CompaniaNombre =
                    reader.GetString(
                        reader.GetOrdinal("CompaniaNombre")),

                NombreContacto =
                    reader.IsDBNull(
                        reader.GetOrdinal("NombreContacto"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("NombreContacto")),

                CargoContacto =
                    reader.IsDBNull(
                        reader.GetOrdinal("CargoContacto"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("CargoContacto")),

                Direccion =
                    reader.IsDBNull(
                        reader.GetOrdinal("Direccion"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Direccion")),

                Ciudad =
                    reader.IsDBNull(
                        reader.GetOrdinal("Ciudad"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Ciudad")),

                CodigoPostal =
                    reader.IsDBNull(
                        reader.GetOrdinal("CodigoPostal"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("CodigoPostal")),

                Pais =
                    reader.IsDBNull(
                        reader.GetOrdinal("Pais"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Pais")),

                Telefono =
                    reader.IsDBNull(
                        reader.GetOrdinal("Telefono"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Telefono")),

                Fax =
                    reader.IsDBNull(
                        reader.GetOrdinal("Fax"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Fax"))
            });
        }

        return lista;
    }

    public async Task<int> InsertarAsync(Proveedor proveedor)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Proveedores_Insertar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        AgregarParametros(command, proveedor);

        await connection.OpenAsync();

        return Convert.ToInt32(
            await command.ExecuteScalarAsync());
    }

    public async Task ActualizarAsync(Proveedor proveedor)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Proveedores_Actualizar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ProveedorID",
            SqlDbType.Int).Value =
            proveedor.ProveedorID;

        AgregarParametros(command, proveedor);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    public async Task EliminarAsync(int proveedorId)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Proveedores_Eliminar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            "@ProveedorID",
            SqlDbType.Int).Value =
            proveedorId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    private static void AgregarParametros(
        SqlCommand command,
        Proveedor p)
    {
        command.Parameters.Add(
            "@CompaniaNombre",
            SqlDbType.NVarChar,
            60).Value = p.CompaniaNombre;

        command.Parameters.Add(
            "@NombreContacto",
            SqlDbType.NVarChar,
            40).Value =
            (object?)p.NombreContacto ?? DBNull.Value;

        command.Parameters.Add(
            "@CargoContacto",
            SqlDbType.NVarChar,
            40).Value =
            (object?)p.CargoContacto ?? DBNull.Value;

        command.Parameters.Add(
            "@Direccion",
            SqlDbType.NVarChar,
            80).Value =
            (object?)p.Direccion ?? DBNull.Value;

        command.Parameters.Add(
            "@Ciudad",
            SqlDbType.NVarChar,
            30).Value =
            (object?)p.Ciudad ?? DBNull.Value;

        command.Parameters.Add(
            "@CodigoPostal",
            SqlDbType.NVarChar,
            10).Value =
            (object?)p.CodigoPostal ?? DBNull.Value;

        command.Parameters.Add(
            "@Pais",
            SqlDbType.NVarChar,
            30).Value =
            (object?)p.Pais ?? DBNull.Value;

        command.Parameters.Add(
            "@Telefono",
            SqlDbType.NVarChar,
            24).Value =
            (object?)p.Telefono ?? DBNull.Value;

        command.Parameters.Add(
            "@Fax",
            SqlDbType.NVarChar,
            24).Value =
            (object?)p.Fax ?? DBNull.Value;
    }
}

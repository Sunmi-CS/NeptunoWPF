using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;

using System.Data;

namespace NeptunoWPF.Data;

public class PedidoRepository : IPedidoRepository
{
    public async Task<List<Pedido>> ListarAsync()
    {
        var lista = new List<Pedido>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Pedidos_Listar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Pedido
            {
                PedidoID =
                    reader.GetInt32(
                        reader.GetOrdinal("PedidoID")),

                ClienteID = reader.IsDBNull(
                    reader.GetOrdinal("ClienteID"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("ClienteID")),

                Cliente = reader.IsDBNull(
                    reader.GetOrdinal("Cliente"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Cliente")),

                EmpleadoID = reader.IsDBNull(
                    reader.GetOrdinal("EmpleadoID"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("EmpleadoID")),

                Empleado = reader.IsDBNull(
                    reader.GetOrdinal("Empleado"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Empleado")),

                FechaPedido =
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaPedido")),

                FechaRequerida = reader.IsDBNull(
                    reader.GetOrdinal("FechaRequerida"))
                    ? null
                    : reader.GetDateTime(
                        reader.GetOrdinal("FechaRequerida")),

                FechaEnvio = reader.IsDBNull(
                    reader.GetOrdinal("FechaEnvio"))
                    ? null
                    : reader.GetDateTime(
                        reader.GetOrdinal("FechaEnvio")),

                TransportistaID = reader.IsDBNull(
                    reader.GetOrdinal("TransportistaID"))
                    ? null
                    : reader.GetInt32(
                        reader.GetOrdinal("TransportistaID")),

                Transportista = reader.IsDBNull(
                    reader.GetOrdinal("Transportista"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Transportista")),

                Destinatario = reader.IsDBNull(
                    reader.GetOrdinal("Destinatario"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Destinatario")),

                CiudadDestino = reader.IsDBNull(
                    reader.GetOrdinal("CiudadDestino"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("CiudadDestino")),

                PaisDestino = reader.IsDBNull(
                    reader.GetOrdinal("PaisDestino"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("PaisDestino"))
            });
        }

        return lista;
    }

    public async Task<int> InsertarAsync(Pedido pedido)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Pedidos_Insertar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        AgregarParametros(command, pedido);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return 1;
    }

    public async Task<int> ActualizarAsync(Pedido pedido)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Pedidos_Actualizar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PedidoID",
            SqlDbType.Int).Value =
            pedido.PedidoID;

        AgregarParametros(command, pedido);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
        return 1;
    }

    public async Task<int> EliminarAsync(int pedidoId)
    {
        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_Pedidos_Eliminar",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            "@PedidoID",
            SqlDbType.Int).Value =
            pedidoId;

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
        return 1;
    }

    public async Task<List<DetallePedidoReporte>> DetallePorFechasAsync(
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        var lista = new List<DetallePedidoReporte>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "sp_DetallePedidos_PorFechas",
                connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.Add(
            "@FechaInicio",
            SqlDbType.Date).Value =
            fechaInicio.Date;

        command.Parameters.Add(
            "@FechaFin",
            SqlDbType.Date).Value =
            fechaFin.Date;

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new DetallePedidoReporte
            {
                PedidoID =
                    reader.GetInt32(
                        reader.GetOrdinal("PedidoID")),

                FechaPedido =
                    reader.GetDateTime(
                        reader.GetOrdinal("FechaPedido")),

                ProductoID =
                    reader.GetInt32(
                        reader.GetOrdinal("ProductoID")),

                NombreProducto =
                    reader.GetString(
                        reader.GetOrdinal("NombreProducto")),

                PrecioUnidad =
                    reader.GetDecimal(
                        reader.GetOrdinal("PrecioUnidad")),

                Cantidad =
                    reader.GetInt16(
                        reader.GetOrdinal("Cantidad")),

                Descuento =
                    reader.GetDecimal(
                        reader.GetOrdinal("Descuento")),

                Subtotal =
                    reader.GetDecimal(
                        reader.GetOrdinal("Subtotal"))
            });
        }

        return lista;
    }

    public async Task<List<Cliente>> ListarClientesAsync()
    {
        var lista = new List<Cliente>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                "SELECT ClienteID, Empresa FROM Clientes ORDER BY Empresa",
                connection);

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Cliente
            {
                ClienteID = reader.GetInt32(0),
                Empresa = reader.GetString(1)
            });
        }

        return lista;
    }

    public async Task<List<Empleado>> ListarEmpleadosAsync()
    {
        var lista = new List<Empleado>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                """
                SELECT EmpleadoID,
                       CONCAT(Nombre, ' ', Apellidos) AS NombreCompleto
                FROM Empleados
                ORDER BY Nombre, Apellidos
                """,
                connection);

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Empleado
            {
                EmpleadoID = reader.GetInt32(0),
                NombreCompleto = reader.GetString(1)
            });
        }

        return lista;
    }

    public async Task<List<Transportista>> ListarTransportistasAsync()
    {
        var lista = new List<Transportista>();

        using var connection =
            new SqlConnection(DbConfig.ConnectionString);

        using var command =
            new SqlCommand(
                """
                SELECT TransportistaID, CompaniaNombre
                FROM Transportistas
                ORDER BY CompaniaNombre
                """,
                connection);

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new Transportista
            {
                TransportistaID = reader.GetInt32(0),
                CompaniaNombre = reader.GetString(1)
            });
        }

        return lista;
    }

    private static void AgregarParametros(
        SqlCommand command,
        Pedido pedido)
    {
        command.Parameters.Add("@ClienteID", SqlDbType.Int)
            .Value = (object?)pedido.ClienteID ?? DBNull.Value;

        command.Parameters.Add("@EmpleadoID", SqlDbType.Int)
            .Value = (object?)pedido.EmpleadoID ?? DBNull.Value;

        command.Parameters.Add("@FechaPedido", SqlDbType.Date)
            .Value = pedido.FechaPedido.Date;

        command.Parameters.Add("@FechaRequerida", SqlDbType.Date)
            .Value = (object?)pedido.FechaRequerida?.Date
                     ?? DBNull.Value;

        command.Parameters.Add("@FechaEnvio", SqlDbType.Date)
            .Value = (object?)pedido.FechaEnvio?.Date
                     ?? DBNull.Value;

        command.Parameters.Add("@TransportistaID", SqlDbType.Int)
            .Value = (object?)pedido.TransportistaID
                     ?? DBNull.Value;

        command.Parameters.Add("@Destinatario",
            SqlDbType.NVarChar, 60)
            .Value = (object?)pedido.Destinatario
                     ?? DBNull.Value;

        command.Parameters.Add("@CiudadDestino",
            SqlDbType.NVarChar, 30)
            .Value = (object?)pedido.CiudadDestino
                     ?? DBNull.Value;

        command.Parameters.Add("@PaisDestino",
            SqlDbType.NVarChar, 30)
            .Value = (object?)pedido.PaisDestino
                     ?? DBNull.Value;
    }
}

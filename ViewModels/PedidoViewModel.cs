using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class PedidoViewModel : ViewModelBase
{
    private readonly IPedidoRepository _repository;

    public ObservableCollection<Pedido> Pedidos { get; } = new();

    public ObservableCollection<DetallePedidoReporte> ReporteDetalles
    { get; } = new();

    public ObservableCollection<Cliente> Clientes { get; } = new();

    public ObservableCollection<Empleado> Empleados { get; } = new();

    public ObservableCollection<Transportista> Transportistas { get; } = new();

    private Pedido? _pedidoSeleccionado;

    public Pedido? PedidoSeleccionado
    {
        get => _pedidoSeleccionado;
        set => SetProperty(ref _pedidoSeleccionado, value);
    }

    private DateTime _fechaInicio =
        new DateTime(2026, 8, 1);

    public DateTime FechaInicio
    {
        get => _fechaInicio;
        set => SetProperty(ref _fechaInicio, value);
    }

    private DateTime _fechaFin =
        new DateTime(2026, 8, 31);

    public DateTime FechaFin
    {
        get => _fechaFin;
        set => SetProperty(ref _fechaFin, value);
    }

    public PedidoViewModel()
    {
        _repository = new PedidoRepository();
    }

    public async Task CargarAsync()
    {
        try
        {
            var lista = await _repository.ListarAsync();

            Pedidos.Clear();

            foreach (var pedido in lista)
                Pedidos.Add(pedido);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    public async Task CargarCombosAsync()
    {
        try
        {
            Clientes.Clear();

            foreach (var cliente
                in await _repository.ListarClientesAsync())
            {
                Clientes.Add(cliente);
            }

            Empleados.Clear();

            foreach (var empleado
                in await _repository.ListarEmpleadosAsync())
            {
                Empleados.Add(empleado);
            }

            Transportistas.Clear();

            foreach (var transportista
                in await _repository.ListarTransportistasAsync())
            {
                Transportistas.Add(transportista);
            }
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    public async Task GenerarReporteAsync()
    {
        if (FechaInicio > FechaFin)
        {
            MessageBox.Show(
                "La fecha inicial no puede ser mayor que la fecha final.",
                "Fechas incorrectas",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        try
        {
            var lista =
                await _repository.DetallePorFechasAsync(
                    FechaInicio,
                    FechaFin);

            ReporteDetalles.Clear();

            foreach (var detalle in lista)
                ReporteDetalles.Add(detalle);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    public async Task EliminarAsync()
    {
        if (PedidoSeleccionado == null)
            return;

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar el pedido seleccionado?",
            "Confirmar",
            MessageBoxButton.YesNo);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                PedidoSeleccionado.PedidoID);

            await CargarAsync();
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private static void MostrarError(Exception ex)
    {
        MessageBox.Show(
            ex.Message,
            "Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}

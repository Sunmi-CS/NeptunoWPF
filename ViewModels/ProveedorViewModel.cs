using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class ProveedorViewModel : ViewModelBase
{
    private readonly IProveedorRepository _repository;

    public ObservableCollection<Proveedor> Proveedores { get; } = new();

    private Proveedor? _proveedorSeleccionado;

    public Proveedor? ProveedorSeleccionado
    {
        get => _proveedorSeleccionado;
        set => SetProperty(ref _proveedorSeleccionado, value);
    }

    private string _nombreContactoFiltro = string.Empty;

    public string NombreContactoFiltro
    {
        get => _nombreContactoFiltro;
        set => SetProperty(ref _nombreContactoFiltro, value);
    }

    private string _ciudadFiltro = string.Empty;

    public string CiudadFiltro
    {
        get => _ciudadFiltro;
        set => SetProperty(ref _ciudadFiltro, value);
    }

    public ProveedorViewModel()
    {
        _repository = new ProveedorRepository();
    }

    public async Task CargarAsync()
    {
        try
        {
            var lista = await _repository.ListarAsync();

            Proveedores.Clear();

            foreach (var proveedor in lista)
                Proveedores.Add(proveedor);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    public async Task BuscarAsync()
    {
        try
        {
            var lista = await _repository.BuscarAsync(
                NombreContactoFiltro,
                CiudadFiltro);

            Proveedores.Clear();

            foreach (var proveedor in lista)
                Proveedores.Add(proveedor);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    public async Task LimpiarFiltrosAsync()
    {
        NombreContactoFiltro = string.Empty;
        CiudadFiltro = string.Empty;

        await CargarAsync();
    }

    public async Task EliminarAsync()
    {
        if (ProveedorSeleccionado == null)
            return;

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar el proveedor seleccionado?",
            "Confirmar",
            MessageBoxButton.YesNo);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                ProveedorSeleccionado.ProveedorID);

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

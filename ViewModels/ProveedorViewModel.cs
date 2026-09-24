using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;

namespace NeptunoWPF.ViewModels;

public class ProveedorViewModel : ViewModelBase
{
    private readonly IProveedorRepository _repository;

    public ObservableCollection<Proveedor> Proveedores { get; }
        = new();

    private Proveedor? _proveedorSeleccionado;

    public Proveedor? ProveedorSeleccionado
    {
        get => _proveedorSeleccionado;
        set => SetProperty(
            ref _proveedorSeleccionado,
            value);
    }

    public string NombreContactoBusqueda { get; set; }
        = string.Empty;

    public string CiudadBusqueda { get; set; }
        = string.Empty;

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
            {
                Proveedores.Add(proveedor);
            }

            ProveedorSeleccionado = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudieron cargar los proveedores.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public async Task BuscarAsync()
    {
        try
        {
            var lista = await _repository.BuscarAsync(
                NombreContactoBusqueda,
                CiudadBusqueda);

            Proveedores.Clear();

            foreach (var proveedor in lista)
            {
                Proveedores.Add(proveedor);
            }

            ProveedorSeleccionado = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo realizar la búsqueda.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    public async Task LimpiarFiltrosAsync()
    {
        NombreContactoBusqueda = string.Empty;
        CiudadBusqueda = string.Empty;

        await CargarAsync();
    }


    public async Task EliminarAsync()
    {
        if (ProveedorSeleccionado == null)
        {
            MessageBox.Show(
                "Selecciona un proveedor.",
                "Aviso",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar el proveedor seleccionado?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                ProveedorSeleccionado.ProveedorID);

            await CargarAsync();

            MessageBox.Show(
                "Proveedor eliminado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo eliminar el proveedor.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
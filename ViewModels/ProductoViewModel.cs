using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class ProductoViewModel : ViewModelBase
{
    private readonly IProductoRepository _repository;

    public ObservableCollection<Producto> Productos { get; } = new();

    private Producto? _productoSeleccionado;

    public Producto? ProductoSeleccionado
    {
        get => _productoSeleccionado;
        set => SetProperty(ref _productoSeleccionado, value);
    }

    public ProductoViewModel()
    {
        _repository = new ProductoRepository();
    }

    public async Task CargarAsync()
    {
        try
        {
            var lista = await _repository.ListarAsync();

            Productos.Clear();

            foreach (var producto in lista)
                Productos.Add(producto);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public async Task EliminarAsync()
    {
        if (ProductoSeleccionado == null)
            return;

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar el producto seleccionado?",
            "Confirmar",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                ProductoSeleccionado.ProductoID);

            await CargarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "No se pudo eliminar",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}

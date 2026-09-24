using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;

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
            {
                Productos.Add(producto);
            }

            ProductoSeleccionado = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudieron cargar los productos.\n\n" + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public async Task EliminarAsync()
    {
        if (ProductoSeleccionado == null)
        {
            MessageBox.Show(
                "Selecciona un producto.",
                "Aviso",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar el producto seleccionado?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                ProductoSeleccionado.ProductoID);

            await CargarAsync();

            MessageBox.Show(
                "Producto eliminado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo eliminar el producto.\n\n" + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
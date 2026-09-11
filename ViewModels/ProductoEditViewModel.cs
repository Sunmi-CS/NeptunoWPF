using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class ProductoEditViewModel : ViewModelBase
{
    private readonly IProductoRepository _repository;

    public Producto Producto { get; }

    public bool EsEdicion { get; }

    public ObservableCollection<Categoria> Categorias { get; }
    public ObservableCollection<Proveedor> Proveedores { get; }

    public ProductoEditViewModel(
        Producto producto,
        bool esEdicion,
        ObservableCollection<Categoria> categorias,
        ObservableCollection<Proveedor> proveedores)
    {
        _repository = new ProductoRepository();

        Producto = producto;
        EsEdicion = esEdicion;

        Categorias = categorias;
        Proveedores = proveedores;
    }

    public async Task<bool> GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(Producto.NombreProducto))
        {
            MessageBox.Show(
                "El nombre del producto es obligatorio.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        if (Producto.ProveedorID == null)
        {
            MessageBox.Show(
                "Selecciona un proveedor.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        if (Producto.CategoriaID == null)
        {
            MessageBox.Show(
                "Selecciona una categoría.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        if (Producto.PrecioUnidad < 0)
        {
            MessageBox.Show(
                "El precio no puede ser negativo.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        if (Producto.UnidadesEnExistencia < 0)
        {
            MessageBox.Show(
                "El stock no puede ser negativo.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        if (EsEdicion)
        {
            await _repository.ActualizarAsync(Producto);
        }
        else
        {
            Producto.ProductoID =
                await _repository.InsertarAsync(Producto);
        }

        return true;
    }
}
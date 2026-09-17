using System;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Models;

namespace NeptunoWPF.ViewModels;

public class ProductoEditViewModel : ViewModelBase
{
    private readonly IProductoRepository _repository;

    public Producto Producto { get; }

    public bool EsEdicion { get; }

    public ProductoEditViewModel(
        Producto producto,
        bool esEdicion)
    {
        _repository = new ProductoRepository();

        Producto = producto;
        EsEdicion = esEdicion;
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

        if (Producto.PrecioUnidad < 0)
        {
            MessageBox.Show(
                "El precio no puede ser negativo.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        try
        {
            if (EsEdicion)
            {
                await _repository.ActualizarAsync(Producto);
            }
            else
            {
                await _repository.InsertarAsync(Producto);
            }

            MessageBox.Show(
                EsEdicion
                    ? "Producto actualizado correctamente."
                    : "Producto registrado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo guardar el producto.\n\n" + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            return false;
        }
    }
}
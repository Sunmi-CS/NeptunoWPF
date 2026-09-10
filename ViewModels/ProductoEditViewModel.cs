using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Windows;

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

    public async Task GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(
            Producto.NombreProducto))
        {
            MessageBox.Show(
                "El nombre del producto es obligatorio.");

            return;
        }

        if (Producto.PrecioUnidad < 0)
        {
            MessageBox.Show(
                "El precio no puede ser negativo.");

            return;
        }

        if (EsEdicion)
        {
            await _repository.ActualizarAsync(Producto);
        }
        else
        {
            await _repository.InsertarAsync(Producto);
        }
    }
}
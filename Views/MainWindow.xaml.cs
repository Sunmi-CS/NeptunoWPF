using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using NeptunoWPF.Data.Models;
using NeptunoWPF.ViewModels;
using System.Windows;

namespace NeptunoWPF.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        _viewModel = new MainViewModel();

        DataContext = _viewModel;

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Productos.CargarAsync();
        await _viewModel.Categorias.CargarAsync();
        await _viewModel.Proveedores.CargarAsync();
        await _viewModel.Pedidos.CargarAsync();
        await _viewModel.Pedidos.CargarCombosAsync();
    }

    private async void ActualizarProductos_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Productos.CargarAsync();
    }

    private async void EliminarProducto_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Productos.EliminarAsync();
    }

    private async void ActualizarCategorias_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Categorias.CargarAsync();
    }

    private async void EliminarCategoria_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Categorias.EliminarAsync();
    }

    private async void BuscarProveedor_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Proveedores.BuscarAsync();
    }

    private async void LimpiarProveedor_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Proveedores.LimpiarFiltrosAsync();
    }

    private async void EliminarProveedor_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Proveedores.EliminarAsync();
    }

    private async void ActualizarPedidos_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Pedidos.CargarAsync();
    }

    private async void EliminarPedido_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Pedidos.EliminarAsync();
    }

    private async void GenerarReporte_Click(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.Pedidos.GenerarReporteAsync();
    }

    private void NuevoProducto_Click(
    object sender,
    RoutedEventArgs e)
    {
        var ventana = new ProductoEditWindow(
            new Producto(),
            false);

        ventana.Owner = this;

        if (ventana.ShowDialog() == true)
        {
            _ = _viewModel.Productos.CargarAsync();
        }
    }


    private void EditarProducto_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.Productos.ProductoSeleccionado == null)
        {
            MessageBox.Show("Selecciona un producto.");
            return;
        }

        var ventana = new ProductoEditWindow(
            _viewModel.Productos.ProductoSeleccionado,
            true);

        ventana.Owner = this;

        if (ventana.ShowDialog() == true)
        {
            _ = _viewModel.Productos.CargarAsync();
        }
    }

    private void NuevoCategoria_Click(
        object sender,
        RoutedEventArgs e)
    {
        var ventana = new CategoriaEditWindow(
            new Categoria(),
            false);

        ventana.ShowDialog();

        _ = _viewModel.Categorias.CargarAsync();
    }

    private void EditarCategoria_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.Categorias.CategoriaSeleccionada == null)
        {
            MessageBox.Show("Selecciona una categoría.");
            return;
        }

        var ventana = new CategoriaEditWindow(
            _viewModel.Categorias.CategoriaSeleccionada,
            true);

        ventana.ShowDialog();

        _ = _viewModel.Categorias.CargarAsync();
    }

    private void NuevoProveedor_Click(
        object sender,
        RoutedEventArgs e)
    {
        var ventana = new ProveedorEditWindow(
            new Proveedor(),
            false);

        ventana.ShowDialog();

        _ = _viewModel.Proveedores.CargarAsync();
    }

    private void EditarProveedor_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.Proveedores.ProveedorSeleccionado == null)
        {
            MessageBox.Show("Selecciona un proveedor.");
            return;
        }

        var ventana = new ProveedorEditWindow(
            _viewModel.Proveedores.ProveedorSeleccionado,
            true);

        ventana.ShowDialog();

        _ = _viewModel.Proveedores.CargarAsync();
    }

    private void NuevoPedido_Click(
        object sender,
        RoutedEventArgs e)
    {
        var ventana = new PedidoEditWindow(
            new Pedido(),
            false,
            _viewModel.Pedidos.Clientes,
            _viewModel.Pedidos.Empleados,
            _viewModel.Pedidos.Transportistas);

        ventana.ShowDialog();

        _ = _viewModel.Pedidos.CargarAsync();
    }

    private void EditarPedido_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.Pedidos.PedidoSeleccionado == null)
        {
            MessageBox.Show("Selecciona un pedido.");
            return;
        }

        var ventana = new PedidoEditWindow(
            _viewModel.Pedidos.PedidoSeleccionado,
            true,
            _viewModel.Pedidos.Clientes,
            _viewModel.Pedidos.Empleados,
            _viewModel.Pedidos.Transportistas);

        ventana.ShowDialog();

        _ = _viewModel.Pedidos.CargarAsync();
    }
}
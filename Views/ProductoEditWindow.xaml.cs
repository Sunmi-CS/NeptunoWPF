using NeptunoWPF.Models;
using NeptunoWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.Views;

public partial class ProductoEditWindow : Window
{
    private readonly ProductoEditViewModel _viewModel;

    public ProductoEditWindow(
    Producto producto,
    bool esEdicion,
    ObservableCollection<Categoria> categorias,
    ObservableCollection<Proveedor> proveedores)
    {
        InitializeComponent();

        _viewModel = new ProductoEditViewModel(
            producto,
            esEdicion,
            categorias,
            proveedores);

        DataContext = _viewModel;

        if (esEdicion)
        {
            Title = "Editar producto";
            TituloFormulario.Text = "Editar producto";
        }
        else
        {
            Title = "Nuevo producto";
            TituloFormulario.Text = "Nuevo producto";
        }
    }

    private async void Guardar_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            bool guardado = await _viewModel.GuardarAsync();

            if (guardado)
            {
                DialogResult = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al guardar",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void Cancelar_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
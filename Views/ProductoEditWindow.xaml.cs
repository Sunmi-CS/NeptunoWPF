using System.Windows;

using NeptunoWPF.Models;
using NeptunoWPF.ViewModels;

namespace NeptunoWPF.Views;

public partial class ProductoEditWindow : Window
{
    private readonly ProductoEditViewModel _viewModel;

    public ProductoEditWindow(
        Producto producto,
        bool esEdicion)
    {
        InitializeComponent();

        _viewModel = new ProductoEditViewModel(
            producto,
            esEdicion);

        DataContext = _viewModel;
    }

    private async void Guardar_Click(
        object sender,
        RoutedEventArgs e)
    {
        bool guardado = await _viewModel.GuardarAsync();

        if (guardado)
        {
            DialogResult = true;
        }
    }

    private void Cancelar_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}